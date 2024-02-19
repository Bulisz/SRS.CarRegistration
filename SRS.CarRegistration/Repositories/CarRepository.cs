using Microsoft.EntityFrameworkCore;
using SRS.CarRegistration.Abstractions;
using SRS.CarRegistration.Database;
using SRS.CarRegistration.Models;

namespace SRS.CarRegistration.Repositories;

public class CarRepository(AppDbContext context) : ICarRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Car>> GetCarsByOwnerIdAsync(int ownerId)
    {
        return await _context.Cars.FromSqlInterpolated($"GetCarsByOwnerId {ownerId}").ToListAsync();
    }

    public async Task<Car> RegisterNewCarAsync(Car newCar)
    {
        _context.Cars.Add(newCar);
        await _context.SaveChangesAsync();
        return newCar;
    }

    public async Task<Car?> GetCarByIdAsync(int carId)
    {
        return await _context.Cars
            .Include(c => c.Owners)
            .FirstOrDefaultAsync(c => c.CarID == carId);
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<Car?> UpdateCarAsync(Car updatedData)
    {
        var carToUpdate = await _context.Cars.FindAsync(updatedData.CarID);

        if (carToUpdate is not null)
        {
            carToUpdate.Brand = updatedData.Brand;
            carToUpdate.Model = updatedData.Model;
            carToUpdate.RegistrationNumber = updatedData.RegistrationNumber;
            carToUpdate.ProductionDate = updatedData.ProductionDate;
            _context.Cars.Update(carToUpdate);
            await _context.SaveChangesAsync();
            return carToUpdate;
        }
        else
            return null;
    }
}
