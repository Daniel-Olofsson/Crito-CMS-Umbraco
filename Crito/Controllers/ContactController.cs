using Crito.Models;
using Crito.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Crito.Controllers;

public class ContactController : SurfaceController
{
    public ContactController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {

    }
    [HttpPost]
    public async Task<IActionResult> Index(FormModel formModel)
    {
        if (!ModelState.IsValid) 
        {
            return RedirectToCurrentUmbracoPage();
        }

        using var mail = new MailService("no-reply@crito.com", "smtp.freesmtpservers.com", 25, "UmbracoStudentDev@hotmail.com", "mypassword123");
        //Sender then receiver
        await mail.SendAsync(formModel.Email, "Your contact request was received", "Thanks for contacting us! When the moon shines in its forth cycle you will hear from us..");

        await mail.SendAsync("UmbracoStudentDev@hotmail.com", $"{ formModel.Name} sent you a message", $"{formModel.Message}");

        return LocalRedirect(formModel.RedirectUrl ?? "/");

    }
    //USE MAILTRAP.IO AND SWITCH EMAIL AND IT WORKS!!!!!
    [HttpPost]
    public ActionResult Subscribe(SubscribeModel subModel)
    {
        if (ModelState.IsValid)
        {
            using (var client = new SmtpClient("sandbox.smtp.mailtrap.io"))
            {
                client.Port = 2525;
                client.Credentials = new NetworkCredential("b4263e36145a22", "2dcfc3a62b301f");
                client.EnableSsl = true; // Use SSL for secure connection

                var message = new MailMessage
                {
                    From = new MailAddress("daniel-olofsson@hotmail.se"), // Replace with your email
                    Subject = "Subscription Confirmation",
                    Body = "Thank you for subscribing!",
                    IsBodyHtml = true // You can use HTML in the email body
                };

                message.To.Add(subModel.Email);

                client.Send(message);
            }
        }
        //USE MAILTRAP AND SWITCH EMAIL AND IT WORKS!!!!!
        //USE MAILTRAP AND SWITCH EMAIL AND IT WORKS!!!!!
        return LocalRedirect("/");
    }
}
