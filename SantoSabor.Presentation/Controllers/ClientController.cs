using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SantoSabor.Application.DTOS;
using SantoSabor.Application.Interfaces;
using System.Runtime.InteropServices;

namespace SantoSabor.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary> Retorna todos os Clientes cadastrados no sistema.</summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllAsync();
                return Ok(clients);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        /// <summary>
        /// Retorna um Cliente específico pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClientById(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if(client == null)
            {
                return NotFound("Cliente não encontrado!");
            }
            return Ok(client);
        }

        /// <summary>
        /// Cria um novo cliente no sistema.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateClient(ClientCreateDTO clientCreateDto)
        {
            try
            {
                await _clientService.CreateClientAsync(clientCreateDto);
                return Ok(new { message = "Cliente criado com sucesso!" });
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao criar cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza os dados de um cliente pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(Guid id, ClientUpdateDTO clientUpdateDto)
        {
            try
            {
                var result = await _clientService.UpdateClientAsync(id, clientUpdateDto);
                return result ? Ok("Cliente atualizado com sucesso!") : BadRequest("Erro ao atualizar o cliente");
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao atualizar o cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove um cliente do sistema pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClient(Guid id)
        {
            var result = await _clientService.RemoveClientAsync(id);
            if (!result)
            {
                return BadRequest("Não foi possível remover o cliente!");
            }

            return Ok("Cliente removido com sucesso!");
        }

    }
}
