using System;
using System.Threading;
using System.Threading.Tasks;
using APIAnnouncements.dbo;
using APIAnnouncements.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIAnnouncements.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }        

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            UserResponse item = await _userService.Get(id, cancellationToken);

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequest userItem, CancellationToken cancellationToken)
        {
            if (userItem == null)
            {
                return BadRequest();
            }
            await _userService.Create(userItem, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserRequest updatedUserItem, CancellationToken cancellationToken)
        {
            if (updatedUserItem == null)
            {
                return BadRequest();
            }

            await _userService.Update(id, updatedUserItem, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id, CancellationToken cancellationToken)
        {
            var deletedUserItem = _userService.Delete(id, cancellationToken);

            if (deletedUserItem == null)
            {
                return BadRequest();
            }
            return new ObjectResult(deletedUserItem);
        }
    }
}
