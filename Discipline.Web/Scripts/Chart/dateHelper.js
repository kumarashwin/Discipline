var DateHelper = (function () {
    function DateHelper(dateString) {
        this.dateString = dateString;

        if (this.dateString) {
            var dateArray = dateString.split("-");
            this.dateObject = new Date(dateArray[0], dateArray[1] - 1, dateArray[2], 0, 0, 0, 0);
        }
        return this;
    }

    DateHelper.prototype.today = function () {
        var today = new Date();
        today.setHours(0, 0, 0, 0);

        var result = new DateHelper();
        result.dateObject = today;
        result.dateString = convertToViableDateString(today);
        return result;
    };

    DateHelper.prototype.addDays = function (day) {
        var newDate = new Date(this.dateObject);
        newDate = new Date(newDate.setDate(newDate.getDate() + day));
        
        var result = new DateHelper();
        result.dateObject = newDate;
        result.dateString = convertToViableDateString(newDate);
        return result;
    };

    DateHelper.prototype.getDayOfTheWeek = function () {
        var dayOfTheWeek = this.dateObject.getUTCDay();
        switch (dayOfTheWeek) {
            case 0:
                return 'Sunday';
                break;
            case 1:
                return 'Monday'
                break;
            case 2:
                return 'Tuesday'
                break;
            case 3:
                return 'Wednesday'
                break;
            case 4:
                return 'Thursday'
                break;
            case 5:
                return 'Friday'
                break;
            case 6:
                return 'Saturday'
                break;
        }
    }

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