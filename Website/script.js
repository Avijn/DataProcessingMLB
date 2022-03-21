console.log("test")

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
            teamdropdown = [];
            for(teams in response.data)
            {
                teamdropdown += "<option value= "+ teams +">"+ teams +"</option>"
            }
            Document.getElementById("teamDropdown").innerhtml = teamdropdown
        });