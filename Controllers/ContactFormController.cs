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
using ThomasGrant.Utility;
using Org.BouncyCastle.Asn1.X509;

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

            #region Pull Data Frontend

            var sender = "noreply@thomasgrant.uk";
            Contact contactForm = null;

            using (var umbracoContextReference = _contextFactory.EnsureUmbracoContext())
            {
                IPublishedContent content = umbracoContextReference.UmbracoContext.Content.GetAtRoot().FirstOrDefault();

                if (content != null)
                {
                    Master master = content as Master;
                    sender = (!String.IsNullOrEmpty(master.NotificationSender)) ? master.NotificationSender : sender;
                    contactForm = master.ContactForm as Contact;

                }
            }

            #endregion 

            #region Check State 
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                foreach (var error in errors)
                {
                    TempData["Errors"] = error;
                }
                return CurrentUmbracoPage();
            }


            #endregion

            try
            {
                if(contactForm != null)
                {
                    var submission = _contentService.Create(model.Name + " | Form Submission", contactForm.Id, "formSubmission", -1);
                    submission.SetValue("sender", model.Name);
                    submission.SetValue("emailAddress", model.Email);
                    submission.SetValue("message", model.Message);
                    _contentService.SaveAndPublish(submission);
                }
                
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to capture submission: " + ex.Message);
            }


            #region Notification

            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(sender);
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

                    EmailSender.SendEmail(message);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Faild to send Notification: " + ex.Message);
            }


            #endregion

            #region Confirmation
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(sender);
                    message.IsBodyHtml = true;

                    EmailViewModel confBody = new EmailViewModel()
                    {
                        Title = "Thanks For Getting in Touch",
                        Name = model.Name,
                        Email = model.Email,
                        Message = model.Message
                    };

                    if ((model.FileRequest != null) && (model.FileRequest == true))
                    {
                        using (var umbracoContextReference = _contextFactory.EnsureUmbracoContext())
                        {
                            IPublishedContent content = umbracoContextReference.UmbracoContext.Content.GetAtRoot().FirstOrDefault();

                            if (content != null)
                            {
                                Master master = content as Master;
                                contactForm = master.ContactForm as Contact;
                                var file = contactForm.CV;

                                if (file != null)
                                {
                                    string dir = Directory.GetCurrentDirectory();
                                    string p = "/wwwroot/" + file;
                                    string path = dir + file;

                                    message.Attachments.Add(new Attachment(path));

                                }
                            }
                        }
                    }

                    message.To.Add(new MailAddress(model.Email));
                    message.Subject = "Thanks For Getting in Touch";
                    message.Body = await RazorTemplateEngine.RenderAsync("Confirmation", confBody);
                    EmailSender.SendEmail(message);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to send confirmation: " + ex.Message);
            }

            #endregion


            return Redirect("/thanks");
        }
    }
}