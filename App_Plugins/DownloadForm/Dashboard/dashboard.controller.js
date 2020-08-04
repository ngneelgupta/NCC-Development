angular.module("umbraco").controller("DownloadForm.DownloadFormDashboardController",
    function ($scope, $window, $http, notificationsService) {
        $scope.ButtonClick = false;
        $scope.FormsData = [];

        //Get all forms data
        $http({
            url: "/umbraco/surface/ContactForm/GetAllFormList",
            method: 'get'
        }).then(function (response) {
            $scope.FormsData = response.data;
        });

        $scope.DownloadFormCSV = function (id) {
            var SelectedPatientFormIds = [];
            $("#" + id).closest("table").find("tbody input:checked").each(function () {
                SelectedPatientFormIds.push($(this).val());
            });

            if (!$scope.ButtonClick) {
                $scope.ButtonClick = true;
                let startDate = $("#fromDate").val();
                let endDate = $("#toDate").val();

                $http({
                    url: "/umbraco/surface/ContactForm/DownloadFormCSV?id=" + id + "&startDate=" + startDate + "&endDate=" + endDate + "&SelectedPatientFormIds=" + SelectedPatientFormIds,
                    method: 'get'
                }).then(function (response) {
                    $scope.ButtonClick = false;
                    if (response.data.length == 0) {
                        notificationsService.error("Download form file", "Sorry, no record found !!");
                    }
                    else {
                        blob = new Blob([response.data], { type: 'text/csv' }),
                            url = $window.URL || $window.webkitURL;
                        var downloadLink = angular.element('<a></a>');
                        downloadLink.attr('href', url.createObjectURL(blob));
                        downloadLink.attr('target', '_self');
                        if (id == 1301)
                            downloadLink.attr('download', 'Quality of Life Assessment.csv');
                        else if (id == 1306)
                            downloadLink.attr('download', 'Depression Anxiety Stress Score.csv');
                        else if (id == 1313)
                            downloadLink.attr('download', 'Brief Pain Inventory.csv');
                        else if (id == 1231)
                            downloadLink.attr('download', 'Referral Forms.csv');
                        else if (id == 1178)
                            downloadLink.attr('download', 'Patient Form.csv');
                        downloadLink[0].click();
                        notificationsService.success("Download form file", "File download successfully !!");
                    }
                });
            }
            else {
                notificationsService.error("Download form file", "Request in progress. Please wait..");
            }
        };

        $scope.SelectAll = function (id) {
            var isChecked = $("#" + id).prop("checked");
            $("#" + id).closest("table").find("tbody input").prop("checked", isChecked);
        }

        $scope.SelectSingle = function (id) {
            var isAllSelect = true;
            $("#" + id).closest("tbody").find("input").each(function () {
                if (!$(this).prop("checked")) {
                    isAllSelect = false;
                }
            });
            
            $("#" + id).closest("table").find("thead input").prop("checked", isAllSelect);
        }

        $(function () {
            app.init();
        });
    });

var app = function () {
    var init = function () {
        $("#fromDate").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            //numberOfMonths: 1,
            onClose: function (selectedDate) {
                $("#toDate").datepicker("option", "minDate", selectedDate);
            }
        });
        $("#toDate").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            changeYear: true,
            //numberOfMonths: 1,
            onClose: function (selectedDate) {
                $("#fromDate").datepicker("option", "maxDate", selectedDate);
            }
        });
    };
    return {
        init: init
    }
}();