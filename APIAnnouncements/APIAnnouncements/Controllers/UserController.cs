using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject.UserDTO;
using BusinessLayer.Interfaces;
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
            var item = await _userService.Get(id, cancellationToken);

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequestDto userItem, CancellationToken cancellationToken)
        {
            if (userItem == null)
            {
                return BadRequest();
            }

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(userItem, new ValidationContext(userItem), results, true))
            {
                return BadRequest(results);
            }

            await _userService.Create(userItem, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserRequestDto updatedUserItem, CancellationToken cancellationToken)
        {
            if (updatedUserItem == null)
            {
                return BadRequest();
            }

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(updatedUserItem, new ValidationContext(updatedUserItem), results, true))
            {
                return BadRequest(results);
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
