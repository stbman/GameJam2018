$(document).ready(function() {
	
    for (var i = 0; i < stationList.length; i++) {
        var train = stationList[i];
		
		var trainElem = "<area class=\"mrt-map-area\" id=\"" + train["id"] + "\" shape=\"circle\" coords=\"" + train["coords"] + "\"" + " href=\"#\"  alt=\"" + train["alt"] + "\">";  
		$("#mrtmap").append(trainElem);
	}
	
	$("#map-container").click(function (event) {
	    console.log(event.clientX + " " + event.clientY);
	});

	$("area.mrt-map-area").mouseover(function (event) {
		/*
        if (hide) {
            clearTimeout(hide);
        }
		*/
        mrt_id = $(this).attr("id");
        mrt_name = $(this).attr("alt");
		
		console.log(mrt_id, mrt_name)
		

    })
	/*
	.mouseout(function (event) {
        hide = setTimeout(function () {
            $("#mrt-info-cont").hide();
        }, 500);
    });
	*/
	
    $("area.mrt-map-area").click(function() {
		mrt_id = $(this).attr("id");
        mrt_name = $(this).attr("alt");
		alert(mrt_id + " " + mrt_name);
	});

});
