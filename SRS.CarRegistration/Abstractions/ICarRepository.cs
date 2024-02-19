using SRS.CarRegistration.Models;

namespace SRS.CarRegistration.Abstractions;

public interface ICarRepository
{
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<Car?> GetCarByIdAsync(int carId);
    Task<IEnumerable<Car>> GetCarsByOwnerIdAsync(int ownerId);
    Task<Car> RegisterNewCarAsync(Car car);
    Task<Car?> UpdateCarAsync(Car updatedData);
}