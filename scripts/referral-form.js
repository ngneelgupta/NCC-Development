$(function () {
    var personalDetails = {}, personalHistory = {}, practitionerDetails = {};

    $(".presonal-details-block button.next-btn").click(function () {
        var isValid = CheckPersonalDetailsFormValidate();
        if (isValid) {
            personalDetails = {
                "title": $(".presonal-details-block .patient-form select.title-select").val(),
                "f_name": $(".presonal-details-block .patient-form input[name=f_name]").val(),
                "l_name": $(".presonal-details-block .patient-form input[name='l_name']").val(),
                "gender": $(".presonal-details-block .patient-form select.gender-select").val(),
                "birthdate": $(".presonal-details-block .patient-form input[name='birth-date']").val(),
                "phone": $(".presonal-details-block .patient-form input[name=phone]").val(),
                "email": $(".presonal-details-block .patient-form input[name=email]").val(),
                "address": $(".presonal-details-block .patient-form textarea[name=address]").val(),
                "suburb": $(".presonal-details-block .patient-form input[name=suburb]").val(),
                "state": $(".presonal-details-block .patient-form select.state-select").val(),
                "postcode": $(".presonal-details-block .patient-form input[name=postcode]").val(),
                "medicare": $(".presonal-details-block .patient-form input[name=medicare]").val(),
                "refno": $(".presonal-details-block .patient-form input[name='ref-no']").val(),
                "medicareexpiry": $(".presonal-details-block .patient-form input[name='medicare-expiry']").val(),
            };

            $(".personal-history-block").removeAttr('style');
            $('.presonal-details-block').hide();
            GoToSection('#FocusSection');
        }
    });

    $(".personal-history-block button.next-btn").click(function () {
        let patientSufferCondition = [];
        $(".personal-history-block .patient-form input[type=checkbox]:checked").each(function () {
            patientSufferCondition.push($(this).parent().find('label').text());
        });

        personalHistory = {
            "patientsuffercondition": patientSufferCondition,
            "othercondition": $(".personal-history-block .patient-form input[name='other-condition']").val(),
            "referralpastmedication": $(".personal-history-block .patient-form textarea[name='past-medication']").val(),
            "reasonreferral": $(".personal-history-block .patient-form textarea[name='reason-referral']").val(),
            "currentcareplan": $(".personal-history-block .patient-form input[name='currently']:checked").parent().find('label').text() == "Yes" ? true : false,
            "review": $(".personal-history-block .patient-form input[name='review']:checked").parent().find('label').text() == "Yes" ? true : false,
            "initiate": $(".personal-history-block .patient-form input[name='initiate']:checked").parent().find('label').text() == "Yes" ? true : false,
        };

        $(".practitioner-details-block").removeAttr('style');
        $('.personal-history-block').hide();
        GoToSection('#FocusSection');
    });

    $(".practitioner-details-block button.next-btn").click(function () {
        let isValid = CheckPractitionerDetailsFormValidate();

        if (isValid) {
            practitionerDetails = {
                "full_name": $(".practitioner-details-block .patient-form input[name='full_name']").val(),
                "pract_type": $(".practitioner-details-block .patient-form input[name='pract_type']").val(),
                "Phoneno": $(".practitioner-details-block .patient-form input[name='Phone-no']").val(),
                "email": $(".practitioner-details-block .patient-form input[name='email']").val(),
                "address": $(".practitioner-details-block .patient-form textarea[name='address']").val(),
                "provider_no": $(".practitioner-details-block .patient-form input[name='provider_no']").val(),
                "healthlink_no": $(".practitioner-details-block .patient-form input[name='healthlink_no']").val(),
                "conditions": $(".practitioner-details-block .patient-form input#conditions").prop('checked'),
                "history": $(".practitioner-details-block .patient-form input#history").prop('checked'),
                "date": $(".practitioner-details-block .patient-form input[name='date']").val(),
            };

            let formData = {
                "NodeId": $("#ReferralFormNodeId").val(),
                "personalDetails": personalDetails,
                "personalHistory": personalHistory,
                "practitionerDetails": practitionerDetails
            }

            $('.loader').show();

            $.ajax({
                url: "/umbraco/surface/ContactForm/ReferralForm",
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
                },
                failure: function (response) {
                    $('.loader').hide();
                    $(".alert-danger").text(response.message);
                    $(".alert-danger").slideDown("slow");
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

    $(".practitioner-details-block button.prev-btn").click(function () {
        $(".personal-history-block").removeAttr('style');
        $(".practitioner-details-block").hide();
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

    if (isValid) {
        $(".presonal-details-block .patient-form select").each(function () {
            if ($(this).val() == "" || $(this).val() == null) {
                $(this).focus();
                isValid = false;
                return false;
            }
        });
    }

    if (isValid) {
        if ($(".presonal-details-block .patient-form textarea").val() == "" || $(".presonal-details-block .patient-form textarea").val().length <= 0) {
            $(".presonal-details-block .patient-form textarea").focus();
            isValid = false;
        }
    }

    return isValid;
}

function CheckPractitionerDetailsFormValidate() {
    let isValid = true;
    if (!$(".practitioner-details-block .patient-form input#conditions").prop('checked')) {
        $(".practitioner-details-block .patient-form input#conditions").focus();
        isValid = false;
        return isValid;
    }

    $(".practitioner-details-block .patient-form input:required").each(function () {
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