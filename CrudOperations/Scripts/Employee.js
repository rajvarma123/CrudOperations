var testApp = angular.module('EmployeeModule', []);

testApp.controller('Employee', function ($scope, $http) {
    $http.get('http://localhost:61465/Api/Employee/GetAllEmployees').then(function (response)
         {
        $scope.Employees = response.data;
    });
    });
