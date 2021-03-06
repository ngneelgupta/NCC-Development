﻿$(function () {
    var personalDetails = {}, personalHistory = {}, additionalInfo = {};

    $(".presonal-details-block button.next-btn").click(function () {
        var isValid = CheckPersonalDetailsFormValidate();
        if (isValid) {
            let emergencycontact = [];
            let emergencycontactJson = {
                "relativename": $(".presonal-details-block .patient-form input[name='contact-1']").val(),
                "relationship": $(".presonal-details-block .patient-form input[name='relationship-1']").val(),
                "phonenumber": $(".presonal-details-block .patient-form input[name='p-no-1']").val()
            }

            if (emergencycontactJson.relativename != "" || emergencycontactJson.relationship != '' || emergencycontactJson.phonenumber != '') {
                emergencycontact.push(emergencycontactJson);
            }

            emergencycontactJson = {
                "relativename": $(".presonal-details-block .patient-form input[name='contact-2']").val(),
                "relationship": $(".presonal-details-block .patient-form input[name='relationship-2']").val(),
                "phonenumber": $(".presonal-details-block .patient-form input[name='p-no-2']").val()
            }

            if (emergencycontactJson.relativename != "" || emergencycontactJson.relationship != '' || emergencycontactJson.phonenumber != '') {
                emergencycontact.push(emergencycontactJson);
            }

            let aboutNCC = [];
            $(".presonal-details-block .patient-form input[name=example2]:checked").each(function () {
                aboutNCC.push($(this).parent().find('label').text());
            });

            personalDetails = {
                "title": $(".presonal-details-block .patient-form select.title-select").val(),
                "surname": $(".presonal-details-block .patient-form input[name=surname]").val(),
                "givenname": $(".presonal-details-block .patient-form input[name='given-name']").val(),
                "preferredname": $(".presonal-details-block .patient-form input[name='preferred-name']").val(),
                "gender": $(".presonal-details-block .patient-form select.select-gender").val(),
                "birthdate": $(".presonal-details-block .patient-form input[name='birth-date']").val(),
                "phone": $(".presonal-details-block .patient-form input[name=phone]").val(),
                "email": $(".presonal-details-block .patient-form input[name=email]").val(),
                "straisislander": $(".presonal-details-block .patient-form input[name=example1]:checked").parent().find('label').text() == "Yes" ? true : false,
                "occupation": $(".presonal-details-block .patient-form input[name=occupation]").val(),
                "address": $(".presonal-details-block .patient-form textarea[name=address]").val(),
                "suburb": $(".presonal-details-block .patient-form input[name=suburb]").val(),
                "state": $(".presonal-details-block .patient-form select.state-select").val(),
                "postcode": $(".presonal-details-block .patient-form input[name=postcode]").val(),
                "homephone": $(".presonal-details-block .patient-form input[name='home-phone']").val(),
                "workphone": $(".presonal-details-block .patient-form input[name='work-phone']").val(),
                "mobile": $(".presonal-details-block .patient-form input[name=mobile]").val(),
                "workemail": $(".presonal-details-block .patient-form input[name=email2]").val(),
                "medicare": $(".presonal-details-block .patient-form input[name=medicare]").val(),
                "refno": $(".presonal-details-block .patient-form input[name='ref-no']").val(),
                "medicareexpiry": $(".presonal-details-block .patient-form input[name='medicare-expiry']").val(),
                "dva": $(".presonal-details-block .patient-form input[name='dva']").val(),
                "expirydate": $(".presonal-details-block .patient-form input[name='expiry-date']").val(),
                "nativelanguage": $(".presonal-details-block .patient-form input[name='native-language']").val(),
                "certificate": $(".presonal-details-block .patient-form input[name=certificate]").val(),
                "emergencycontact": emergencycontact,
                "employment": aboutNCC,
                "other": $(".presonal-details-block .patient-form input[name=other]").val()
            };

            $(".personal-history-block").removeAttr('style');
            $('.presonal-details-block').hide();
            GoToSection('#FocusSection');
        }
    });

    $(".personal-history-block button.next-btn").click(function () {
        var isValid = CheckHistoryBlockFormValidate();
        if (isValid) {
            let currentlyExperience = [];
            $(".personal-history-block .patient-form input[type=checkbox]:checked").each(function () {
                currentlyExperience.push($(this).parent().find('label').text());
            });

            let pastMedication = [];
            let medication = {
                "Medication": $(".personal-history-block .patient-form input[name='Medication']").val(),
                "time": $(".personal-history-block .patient-form input[name='time']").val()
            };

            if (medication.Medication != "") {
                pastMedication.push(medication);
            }

            medication = {
                "Medication": $(".personal-history-block .patient-form input[name='Medication2']").val(),
                "time": $(".personal-history-block .patient-form input[name='time2']").val()
            };

            if (medication.Medication != "") {
                pastMedication.push(medication);
            }

            medication = {
                "Medication": $(".personal-history-block .patient-form input[name='Medication3']").val(),
                "time": $(".personal-history-block .patient-form input[name='time3']").val()
            };

            if (medication.Medication != "") {
                pastMedication.push(medication);
            }

            medication = {
                "Medication": $(".personal-history-block .patient-form input[name='Medication4']").val(),
                "time": $(".personal-history-block .patient-form input[name='time4']").val()
            };

            if (medication.Medication != "") {
                pastMedication.push(medication);
            }

            medication = {
                "Medication": $(".personal-history-block .patient-form input[name='Medication5']").val(),
                "time": $(".personal-history-block .patient-form input[name='time5']").val()
            };

            if (medication.Medication != "") {
                pastMedication.push(medication);
            }

            personalHistory = {
                "currentlyexperience": currentlyExperience,
                "othercondition": $(".personal-history-block .patient-form input[name='other-condition']").val(),
                "potentialcontraindications": $(".personal-history-block .patient-form input[name='currently']:checked").parent().find('label').text(),
                "pastmedication": pastMedication,
                "allergies": $(".personal-history-block .patient-form textarea[name='allergies']").val(),
                "pregnant": $(".personal-history-block .patient-form textarea[name='pregnant']").val(),
                "smoking": $(".personal-history-block .patient-form input[name='smoking']:checked").parent().find('label').text(),
                "smokernumber": $(".personal-history-block .patient-form input[name='smoker-number']").val(),
                "alcohol": $(".personal-history-block .patient-form input[name='alcohol']:checked").parent().find('label').text(),
                "drinknumber": $(".personal-history-block .patient-form input[name='drink-number']").val(),
                "checkup": $(".personal-history-block .patient-form input[name='checkup']").val(),
                "overallcheckup": $(".personal-history-block .patient-form input[name='overall-checkup']:checked").parent().find('label').text(),
                "drivingyes": $(".personal-history-block .patient-form input[name='driving-yes']:checked").parent().find('label').text(),
                "continuedriving": $(".personal-history-block .patient-form input[name='continue-driving']:checked").parent().find('label').text()
            };

            $(".additional-info-block").removeAttr('style');
            $('.personal-history-block').hide();
            GoToSection('#FocusSection');
        }
    });

    $(".additional-info-block button.next-btn").click(function () {
        let isValid = CheckAdditionalInfoFormValidate();

        if (isValid) {
            let currentMedicalList = [];
            let currentMedical = {
                "practitioner": $(".additional-info-block .patient-form input[name='practitioner']").val(),
                "speciality": $(".additional-info-block .patient-form input[name='speciality']").val(),
                "Phoneno": $(".additional-info-block .patient-form input[name='Phone-no']").val(),
                "practiceaddress": $(".additional-info-block .patient-form input[name='practice-address']").val(),
                "conclusion": $(".additional-info-block .patient-form input[name='conclusion']").val()
            };

            if (currentMedical.practitioner != "" && currentMedical.Phoneno != null && currentMedical.speciality != null && currentMedical.conclusion != "") {
                currentMedicalList.push(currentMedical);
            }

            currentMedical = {
                "practitioner": $(".additional-info-block .patient-form input[name='practitioner2']").val(),
                "speciality": $(".additional-info-block .patient-form input[name='speciality2']").val(),
                "Phoneno": $(".additional-info-block .patient-form input[name='Phone-no2']").val(),
                "practiceaddress": $(".additional-info-block .patient-form input[name='practice-address2']").val(),
                "conclusion": $(".additional-info-block .patient-form input[name='conclusion2']").val()
            };

            if (currentMedical.practitioner != "" && currentMedical.Phoneno != null && currentMedical.speciality != null && currentMedical.conclusion != "") {
                currentMedicalList.push(currentMedical);
            }

            currentMedical = {
                "practitioner": $(".additional-info-block .patient-form input[name='practitioner3']").val(),
                "speciality": $(".additional-info-block .patient-form input[name='speciality3']").val(),
                "Phoneno": $(".additional-info-block .patient-form input[name='Phone-no3']").val(),
                "practiceaddress": $(".additional-info-block .patient-form input[name='practice-address3']").val(),
                "conclusion": $(".additional-info-block .patient-form input[name='conclusion3']").val()
            };

            if (currentMedical.practitioner != "" && currentMedical.Phoneno != null && currentMedical.speciality != null && currentMedical.conclusion != "") {
                currentMedicalList.push(currentMedical);
            }

            additionalInfo = {
                "currentmedicalList": currentMedicalList,
                "termandcondition": $(".additional-info-block .patient-form input[name='submitting']").prop('checked'),
                "agreetoall": $(".additional-info-block .patient-form input[name='submitting1']").prop('checked'),
                "understandthat": $(".additional-info-block .patient-form input[name='submitting2']").prop('checked'),
                "patientname": $(".additional-info-block .patient-form input[name='patient-name']").val(),
                "patientdate": $(".additional-info-block .patient-form input[name='patient-date']").val(),
                "sourceofdecision": $(".additional-info-block .patient-form input[name='currently1']:checked").parent().find('label').text(),
                "decisionmakername": $(".additional-info-block .patient-form input[name='decisionMaker']").val(),
                "decisionmakerrealtion": $(".additional-info-block .patient-form input[name='decisionMakerrealtion']").val(),
                "decisionmakerdate": $(".additional-info-block .patient-form input[name='decisionMakerdate']").val(),
                "witness": $(".additional-info-block .patient-form input[name='witness']").val(),
                "witnessrelation": $(".additional-info-block .patient-form input[name='witnessrelation']").val(),
                "witnessdate": $(".additional-info-block .patient-form input[name='witnessdate']").val(),
            };

            let formData = {
                "NodeId": $("#PatientFormNodeId").val(),
                "PatientNodeId": $("#patientNodeId").val(),
                "GlobalsettingId": $('#globalsettingId').val(),
                "personalDetails": personalDetails,
                "personalHistory": personalHistory,
                "additionalInfo": additionalInfo
            }

            $('.loader').show();

            $.ajax({
                url: "/umbraco/surface/ContactForm/RegisterPatient",
                type: "post",
                dataType: "json",
                data: { "model": formData },
                success: function (response) {
                    
                    if (response.result) {
                        $('.loader').hide();

                        $(".alert-success").text(response.message);
                        $(".alert-success").slideDown("slow");
                        setTimeout(function () {
                            location.reload(true);
                        }, 3000);

                    }
                    else {
                        $('.loader').hide();
                        $(".alert-danger").text("Recode save failure !!");
                        $(".alert-danger").slideDown("slow");
                    }
                    setTimeout(function () {
                        $(".alert").slideUp("slow");
                    }, 5000);
                }
            });
        }
    });

    $(".personal-history-block button.prev-btn").click(function () {
        $(".presonal-details-block").removeAttr('style');
        $(".personal-history-block").hide();
        GoToSection('#FocusSection');
    });

    $(".additional-info-block button.prev-btn").click(function () {
        $(".personal-history-block").removeAttr('style');
        $(".additional-info-block").hide();
        GoToSection('#FocusSection');
    });

    $(".accept-only-num").keypress(function (e) {
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
});

function validateEmail(email) {
    var re = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return re.test(email);
}
function validatephone(phone) {
    var re = /^((\[0-9]{10})|([0-9]{10}))$/;
    return re.test(phone);
}

function CheckPersonalDetailsFormValidate() {
    let isValid = true;
    $(".presonal-details-block .patient-form input").removeClass('error');
    $(".presonal-details-block .patient-form input:required").each(function () {
        if ($(this).val() == "" || $(this).val() == null || $(this).val().length <= 0) {
            $(this).focus();
            isValid = false;
            return false;
        }

        else if ($(this).attr('type') == 'email') {
            if (!validateEmail($(this).val().trim())) {
                $(this).addClass("error");
                $(this).focus();
                isValid = false;
                return false;
            }
        }

        else if ($(this).attr('name') == 'phone') {
            if (!validatephone($(this).val().trim())) {
                $(this).addClass("error");
                $(this).focus();
                isValid = false;
                return false;
            }
        }
    });

    //if (isValid) {
    //    isValid = $(".presonal-details-block .patient-form input[name=example1]").is(":checked");
    //    if (!isValid) {
    //        $(".presonal-details-block .patient-form input#customRadio1").focus();
    //    }
    //}

    //if (isValid) {
    //    $(".presonal-details-block .patient-form select").each(function () {
    //        if ($(this).val() == "" || $(this).val() == null) {
    //            $(this).focus();
    //            isValid = false;
    //            return false;
    //        }
    //    });
    //}

    //if (isValid) {
    //    if ($(".presonal-details-block .patient-form textarea").val() == "" || $(".presonal-details-block .patient-form textarea").val().length <= 0) {
    //        $(".presonal-details-block .patient-form textarea").focus();
    //        isValid = false;
    //    }
    //}

    return isValid;
}

function CheckHistoryBlockFormValidate() {
    let isValid = true;
    //if (isValid) {
    //    isValid = $(".personal-history-block .patient-form input[name='alcohol']").is(":checked");

    //    if (!isValid) {
    //        $(".personal-history-block .patient-form input#alcohol-yes").focus();
    //    }
    //}

    return isValid;
}

function CheckAdditionalInfoFormValidate() {
    let isValid = true;
    //if (!$(".additional-info-block .patient-form input[name=submitting]").prop('checked')) {
    //    $(".additional-info-block .patient-form input[name=submitting]").focus();
    //    isValid = false;
    //    return isValid;
    //}

    $(".additional-info-block .patient-form input:required").each(function () {
        if ($(this).val() == "" || $(this).val() == null || $(this).val().length <= 0) {
            $(this).focus();
            isValid = false;
            return false;
        }
    });

    return isValid;
}

function GoToSection(id) {
    var scrollY = 0;
    var offset = $(id).offset().top;

    if (0 < offset && (offset < scrollY || scrollY === 0)) {
        scrollY = offset - 82;
    }

    if (scrollY > 0) {
        window.scrollTo(0, scrollY);
    }
}