using Kolokwium2API.DTOs;
using Kolokwium2API.Models;
using Kolokwium2API.Data;
using Kolokwium2API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2API.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<NurseryDto> GetNurseryWithBatchesAsync(int id)
    {
        var nursery = await _context.Nurseries
            .Include(n => n.SeedlingBatches)
                .ThenInclude(b => b.TreeSpecies)
            .Include(n => n.SeedlingBatches)
                .ThenInclude(b => b.Responsibles)
                    .ThenInclude(r => r.Employee)
            .FirstOrDefaultAsync(n => n.NurseryId == id);

        if (nursery == null)
            throw new NotFoundException($"Nursery with id {id} not found");

        return new NurseryDto
        {
            NurseryId = nursery.NurseryId,
            Name = nursery.Name,
            EstablishedDate = nursery.EstablishedDate,
            Batches = nursery.SeedlingBatches.Select(b => new SeedlingBatchDto
            {
                BatchId = b.BatchId,
                Quantity = b.Quantity,
                SownDate = b.SownDate,
                ReadyDate = b.ReadyDate,
                Species = new TreeSpeciesDto
                {
                    LatinName = b.TreeSpecies.LatinName,
                    GrowthTimeInYears = b.TreeSpecies.GrowthTimeInYears
                },
                Responsibles = b.Responsibles.Select(r => new ResponsibleDto
                {
                    FirstName = r.Employee.FirstName,
                    LastName = r.Employee.LastName,
                    Role = r.Role
                }).ToList()
            }).ToList()
        };
    }

    public async Task AddBatchAsync(AddBatchDto dto)
    {
        var species = await _context.TreeSpecies.FirstOrDefaultAsync(s => s.LatinName == dto.Species);
        var nursery = await _context.Nurseries.FirstOrDefaultAsync(n => n.Name == dto.Nursery);

        if (species == null)
            throw new NotFoundException("Tree species not found");

        if (nursery == null)
            throw new NotFoundException("Nursery not found");

        var newBatch = new SeedlingBatch
        {
            Quantity = dto.Quantity,
            SpeciesId = species.SpeciesId,
            NurseryId = nursery.NurseryId,
            Responsibles = new List<Responsible>()
        };

        foreach (var r in dto.Responsibles)
        {
            var employee = await _context.Employees.FindAsync(r.EmployeeId);
            if (employee == null)
                throw new ValidationException($"Employee with ID {r.EmployeeId} not found");

            newBatch.Responsibles.Add(new Responsible
            {
                EmployeeId = r.EmployeeId,
                Role = r.Role
            });
        }

        _context.SeedlingBatches.Add(newBatch);
        await _context.SaveChangesAsync();
    }
}