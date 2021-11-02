using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace APIAnnouncements.Services.RecaptchaService
{
   public interface IRecaptchaService
    {
        Task<RecaptchaResponse> Validate(IFormCollection form);
    }
}
