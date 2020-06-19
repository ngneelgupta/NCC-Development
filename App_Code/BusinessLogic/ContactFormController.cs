using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Core;
using NCC.Models;
/// <summary>
/// Summary description for ContactFormController
/// </summary>
///
namespace NCC.BusinessLogic
{
    public class ContactFormController : SurfaceController
    {
        [HttpPost]
        public ActionResult BlogReply(BlogCommentModel model)
        {
            try
            {
                string name = model.ContactName;
                string email = model.Email;
                string message = model.Message;
                string blogid = model.BlogPostID;
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message))
                {
                    var contentService = Services.ContentService;
                    var parent = contentService.GetById(int.Parse(blogid));
                    var BlogComment = contentService.CreateContent(name, parent.GetUdi(), "blogPostComment");

                    BlogComment.SetValue("Commentedname", name);
                    BlogComment.SetValue("email", email);
                    BlogComment.SetValue("comment", message);
                    contentService.SaveAndPublish(BlogComment);


                    return Json(new ContactFormResult
                    {
                        result = true,
                        message = "Comment Save Successfully."
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {

                    return Json(new ContactFormResult
                    {
                        result = false,
                        message = "Please provide a valid e-mail, phone number and message."
                    }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ee)
            {

                return Json(new ContactFormResult
                {
                    result = false,
                    message = "Some unexpected error !" + ee
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult ContactUs(ContactModel model)
        {
            try
            {
                var GlobalsettingId = Umbraco.Content(model.GlobalsettingId);
                var NodeId = Umbraco.Content(model.NodeId);
                string MailSenderDisplayName = GlobalsettingId.GetProperty("mailSenderDisplayName").GetValue().ToString();
                string MailSenderUserName = GlobalsettingId.GetProperty("mailSenderUserName").GetValue().ToString();
                string MailSenderPass = GlobalsettingId.GetProperty("mailSenderPassword").GetValue().ToString();
                string MailHost = GlobalsettingId.GetProperty("mailHost").GetValue().ToString();
                int PortNumber = int.Parse(GlobalsettingId.GetProperty("portNumber").GetValue().ToString());
                bool EnableSsl = Convert.ToBoolean(GlobalsettingId.GetProperty("enableSsl").GetValue());

                string receiverEmailId = GlobalsettingId.GetProperty("receiverEmailId").GetValue().ToString();
                string name = model.ContactName;
                string email = model.Email;
                string phone = model.PhoneNumber;
                string message = model.Message;
                string disease = model.disease;

                string subject = NodeId.GetProperty("contactUsEmailSubject").GetValue().ToString();
                var Clientemailtemplate = NodeId.GetProperty("contactUsEmailTemplate").GetValue().ToString();
                //var Useremailtemplate = Node.GetProperty("contactUsUserEmailTemplate").GetValue().ToString();
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(disease))
                {

                    Clientemailtemplate = Clientemailtemplate.Replace("/@logo", "http://13.239.35.111/images/logo.png");
                    Clientemailtemplate = Clientemailtemplate.Replace("@username", name);
                    Clientemailtemplate = Clientemailtemplate.Replace("@email", email);
                    Clientemailtemplate = Clientemailtemplate.Replace("@phone", phone);
                    Clientemailtemplate = Clientemailtemplate.Replace("@Disease", disease);
                    Clientemailtemplate = Clientemailtemplate.Replace("@message", message);

                    //Useremailtemplate = Useremailtemplate.Replace("/@logo", "https://mintworxs.azurewebsites.net/media/o0dneckd/websitelogo.svg");
                    //Useremailtemplate = Useremailtemplate.Replace("@username", name);

                    MailSender mail = new MailSender();
                    mail.SendMail(receiverEmailId, Clientemailtemplate, subject, MailSenderDisplayName, MailSenderUserName, MailSenderPass, MailHost, PortNumber, EnableSsl);
                    //mail.SendMail(email, Useremailtemplate, subject, MailSenderDisplayName, MailSenderUserName, MailSenderPass, MailHost, PortNumber, EnableSsl);
                    return Json(new ContactFormResult
                    {
                        result = true,
                        message = "Message Sent Successfully."
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {

                    return Json(new ContactFormResult
                    {
                        result = false,
                        message = "Please provide a valid e-mail, phone number, subject and message."
                    }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ee)
            {

                return Json(new ContactFormResult
                {
                    result = false,
                    message = "Some unexpected error !" + ee
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
    public class ContactFormResult
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}