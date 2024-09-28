using static Backend_Teamwork.src.DTO.CustomerDTO;

namespace Backend_Teamwork.src.Services.customer
{
    public interface ICustomerService
    {
        // Get all
        Task<List<CustomerReadDto>> GetAllAsync();

        // create
        Task<CustomerReadDto> CreateOneAsync(CustomerCreateDto createDto);

        // Get by id
        Task<CustomerReadDto> GetByIdAsync(Guid id);

        // delete
        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, CustomerUpdateDto updateDto);

        Task<CustomerReadDto> GetByEmailAsync(string email);
    }
}
