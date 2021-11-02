using System;
using System.Threading;
using System.Threading.Tasks;
using APIAnnouncements.dbo;
using APIAnnouncements.Services;
using APIAnnouncements.Utils;
using Microsoft.AspNetCore.Mvc;
using APIAnnouncements.Services.RecaptchaService;
using APIAnnouncements.Exceptions;

namespace APIAnnouncements.Controllers
{
	[Route("api/[controller]")]
	public class AnnouncingController : Controller
	{
        private IAnnouncService _announcService;       
        private IRecaptchaService _recaptcha;

        public AnnouncingController(IAnnouncService announcService, IRecaptchaService recaptcha)
        {
            _announcService = announcService;
            _recaptcha = recaptcha ?? throw new ArgumentNullException(nameof(recaptcha));
        }       

        [HttpGet]
        public async Task<IActionResult> Get(Guid Id, CancellationToken cancellationToken)
        {
            AnnoncResponse AnnouncingItem = await _announcService.Get(Id, cancellationToken).ConfigureAwait(false);

            if (AnnouncingItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(AnnouncingItem);
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult> GetArray([FromQuery] QueryParameters queryParameters, int page = 1, int pageSize = 25, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _announcService.GetObjectArray(queryParameters, page, pageSize, cancellationToken));

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnnoncRequest AnnouncingItem, CancellationToken cancellationToken)
        {
            var captchaResponse = await _recaptcha.Validate(Request.Form);
            if (!captchaResponse.Success) throw new ReCaptchaErrorException("Ошибка ReCaptcha. Не прошел проверку.");
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
