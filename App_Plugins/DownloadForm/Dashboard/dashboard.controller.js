angular.module("umbraco").controller("DownloadForm.DownloadFormDashboardController",
    function ($scope, $window, $http) {
        
        $scope.BriefPainInventoryCSV = function () {
            $http({
                url: "/umbraco/surface/ContactForm/BriefPainInventoryFormCSV?id=1313",
                method: 'get'
            }).then(function (response) {
                blob = new Blob([response.data], { type: 'text/csv' }),
                    url = $window.URL || $window.webkitURL;

                var downloadLink = angular.element('<a></a>');
                downloadLink.attr('href', url.createObjectURL(blob));
                downloadLink.attr('target', '_self');
                downloadLink.attr('download', 'Brief Pain Inventory.csv');
                downloadLink[0].click();
            });
        };
    });

