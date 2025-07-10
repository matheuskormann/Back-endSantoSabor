using Microsoft.EntityFrameworkCore;
using SantoSabor.Core.Entities;
using SantoSabor.Core.Interfaces;
using SantoSabor.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MyDbContext _context;

        public ClientRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByCpfAsync(string cpf)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task RegisterClientAsync(Client client)
        {
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveClientAsync(Client client)
        {
            _context.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
