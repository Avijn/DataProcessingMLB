var data = [];

async function getTeams()
{
    //var data = [];

    let x = await axios({
        method: 'get',
        url: 'http://localhost:27508/api/MLBGames/GetAllTeams2013'
    });

    linkTableArray = x.data
    for(i=0;i<linkTableArray.length;i++)
    {
        data.push(
            {
                team : linkTableArray[i].team,
                city: linkTableArray[i].city,
                nickName: linkTableArray[i].nickName
            }
        );
    }
    return data
}

async function loadTeams()
{
        const data = await getTeams();
        teamdropdown = [];
        for(i=0;i<data.length;i++)
        {
            teamdropdown += "<option value='"+ data[i].team +"'>"+ data[i].team +"</option>"
        }
        document.getElementById("teams").innerHTML = teamdropdown;
}

async function loadMatchTeams()
{
        const data = await getTeams();
        var teamdropdown = [];
        for(i=0;i<data.length;i++)
        {
            teamdropdown += "<option value='"+ data[i].nickName +"'>"+ data[i].team +"</option>"
        }
        document.getElementById("teamDropdown").innerHTML = teamdropdown;
        document.getElementById("oppDropdown").innerHTML = teamdropdown;
}

async function loadBeerTeams()
{
        const data = await getTeams();
        var teamdropdown = [];
        var teamNickNamedropdown =[];
        for(i=0;i<data.length;i++)
        {
            teamdropdown += "<option value='"+ data[i].team +"'>"+ data[i].team +"</option>";
            teamNickNamedropdown += "<option value='"+ data[i].nickName +"'>"+ data[i].nickName +"</option>";
        }
        document.getElementById("BeerteamDropdown").innerHTML = teamdropdown;
        document.getElementById("beerTeamNickname").innerHTML = teamNickNamedropdown;
}

    google.charts.load('current',{packages:['corechart']});

    
    function drawBeerChart(data) {
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
        var team = document.getElementById("teams").value;

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
            
            drawBeerChart(data);
        });
    }

    function GetMatchesPerTeamPerYear(){
        var team = document.getElementById("teams").value;
        var year = document.getElementById("year").value;
        var currTeam;

        for(i=0;i<data.length;i++)
        {
            if(team == data[i].team)
            {
                currTeam = data[i];
            }
        }

        axios({
            method: 'get',
            url: 'http://localhost:27508/api/MLBGames/' + currTeam.nickName + "/" + year,
            headers: {
                'Content-Type': document.getElementById("switchbutton").value,
                'Accept': document.getElementById("switchbutton").value }
        })
        .then(function(response){
            var data = [];
            for(i=0;i<response.data.length;i++)
            {
                data.push({
                    game: response.data[i].g,
                    win_lose : response.data[i].win_or_lose
                })
            }
            drawMatchChart(data);
        })
        
    }

    function getCPI(){
        var year = document.getElementById("year").value;

        axios({
            method: 'get',
            url: 'http://localhost:27508/api/USCPI/' + year,
            headers: {
                'Content-Type': document.getElementById("switchbutton").value,
                'Accept': document.getElementById("switchbutton").value }
        })
        .then(function(response){
            data = response.data;
            drawCPIChart(data);
        })
    }

    function drawMatchChart(data) {

        var graphArray = [];
        var wins = 0;
        for(i=0;i<data.length;i++)
        {
            if(data[i].win_lose === "W")
            {
                wins++;
            }
            graphArray.push([data[i].game,wins]);
        }
        var graph = new google.visualization.DataTable();
        graph.addColumn("number", "Matches");
        graph.addColumn("number", "Wins");
        graph.addRows(graphArray)

        var options = {
        title: 'Wins per team',
        hAxis: {title: 'Amount of matches'},
        vAxis: {title: 'Wins'},
        legend: 'none'
        };

        var chart = new google.visualization.LineChart(document.getElementById('WinsPerTeam'));
        chart.draw(graph, options);
    }

    function drawCPIChart(data) {
        console.log(data)
        var graphArray = [];
        for(i=0;i<data.length;i++)
        {
            graphArray.push([i,data[i].cpi])
        }
        console.log(graphArray);
        var graph = new google.visualization.DataTable();
        graph.addColumn("number", "Months");
        graph.addColumn("number", "CPI");
        graph.addRows(graphArray)

        var options = {
        title: 'CPI',
        hAxis: {title: 'Months'},
        vAxis: {title: 'CPI'},
        legend: 'none'
        };

        var chart = new google.visualization.LineChart(document.getElementById('cpi'));
        chart.draw(graph, options);
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

    function addTeam(){
        var newteam = document.getElementById("teamName").value;
        var newcity = document.getElementById("teamCity").value;
        var newnickName = document.getElementById("teamNickname").value;
        var newcityNickName = document.getElementById("teamCityNickname").value;
        console.log(newteam)
        axios.post('http://localhost:27508/api/MLBGames/newTeam', 
        {
            team: newteam,
            nickName: newcity,
            city: newnickName,
            cityNickName: newcityNickName,
        });

    }

    function addMatch(){
        var teamDropdown = document.getElementById("teamDropdown").value;
        var oppDropdown = document.getElementById("oppDropdown").value;
        var w_l = document.getElementById("w_l").value;
        var home_away = document.getElementById("home_away").value;
        var newRank = document.getElementById("Rank").value;
        var newDate = document.getElementById("date").value;
        var newYear = document.getElementById("year").value;
        var newgameNumber = document.getElementById("g").value;
        
        axios.post('http://localhost:27508/api/MLBGames/newGame', 
        {
            team: teamDropdown,
            home_away: home_away,
            opp: oppDropdown,
            win_or_lose: w_l,
            rank: newRank,
            date: newDate,
            year: newYear,
            g: newgameNumber
        });
    }

    function addBeer(){        
        var beerTeam = document.getElementById("BeerteamDropdown").value;
        var beerYear = document.getElementById("beerYear").value;
        var beerNickname = document.getElementById("beerTeamNickname").value;
        var beerPrice = document.getElementById("beerPrice").value;
        var beerSize = document.getElementById("beersize").value;
    axios.post('http://localhost:27508/api/MLBBeer', 
    {
        team: beerTeam,
        year: beerYear,
        nickname: beerNickname,
        price: beerPrice,
        size: beerSize,
        price_per_ounce: (beerSize / beerPrice)
    });

    }

    async function GetCPI(){
        var year = document.getElementById("year").value;

        let result = await axios({
            method: 'get',
            url: 'http://localhost:27508/api/USCPI/' + year
        })
        console.log(result)
        for(i=0;i<result.data.length;i++)
        {
            document.getElementById("cpiList").innerHTML += "<ul>Date: "+ result.data[i].yearmon +" CPI: <input id="+ result.data[i].yearmon +" type='textfield' value='"+ result.data[i].cpi + "'><button onclick=putCpi('" + result.data[i].yearmon + "')>Change!</button></ul>";
        }
    }

    function putCpi(date){
        var newCpi = document.getElementById(date).value
        axios.put('http://localhost:27508/api/USCPI',
        {
            yearmon : date,
            cpi : newCpi
        })
    }