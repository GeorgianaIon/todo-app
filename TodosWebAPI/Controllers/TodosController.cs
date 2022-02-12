using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private ITodoData TodoData;

        public TodosController(ITodoData data)
        {
            this.TodoData = data;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Todo>>> GetTodos([FromQuery] bool? isCompleted, [FromQuery] int? userId)
        {
            try
            {
                // TodoData.filterById = userId;
                // TodoData.filterByIsCompleted = isCompleted;
                // TodoData.ExecuteFilter();
                IList<Todo> todos = await TodoData.GetTodos();
                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id:int}")]
        public async Task<ActionResult<Todo>> GetTodoAsync([FromRoute] int id)
        {
            try
            {
                Todo todo = await TodoData.Get(id);
                return todo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{isCompleted:bool},{userId:int}")]
        public async Task<ActionResult<IList<Todo>>> GetFilteredTodosByBoth([FromRoute] bool? isCompleted,
            [FromRoute] int? UserId)
        {
            try
            {
                IList<Todo> todos = await TodoData.GetTodosFilteredByBothAsync(isCompleted, UserId);
                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{isCompleted:bool}")]
        public async Task<ActionResult<IList<Todo>>> GetFilteredTodos([FromRoute] bool? isCompleted)
        {
            try
            {
                IList<Todo> todos = await TodoData.GetTodosFilteredAsync(isCompleted);
                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

    [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("{id:int}")]
        public async Task<ActionResult<Todo>> DeleteTodo([FromRoute] int id)
        {
            try
            {
                await TodoData.RemoveTodo(id);
                // Todo dtd = TodoData.Get(id);
                //
                // TodoData.RemoveTodo(dtd.TodoId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StatusCode(500, e.Message);
            }

            return StatusCode(200);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Todo added = await TodoData.AddTodoAsync(todo);
                return Created($"/{added.TodoId}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Microsoft.AspNetCore.Mvc.Route("{id:int}")]
        public async Task<ActionResult<Todo>> UpdateTodo([FromBody] Todo todo)
        {
            try
            {
                Todo upd = await TodoData.Update(todo);
                return Created($"/{upd.TodoId}", upd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}