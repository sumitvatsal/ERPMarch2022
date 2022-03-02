app.controller("mycntrl1", function ($scope, myService, $timeout) {
  

    $scope.findSquare = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var country = {
                number: $scope.number,
              
            };
            var getData = myService.AddCountry(country);
            getData.then(function (msg) {
              
                $("#divsuccesmsg").fadeIn();
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                    //  $scope.IsVisible = false;
                }, 2000);

              
            }, function () {
                alert('Error in adding record');
            });
        }
    }
});