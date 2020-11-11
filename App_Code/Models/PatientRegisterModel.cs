using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace NCC.BusinessLogic.Models
{
    public class BriefPainInventoryDataModel
    {
        public int NodeId { get; set; }
        public string fullname { get; set; }
        public string surname { get; set; }
        public DateTime? dob { get; set; }
        public string pain { get; set; }
        public string leastpain { get; set; }
        public string average { get; set; }
        public string HowMuchPain { get; set; }
        public string title { get; set; }
        public string medications { get; set; }
        public string generalActivity { get; set; }
        public string mood { get; set; }
        public string ability { get; set; }
        public string housework { get; set; }
        public string Relations { get; set; }
        public string sleep { get; set; }
        public string Enjoyment { get; set; }
        public string bodyMaker { get; set; }
        public string GlobalsettingId { get; set; }
    }

    public class DASSDataModel
    {
        public int NodeId { get; set; }
        public string fullname { get; set; }
        public string surname { get; set; }
        public DateTime? dob { get; set; }
        public string HardWind { get; set; }
        public string dryness { get; set; }
        public string experience { get; set; }
        public string breathing { get; set; }
        public string initiative { get; set; }
        public string overreact { get; set; }
        public string trembling { get; set; }
        public string nervous { get; set; }
        public string worried { get; set; }
        public string lookForward { get; set; }
        public string agitated { get; set; }
        public string relax { get; set; }
        public string downhearted { get; set; }
        public string intolerant { get; set; }
        public string panic { get; set; }
        public string enthusiastic { get; set; }
        public string person { get; set; }
        public string touchy { get; set; }
        public string exertion { get; set; }
        public string scared { get; set; }
        public string meaningless { get; set; }
        public string GlobalsettingId { get; set; }
    }

    public class QualityOfLifeAssessmentDataModel
    {
        public int NodeId { get; set; }
        public string fullname { get; set; }
        public string surname { get; set; }
        public DateTime? dob { get; set; }
        public string mobility { get; set; }
        public string selfcare  { get; set; }
        public string usualactivities { get; set; }
        public string discomfort { get; set; }
        public string depression { get; set; }
        public string health { get; set; }
        public string GlobalsettingId { get; set; }
    }

    public class RegisterDataModel
    {
        public int NodeId { get; set; }
        public string GlobalsettingId { get; set; }
        public PersonalDetails personalDetails { get; set; }
        public PersonalHistory personalHistory { get; set; }
        public AdditionalInfo additionalInfo { get; set; }
        public PractitionerDetails practitionerDetails { get; set; }
    }

    public class PersonalDetails
    {
        public string title { get; set; }
        public string surname { get; set; }
        public string givenname { get; set; }
        public string preferredname { get; set; }
        public string gender { get; set; }
        public DateTime? birthdate { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool straisislander { get; set; }
        public string occupation { get; set; }
        public string address { get; set; }
        public string suburb { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string homephone { get; set; }
        public string workphone { get; set; }
        public string mobile { get; set; }
        public string workemail { get; set; }
        public string medicare { get; set; }
        public string refno { get; set; }
        public DateTime? medicareexpiry { get; set; }
        public string dva { get; set; }
        public DateTime? expirydate { get; set; }
        public string nativelanguage { get; set; }
        public string certificate { get; set; }
        public List<EmergencyContact> emergencycontact { get; set; }
        public List<string> employment { get; set; }
        public string other { get; set; }

        //Referral Form
        public string f_name { get; set; }
        public string l_name { get; set; }
    }

    public class EmergencyContact
    {
        public string relativename { get; set; }
        public string relationship { get; set; }
        public string phonenumber { get; set; }
    }

    public class PersonalHistory
    {
        public List<string> currentlyexperience { get; set; }
        public string othercondition { get; set; }
        public string potentialcontraindications { get; set; }
        public List<PastMedication> pastmedication { get; set; }
        public string allergies { get; set; }
        public string pregnant { get; set; }
        public string smoking { get; set; }
        public string smokernumber { get; set; }
        public string alcohol { get; set; }
        public string drinknumber { get; set; }
        public string checkup { get; set; }
        public string overallcheckup { get; set; }
        public string drivingyes { get; set; }
        public string continuedriving { get; set; }

        //Referral Form
        public List<string> patientsuffercondition { get; set; }
        public bool currentcareplan { get; set; }
        public string referralpastmedication { get; set; }
        public string reasonreferral { get; set; }
        public bool review { get; set; }
        public bool initiate { get; set; }
    }

    public class PastMedication
    {
        public string Medication { get; set; }
        public string time { get; set; }
    }

    public class AdditionalInfo
    {
        public List<CurrentMedical> currentmedicalList { get; set; }
        public bool termandcondition { get; set; }
        public bool agreetoall { get; set; }
        public bool understandthat { get; set; }
        public string patientname { get; set; }
        public DateTime? patientdate { get; set; }
        public string sourceofdecision { get; set; }
        public string decisionmakername { get; set; }
        public string decisionmakerrealtion { get; set; }
        public DateTime? decisionmakerdate { get; set; }
        public string witness { get; set; }
        public string witnessrelation { get; set; }
        public DateTime? witnessdate { get; set; }
    }

    public class CurrentMedical
    {
        public string practitioner { get; set; }
        public string speciality { get; set; }
        public string Phoneno { get; set; }
        public string practiceaddress { get; set; }
        public DateTime? conclusion { get; set; }
    }

    public class PractitionerDetails
    {
        public string full_name { get; set; }
        public string pract_type { get; set; }
        public string Phoneno { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string provider_no { get; set; }
        public string healthlink_no { get; set; }
        public bool conditions { get; set; }
        public bool history { get; set; }
        public DateTime? date { get; set; }
    }

    public class ContactFormResult
    {
        public bool result { get; set; }
        public string message { get; set; }
        public FileContentResult data { get; set; }
    }

    public class FormResultDataModel
    {
        public int formId { get; set; }
        public string formName { get; set; }
        public List<FormDataModel> data { get; set; }
    }

    public class FormDataModel
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CreateDate { get; set; }
    }

    public class TextNCDataModel
    {
        public string key { get; set; }
        public string ncContentTypeAlias { get; set; }
        public string content { get; set; }
    }

    public class EmergencyContactsInformation
    {
        public string key { get; set; }
        public string ncContentTypeAlias { get; set; }
        public string relativeName { get; set; }
        public string relationship { get; set; }
        public string contactNumber { get; set; }
    }

    public class PersonalMedicalHistoryAllPastMedication
    {
        public string lengthOfTime { get; set; }
        public string nameOfMedication { get; set; }
    }

    public class AdditionalInformationListOfAllCurrentMedicalAndSpecialist
    {
        public DateTime approximateDateOfLastConsultaion { get; set; }
        public string phoneNumber { get; set; }
        public string practiceAddress { get; set; }
        public string practitionerName { get; set; }
        public string specialty { get; set; }
    }
}