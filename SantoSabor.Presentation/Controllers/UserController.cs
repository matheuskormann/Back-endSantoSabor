using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoSabor.Application.DTOs;
using SantoSabor.Application.Interfaces;
using System.Security.Cryptography;
using System.Security.Claims;

namespace SantoSabor.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary> Retorna todos os usuários cadastrados no sistema.</summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retorna um usuário específico pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado!");

            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateDTO userCreateDTO)
        {
            try
            {
                await _userService.CreateUserAsync(userCreateDTO);
                return Ok(new { message = "Usuario Criado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar usuário: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, userUpdateDTO);
                return result ? Ok("Usuário atualizado com sucesso!") : BadRequest("Erro ao atualizar usuário.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove um usuário do sistema pelo ID.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return BadRequest("Erro ao remover usuário!");

            return Ok("Usuário removido com sucesso!");
        }

        [Authorize]
        [HttpGet("check-roles")]
        public IActionResult CheckRoles()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            Console.WriteLine($"Header Authorization recebido no controller: '{authHeader}'");

            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            return Ok(new { roles });
        }

    }
}