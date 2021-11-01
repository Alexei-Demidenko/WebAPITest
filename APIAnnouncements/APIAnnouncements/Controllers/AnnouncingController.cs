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
	public class AnnouncingController : Controller
	{
        private IAnnouncService _announcService;

        public AnnouncingController(IAnnouncService announcService)
        {
            _announcService = announcService;
        }       

        [HttpGet("{id}", Name = "GetAnnouncingItem")]
        public async Task<IActionResult> Get(Guid Id, CancellationToken cancellationToken)
        {
            AnnoncResponse AnnouncingItem = await _announcService.Get(Id, cancellationToken).ConfigureAwait(false);

            if (AnnouncingItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(AnnouncingItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnnoncRequest AnnouncingItem, CancellationToken cancellationToken)
        {
            if (AnnouncingItem == null)
            {
                return BadRequest();
            }
           await _announcService.Create(AnnouncingItem, cancellationToken);
            return (IActionResult)AnnouncingItem;
        }

        [HttpPut]
        public async Task<IActionResult>Update(Guid Id, [FromBody] AnnoncRequest updatedAnnouncingItem, CancellationToken cancellationToken)
        {
            if (updatedAnnouncingItem == null)
            {
                return BadRequest();
            }
            await _announcService.Update(Id,updatedAnnouncingItem, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id, CancellationToken cancellationToken)
        {
            var deletedAnnouncingItem = _announcService.Delete(Id, cancellationToken);

            if (deletedAnnouncingItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedAnnouncingItem);
        }
    }
}
