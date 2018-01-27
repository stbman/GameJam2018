var trainList = [{
  "name": "PENGUIN STOP",
  "coords": "488,98,6",
  "id": "TEST",
  "alt": "help"
  }
]

//var hide = false;

$(document).ready(function() {
	
	for (var i=0; i<trainList.length; i++) {
		var train = trainList[i];
		
		var trainElem = "<area class=\"mrt-map-area\" id=\"" + train["id"] + "\" shape=\"circle\" coords=\"" + train["coords"] + "\"" + " href=\"#\"  alt=\"" + train["alt"] + "\">";  
		console.log(trainElem);
		$("#mrtmap").append(trainElem);
	}
	
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
