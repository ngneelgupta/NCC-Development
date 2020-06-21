using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Core;
using NCC.Models;
using NCC.BusinessLogic.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
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

        [HttpPost]
        public ActionResult RegisterPatient(PatientRegisterDataModel model)
        {
            try
            {
                if (model != null)
                {
                    var contentService = Services.ContentService;
                    var resultNode = contentService.GetById(model.NodeId);

                    var patientNode = contentService.CreateAndSave(model.additionalInfo.patientname, model.NodeId, "patientInformation");

                    if (model.personalDetails != null)
                    {
                        //Emergency Contacts
                        List<Dictionary<string, object>> emergencycontact = new List<Dictionary<string, object>>();
                        if (model.personalDetails.emergencycontact != null && model.personalDetails.emergencycontact.Count > 0)
                        {
                            foreach (var item in model.personalDetails.emergencycontact)
                            {
                                emergencycontact.Add(new Dictionary<string, object>(){
                                { "key", Guid.NewGuid() },
                                { "ncContentTypeAlias", "emergencyContactInfoNC" },
                                { "relativeName", item.relativename },
                                { "relationship", item.relationship },
                                { "contactNumber", item.phonenumber }
                            });
                            }
                        }

                        patientNode.SetValue("patientSPersonalDetailsTitle", model.personalDetails.title);
                        patientNode.SetValue("patientSPersonalDetailsSurname", model.personalDetails.surname);
                        patientNode.SetValue("patientSPersonalDetailsGivenNameS", model.personalDetails.givenname);
                        patientNode.SetValue("patientSPersonalDetailsPreferredName", model.personalDetails.preferredname);
                        patientNode.SetValue("patientSPersonalDetailsDateOfBirth", model.personalDetails.birthdate);
                        patientNode.SetValue("patientSPersonalDetailsPhone", model.personalDetails.phone);
                        patientNode.SetValue("patientSPersonalDetailsEmail", model.personalDetails.email);
                        patientNode.SetValue("patientSPersonalDetailsAboriginalOrTorresStraitIslander", model.personalDetails.straisislander); //bool
                        patientNode.SetValue("patientSPersonalDetailsOccupation", model.personalDetails.occupation);
                        patientNode.SetValue("patientSPersonalDetailsAddress", model.personalDetails.address);
                        patientNode.SetValue("patientSPersonalDetailsSuburb", model.personalDetails.suburb);
                        patientNode.SetValue("patientSPersonalDetailsState", model.personalDetails.state);
                        patientNode.SetValue("patientSPersonalDetailsPostCode", model.personalDetails.postcode);
                        patientNode.SetValue("patientSPersonalDetailsHomePhone", model.personalDetails.homephone);
                        patientNode.SetValue("patientSPersonalDetailsWorkPhone", model.personalDetails.workphone);
                        patientNode.SetValue("patientSPersonalDetailsMobileNumber", model.personalDetails.mobile);
                        patientNode.SetValue("patientSPersonalDetailsWorkEmail", model.personalDetails.workemail);
                        patientNode.SetValue("patientSPersonalDetailsMedicareNumber", model.personalDetails.medicare);
                        patientNode.SetValue("patientSPersonalDetailsRefNo", model.personalDetails.refno);
                        patientNode.SetValue("patientSPersonalDetailsMedicareExpiry", model.personalDetails.medicareexpiry);
                        patientNode.SetValue("patientSPersonalDetailsDVANo", model.personalDetails.dva);
                        patientNode.SetValue("patientSPersonalDetailsExpiryDate", model.personalDetails.expirydate);
                        patientNode.SetValue("patientSPersonalDetailsNativeLanguage", model.personalDetails.nativelanguage);
                        patientNode.SetValue("patientSPersonalDetailsIfOtherThanLanguageWellYouRequiredACertifiedTranslator", model.personalDetails.certificate);
                        patientNode.SetValue("patientSPersonalDetailsEmergencyContactsInformation", JsonConvert.SerializeObject(emergencycontact)); //NC
                        patientNode.SetValue("patientSPersonalDetailsEmploymentStatus", model.personalDetails.employment);
                    }

                    if (model.personalHistory != null)
                    {
                        //Past Medication
                        List<Dictionary<string, object>> pastmedication = new List<Dictionary<string, object>>();
                        if (model.personalHistory.pastmedication != null && model.personalHistory.pastmedication.Count > 0)
                        {
                            foreach (var item in model.personalHistory.pastmedication)
                            {
                                pastmedication.Add(new Dictionary<string, object>(){
                                { "key", Guid.NewGuid() },
                                { "ncContentTypeAlias", "personalMedicalHistoryAllPastMedicationsNC" },
                                { "nameOfMedication", item.Medication},
                                { "lengthOfTime", item.time }
                            });
                            }
                        }

                        patientNode.SetValue("personalMedicalHistoryDoYouCurrentlyExperienceOrHaveAHistoryOfAnyOfTheFollowingMedicalCondition", model.personalHistory.currentlyexperience);
                        patientNode.SetValue("personalMedicalHistoryIfOther", model.personalHistory.othercondition);
                        patientNode.SetValue("personalMedicalHistoryPotentialContraindications", model.personalHistory.potentialcontraindications);
                        patientNode.SetValue("personalMedicalHistoryListOfAllPastMedicationsTakenForYourConditionsAndTheLengthOfTimeTakenOrTrailed", JsonConvert.SerializeObject(pastmedication)); //NC
                    }

                    if (model.additionalInfo != null)
                    {
                        //Current Medical
                        List<Dictionary<string, object>> currentmedical = new List<Dictionary<string, object>>();
                        if (model.additionalInfo.currentmedicalList != null && model.additionalInfo.currentmedicalList.Count > 0)
                        {
                            foreach (var item in model.additionalInfo.currentmedicalList)
                            {
                                currentmedical.Add(new Dictionary<string, object>(){
                                { "key", Guid.NewGuid() },
                                { "ncContentTypeAlias", "additionalInformationListOfAllCurrentMedicalAndSpecialistNC" },
                                { "practitionerName", item.practitioner},
                                { "specialty", item.speciality },
                                { "phoneNumber", item.Phoneno },
                                { "practiceAddress", item.practiceaddress },
                                { "approximateDateOfLastConsultaion", item.conclusion }
                            });
                            }
                        }

                        patientNode.SetValue("additionalInformationListOfAllCurrentMedicalAndSpecialist", JsonConvert.SerializeObject(currentmedical)); //NC
                        patientNode.SetValue("acceptNCTermsAndConditions", model.additionalInfo.termandcondition); //bool
                        patientNode.SetValue("agreeToAllNationalCanabinoidClinicToAcessYourMedicalHistoryRecordsIfNeeded", model.additionalInfo.agreetoall);  //bool
                        patientNode.SetValue("understandThatAssessmentByOurDoctorsDoesNotEnsureApprovalAndAccessToMedicalCannabis", model.additionalInfo.understandthat);  //bool
                        patientNode.SetValue("additionalInformationPatientFullName", model.additionalInfo.patientname);
                        patientNode.SetValue("additionalInformationDateOfBirth", model.additionalInfo.patientdate);
                        patientNode.SetValue("additionalInformationSourceOfDecisionMakingAuthority", model.additionalInfo.sourceofdecision);
                        patientNode.SetValue("additionalInformationDecisionMakerName", model.additionalInfo.decisionmakername);
                        patientNode.SetValue("additionalInformationDecisionMakerRelation", model.additionalInfo.decisionmakerrealtion);
                        patientNode.SetValue("additionalInformationDecisionMakerDate", model.additionalInfo.decisionmakerdate);
                        patientNode.SetValue("additionalInformationWitness", model.additionalInfo.witness);
                        patientNode.SetValue("additionalInformationWitnessRelation", model.additionalInfo.witnessrelation);
                        patientNode.SetValue("additionalInformationWitnessDate", model.additionalInfo.witnessdate);
                    }

                    contentService.SaveAndPublish(patientNode);

                    return Json(new ContactFormResult()
                    {
                        message = patientNode.Id.ToString(),
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new ContactFormResult()
                    {
                        message = "Please fill the patient registration form !!",
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ContactFormResult()
                {
                    message = ex.Message,
                    result = false
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