$(document).ready(function () {

    appendTrainMap();

    var listOfRandomTrains = createRandomTrains(10);
    createTrainTable(listOfRandomTrains);
	
	$("#map-container").click(function (event) {
	    console.log(event.clientX + " " + event.clientY);
	});

	$("area.mrt-map-area").mouseover(function (event) {
	    mrt_id = $(this).attr("id");
	    mrt_name = $(this).attr("alt");

	    console.log(mrt_id, mrt_name);
	});
	
    $("area.mrt-map-area").click(function() {
		mrt_id = $(this).attr("id");
        mrt_name = $(this).attr("alt");
		alert(mrt_id + " " + mrt_name);
	});

});

function appendTrainMap() {
    for (var i = 0; i < stationList.length; i++) {
        var train = stationList[i];

        var trainElem = "<area class=\"mrt-map-area\" id=\"" + train["id"] + "\" shape=\"circle\" coords=\"" + train["coords"] + "\"" + " href=\"#\"  alt=\"" + train["alt"] + "\">";
        $("#mrtmap").append(trainElem);
    }
}

function createTrainTable(listOfRandomTrains) {
    console.log(listOfRandomTrains)
    var trainBarId = "#train-list";
    $(trainBarId).html("");

    var trainHTML = "";
    for (var i = 0; i < listOfRandomTrains.length; i++) {
        var trainObject = "";
        trainObject = 
            "<div class=\"row\" id=\"train-object\">" +
            "<div class=\"col-sm\">" + i + 
            "</div>" +
            "<div class=\"col-sm\">" + listOfRandomTrains[i]["stationNow"]["id"] + 
            "</div>" +
            "<div class=\"col-sm\">" + "A TRAIN HERE"
            "</div>" +
            "</div>";

        trainHTML += trainObject;
    }

    console.log(trainHTML);

    $(trainBarId).html(trainHTML);
}
