using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Entities;
using TodoApi.Extensions;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services;
using TodoApi.ViewModels;
using TodoApi.ViewModels.TodoViewModel;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _userRepository;

        public TodoController([FromServices] ITodoRepository todoRepository,
            [FromServices] IUserRepository userRepository)
        {
            _todoRepository = todoRepository;
            _userRepository = userRepository;

        }
        [HttpGet("")]
        public async Task<IActionResult> GetTodos() 
        {
            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var todos = await _todoRepository.GetAllAsync(user.Id);
            var total = todos.Count();

            if (total == 0)
                return NotFound(new ResultViewModel(false, "Você ainda não tem tarefas", null));

            return Ok(new ResultViewModel(true, "Suas Tarefas", todos));
        }
        [HttpGet("done")]
        public async Task<IActionResult> GetAllDone() 
        {
            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var todos = await _todoRepository.GetAllDoneAsync(user.Id);

            return Ok(new ResultViewModel(true, "Suas tarefas concluídas", todos));
        }
        [HttpGet("undone")]
        public async Task<IActionResult> GetAllUndone() 
        {
            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var todos = await _todoRepository.GetAllUndoneAsync(user.Id);

            return Ok(new ResultViewModel(true, "Suas tarefas não concluídas", todos));
        }

        [HttpPost("create-todo")]
        public async Task<IActionResult> PostTodo(
            [FromBody] CreateTodoViewModel model) 
        {
            if (model == null)
                return BadRequest(new ResultViewModel(false, "Tarefa inválida", model.Notifications));

            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            try
            {
                var todo = new Todo(model.Title, model.Date, user.Id);
                await _todoRepository.CreateAsync(todo);

                return StatusCode(200, new ResultViewModel(true, "Tarefa Criada com sucesso",
                    new ListTodoViewModel(todo.Title, todo.Date,todo.Done, user.FirstName + " " + user.LastName)));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel(false, "05X12 - Falha Interna no Servidor", null));
            }
        }
        [HttpPut("update-todo")]
        public async Task<IActionResult> PutTitle(
            [FromBody] UpdateTodoViewModel model) 
        {
            if (model == null)
                return BadRequest(new ResultViewModel(false, "Tarefa inválida", null));

            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var todo = await _todoRepository.GetByIdAsync(model.TodoId, user.Id);
            if (todo == null)
                return NotFound(new ResultViewModel(false, "Tarefa não encontrada", null));
            try
            {
                todo.UpdateTitle(model.Title);
                await _todoRepository.UpdateAsync(todo);

                return Ok(new ResultViewModel(true, "Tarefa atualizada com sucesso",
                    new ListTodoViewModel(todo.Title,todo.Date.Date, todo.Done, user.FirstName + " "+ user.LastName)));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel(false, "05X13 - Falha interna no servidor", null));
            }
        }
        [HttpPut("mark-as-done/{id:int}")]
        public async Task<IActionResult> MarkAsDone(
            [FromRoute] int id) 
        {
            if (id == null)
                return BadRequest(new ResultViewModel(false, "Tarefa inválida", null));

            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);        
            var todo = await _todoRepository.GetByIdAsync(id, user.Id);
            if (todo == null)
                return NotFound(new ResultViewModel(false, "Tarefa não encontrada", null));
            try
            {
                todo.MarkAsDone();
                await _todoRepository.UpdateAsync(todo);

                return Ok(new ResultViewModel(true, "Tarefa alterada com sucesso", todo));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel(false, "05X14 - Falha interna no servidor", null));
            }
        }
        [HttpPut("mark-as-undone/{id:int}")]
        public async Task<IActionResult> MarkAsUnone([FromRoute] int id) 
        {
            if (id == null)
                return BadRequest(new ResultViewModel(false, "Tarefa inválida", null));

            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var todo = await _todoRepository.GetByIdAsync(id, user.Id);
            if (todo == null)
                return NotFound(new ResultViewModel(false, "Tarefa não encontrada", null));
            try
            {
                todo.MarkAsUndone();
                await _todoRepository.UpdateAsync(todo);

                return Ok(new ResultViewModel(true, "Tarefa alterada com sucesso", todo));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel(false, "05X14 - Falha interna no servidor", null));
            }
        }
        [HttpDelete("delete-todo/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id) 
        {
            if (id == null)
                return NotFound(new ResultViewModel(false, "Tarefa não encontrada", null));

            var user = await _userRepository.GetByEmailAsync(User.Identity.Name);
            var todo = await _todoRepository.GetByIdAsync(id, user.Id);
            if(todo == null)
                return NotFound(new ResultViewModel(false, "Tarefa não encontrada", null));
            try
            {
                await _todoRepository.Delete(todo);

                return Ok(new ResultViewModel(true, "Tarefa deletada com sucesso", null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel(false, "05X15 - Falha interna no servidor", null));
            }
          
        }
    }
}
