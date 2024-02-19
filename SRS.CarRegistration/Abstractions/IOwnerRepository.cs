using SRS.CarRegistration.Models;

namespace SRS.CarRegistration.Abstractions;

public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetAllOwnersAsync();
    Task<Owner?> GetOwnerByIdAsync(int ownerId);
    Task<Owner> RegisterNewOwnerAsync(Owner newOwner);
    Task<Owner?> UpdateOwnerAsync(Owner updatedData);
    Task RemoveCarFromOwnerAsync(int ownerID, int carID);
    Task AddCarToOwnerAsync(int ownerID, int carID);
}