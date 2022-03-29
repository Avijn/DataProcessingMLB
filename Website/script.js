axios({
    method: 'get',
    url: 'http://localhost:27508/api/MLBGames/GetAllTeams2013'
})
    .then(function(response) {
        linkTableArray = response.data;
        teamdropdown = [];
        for(i=0;i<linkTableArray.length;i++)
        {
            teamdropdown += "<option value='"+ { team : linkTableArray[i].team, "city": linkTableArray[i].city, "nickName": linkTableArray[i].nickName } +"'>"+ linkTableArray[i].team +"</option>"
        }
        document.getElementById("teamDropdown").innerHTML = teamdropdown
    });

    google.charts.load('current',{packages:['corechart']});

    
    function drawChart(data) {
        var graph = google.visualization.arrayToDataTable([
            ['Year', 'Price'],
            [data[0].year,data[0].price],[data[1].year,data[1].price],[data[2].year,data[2].price],[data[3].year,data[3].price],[data[4].year,data[4].price]
        ]);

        var options = {
        title: 'Beer prices per year per team',
        hAxis: {title: 'Year'},
        vAxis: {title: 'Price'},
        legend: 'none'
        };

        var chart = new google.visualization.LineChart(document.getElementById('BeerPricesPerTeam'));
        chart.draw(graph, options);
    }

    function GetBeerPrices(){
        var team = document.getElementById("teamDropdown").value;
        console.log(team)

        axios({
            method: 'get',
            url: "http://localhost:27508/api/MLBBeer/" + team,
            headers: {
            'Content-Type': document.getElementById("switchbutton").value,
            'Accept': document.getElementById("switchbutton").value }
        })
        .then(function(response){
            if(document.getElementById("switchbutton").value == "text/xml")
            {
                var data= [];
                var parser = new DOMParser();
                var xmldata = parser.parseFromString(response.data, "text/xml");
                for(i=0;i<xmldata.getElementsByTagName("price").length;i++)
                {
                    var price = xmldata.getElementsByTagName("price")[i].textContent;
                    var year = xmldata.getElementsByTagName("year")[i].textContent;
                    data.push({ 
                        price: parseInt(price), 
                        year: parseInt(year)
                    });
                }
            }

            if(document.getElementById("switchbutton").value == "application/json")
            {
                data = response.data;
            }
            
            drawChart(data);
        });
    }

    function GetMatchesPerTeamPerYear(){
        var team = document.getElementById("teamDropdown").value;
        var year = document.getElementById("year").value;

        axios({
            method: 'get',
            url: 'http://localhost:27508/api/MLBGames/GetAllTeams2013'
        })
        
    }

    function XMLJSONswtich()
    {
        var state = document.getElementById("switchbutton").value

        if(state == "application/json")
        {
            document.getElementById("switchbutton").value = "text/xml";
            document.getElementById("switchbutton").textContent = "XML";
        }
        if(state == "text/xml")
        {
            document.getElementById("switchbutton").value = "application/json";
            document.getElementById("switchbutton").textContent = "JSON";
        }
    }