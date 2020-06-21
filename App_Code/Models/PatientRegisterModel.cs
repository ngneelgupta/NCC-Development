using System;
using System.Collections.Generic;

namespace NCC.BusinessLogic.Models
{
    public class PatientRegisterDataModel
    {
        public int NodeId { get; set; }
        public PersonalDetails personalDetails { get; set; }
        public PersonalHistory personalHistory { get; set; }
        public AdditionalInfo additionalInfo { get; set; }
    }

    public class PersonalDetails
    {
        public string title { get; set; }
        public string surname { get; set; }
        public string givenname { get; set; }
        public string preferredname { get; set; }
        public DateTime birthdate { get; set; }
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
        public string medicareexpiry { get; set; }
        public string dva { get; set; }
        public DateTime expirydate { get; set; }
        public string nativelanguage { get; set; }
        public string certificate { get; set; }
        public List<EmergencyContact> emergencycontact { get; set; }
        public string employment { get; set; }
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
        public DateTime patientdate { get; set; }
        public string sourceofdecision { get; set; }
        public string decisionmakername { get; set; }
        public string decisionmakerrealtion { get; set; }
        public DateTime decisionmakerdate { get; set; }
        public string witness { get; set; }
        public string witnessrelation { get; set; }
        public DateTime witnessdate { get; set; }
    }

    public class CurrentMedical
    {
        public string practitioner { get; set; }
        public string speciality { get; set; }
        public string Phoneno { get; set; }
        public string practiceaddress { get; set; }
        public DateTime conclusion { get; set; }
    }
}