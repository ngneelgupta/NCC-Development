$(function () {
    $(".submit-btn button").click(function () {

        var model = {};

        //Personal Details
        model.ContactName = $("#commentorname").val();
        model.Email = $("#commentoremail").val();
        model.message = $("#commentormessage").val();
        


        var IsOk = true;
        
        if (model.ContactName === "" || model.ContactName === null) {
            $("#commentorname").addClass("error");
            IsOk = false;
        }
        else {
            $("#commentorname").removeClass("error");

        }
       
        if (model.message === "" || model.message === null) {
            $("#commentormessage").addClass("error");
            IsOk = false;
        }
        else {
            $("#commentormessage").removeClass("error");

        }

        if (model.Email === "" || model.Email === null) {
            $("#commentoremail").addClass("error");

            IsOk = false;
        }
        else {
            if (validateEmail(model.Email)) {
                $("#commentoremail").removeClass("error");

            }
            else {
                $("#commentoremail").addClass("error");

                IsOk = false;
            }
        }



        if (IsOk) {
            $('.loader').show();
            model.BlogPostID = $("#BlogPostID").val();
            
            $.ajax({
                type: "post",
                url: "/umbraco/surface/ContactForm/BlogReply",
                dataType: "json",
                data: model,
                success: function (result) {
                    if (result.result) {
                        $('.loader').hide();
                        $("#blogReply .alert-success").text(result.message);
                        $("#blogReply .alert-success").slideDown("slow");
                        $("input").val('');
                        $("textarea").val('');
                        setTimeout(function () {
                            location.reload(true);
                        }, 3000);
                        
                    }
                    else {
                        $('.loader').hide();
                        $("#blogReply .alert-danger").text("Recode save failure !!");
                        $("#blogReply .alert-danger").slideDown("slow");
                    }
                    setTimeout(function () {
                        $("#blogReply .alert").slideUp("slow");
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
