using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAPP.Application.DTOs.Account;
using ShopAPP.Application.Interfaces.Account;
using ShopAPP.Infrastructure.Identity.Models;

namespace ShopAPP.API.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _userService = userService;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Realiza o registro de um novo usuário.
        /// </summary>
        /// <param name="dto">Dados do novo usuário.</param>
        /// <returns>Mensagem de sucesso ou lista de erros.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterUserDto dto)
        {
            try
            {
                // Salva imagem (se houver)
                string? imageUrl = await SaveImageAsync(dto.ImageFile, "users");

                var user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    FullName = dto.FullName,
                    Document = dto.Document,
                    UserType = dto.Role,
                    ProfileImageUrl = imageUrl
                };

                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await _userManager.AddToRoleAsync(user, dto.Role);

                return Ok(new { user.Id, user.Email, user.FullName, user.ProfileImageUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                return StatusCode(500, new { ex.Message });
            }
        }

        /// <summary>
        /// Realiza o login e retorna o token JWT.
        /// </summary>
        /// <param name="loginDto">Credenciais do usuário.</param>
        /// <returns>Token JWT e dados do usuário autenticado.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var result = await _userService.AuthenticateAsync(loginDto);

                if (result == null)
                    return Unauthorized(new { message = "Credenciais inválidas." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao autenticar usuário.");
                return StatusCode(500, new { message = "Erro interno no servidor ao realizar login." });
            }
        }

        private async Task<string?> SaveImageAsync(IFormFile? imageFile, string folder)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{uniqueFileName}";
        }

    }
}
