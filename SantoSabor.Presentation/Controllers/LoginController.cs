using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantoSabor.Application.DTOs;
using SantoSabor.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SantoSabor.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using SantoSabor.Application.DTOS;

namespace SantoSabor.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;

        public LoginController(IUserService userService, TokenService tokenService, UserManager<IdentityUser> userManager, IUserRepository userRepository)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Realiza o login de um usuário e retorna o token JWT com roles.
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(loginDto.Email);
                if (identityUser == null)
                    return Unauthorized(new { message = "Usuário não encontrado!" });

                var passwordIsValid = await _userManager.CheckPasswordAsync(identityUser, loginDto.Password);
                if (!passwordIsValid)
                    return Unauthorized(new { message = "Senha incorreta!" });

                var user = await _userRepository.GetByEmailAsync(identityUser.Email);
                if (user == null)
                    return Unauthorized(new { message = "Usuário não encontrado no sistema interno." });

                var roles = await _userManager.GetRolesAsync(identityUser);

                // Monta claims incluindo as roles
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Gere o token passando as claims para o TokenService
                var token = _tokenService.GenerateToken(claims);

                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao fazer login: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna os dados do usuário logado com base no token.
        /// </summary>
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdStr))
            {
                return Unauthorized("Usuário não encontrado no token");
            }

            var identityUser = await _userManager.FindByIdAsync(userIdStr);

            if (identityUser == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            var roles = await _userManager.GetRolesAsync(identityUser);
            var role = roles.FirstOrDefault() ?? "user";

            var usuario = await _userService.GetUserByEmailAsync(identityUser.Email);
            if (usuario == null)
            {
                return NotFound("Dados do usuário não encontrados");
            }

            var profile = new UserProfileDTO
            {
                UserId = usuario.UserId,
                Name = usuario.Name,
                Email = usuario.Email,
                Role = role,
                CreatedAt = usuario.CreatedAt
            };

            return Ok(profile);
        }
    }
}
