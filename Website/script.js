
axios({
    method: 'get',
    url: 'http://localhost:27508/api/MLBBeer'
})
    .then(function(response) {
        console.log(response.data);
    });

axios({
    method: 'get',
    url: 'http://localhost:27508/api/MLBGames/GetAllTeams2013'
})
    .then(function(response) {
        console.log(response.data);
        linkTableArray = response.data;
        teamdropdown = [];
        for(i=0;i<linkTableArray.length;i++)
        {
            teamdropdown += "<option value= "+ linkTableArray[i] +">"+ linkTableArray[i] +"</option>"
        }
        document.getElementById("teamDropdown").innerHTML = teamdropdown
    });

new Vue(
    el: "#homepage",
    data: {

    },
    created: function() {
        console.log("test")
    }
);