﻿using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Core;
using NCC.Models;
using NCC.BusinessLogic.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
/// <summary>
/// Summary description for ContactFormController
/// </summary>
///
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
        public ActionResult BriefPainInventoryFormCSV(int id)
        {
            try
            {
                var Node = Umbraco.Content(id);

                //before your loop
                var csv = new StringBuilder();

                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                            "Full Name", "Surname", "Date Of Birth", "Please rate your pain by selecting the one number that best describes your pain at its worst in the last week",
                            "Please rate your pain by selecting the one number that best describes your pain at its least in the last week",
                            "Please rate your pain by selecting the one number that best describes your pain on average",
                            "Please rate your pain by selecting the one number that tells how much pain you have right now",
                            "What treatments or medications are you receiving for yourpain?",
                            "In the last week how much relief have pain treatments or medications provided? Please circle the one percentage that best shows how much relief you have received",
                            "General activity", "Mood", "Walking ability", "Normal work (includes both outside the home and housework)",
                            "Relations with other people", "Sleep", "Enjoyment of life");
                csv.AppendLine(newLine);

                if (Node.Children != null)
                {
                    foreach (var item in Node.Children)
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

                return File(new UTF8Encoding().GetBytes(csv.ToString()), "text/csv", Node.Name + ".csv");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    public class ContactFormResult
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}