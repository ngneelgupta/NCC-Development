using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Core;
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
        
    }
    public class ContactFormResult
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}