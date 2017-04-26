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