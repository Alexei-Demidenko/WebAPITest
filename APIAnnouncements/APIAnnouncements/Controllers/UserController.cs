using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APIAnnouncements.dbo;
using APIAnnouncements.Models;
using APIAnnouncements.Services;
using Microsoft.AspNetCore.Mvc;


namespace APIAnnouncements.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        

        [HttpGet]
        public async Task<IActionResult> Get(Guid Id, CancellationToken cancellationToken)
        {
            UserResponse item = await _userService.Get(Id, cancellationToken);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequest UserItem, CancellationToken cancellationToken)
        {
            if (UserItem == null)
            {
                return BadRequest();
            }
            await _userService.Create(UserItem, cancellationToken);
            return (IActionResult)UserItem;
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid Id, [FromBody] UserRequest updatedUserItem, CancellationToken cancellationToken)
        {
            if (updatedUserItem == null)
            {
                return BadRequest();
            }

           await _userService.Update(Id, updatedUserItem, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id, CancellationToken cancellationToken)
        {
            var deletedUserItem = _userService.Delete(Id, cancellationToken);

            if (deletedUserItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedUserItem);
        }
    }
}
