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
        var randomTrain = createTrain(i);

        listOfRandomTrains.push(randomTrain);
    }
    return listOfRandomTrains;
}

function createTrain(idx) {
    var randomTrain = {
        "id": idx,
        "breakdown": false
    };

    randomTrain["travelDelta"] = 0;

    randomLine = generateRandomLine();
    randomStationIdx = generateRandomNumber(0, randomLine.length);

    randomTrain["stationNow"] = randomLine[randomStationIdx];
    randomTrain["stationNext"] = randomLine[randomStationIdx + 1];

    return randomTrain;
}