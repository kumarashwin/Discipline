function returnSevenDaysAroundDate(date, data) {
    var result = {};
    var centerDate = new Date(date);

    for (var i = -3; i <= 3; i++) {
        var viableDateString = convertToViableDateString(new Date(date).setDate(centerDate.getDate() + i));
        result[viableDateString] = data[viableDateString];
    }
    return result;
}

function convertToViableDateString(date) {
    function get2D(num) {
        return (num.toString().length < 2 ? "0" + num : num).toString();
    }

    date = new Date(date);
    var year = date.getFullYear();
    var month = get2D(date.getMonth() + 1);
    var day = get2D(date.getDate() + 1);
    return year + "-" + month + "-" + day;
}