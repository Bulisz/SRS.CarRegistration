using Microsoft.EntityFrameworkCore;
using SRS.CarRegistration.Abstractions;
using SRS.CarRegistration.Database;
using SRS.CarRegistration.Models;

namespace SRS.CarRegistration.Repositories;

public class OwnerRepository(AppDbContext context) : IOwnerRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Owner> RegisterNewOwnerAsync(Owner newOwner)
    {
        _context.Owners.Add(newOwner);
        await _context.SaveChangesAsync();
        return newOwner;
    }

    public async Task<Owner?> GetOwnerByIdAsync(int ownerId)
    {
        return await _context.Owners
            .Include(o => o.Cars)
            .FirstOrDefaultAsync(o => o.OwnerID == ownerId);
    }

    public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
    {
        return await _context.Owners.ToListAsync();
    }

    public async Task<Owner?> UpdateOwnerAsync(Owner updatedData)
    {
        var ownerToUpdate = await _context.Owners.FindAsync(updatedData.OwnerID);

        if (ownerToUpdate is not null)
        {
            ownerToUpdate.FirstName = updatedData.FirstName;
            ownerToUpdate.LastName = updatedData.LastName;
            ownerToUpdate.BirthDate = updatedData.BirthDate;
            _context.Owners.Update(ownerToUpdate);
            await _context.SaveChangesAsync();
            return ownerToUpdate;
        }
        else
            return null;
    }

    public async Task RemoveCarFromOwnerAsync(int ownerID, int carID)
    {
        var owner = await GetOwnerByIdAsync(ownerID);

        if (owner is not null)
        {
            var carToRemove = owner.Cars.FirstOrDefault(c => c.CarID == carID);
            owner.Cars.Remove(carToRemove!);
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddCarToOwnerAsync(int ownerID, int carID)
    {
        var owner = await GetOwnerByIdAsync(ownerID);
        var car = await _context.Cars.FindAsync(carID);
        bool alreadyHasThisCar = false;

        if (owner is not null && car is not null)
            alreadyHasThisCar = owner.Cars.FirstOrDefault(c => c.CarID == carID) is not null;

        if (!alreadyHasThisCar)
        {
            owner!.Cars.Add(car!);
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
        }
    }
}
