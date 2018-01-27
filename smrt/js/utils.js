function generateRandomNumber(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}

function generateRandomLine() {
    console.log("Generate Random Line");
    var num = generateRandomNumber(0, allTrainLists.length);
    console.log(num);
    console.log(allTrainLists[num]);
    /*
    greenLine
    redLine
    purpleLine
    circleLine
    downtownLine
    LRTBPLine
    LRTPELine
    LRTPWLine
    LRTSELine
    LRTSWLine
    */
}