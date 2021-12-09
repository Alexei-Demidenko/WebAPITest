using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DataTransferObject.AnnoncDTO;
using BusinessLayer.Interfaces;
using QueryParameters = BusinessLayer.Utils.QueryParameters;

namespace APIAnnouncements.Controllers
{
    [Route("api/[controller]")]
    public class AnnouncingController : Controller
    {
        private readonly IAnnouncService _announcService;
        //private readonly IRecaptchaService _recaptcha;

        public AnnouncingController(IAnnouncService announcService/*, IRecaptchaService recaptcha*/)
        {
            _announcService = announcService;
            //_recaptcha = recaptcha ?? throw new ArgumentNullException(nameof(recaptcha));
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var announcingItem = await _announcService.Get(id, cancellationToken).ConfigureAwait(false);

            if (announcingItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(announcingItem);
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult> GetArray([FromQuery] QueryParameters queryParameters, int page = 1, int pageSize = 25, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _announcService.GetObjectArray(queryParameters, page, pageSize, cancellationToken));

        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AnnoncCreateRequestDto announcingItem, CancellationToken cancellationToken)
        {
            //var captchaResponse = await  _recaptcha.Validate(Request.Form);
            //if (!captchaResponse.Success) throw new ReCaptchaErrorException("Ошибка ReCaptcha. Не прошел проверку.");
            if (announcingItem == null)
            {
                return BadRequest();
            }

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(announcingItem, new ValidationContext(announcingItem), results, true))
            {
                return BadRequest(results);
            }

            await _announcService.Create(announcingItem, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] AnnoncUpdateRequestDto updatedAnnouncingItem, CancellationToken cancellationToken)
        {
            if (updatedAnnouncingItem == null)
            {
                return BadRequest();
            }

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(updatedAnnouncingItem, new ValidationContext(updatedAnnouncingItem), results, true))
            {
                return BadRequest(results);
            }

            await _announcService.Update(id, updatedAnnouncingItem, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deletedAnnouncingItem = await _announcService.Delete(id, cancellationToken);

            if (deletedAnnouncingItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedAnnouncingItem);
        }
    }
}
