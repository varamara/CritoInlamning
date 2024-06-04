using CritoInlamning.Models;
using CritoInlamning.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace CritoInlamning.Controllers
{
    public class ContactsController : SurfaceController
    {
        public ContactsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]

        public IActionResult Index(ContactForm contactForm)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            using var mail = new MailService("no-reply@crito.com", "smtp.crito.se", 587, "contactform@crito.com", "BytMig123!");
            // to sender 
            mail.SendAsync(contactForm.Email, "Your contact request was recieved", contactForm.Message).ConfigureAwait(false);

            //to me
            mail.SendAsync("contactform@crito.com", $"Contact request from {contactForm.Name}", contactForm.Message).ConfigureAwait(false);

            return LocalRedirect(contactForm.RedirectUrl ?? "/");
        }
    }
}
