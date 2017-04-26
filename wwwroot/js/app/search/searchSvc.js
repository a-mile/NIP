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