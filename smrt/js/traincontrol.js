// Dummy object 
var dummyTrain = {
    "id": 0,
    "stationNow": [],
    "stationNext": [],
    "travelDelta": 0.5,
    "breakdown": false
}

// Called in main.js
function createRandomTrains(numOfTrains) {
    var listOfRandomTrains = [];

    for (var i = 0; i < numOfTrains; i++) {
        var randomTrain = createTrain;

        listOfRandomTrains.push(randomTrain);
    }
    console.log(listOfRandomTrains)
    return listOfRandomTrains;
}

function createTrain() {
    var randomTrain = {
        "id": i,
        "breakdown": false
    };

    randomTrain["travelDelta"] = 0;

    randomLine = generateRandomLine();
    randomStationIdx = generateRandomNumber(0, randomLine.length);

    randomTrain["stationNow"] = randomLine[randomStationIdx];
    randomTrain["stationNext"] = randomLine[randomStationIdx + 1];

    return randomTrain;
}