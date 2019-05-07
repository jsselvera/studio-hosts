using SS.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contact)
        {
            //Create body of email
            string body = $"Name: {contact.Name}<br>Email: {contact.Email}<br>Subject: {contact.Subject}" +
                $"<br>Message:<br>{contact.Message}";

            //Create and configure the MailMessage object (using System.Net.Mail)
            MailMessage msg = new MailMessage(
                "no-reply@jakeuery.com", //message from
                "jakeselvera@outlook.com", //message to
                "StudioHosts - " + contact.Subject,
                body
                );

            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;

            //Create and configure the SmtpClient (using System.Net.Mail)
            SmtpClient client = new SmtpClient("mail.jakeuery.com");
            client.Credentials = new NetworkCredential("no-reply@jakeuery.com", "Jake25rd*");

            //Attempt to send an email

            //using makes use of object and then disposes of it
            //after using, client object is disposed and connection is closed
            using (client)
            {
                //try catch is used to test potentially dangerous code
                //if the code fails, we will stop the exception from halting the app,
                //and instead display a user friendly error message
                try
                {
                    if (ModelState.IsValid)
                    {
                        client.Send(msg);
                    }

                    else
                    {
                        return View(contact);
                    }
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = "There was an error sending your message. Please try again.";
                    return View(contact);
                }
            }

            //If email sends and validation is passed, send confirmation.
            return View("ContactConfirmation", contact);
        }

    }
}
