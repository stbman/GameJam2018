function generateRandomNumber(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}

function generateRandomLine() {
    var num = generateRandomNumber(0, allTrainLists.length);
    return allTrainLists[num];
}