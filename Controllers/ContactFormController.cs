using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Mail;
using Umbraco.Cms.Core.Models.Email;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Cms.Web.Common.Filters;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;
using Razor.Templating.Core;
using Umbraco.Cms.Web.Common.UmbracoContext;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Core.Models.PublishedContent;
using Org.BouncyCastle.Cms;
using ThomasGrant.ViewModels;
using Umbraco.Extensions;

namespace ThomasGrant.Controllers
{
    public class ContactFormController : SurfaceController
    {
        string Sender;
        IContentService _contentService;
        IUmbracoContextFactory _contextFactory;
        public ContactFormController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IContentService contentSerivce,
            IUmbracoContextFactory contextFactory)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _contentService = contentSerivce;
            _contextFactory = contextFactory;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateUmbracoFormRouteString]
        public async Task<IActionResult> Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var submission = _contentService.Create(model.Name + " | Form Submission", 2082, "formSubmission", -1);
            submission.SetValue("sender", model.Name);
            submission.SetValue("emailAddress", model.Email);
            submission.SetValue("message", model.Message);
            _contentService.SaveAndPublish(submission);

            var message = new MailMessage();
            message.From = new MailAddress("noreply@test.co.uk");
            message.IsBodyHtml = true;
            EmailViewModel emailModel = new EmailViewModel()
            {
                Title = "Contact Form Submission",
                Name = model.Name,
                Email = model.Email,
                Message = model.Message
            };

            message.To.Add(new MailAddress("thomasgrant0801@gmail.com"));
            message.CC.Add(new MailAddress("tomtomtto@gmail.com"));
            message.Subject = "New Contact Form Submission";
            message.Body = await RazorTemplateEngine.RenderAsync("Email.cshtml", emailModel);

            using (var client = new SmtpClient())
            {
                client.Host = "84.18.208.150";
                client.Credentials = new System.Net.NetworkCredential("test@moiraelive.co.uk", "Password1234*");
                client.Send(message);
            }

            return Redirect("/thanks");
        }
    }
}