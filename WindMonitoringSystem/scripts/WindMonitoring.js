var AngularMVCApp = angular.module('AngularMVCApp', []);

var WindController = function ($scope, $http) {
    $scope.model = {};
    $scope.Date = new Date();
    $http({
        method: "GET",
        url: "WindMonitoring/GetInitialData"
    }).then(function mySucces(response) {
        $scope.model = response.data;
        $scope.StateList = $scope.model.StateList;
    }, function myError(response) {
    });

    $scope.GetCitiesByState = function () {
        if ($scope.model.SelectedState && $scope.model.CityList) {
            $scope.CityList = $.grep($scope.model.CityList, function (v) {
                return v.StateId === parseInt($scope.model.SelectedState);
            });
        }
    };

    $scope.GetStationCode = function () {
        if ($scope.model.CityList && $scope.model.SelectedCity) {
            $.each($scope.model.CityList, function (index, val) {
                if (val.CityId === parseInt($scope.model.SelectedCity)) {
                    $scope.model.StationCode = val.StationCode;
                    $scope.GetPredictedSpeed();
                }
            });
        }
    };

    $scope.GetPredictedSpeed = function () {
        var stationCode = $scope.model.StationCode;
        $http({
            method: "GET",
            url: "WindMonitoring/GetPredictedSpeed",
            params: { stationCode: stationCode.toString() }
        }).then(function mySucces(response) {
            $scope.model.PredictedSpeed = response.data;
        });
    };

    $scope.GetVariance = function () {
        if (parseInt($scope.model.ActualSpeed) && parseInt($scope.model.PredictedSpeed)) {
            $scope.model.Variance = parseInt($scope.model.ActualSpeed) - parseInt($scope.model.PredictedSpeed);
        }
    };

    $scope.GetVarianceColor = function () {
        var variance = parseInt($scope.model.ActualSpeed) - parseInt($scope.model.PredictedSpeed);
        if (variance === 1 || variance === -1) { return "color-black" }
        else if ((variance === 2 || variance === 3) || (variance === -2 || variance === -3)) { return "color-purple" }
        else if ((variance <= 5 || variance > 3) || (variance <= -5 || variance > -3)) { return "color-red" }
    };

    $scope.SaveWindMonitoringSys = function () {
        var model = $scope.model;
        model.Date = $scope.Date;
        $http({
            dataType: 'json',
            method: "POST",
            url: "WindMonitoring/SaveWindMonitoring",
            data: { windMonitoring: model }
        }).then(function mySucces(response) {
            if (response.data === true) {
                alert("Data saved successfully!")
            }
        });
    };
};

AngularMVCApp.controller('WindController', WindController);
WindController.$inject = ['$scope', '$http'];