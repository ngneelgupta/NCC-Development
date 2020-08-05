$(function () {
    var formData = {};
    $(".submit-form-btn").click(function () {
        var isValid = CheckFormValidate();

        if (isValid) {
            formData = {
                "fullname": $(".assessment-form .patient-form input[name='full_name']").val(),
                "surname": $(".assessment-form .patient-form input[name=surname]").val(),
                "dob": $(".assessment-form .patient-form input[name='dob']").val(),
                "pain": $(".assessment-form .patient-form input[name='pain']:checked").siblings().text(),
                "leastpain": $(".assessment-form .patient-form input[name='leastpain']:checked").siblings().text(),
                "average": $(".assessment-form .patient-form input[name='average']:checked").siblings().text(),
                "HowMuchPain": $(".assessment-form .patient-form input[name=HowMuchPain]:checked").siblings().text(),
                "title": $(".assessment-form .patient-form input[name=title]").val(),
                "medications": $(".assessment-form .patient-form input[name='medications']:checked").siblings().text(),
                "generalActivity": $(".assessment-form .patient-form input[name='generalActivity']:checked").siblings().text(),
                "mood": $(".assessment-form .patient-form input[name='mood']:checked").siblings().text(),
                "ability": $(".assessment-form .patient-form input[name='ability']:checked").siblings().text(),
                "housework": $(".assessment-form .patient-form input[name='housework']:checked").siblings().text(),
                "Relations": $(".assessment-form .patient-form input[name='Relations']:checked").siblings().text(),
                "sleep": $(".assessment-form .patient-form input[name='sleep']:checked").siblings().text(),
                "Enjoyment": $(".assessment-form .patient-form input[name='Enjoyment']:checked").siblings().text(),
                "NodeId": $("#BriefInventoryNodeId").val()
            };

            $('.loader').show();

            $.ajax({
                url: "/umbraco/surface/ContactForm/BriefPainInventoryForm",
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
        let radioNameList = ["pain", "leastpain", "average", "HowMuchPain", "medications", "generalActivity", "mood", "ability", "housework", "Relations", "sleep", "Enjoyment"];
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