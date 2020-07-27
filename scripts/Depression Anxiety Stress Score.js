$(function () {
    var formData = {};
    $(".submit-form-btn").click(function () {
        var isValid = CheckFormValidate();

        if (isValid) {
            formData = {
                "fullname": $(".assessment-form .patient-form input[name='full_name']").val(),
                "surname": $(".assessment-form .patient-form input[name=surname]").val(),
                "dob": $(".assessment-form .patient-form input[name='dob']").val(),
                "HardWind": $(".assessment-form .patient-form input[name='HardWind']:checked").siblings().text(),
                "dryness": $(".assessment-form .patient-form input[name='dryness']:checked").siblings().text(),
                "experience": $(".assessment-form .patient-form input[name='experience']:checked").siblings().text(),
                "breathing": $(".assessment-form .patient-form input[name=breathing]:checked").siblings().text(),
                "initiative": $(".assessment-form .patient-form input[name=initiative]:checked").siblings().text(),
                "overreact": $(".assessment-form .patient-form input[name='over-react']:checked").siblings().text(),
                "trembling": $(".assessment-form .patient-form input[name='trembling']:checked").siblings().text(),
                "nervous": $(".assessment-form .patient-form input[name='nervous']:checked").siblings().text(),
                "worried": $(".assessment-form .patient-form input[name='worried']:checked").siblings().text(),
                "lookForward": $(".assessment-form .patient-form input[name='lookForward']:checked").siblings().text(),
                "agitated": $(".assessment-form .patient-form input[name='agitated']:checked").siblings().text(),
                "relax": $(".assessment-form .patient-form input[name='relax']:checked").siblings().text(),
                "downhearted": $(".assessment-form .patient-form input[name='down-hearted']:checked").siblings().text(),
                "intolerant": $(".assessment-form .patient-form input[name='intolerant']:checked").siblings().text(),
                "panic": $(".assessment-form .patient-form input[name='panic']:checked").siblings().text(),
                "enthusiastic": $(".assessment-form .patient-form input[name='enthusiastic']:checked").siblings().text(),
                "person": $(".assessment-form .patient-form input[name='person']:checked").siblings().text(),
                "touchy": $(".assessment-form .patient-form input[name='touchy']:checked").siblings().text(),
                "exertion": $(".assessment-form .patient-form input[name='exertion']:checked").siblings().text(),
                "scared": $(".assessment-form .patient-form input[name='scared']:checked").siblings().text(),
                "meaningless": $(".assessment-form .patient-form input[name='meaningless']:checked").siblings().text(),
                "NodeId": $("#DASSNodeId").val()
            };
            console.log(formData);

            $('.loader').show();

            $.ajax({
                url: "/umbraco/surface/ContactForm/DASSForm",
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

    return isValid;
}