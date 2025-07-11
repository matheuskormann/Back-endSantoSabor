using SantoSabor.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDTO>> GetAllAsync();
        Task<ClientDTO> GetClientByIdAsync(Guid id);
        Task<Guid> CreateClientAsync(ClientCreateDTO clientCreateDto);
        Task<bool> UpdateClientAsync(Guid id, ClientUpdateDTO clientUpdateDto);
        Task<bool> RemoveClientAsync(Guid id);
    }
}
