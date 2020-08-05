$(function () {
    var formData = {};
    $(".submit-form-btn").click(function () {
        var isValid = CheckFormValidate();

        if (isValid) {
            formData = {
                "fullname": $(".assessment-form .patient-form input[name='full_name']").val(),
                "surname": $(".assessment-form .patient-form input[name=surname]").val(),
                "dob": $(".assessment-form .patient-form input[name='dob']").val(),
                "mobility": $(".assessment-form .patient-form input[name='employment']:checked").siblings().text(),
                "selfcare": $(".assessment-form .patient-form input[name='self-care']:checked").siblings().text(),
                "usualactivities": $(".assessment-form .patient-form input[name='usual-activities']:checked").siblings().text(),
                "discomfort": $(".assessment-form .patient-form input[name=discomfort]:checked").siblings().text(),
                "depression": $(".assessment-form .patient-form input[name=depression]:checked").siblings().text(),
                "health": $(".assessment-form .patient-form input[name=health]").val(),
                "NodeId": $("#QualityOfLifeNodeId").val()
            };

            $('.loader').show();

            $.ajax({
                url: "/umbraco/surface/ContactForm/QualityOfLifeAssessmentForm",
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

    $(".accept-only-num").keypress(function (e) {
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
});

function CheckFormValidate() {
    let isValid = true;
    $(".assessment-form .patient-form input").removeClass('error');
    $(".form-accordian .card .card-header").removeAttr("style");
    $(".assessment-form .patient-form input:required").each(function () {
        if ($(this).val() == "" || $(this).val() == null || $(this).val().length <= 0) {
            $(this).closest('.card').find('.card-header').css("background", 'red');
            if ($(this).attr('name') != "health") {
                $(this).closest('.card').find('.card-header a').trigger('click');
            }
            $(this).addClass('error');
            $(this).focus();
            isValid = false;
            return false;
        }
    });

    if (isValid) {
        let radioNameList = ["employment", "self-care", "usual-activities", "discomfort", "depression"];
        for (var i = 0; i < radioNameList.length; i++) {
            isValid = $(".assessment-form .patient-form input[name='" + radioNameList[i] + "']").is(':checked');
            if (!isValid) {
                $(".assessment-form .patient-form input[name='" + radioNameList[i] + "']:eq(0)").focus();
                return isValid;
            }
        }
    }

    return isValid;
}