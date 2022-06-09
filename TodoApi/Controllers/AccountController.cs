using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureIdentity.Password;
using TodoApi.Entities;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services;
using TodoApi.ViewModels;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AccountController : ControllerBase 
    {
       
        private readonly IUserRepository _userRepository;

        public AccountController([FromServices] IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("accounts/register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterViewModel model)
        {
            if (model == null)
                return BadRequest(new ResultViewModel(false, "Usúario Inválido", model.Notifications));
            try
            {

                var user = new User(
                    model.FirstName,
                    model.LastName,
                    model.Email,
                    PasswordHasher.Hash(model.Password));

                await _userRepository.CreateAsync(user);

                model.Password = "";

                return StatusCode(200, new ResultViewModel(true, "Usuário registrado com sucesso", model));
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new ResultViewModel(false, "05X10 - Falha interna do servidor", model.Notifications));
            }
        }
        [HttpPost("accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model, 
            [FromServices] TokenService service)
        {
            if (model == null)
                return BadRequest(new ResultViewModel(false, "Usúario Inválido", model.Notifications));

            var user = await _userRepository.GetByEmailAsync(model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel(false, "Usuário ou senha inválidos", null));
            if (!PasswordHasher.Verify(user.Password, model.Password))
                return StatusCode(401, new ResultViewModel(false, "Usuário ou senha inválidos", null));

            try
            {
                var token = service.GenerateToken(user);
                return Ok(new ResultViewModel(true, "Usuário Logado com sucesso", token));
            }
            catch (Exception)
            {

                return StatusCode(500, new ResultViewModel(false, "05X11 - Falha Interna no Servidor", null));
            }
        }
        [HttpDelete("accounts/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync() 
        {
            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            try
            {
                await _userRepository.DeleteAsync(user);

                return Ok(new ResultViewModel(true, "Usuário deletado com sucesso", null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel(false, "05X09 - Falha interna no servidor", null));
            }
        }
    }
}
