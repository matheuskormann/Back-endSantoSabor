using SantoSabor.Application.DTOS;
using SantoSabor.Application.Interfaces;
using SantoSabor.Core.Entities;
using SantoSabor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Guid> CreateClientAsync(ClientCreateDTO clientCreateDto)
        {
            var ExistingClient = await _clientRepository.GetByCpfAsync(clientCreateDto.Cpf);
            if(ExistingClient != null)
            {
                throw new Exception("Cpf já cadastrado!");
            }

            var client = new Client
            {
                ClientId = Guid.NewGuid(),
                Name = clientCreateDto.Name,
                Address = clientCreateDto.Address,
                Cpf = clientCreateDto.Cpf,
                Company = clientCreateDto.Company,
                Phone = clientCreateDto.Phone,
                CreatedAt = DateTime.UtcNow
            };

            await _clientRepository.RegisterClientAsync(client);
            return client.ClientId;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients.Select(client => new ClientDTO
            {
                ClientId = client.ClientId,
                Name = client.Name,
                Address = client.Address,
                Cpf = client.Cpf,
                Company = client.Company ?? "Não informado!",
                Phone = client.Phone ?? "Não informado!",
                CreatedAt = client.CreatedAt
            });
        }

        public async Task<ClientDTO> GetClientByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if(client == null)
            {
                throw new Exception("Cliente não foi encontrado com o id passado!");
            }

            return new ClientDTO
            {
                ClientId = client.ClientId,
                Name = client.Name,
                Address = client.Address,
                Cpf = client.Cpf,
                Company = client.Company ?? "Não informado!",
                Phone = client.Phone ?? "Não informado!",
                CreatedAt = client.CreatedAt
            };
        }

        public async Task<bool> RemoveClientAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new Exception("Cliente com o id passado não foi encontrado!");
            }

            await _clientRepository.RemoveClientAsync(client);
            return true;
        }

        public async Task<bool> UpdateClientAsync(Guid id, ClientUpdateDTO clientUpdateDto)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if(client == null)
            {
                throw new Exception("Cliente com o id passado não foi encontrado!");
            }

            client.Name = clientUpdateDto.Name;
            client.Address = clientUpdateDto.Address;
            client.Company = clientUpdateDto.Company;
            client.Phone = clientUpdateDto.Phone;

            await _clientRepository.UpdateClientAsync(client);
            return true;
        }
    }
}
