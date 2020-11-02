using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Core;
using NCC.Models;
using NCC.BusinessLogic.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using Umbraco.Web;

namespace NCC.BusinessLogics
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
                

                string subject = NodeId.GetProperty("contactUsEmailSubject").GetValue().ToString();
                var Clientemailtemplate = NodeId.GetProperty("contactUsEmailTemplate").GetValue().ToString();
                //var Useremailtemplate = Node.GetProperty("contactUsUserEmailTemplate").GetValue().ToString();
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(subject))
                {

                    Clientemailtemplate = Clientemailtemplate.Replace("/@logo", "https://ncclinics.com.au/images/logo.png");
                    Clientemailtemplate = Clientemailtemplate.Replace("@username", name);
                    Clientemailtemplate = Clientemailtemplate.Replace("@email", email);
                    Clientemailtemplate = Clientemailtemplate.Replace("@phone", phone);
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
        public ActionResult eligibilityform(ContactModel model)
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

                    Clientemailtemplate = Clientemailtemplate.Replace("/@logo", "https://ncclinics.com.au/images/logo.png");
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
        public ActionResult RegisterPatient(RegisterDataModel model)
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

                        List<Dictionary<string, object>> aboutNCC = new List<Dictionary<string, object>>();
                        if (model.personalDetails.employment != null && model.personalDetails.employment.Count > 0)
                        {
                            foreach (var item in model.personalDetails.employment)
                            {
                                aboutNCC.Add(new Dictionary<string, object>(){
                                { "key", Guid.NewGuid() },
                                { "ncContentTypeAlias", "textNC" },
                                { "content", item }
                            });
                            }
                        }

                        patientNode.SetValue("patientSPersonalDetailsTitle", model.personalDetails.title);
                        patientNode.SetValue("patientSPersonalDetailsSurname", model.personalDetails.surname);
                        patientNode.SetValue("patientSPersonalDetailsGivenNameS", model.personalDetails.givenname);
                        patientNode.SetValue("patientSPersonalDetailsPreferredName", model.personalDetails.preferredname);
                        patientNode.SetValue("patientSPersonalDetailsGender", model.personalDetails.gender);
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
                        patientNode.SetValue("patientSPersonalDetailsEmploymentStatus", JsonConvert.SerializeObject(aboutNCC)); //NC
                        patientNode.SetValue("patientSPersonalDetailsOtherPleaseSpec", model.personalDetails.other);
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

                        //Patient Suffer Condition
                        List<Dictionary<string, object>> currentlyexperience = new List<Dictionary<string, object>>();
                        if (model.personalHistory.currentlyexperience != null && model.personalHistory.currentlyexperience.Count > 0)
                        {
                            foreach (var item in model.personalHistory.currentlyexperience)
                            {
                                currentlyexperience.Add(new Dictionary<string, object>(){
                                { "key", Guid.NewGuid() },
                                { "ncContentTypeAlias", "textNC" },
                                { "content", item }
                            });
                            }
                        }

                        patientNode.SetValue("personalMedicalHistoryDoYouCurrentlyExperienceOrHaveAHistoryOfAnyOfTheFollowingMedicalCondition", JsonConvert.SerializeObject(currentlyexperience)); //NC
                        patientNode.SetValue("personalMedicalHistoryIfOther", model.personalHistory.othercondition);
                        patientNode.SetValue("personalMedicalHistoryPotentialContraindications", model.personalHistory.potentialcontraindications);
                        patientNode.SetValue("personalMedicalHistoryListOfAllPastMedicationsTakenForYourConditionsAndTheLengthOfTimeTakenOrTrailed", JsonConvert.SerializeObject(pastmedication)); //NC

                        patientNode.SetValue("personalMedicalHistoryDoYouHaveAnyKnownAllergiesPleaseList", model.personalHistory.allergies);
                        patientNode.SetValue("personalMedicalHistoryAreYouCurrentlyPregnantPlanningToBecomePregnantOrBreastfeeding", model.personalHistory.pregnant);
                        patientNode.SetValue("personalMedicalHistorySmokingStatus", model.personalHistory.smoking);
                        patientNode.SetValue("personalMedicalHistoryIfSmokerNumberPerDay", model.personalHistory.smokernumber);
                        patientNode.SetValue("personalMedicalHistoryDoYouDrinkAlcohol", model.personalHistory.alcohol);
                        patientNode.SetValue("personalMedicalHistoryIfYesHowManyStandardDrinksPerWeek", model.personalHistory.drinknumber);
                        patientNode.SetValue("personalMedicalHistoryWhenDidYouLastHaveAnOverallCheckUp", model.personalHistory.checkup);
                        patientNode.SetValue("personalMedicalHistoryWhenDidYouLastHaveAnOverallCheckUpStatus", model.personalHistory.overallcheckup);
                        patientNode.SetValue("personalMedicalHistoryAreYouCurrentlyDriving", model.personalHistory.drivingyes);
                        patientNode.SetValue("personalMedicalHistoryDoYouWantToContinueDriving", model.personalHistory.continuedriving);
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
                        message = "Patient Register Successfully.",
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

        [HttpPost]
        public ActionResult ReferralForm(RegisterDataModel model)
        {
            try
            {
                if (model != null)
                {
                    var contentService = Services.ContentService;
                    var resultNode = contentService.GetById(model.NodeId);

                    var patientNode = contentService.CreateAndSave(model.personalDetails.f_name + " " + model.personalDetails.l_name, model.NodeId, "referralFormInformation");

                    if (model.personalDetails != null)
                    {
                        patientNode.SetValue("tITLE", model.personalDetails.title);
                        patientNode.SetValue("firstName", model.personalDetails.f_name);
                        patientNode.SetValue("lastName", model.personalDetails.l_name);
                        patientNode.SetValue("gender", model.personalDetails.gender);
                        patientNode.SetValue("dateOfBirth", model.personalDetails.birthdate);
                        patientNode.SetValue("phone", model.personalDetails.phone);
                        patientNode.SetValue("email", model.personalDetails.email);
                        patientNode.SetValue("address", model.personalDetails.address);
                        patientNode.SetValue("suburb", model.personalDetails.suburb);
                        patientNode.SetValue("state", model.personalDetails.state);
                        patientNode.SetValue("postcode", model.personalDetails.postcode);
                        patientNode.SetValue("medicareNumber", model.personalDetails.medicare);
                        patientNode.SetValue("refNo", model.personalDetails.refno);
                        patientNode.SetValue("medicareExpiry", model.personalDetails.medicareexpiry);
                    }

                    if (model.personalHistory != null)
                    {
                        //Patient Suffer Condition
                        List<Dictionary<string, object>> patientsuffercondition = new List<Dictionary<string, object>>();
                        if (model.personalHistory.patientsuffercondition != null && model.personalHistory.patientsuffercondition.Count > 0)
                        {
                            foreach (var item in model.personalHistory.patientsuffercondition)
                            {
                                patientsuffercondition.Add(new Dictionary<string, object>(){
                                { "key", Guid.NewGuid() },
                                { "ncContentTypeAlias", "textNC" },
                                { "content", item }
                            });
                            }
                        }

                        patientNode.SetValue("dOESTHEPATIENTSUFFERFROMANYOFTHESECONDITIONS", JsonConvert.SerializeObject(patientsuffercondition)); //NC
                        patientNode.SetValue("iFOTHERPLEASESPECIFY", model.personalHistory.othercondition);
                        patientNode.SetValue("pLEASELISTALLPASTMEDICATIONSFORTHEPATIENTANDTHELENGTHTRIED", model.personalHistory.referralpastmedication);
                        patientNode.SetValue("rEASONFORREFERRAL", model.personalHistory.reasonreferral);
                        patientNode.SetValue("dOESYOURPATIENTHAVEACURRENTCAREPLANINPLACE", model.personalHistory.currentcareplan);
                        patientNode.SetValue("wOULDYOULIKEUSTODOACAREPLANREVIEWFORYOURPATIENT", model.personalHistory.review);
                        patientNode.SetValue("iFYOURPATIENTDOESNOTHAVEACURRENTCAREPLANINPLACEWOULDYOUCONSENTFOROURNCCDOCTORTOINITIATEONE", model.personalHistory.initiate);
                    }

                    if (model.practitionerDetails != null)
                    {
                        patientNode.SetValue("practitionerDetailsFullName", model.practitionerDetails.full_name);
                        patientNode.SetValue("practitionerDetailsPractitionerType", model.practitionerDetails.pract_type);
                        patientNode.SetValue("practitionerDetailsPhoneNumber", model.practitionerDetails.Phoneno);
                        patientNode.SetValue("practitionerDetailsEmail", model.practitionerDetails.email);
                        patientNode.SetValue("practitionerDetailsAddress", model.practitionerDetails.address);
                        patientNode.SetValue("practitionerDetailsProviderNumber", model.practitionerDetails.provider_no);
                        patientNode.SetValue("practitionerDetailsHealthLinkNumber", model.practitionerDetails.healthlink_no);
                        patientNode.SetValue("supportThisPatientToBeTreatedWithMedicinalCannabisIfNeeded", model.practitionerDetails.conditions);
                        patientNode.SetValue("iHereByReferTheAboveNamedPatientToADoctorAndOrSpecialistAtNationalCannabisClinics", model.practitionerDetails.history);
                        patientNode.SetValue("referralDate", model.practitionerDetails.date);
                    }

                    contentService.SaveAndPublish(patientNode);

                    return Json(new ContactFormResult()
                    {
                        message = "Referral form register Successfully.",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new ContactFormResult()
                    {
                        message = "Please fill the referral form information !!",
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

        [HttpPost]
        public ActionResult QualityOfLifeAssessmentForm(QualityOfLifeAssessmentDataModel model)
        {
            try
            {
                if (model != null)
                {
                    var contentService = Services.ContentService;
                    var resultNode = contentService.GetById(model.NodeId);

                    var patientNode = contentService.CreateAndSave(model.fullname, model.NodeId, "qualityOfLifeAssessmentInformation");

                    patientNode.SetValue("patientFullName", model.fullname);
                    patientNode.SetValue("patientSurname", model.surname);
                    patientNode.SetValue("patientDateOfBirth", model.dob);
                    patientNode.SetValue("mobility", model.mobility);
                    patientNode.SetValue("selfCare", model.selfcare);
                    patientNode.SetValue("usualActivities", model.usualactivities);
                    patientNode.SetValue("painDiscomfort", model.discomfort);
                    patientNode.SetValue("anxietyDepression", model.depression);
                    patientNode.SetValue("healthToday", model.health);

                    contentService.SaveAndPublish(patientNode);

                    return Json(new ContactFormResult()
                    {
                        message = "Quality of life assessment successfully.",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new ContactFormResult()
                    {
                        message = "Please fill the quality of life assessment form information !!",
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

        [HttpPost]
        public ActionResult DASSForm(DASSDataModel model)
        {
            try
            {
                if (model != null)
                {
                    var contentService = Services.ContentService;
                    var resultNode = contentService.GetById(model.NodeId);

                    var patientNode = contentService.CreateAndSave(model.fullname, model.NodeId, "depressionAnxietyStressScoreInformation");

                    patientNode.SetValue("patientFullName", model.fullname);
                    patientNode.SetValue("patientSurname", model.surname);
                    patientNode.SetValue("patientDateOfBirth", model.dob);
                    patientNode.SetValue("iFoundItHardToWindDown", model.HardWind);
                    patientNode.SetValue("iWasAwareOfDrynessOfMyMouth", model.dryness);
                    patientNode.SetValue("iCouldnTSeemToExperienceAnyPositiveFeelingAtAll", model.experience);
                    patientNode.SetValue("iExperiencedBreathingDifficulty", model.breathing);
                    patientNode.SetValue("iFoundItDifficultToWorkUpTheInitiativeToDoThings", model.initiative);
                    patientNode.SetValue("iTendedToOverReactToSituations", model.overreact);
                    patientNode.SetValue("iExperiencedTrembling", model.trembling);
                    patientNode.SetValue("iFeltThatIWasUsingALotOfNervousEnergy", model.nervous);
                    patientNode.SetValue("iWasWorriedAboutSituationsInWhichIMightPanicAndMakeAFoolOfMyself", model.worried);
                    patientNode.SetValue("iFeltThatIHadNothingToLookForwardTo", model.lookForward);
                    patientNode.SetValue("iFoundMyselfGettingAgitated", model.agitated);
                    patientNode.SetValue("iFoundItDifficultToRelax", model.relax);
                    patientNode.SetValue("iFeltDownHeartedAndBlue", model.downhearted);
                    patientNode.SetValue("iWasIntolerantOfAnythingThatKeptMeFromGettingOnWithWhatIWasDoing", model.intolerant);
                    patientNode.SetValue("iFeltIWasCloseToPanic", model.panic);
                    patientNode.SetValue("iWasUnableToBecomeEnthusiasticAboutAnything", model.enthusiastic);
                    patientNode.SetValue("iFeltIWasnTWorthMuchAsAPerson", model.person);
                    patientNode.SetValue("iFeltThatIWasRatherTouchy", model.touchy);
                    patientNode.SetValue("iWasAwareOfTheActionOfMyHeartInTheAbsenceOfPhysicalExertion", model.exertion);
                    patientNode.SetValue("iFeltScaredWithoutAnyGoodReason", model.scared);
                    patientNode.SetValue("iFeltThatLifeWasMeaningless", model.meaningless);

                    contentService.SaveAndPublish(patientNode);

                    return Json(new ContactFormResult()
                    {
                        message = "Depression anxiety stress score successfully.",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new ContactFormResult()
                    {
                        message = "Please fill the depression anxiety stress score form information !!",
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

        [HttpPost]
        public ActionResult BriefPainInventoryForm(BriefPainInventoryDataModel model)
        {
            try
            {
                if (model != null)
                {
                    var contentService = Services.ContentService;
                    var resultNode = contentService.GetById(model.NodeId);

                    var patientNode = contentService.CreateAndSave(model.fullname, model.NodeId, "briefPainInventoryInformation");

                    patientNode.SetValue("patientFullName", model.fullname);
                    patientNode.SetValue("patientSurname", model.surname);
                    patientNode.SetValue("patientDateOfBirth", model.dob);
                    patientNode.SetValue("pleaseRateYourPainBySelectingTheOneNumberThatBestDescribesYourPainAtItsWorstInTheLastWeek", model.pain);
                    patientNode.SetValue("pleaseRateYourPainBySelectingTheOneNumberThatBestDescribesYourPainAtItsLeastInTheLastWeek", model.leastpain);
                    patientNode.SetValue("pleaseRateYourPainBySelectingTheOneNumberThatBestDescribesYourPainOnAverage", model.average);
                    patientNode.SetValue("pleaseRateYourPainBySelectingTheOneNumberThatTellsHowMuchPainYouHaveRightNow", model.HowMuchPain);
                    patientNode.SetValue("whatTreatmentsOrMedicationsAreYouReceivingForYourpain", model.title);
                    patientNode.SetValue("inTheLastWeekHowMuchReliefHavePainTreatmentsOrMedicationsProvidedPleaseCircleTheOnePercentageThatBestShowsHowMuchReliefYouHaveReceived", model.medications);
                    patientNode.SetValue("generalActivity", model.generalActivity);
                    patientNode.SetValue("mood", model.mood);
                    patientNode.SetValue("walkingAbility", model.ability);
                    patientNode.SetValue("normalWork", model.housework);
                    patientNode.SetValue("relationsWithOtherPeople", model.Relations);
                    patientNode.SetValue("sleep", model.sleep);
                    patientNode.SetValue("enjoymentOfLife", model.Enjoyment);
                    patientNode.SetValue("painMarker", model.bodyMaker);

                    contentService.SaveAndPublish(patientNode);

                    return Json(new ContactFormResult()
                    {
                        message = "Brief pain inventory form save successfully.",
                        result = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new ContactFormResult()
                    {
                        message = "Please fill the brief pain inventory form information !!",
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

        //Download form file
        [HttpGet]
        public ActionResult DownloadFormCSV(int id, DateTime? startDate, DateTime? endDate, string SelectedPatientFormIds)
        {
            try
            {
                var Node = Umbraco.Content(id);

                //before your loop
                var csv = new StringBuilder();
                var newLine = string.Empty;

                var childrens = Node.Children != null && Node.Children.Count() > 0 && startDate != null ? Node.Children.Where(x => x.CreateDate >= startDate) : Node.Children;
                childrens = childrens != null && childrens.Count() > 0 && endDate != null ? Node.Children.Where(x => x.CreateDate <= endDate) : childrens;
                childrens = childrens != null && childrens.Count() > 0 && !string.IsNullOrEmpty(SelectedPatientFormIds) ? Node.Children.Where(x => SelectedPatientFormIds.Split(',').Where(y => y == x.Id.ToString()).FirstOrDefault() != null) : childrens;

                if (childrens != null && childrens.Count() > 0)
                {
                    if (id == 1301)
                    {
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                                "Full Name", "Surname", "Date Of Birth", "MOBILITY",
                                "Self Care",
                                "USUAL ACTIVITIES",
                                "PAIN/DISCOMFORT",
                                "ANXIETY/DEPRESSION",
                                "HEALTH TODAY");
                        csv.AppendLine(newLine);

                        foreach (var item in childrens)
                        {
                            //in your loop
                            var fullName = item.GetProperty("patientFullName").GetValue().ToString();
                            var surname = item.GetProperty("patientSurname").GetValue().ToString();
                            var dob = Convert.ToDateTime(item.GetProperty("patientDateOfBirth").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var mobility = item.GetProperty("mobility").GetValue().ToString();
                            var selfCare = item.GetProperty("selfCare").GetValue().ToString();
                            var usualActivities = item.GetProperty("usualActivities").GetValue().ToString();
                            var painDiscomfort = item.GetProperty("painDiscomfort").GetValue().ToString();
                            var anxietyDepression = item.GetProperty("anxietyDepression").GetValue().ToString();
                            var healthToday = item.GetProperty("healthToday").GetValue().ToString();

                            //Suggestion made by KyleMit
                            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                                fullName, surname, dob, mobility, selfCare, usualActivities, painDiscomfort, anxietyDepression, healthToday);
                            csv.AppendLine(newLine);
                        }
                    }
                    else if (id == 1306)
                    {
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23}",
                                "Full Name", "Surname", "Date Of Birth", "iFeltThatLifeWasMeaningless",
                                "I was aware of dryness of my mouth",
                                "I couldn't seem to experience any positive feeling at all",
                                "I experienced breathing difficulty",
                                "I found it difficult to work up the initiative to do things",
                                "I tended to over-react to situations",
                                "I experienced trembling", "I felt that I was using a lot of nervous energy",
                                "I was worried about situations in which I might panic and make a fool of myself",
                                "I felt that I had nothing to look forward to",
                                "I found myself getting agitated", "I found it difficult to relax", "I felt down-hearted and blue",
                                "I was intolerant of anything that kept me from getting on with what I was doing",
                                "I felt I was close to panic",
                                "I was unable to become enthusiastic about anything",
                                "I was unable to become enthusiastic about anything",
                                "I felt that I was rather touchy",
                                "I was aware of the action of my heart in the absence of physical exertion",
                                "I felt scared without any good reason",
                                "I felt that life was meaningless");
                        csv.AppendLine(newLine);

                        foreach (var item in childrens)
                        {
                            //in your loop
                            var fullName = item.GetProperty("patientFullName").GetValue().ToString();
                            var surname = item.GetProperty("patientSurname").GetValue().ToString();
                            var dob = Convert.ToDateTime(item.GetProperty("patientDateOfBirth").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var HardWind = item.GetProperty("iFoundItHardToWindDown").GetValue().ToString();
                            var dryness = item.GetProperty("iWasAwareOfDrynessOfMyMouth").GetValue().ToString();
                            var experience = item.GetProperty("iCouldnTSeemToExperienceAnyPositiveFeelingAtAll").GetValue().ToString();
                            var breathing = item.GetProperty("iExperiencedBreathingDifficulty").GetValue().ToString();
                            var initiative = item.GetProperty("iFoundItDifficultToWorkUpTheInitiativeToDoThings").GetValue().ToString();
                            var overreact = item.GetProperty("iTendedToOverReactToSituations").GetValue().ToString();
                            var trembling = item.GetProperty("iExperiencedTrembling").GetValue().ToString();
                            var nervous = item.GetProperty("iFeltThatIWasUsingALotOfNervousEnergy").GetValue().ToString();
                            var worried = item.GetProperty("iWasWorriedAboutSituationsInWhichIMightPanicAndMakeAFoolOfMyself").GetValue().ToString();
                            var lookForward = item.GetProperty("iFeltThatIHadNothingToLookForwardTo").GetValue().ToString();
                            var agitated = item.GetProperty("iFoundMyselfGettingAgitated").GetValue().ToString();
                            var relax = item.GetProperty("iFoundItDifficultToRelax").GetValue().ToString();
                            var downhearted = item.GetProperty("iFeltDownHeartedAndBlue").GetValue().ToString();
                            var intolerant = item.GetProperty("iWasIntolerantOfAnythingThatKeptMeFromGettingOnWithWhatIWasDoing").GetValue().ToString();
                            var panic = item.GetProperty("iFeltIWasCloseToPanic").GetValue().ToString();
                            var enthusiastic = item.GetProperty("iWasUnableToBecomeEnthusiasticAboutAnything").GetValue().ToString();
                            var person = item.GetProperty("iFeltIWasnTWorthMuchAsAPerson").GetValue().ToString();
                            var touchy = item.GetProperty("iFeltIWasnTWorthMuchAsAPerson").GetValue().ToString();
                            var exertion = item.GetProperty("iWasAwareOfTheActionOfMyHeartInTheAbsenceOfPhysicalExertion").GetValue().ToString();
                            var scared = item.GetProperty("iFeltScaredWithoutAnyGoodReason").GetValue().ToString();
                            var meaningless = item.GetProperty("iFeltThatLifeWasMeaningless").GetValue().ToString();

                            //Suggestion made by KyleMit
                            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23}",
                                fullName, surname, dob, HardWind, dryness, experience, breathing, initiative, overreact, trembling, nervous, worried,
                                lookForward, agitated, relax, downhearted, intolerant, panic, enthusiastic, person, touchy, exertion, scared, meaningless);
                            csv.AppendLine(newLine);
                        }
                    }
                    else if (id == 1313)
                    {
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                                "Full Name", "Surname", "Date Of Birth", "Please rate your pain by selecting the one number that best describes your pain at its worst in the last week",
                                "Please rate your pain by selecting the one number that best describes your pain at its least in the last week",
                                "Please rate your pain by selecting the one number that best describes your pain on average",
                                "Please rate your pain by selecting the one number that tells how much pain you have right now",
                                "What treatments or medications are you receiving for yourpain?",
                                "In the last week how much relief have pain treatments or medications provided? Please circle the one percentage that best shows how much relief you have received",
                                "General activity", "Mood", "Walking ability", "Normal work (includes both outside the home and housework)",
                                "Relations with other people", "Sleep", "Enjoyment of life");
                        csv.AppendLine(newLine);

                        foreach (var item in childrens)
                        {
                            //in your loop
                            var fullName = item.GetProperty("patientFullName").GetValue().ToString();
                            var surname = item.GetProperty("patientSurname").GetValue().ToString();
                            var dob = Convert.ToDateTime(item.GetProperty("patientDateOfBirth").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var pain = item.GetProperty("pleaseRateYourPainBySelectingTheOneNumberThatBestDescribesYourPainAtItsWorstInTheLastWeek").GetValue().ToString();
                            var leastpain = item.GetProperty("pleaseRateYourPainBySelectingTheOneNumberThatBestDescribesYourPainAtItsLeastInTheLastWeek").GetValue().ToString();
                            var average = item.GetProperty("pleaseRateYourPainBySelectingTheOneNumberThatBestDescribesYourPainOnAverage").GetValue().ToString();
                            var HowMuchPain = item.GetProperty("pleaseRateYourPainBySelectingTheOneNumberThatTellsHowMuchPainYouHaveRightNow").GetValue().ToString();
                            var title = item.GetProperty("whatTreatmentsOrMedicationsAreYouReceivingForYourpain").GetValue().ToString();
                            var medications = item.GetProperty("inTheLastWeekHowMuchReliefHavePainTreatmentsOrMedicationsProvidedPleaseCircleTheOnePercentageThatBestShowsHowMuchReliefYouHaveReceived").GetValue().ToString();
                            var generalActivity = item.GetProperty("generalActivity").GetValue().ToString();
                            var mood = item.GetProperty("mood").GetValue().ToString();
                            var ability = item.GetProperty("walkingAbility").GetValue().ToString();
                            var housework = item.GetProperty("normalWork").GetValue().ToString();
                            var Relations = item.GetProperty("relationsWithOtherPeople").GetValue().ToString();
                            var sleep = item.GetProperty("sleep").GetValue().ToString();
                            var Enjoyment = item.GetProperty("enjoymentOfLife").GetValue().ToString();

                            //Suggestion made by KyleMit
                            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                                fullName, surname, dob, pain, leastpain, average, HowMuchPain, title, medications, generalActivity, mood, ability,
                                housework, Relations, sleep, Enjoyment);
                            csv.AppendLine(newLine);
                        }
                    }
                    else if (id == 1231)
                    {
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30}",
                                "Title",
                                "First Name",
                                "Last Name",
                                "Gender",
                                "Date of Birth",
                                "Phone",
                                "Email",
                                "Address",
                                "Suburb",
                                "State",
                                "Postcode",
                                "Medicare Number",
                                "Ref No",
                                "Medicare Expiry",
                                "DOES THE PATIENT SUFFER FROM ANY OF THESE CONDITIONS?",
                                "IF OTHER PLEASE SPECIFY",
                                "PLEASE LIST ALL PAST MEDICATIONS FOR THE PATIENT AND THE LENGTH TRIED",
                                "REASON FOR REFERRAL",
                                "DOES YOUR PATIENT HAVE A CURRENT CARE PLAN IN PLACE?",
                                "WOULD YOU LIKE US TO DO A CARE PLAN REVIEW FOR YOUR PATIENT?",
                                "IF YOUR PATIENT DOES NOT HAVE A CURRENT CARE PLAN IN PLACE WOULD YOU CONSENT FOR OUR NCC DOCTOR TO INITIATE ONE?",
                                "Full Name",
                                "Practitioner Type",
                                "Phone Number",
                                "Practitioner Email",
                                "Practitioner Address",
                                "Practitioner ProviderNumber",
                                "Practitioner HealthLink Number",
                                "Support this patient to be treated with medicinal cannabis if needed",
                                "I here by refer the above named patient to a doctor and/or specialist at national cannabis clinics",
                                "Referral Date");
                        csv.AppendLine(newLine);

                        foreach (var item in childrens)
                        {
                            //in your loop
                            var TITLE = item.GetProperty("tITLE").GetValue().ToString();
                            var FirstName = item.GetProperty("firstName").GetValue().ToString();
                            var LastName = item.GetProperty("lastName").GetValue().ToString();
                            var Gender = item.GetProperty("gender").GetValue().ToString();
                            var DateofBirth = Convert.ToDateTime(item.GetProperty("dateOfBirth").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var Phone = item.GetProperty("phone").GetValue().ToString();
                            var Email = item.GetProperty("email").GetValue().ToString();
                            var Address = ScapeComma(item.GetProperty("address").GetValue().ToString());
                            var Suburb = item.GetProperty("suburb").GetValue().ToString();
                            var State = item.GetProperty("state").GetValue().ToString();
                            var Postcode = item.GetProperty("postcode").GetValue().ToString();
                            var MedicareNumber = item.GetProperty("medicareNumber").GetValue().ToString();
                            var RefNo = item.GetProperty("refNo").GetValue().ToString();
                            var MedicareExpiry = item.GetProperty("medicareExpiry").GetValue().ToString();

                            var sourceValue = item.GetProperty("dOESTHEPATIENTSUFFERFROMANYOFTHESECONDITIONS").GetSourceValue();
                            var jsonObj = JsonConvert.DeserializeObject<List<TextNCDataModel>>(sourceValue.ToString());
                            var DOESTHEPATIENTSUFFERFROMANYOFTHESECONDITIONS = jsonObj?.Count > 0 ? string.Join("|", jsonObj.Select(x => x.content)) : "";

                            var IFOTHERPLEASESPECIFY = item.GetProperty("iFOTHERPLEASESPECIFY").GetValue().ToString();
                            var PLEASELISTALLPASTMEDICATIONSFORTHEPATIENTANDTHELENGTHTRIED = item.GetProperty("pLEASELISTALLPASTMEDICATIONSFORTHEPATIENTANDTHELENGTHTRIED").GetValue().ToString();
                            var REASONFORREFERRAL = item.GetProperty("rEASONFORREFERRAL").GetValue().ToString();
                            var DOESYOURPATIENTHAVEACURRENTCAREPLANINPLACE = Convert.ToBoolean(item.GetProperty("dOESYOURPATIENTHAVEACURRENTCAREPLANINPLACE").GetValue());
                            var WOULDYOULIKEUSTODOACAREPLANREVIEWFORYOURPATIENT = Convert.ToBoolean(item.GetProperty("wOULDYOULIKEUSTODOACAREPLANREVIEWFORYOURPATIENT").GetValue());
                            var IFYOURPATIENTDOESNOTHAVEACURRENTCAREPLANINPLACEWOULDYOUCONSENTFOROURNCCDOCTORTOINITIATEONE = Convert.ToBoolean(item.GetProperty("iFYOURPATIENTDOESNOTHAVEACURRENTCAREPLANINPLACEWOULDYOUCONSENTFOROURNCCDOCTORTOINITIATEONE").GetValue());
                            var FullName = item.GetProperty("practitionerDetailsFullName").GetValue().ToString();
                            var PractitionerType = item.GetProperty("practitionerDetailsPractitionerType").GetValue().ToString();
                            var PhoneNumber = item.GetProperty("practitionerDetailsPhoneNumber").GetValue().ToString();
                            var PractitionerEmail = item.GetProperty("practitionerDetailsEmail").GetValue().ToString();
                            var PractitionerAddress = ScapeComma(item.GetProperty("practitionerDetailsAddress").GetValue().ToString());
                            var PractitionerProviderNumber = item.GetProperty("practitionerDetailsProviderNumber").GetValue().ToString();
                            var PractitionerHealthLinkNumber = item.GetProperty("practitionerDetailsHealthLinkNumber").GetValue().ToString();
                            var Supportthispatienttobetreatedwithmedicinalcannabisifneeded = Convert.ToBoolean(item.GetProperty("supportThisPatientToBeTreatedWithMedicinalCannabisIfNeeded").GetValue());
                            var Iherebyrefertheabovenamedpatienttoadoctorandorspecialistatnationalcannabisclinics = Convert.ToBoolean(item.GetProperty("iHereByReferTheAboveNamedPatientToADoctorAndOrSpecialistAtNationalCannabisClinics").GetValue());
                            var ReferralDate = Convert.ToDateTime(item.GetProperty("referralDate").GetValue().ToString()).ToString("dd/MM/yyyy");





                            //Suggestion made by KyleMit
                            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30}",
                            TITLE,
                            FirstName,
                            LastName,
                            Gender,
                            DateofBirth,
                            Phone,
                            Email,
                            Address,
                            Suburb,
                            State,
                            Postcode,
                            MedicareNumber,
                            RefNo,
                            MedicareExpiry,
                            DOESTHEPATIENTSUFFERFROMANYOFTHESECONDITIONS,
                            IFOTHERPLEASESPECIFY,
                            PLEASELISTALLPASTMEDICATIONSFORTHEPATIENTANDTHELENGTHTRIED,
                            REASONFORREFERRAL,
                            DOESYOURPATIENTHAVEACURRENTCAREPLANINPLACE,
                            WOULDYOULIKEUSTODOACAREPLANREVIEWFORYOURPATIENT,
                            IFYOURPATIENTDOESNOTHAVEACURRENTCAREPLANINPLACEWOULDYOUCONSENTFOROURNCCDOCTORTOINITIATEONE,
                            FullName,
                            PractitionerType,
                            PhoneNumber,
                            PractitionerEmail,
                            PractitionerAddress,
                            PractitionerProviderNumber,
                            PractitionerHealthLinkNumber,
                            Supportthispatienttobetreatedwithmedicinalcannabisifneeded,
                            Iherebyrefertheabovenamedpatienttoadoctorandorspecialistatnationalcannabisclinics,
                            ReferralDate);
                            csv.AppendLine(newLine);
                        }
                    }
                    else if (id == 1178)
                    {
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54}",
                                "Title",
                                "Surname",
                                "Given Name(S)",
                                "Preferred Name",
                                "Gender",
                                "Date of Birth",
                                "Phone",
                                "Email",
                                "Aboriginal Or Torres  Strait Islander",
                                "Occupation",
                                "Address",
                                "Suburb",
                                "State",
                                "Postcode",
                                "Home Phone",
                                "Work Phone",
                                "Mobile Number",
                                "Work Email",
                                "Medicare Number",
                                "Ref. No",
                                "Medicare Expiry",
                                "DVA No",
                                "DVA Expiry Date",
                                "Native Language",
                                "If Other Than Language Well You Required A Certified Translator",
                                "Emergency Contacts Information",
                                "Employment How did you hear about NCC?",
                                "Other ? Please Spec",
                                "Do You Currently Experience Or Have A History Of Any Of The Following Medical Condition?",
                                "If Other",
                                "Potential Contraindications",
                                "List Of All Past Medications Taken For Your Conditions And The Length Of Time Taken Or Trailed",
                                "Do you have any known allergies? Please list",
                                "Are you currently pregnant Planning to become pregnant or breastfeeding?",
                                "Smoking Status",
                                "If smoker number per day",
                                "Do you drink alcohol?",
                                "If yes how many standard drinks per week",
                                "When did you last have an overall check-up?",
                                "Overall check-up status",
                                "Are you currently driving?",
                                "Do you want to continue driving?",
                                "List Of All Current Medical And Specialist",
                                "Accept NC Terms and Conditions",
                                "Agree to all National Canabinoid Clinic to acess your medical history records if needed.",
                                "Understand that assessment by our doctors does not ensure approval and access to medical cannabis",
                                "Patient Full Name",
                                "Date Of Birth",
                                "Source Of Decision-Making Authority",
                                "Decision Maker Name",
                                "Decision Maker Relation",
                                "Decision Maker Date",
                                "Witness",
                                "Witness Relation",
                                "Witness Date");
                        csv.AppendLine(newLine);

                        foreach (var item in childrens)
                        {
                            //in your loop
                            var Title = item.GetProperty("patientSPersonalDetailsTitle").GetValue().ToString();
                            var Surname = item.GetProperty("patientSPersonalDetailsSurname").GetValue().ToString();
                            var GivenName = item.GetProperty("patientSPersonalDetailsGivenNameS").GetValue().ToString();
                            var PreferredName = item.GetProperty("patientSPersonalDetailsPreferredName").GetValue().ToString();
                            var Gender = item.GetProperty("patientSPersonalDetailsGender").GetValue().ToString();
                            var DateofBirth = Convert.ToDateTime(item.GetProperty("patientSPersonalDetailsDateOfBirth").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var Phone = item.GetProperty("patientSPersonalDetailsPhone").GetValue().ToString();
                            var Email = item.GetProperty("patientSPersonalDetailsEmail").GetValue().ToString();
                            var AboriginalOrTorresStraitIslander = Convert.ToBoolean(item.GetProperty("patientSPersonalDetailsAboriginalOrTorresStraitIslander").GetValue());
                            var Occupation = item.GetProperty("patientSPersonalDetailsOccupation").GetValue().ToString();
                            var Address = ScapeComma(item.GetProperty("patientSPersonalDetailsAddress").GetValue().ToString());
                            var Suburb = item.GetProperty("patientSPersonalDetailsSuburb").GetValue().ToString();
                            var State = item.GetProperty("patientSPersonalDetailsState").GetValue().ToString();
                            var Postcode = item.GetProperty("patientSPersonalDetailsPostCode").GetValue().ToString();
                            var HomePhone = item.GetProperty("patientSPersonalDetailsHomePhone").GetValue().ToString();
                            var WorkPhone = item.GetProperty("patientSPersonalDetailsWorkPhone").GetValue().ToString();
                            var MobileNumber = item.GetProperty("patientSPersonalDetailsMobileNumber").GetValue().ToString();
                            var WorkEmail = item.GetProperty("patientSPersonalDetailsWorkEmail").GetValue().ToString();
                            var MedicareNumber = item.GetProperty("patientSPersonalDetailsMedicareNumber").GetValue().ToString();
                            var RefNo = item.GetProperty("patientSPersonalDetailsRefNo").GetValue().ToString();
                            var MedicareExpiry = item.GetProperty("patientSPersonalDetailsMedicareExpiry").Value<DateTime>().ToString("dd/MM/yyyy");
                            var DVANo = item.GetProperty("patientSPersonalDetailsDVANo").GetValue().ToString();
                            var DVAExpiryDate = Convert.ToDateTime(item.GetProperty("patientSPersonalDetailsExpiryDate").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var NativeLanguage = item.GetProperty("patientSPersonalDetailsNativeLanguage").GetValue().ToString();
                            var IfOtherThanLanguageWellYouRequiredACertifiedTranslator = item.GetProperty("patientSPersonalDetailsIfOtherThanLanguageWellYouRequiredACertifiedTranslator").GetValue().ToString();

                            var emergencyContactsInformationSourceValue = item.GetProperty("patientSPersonalDetailsEmergencyContactsInformation").GetSourceValue();
                            var emergencyContactsInformationJsonObj = JsonConvert.DeserializeObject<List<EmergencyContactsInformation>>(emergencyContactsInformationSourceValue.ToString());
                            var emergencyContactsInformation = "";

                            if (emergencyContactsInformationJsonObj?.Count > 0)
                            {
                                foreach (var pc in emergencyContactsInformationJsonObj)
                                {
                                    if (emergencyContactsInformation.Length > 0)
                                    {
                                        emergencyContactsInformation += " | { Relative Name : " + pc.relativeName + " | Relationship : " + pc.relationship + " | Phone Number : " + pc.contactNumber + " }";
                                    }
                                    else
                                    {
                                        emergencyContactsInformation += "{ Relative Name : " + pc.relativeName + " | Relationship : " + pc.relationship + " | Phone Number : " + pc.contactNumber + " }";
                                    }
                                }
                            }

                            var employmentStatusSourceValue = item.GetProperty("patientSPersonalDetailsEmploymentStatus").GetSourceValue();
                            var employmentStatusJsonObj = JsonConvert.DeserializeObject<List<TextNCDataModel>>(employmentStatusSourceValue.ToString());
                            var EmploymentStatus = employmentStatusJsonObj?.Count > 0 ? string.Join("|", employmentStatusJsonObj.Select(x => x.content)) : "";

                            var employeementOtherSpec = item.GetProperty("patientSPersonalDetailsOtherPleaseSpec").GetValue().ToString();

                            var currentlyExperienceSourceValue = item.GetProperty("personalMedicalHistoryDoYouCurrentlyExperienceOrHaveAHistoryOfAnyOfTheFollowingMedicalCondition").GetSourceValue();
                            var currentlyExperienceJsonObj = JsonConvert.DeserializeObject<List<TextNCDataModel>>(currentlyExperienceSourceValue.ToString());
                            var currentlyExperience = currentlyExperienceJsonObj?.Count > 0 ? string.Join("|", currentlyExperienceJsonObj.Select(x => x.content)) : "";

                            var IfOther = ScapeComma(item.GetProperty("personalMedicalHistoryIfOther").GetValue().ToString());

                            var pastMedicationsSourceValue = item.GetProperty("personalMedicalHistoryListOfAllPastMedicationsTakenForYourConditionsAndTheLengthOfTimeTakenOrTrailed").GetSourceValue();
                            var pastMedicationsJsonObj = JsonConvert.DeserializeObject<List<PersonalMedicalHistoryAllPastMedication>>(pastMedicationsSourceValue.ToString());
                            var pastMedications = "";

                            if (pastMedicationsJsonObj?.Count > 0)
                            {
                                foreach (var pc in pastMedicationsJsonObj)
                                {
                                    if (pastMedications.Length > 0)
                                    {
                                        pastMedications += " | { Name Of Medication : " + ScapeComma(pc.nameOfMedication) + " | Length Of Time : " + ScapeComma(pc.lengthOfTime) + " }";
                                    }
                                    else
                                    {
                                        pastMedications = "{ Name Of Medication : " + ScapeComma(pc.nameOfMedication) + " | Length Of Time : " + ScapeComma(pc.lengthOfTime) + " }";
                                    }
                                }
                            }

                            var PotentialContraindications = item.GetProperty("personalMedicalHistoryPotentialContraindications").GetValue().ToString();
                            var allergies = item.GetProperty("personalMedicalHistoryDoYouHaveAnyKnownAllergiesPleaseList").Value<string>();
                            var pregnant = item.GetProperty("personalMedicalHistoryAreYouCurrentlyPregnantPlanningToBecomePregnantOrBreastfeeding").Value<string>();
                            var smokingStatus = item.GetProperty("personalMedicalHistorySmokingStatus").Value<string>();
                            var smokePerDay = item.GetProperty("personalMedicalHistoryIfSmokerNumberPerDay").Value<string>();
                            var drinkAlcohol = item.GetProperty("personalMedicalHistoryDoYouDrinkAlcohol").Value<string>();
                            var alcoholPerDay = item.GetProperty("personalMedicalHistoryIfYesHowManyStandardDrinksPerWeek").Value<string>();
                            var lastCheckUp = item.GetProperty("personalMedicalHistoryWhenDidYouLastHaveAnOverallCheckUp").Value<DateTime>().ToString("dd/MM/yyyy");
                            var checkUpStatus = item.GetProperty("personalMedicalHistoryWhenDidYouLastHaveAnOverallCheckUpStatus").Value<string>();
                            var driving = item.GetProperty("personalMedicalHistoryAreYouCurrentlyDriving").Value<string>();
                            var continueDriving = item.GetProperty("personalMedicalHistoryDoYouWantToContinueDriving").Value<string>();

                            var currentMedicalAndSpecialistSourceValue = item.GetProperty("additionalInformationListOfAllCurrentMedicalAndSpecialist").GetSourceValue();
                            var currentMedicalAndSpecialistJsonObj = JsonConvert.DeserializeObject<List<AdditionalInformationListOfAllCurrentMedicalAndSpecialist>>(currentMedicalAndSpecialistSourceValue.ToString());
                            var currentMedicalAndSpecialist = "";

                            if (currentMedicalAndSpecialistJsonObj?.Count > 0)
                            {
                                foreach (var pc in currentMedicalAndSpecialistJsonObj)
                                {
                                    if (currentMedicalAndSpecialist.Length > 0)
                                    {
                                        currentMedicalAndSpecialist += " | { Practitioner Name : " + pc.practitionerName + " | Specialty : " + ScapeComma(pc.specialty) + " | Phone Number : " + pc.phoneNumber + " | Practice Address : " + ScapeComma(pc.practiceAddress)  + " | Approximate Date Of Last Consultaion : " + pc.approximateDateOfLastConsultaion.ToString("dd/MM/yyyy") + " }";
                                    }
                                    else
                                    {
                                        currentMedicalAndSpecialist = "{ Practitioner Name : " + pc.practitionerName + " | Specialty : " + ScapeComma(pc.specialty) + " | Phone Number : " + pc.phoneNumber + " | Practice Address : " + ScapeComma(pc.practiceAddress) + " | Approximate Date Of Last Consultaion : " + pc.approximateDateOfLastConsultaion.ToString("dd/MM/yyyy") + " }";
                                    }
                                }
                            }

                            var AcceptNCTermsandConditions = Convert.ToBoolean(item.GetProperty("acceptNCTermsAndConditions").GetValue());
                            var AgreetoallNationalCanabinoidClinictoacessyourmedicalhistoryrecordsifneeded = Convert.ToBoolean(item.GetProperty("agreeToAllNationalCanabinoidClinicToAcessYourMedicalHistoryRecordsIfNeeded").GetValue());
                            var Understandthatassessmentbyourdoctorsdoesnotensureapprovalandaccesstomedicalcannabis = Convert.ToBoolean(item.GetProperty("understandThatAssessmentByOurDoctorsDoesNotEnsureApprovalAndAccessToMedicalCannabis").GetValue());
                            var PatientFullName = item.GetProperty("additionalInformationPatientFullName").GetValue().ToString();
                            var DateOfBirth = Convert.ToDateTime(item.GetProperty("additionalInformationDateOfBirth").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var SourceOfDecisionMakingAuthority = item.GetProperty("additionalInformationSourceOfDecisionMakingAuthority").GetValue().ToString();
                            var DecisionMakerName = item.GetProperty("additionalInformationDecisionMakerName").GetValue().ToString();
                            var DecisionMakerRelation = item.GetProperty("additionalInformationDecisionMakerRelation").GetValue().ToString();
                            var DecisionMakerDate = Convert.ToDateTime(item.GetProperty("additionalInformationDecisionMakerDate").GetValue().ToString()).ToString("dd/MM/yyyy");
                            var Witness = item.GetProperty("additionalInformationWitness").GetValue().ToString();
                            var WitnessRelation = item.GetProperty("additionalInformationWitnessRelation").GetValue().ToString();
                            var WitnessDate = Convert.ToDateTime(item.GetProperty("additionalInformationWitnessDate").GetValue().ToString()).ToString("dd/MM/yyyy"); ;

                            //Suggestion made by KyleMit
                            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54}",
                            Title,
                            Surname,
                            GivenName,
                            PreferredName,
                            Gender,
                            DateofBirth,
                            Phone,
                            Email,
                            AboriginalOrTorresStraitIslander,
                            Occupation,
                            Address,
                            Suburb,
                            State,
                            Postcode,
                            HomePhone,
                            WorkPhone,
                            MobileNumber,
                            WorkEmail,
                            MedicareNumber,
                            RefNo,
                            MedicareExpiry,
                            DVANo,
                            DVAExpiryDate,
                            NativeLanguage,
                            IfOtherThanLanguageWellYouRequiredACertifiedTranslator,
                            emergencyContactsInformation,
                            EmploymentStatus,
                            employeementOtherSpec,
                            currentlyExperience,
                            IfOther,
                            PotentialContraindications,
                            pastMedications,
                            allergies,
                            pregnant,
                            smokingStatus,
                            smokePerDay,
                            drinkAlcohol,
                            alcoholPerDay,
                            lastCheckUp,
                            checkUpStatus,
                            driving,
                            continueDriving,
                            currentMedicalAndSpecialist,
                            AcceptNCTermsandConditions,
                            AgreetoallNationalCanabinoidClinictoacessyourmedicalhistoryrecordsifneeded,
                            Understandthatassessmentbyourdoctorsdoesnotensureapprovalandaccesstomedicalcannabis,
                            PatientFullName,
                            DateOfBirth,
                            SourceOfDecisionMakingAuthority,
                            DecisionMakerName,
                            DecisionMakerRelation,
                            DecisionMakerDate,
                            Witness,
                            WitnessRelation,
                            WitnessDate);
                            csv.AppendLine(newLine);
                        }
                    }

                    return File(new UTF8Encoding().GetBytes(csv.ToString()), "text/csv", Node.Name + ".csv");
                }
                else
                {
                    return File(new UTF8Encoding().GetBytes(""), "text/csv", Node.Name + ".csv");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult GetAllFormList()
        {
            int[] formIds = new int[] { 1301, 1306, 1313, 1231, 1178 };
            var result = new List<FormResultDataModel>();
            if (formIds != null && formIds.Length > 0)
            {
                foreach (var id in formIds)
                {
                    if (id == 1231)
                    {
                        var node = Umbraco.Content(id);

                        if (node != null)
                        {
                            List<FormDataModel> data = new List<FormDataModel>();
                            if (node.Children != null && node.Children.Count() > 0)
                            {
                                foreach (var item in node.Children)
                                {
                                    data.Add(new FormDataModel()
                                    {
                                        NodeId = item.Id,
                                        Name = item.GetProperty("firstName").GetValue().ToString(),
                                        Surname = item.GetProperty("lastName").GetValue().ToString(),
                                        CreateDate = item.CreateDate.ToString("dd/MM/yyyy")

                                    });
                                }
                            }
                            result.Add(new FormResultDataModel()
                            {
                                formId = node.Id,
                                formName = node.Name,
                                data = data
                            });
                        }
                    }
                    else if (id == 1178)
                    {
                        var node = Umbraco.Content(id);

                        if (node != null)
                        {
                            List<FormDataModel> data = new List<FormDataModel>();
                            if (node.Children != null && node.Children.Count() > 0)
                            {
                                foreach (var item in node.Children)
                                {
                                    data.Add(new FormDataModel()
                                    {
                                        NodeId = item.Id,
                                        Name = item.GetProperty("patientSPersonalDetailsGivenNameS").GetValue().ToString(),
                                        Surname = item.GetProperty("patientSPersonalDetailsSurname").GetValue().ToString(),
                                        CreateDate = item.CreateDate.ToString("dd/MM/yyyy")

                                    });
                                }
                            }
                            result.Add(new FormResultDataModel()
                            {
                                formId = node.Id,
                                formName = node.Name,
                                data = data
                            });
                        }
                    }
                    else
                    {
                        var node = Umbraco.Content(id);

                        if (node != null)
                        {
                            List<FormDataModel> data = new List<FormDataModel>();
                            if (node.Children != null && node.Children.Count() > 0)
                            {
                                foreach (var item in node.Children)
                                {
                                    data.Add(new FormDataModel()
                                    {
                                        NodeId = item.Id,
                                        Name = item.Name,
                                        Surname = item.GetProperty("patientSurname").GetValue().ToString(),
                                        CreateDate = item.CreateDate.ToString("dd/MM/yyyy")

                                    });
                                }
                            }
                            result.Add(new FormResultDataModel()
                            {
                                formId = node.Id,
                                formName = node.Name,
                                data = data
                            });
                        }
                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string ScapeComma(string data)
        {
            if (data.Contains("\""))
            {
                data = data.Replace("\"", "");
            }
            else if (data.Contains(","))
            {
                data = data.Replace(",", " ");
            }
            return data;
        }
    }
}