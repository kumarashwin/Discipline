function returnSevenDaysAroundDate(dateHelper, data) {
    var result = {};

    for (var i = -3; i <= 3; i++) {
        var currDate = dateHelper.addDays(i);
        result[currDate.dateString] = data[currDate.dateString];
    }
    return result;
}