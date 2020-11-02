$(function () {
    $(".submit-btn button").click(function () {
        
        var model = {};
        var IsOk = true;
        //Personal Details
        model.ContactName = $("#ContactName").val().trim();
        model.Email = $("#Email").val().trim();
        model.PhoneNumber = $("#PhoneNumber").val().trim();
        model.message = $("textarea").val().trim();
        model.disease = $("#disease").val().trim();
       
        
       
        if (model.disease === "" || model.disease === null) {
            $("#disease").addClass("error");
            IsOk = false;
        }
        else {
            $("#disease").removeClass("error");

        }

        if (model.ContactName === "" || model.ContactName === null) {
            $("#ContactName").addClass("error");
            IsOk = false;
        }
        else {
            $("#ContactName").removeClass("error");
            
        }
        if (model.PhoneNumber === "" || model.PhoneNumber === null) {
            $("#PhoneNumber").addClass("error");
            IsOk = false;
        }
        else {
            if (validatephone(model.PhoneNumber)) {
                $("#PhoneNumber").removeClass("error");

            }
            else {
                $("#PhoneNumber").addClass("error");

                IsOk = false;
            }

        }
        if (model.message === "" || model.message === null) {
            $("#message").addClass("error");
            IsOk = false;
        }
        else {
            $("#message").removeClass("error");

        }

        if (model.Email === "" || model.Email === null) {
            $("#Email").addClass("error");
            
            IsOk = false;
        }
        else {
            if (validateEmail(model.Email)) {
                $("#Email").removeClass("error");
                
            }
            else {
                $("#Email").addClass("error");
                
                IsOk = false;
            }
        }

       

        if (IsOk) {
            $('.loader').show();
            model.globalsettingId = $("#globalsettingId").val();
            model.NodeId = $("#NodeId").val();
            $.ajax({
                type: "post",
                url: "/umbraco/surface/ContactForm/eligibilityform",
                dataType: "json",
                data: model,
                success: function (result) {
                  if (result.result) {
                        $('.loader').hide();
                      $("#expertTalk .alert-success").text(result.message);
                        $("#expertTalk .alert-success").slideDown("slow");
                        $("input").val('');
                        $("textarea").val('');
                        
                    }
                    else {
                        $('.loader').hide();
                        $("#expertTalk .alert-danger").text(result.message);
                        $("#expertTalk .alert-danger").slideDown("slow");
                    }
                    setTimeout(function () {
                        $("#expertTalk .alert").slideUp("slow");
                    }, 5000);
                }
            });
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
