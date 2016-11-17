var DateHelper = (function () {
    function DateHelper(dateString) {
        this.dateString = dateString;

        if (this.dateString) {
            var dateArray = dateString.split("-");
            this.dateObject = new Date(dateArray[0], dateArray[1] - 1, dateArray[2], 0, 0, 0, 0);
        }
        return this;
    }

    DateHelper.prototype.addDays = function (day) {
        var newDate = new Date(new Date().setDate(this.dateObject.getDate() + day));
        newDate.setHours(0, 0, 0, 0);
        var result = new DateHelper();
        result.dateObject = newDate;
        result.dateString = convertToViableDateString(newDate);
        return result;
    };

    function leftPadZeros(num) {
        return (num.toString().length < 2 ? "0" + num : num).toString();
    }

    function convertToViableDateString(date) {
        var year = date.getFullYear();
        var month = leftPadZeros(date.getMonth() + 1);
        var day = leftPadZeros(date.getDate());
        return year + "-" + month + "-" + day;
    }

    return DateHelper;
})();