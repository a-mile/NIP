(function () {
	'use strict';

	window.app = angular.module('NIP', []);
})();
(function() {
	'use strict';

	window.app.controller('SearchController', SearchController);

	SearchController.$inject = ['searchSvc'];

	function SearchController(searchSvc) {
		var vm = this;

		vm.form = "searchForm";
		vm.key = "";
		vm.message = null;
		vm.showResult = false;
		vm.business = null;		

		vm.search = function(){
			vm.showResult = true;
			searchSvc.findBusiness(vm.key).then(
				function(response){
					vm.business = response.data;
					vm.message = null;
				},
				function(response){
					vm.business = null;
					vm.message = "Nie znaleziono danych";
				}
			)
		};

	}
})();
(function() {
	window.app.factory('searchSvc', searchSvc);

	searchSvc.$inject = ['$http'];
	function searchSvc($http) {		
		var svc = {
			findBusiness : findBusiness
		};

		return svc;		

        function findBusiness(key){
            return $http.get('api/business/' + key);
        }
	}
})();