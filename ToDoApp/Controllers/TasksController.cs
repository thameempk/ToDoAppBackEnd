using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasks _tasks;

        public TasksController(ITasks tasks)
        {
            _tasks = tasks;
        }


        [HttpGet("{userid}")]
        [Authorize]
        public ActionResult GetTasks(int userid)
        {
            return Ok(_tasks.GetTasks(userid));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddTasks([FromBody] Tasks tasks)
        {
            _tasks.AddTask(tasks);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult UpdateTasks(int id,[FromBody] Tasks tasks)
        {

            _tasks.UpdateTask(id, tasks);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeleteTasks(int id)
        {
            _tasks.DeleteTask(id);
            return Ok();
        }
    }
}
