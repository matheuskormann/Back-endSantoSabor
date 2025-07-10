using SantoSabor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(Guid id);
        Task RegisterClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task RemoveClientAsync(Client client);
        Task<Client?> GetByCpfAsync(string cpf);
        Task SaveChangesAsync();
    }
}
