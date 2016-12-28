var ArrowRight = (function () {
    function ArrowRight(element) {
        this.element = element;
        this.element.addEventListener("click", getArrowEventHandler(1));
        this.hide();
    }

    ArrowRight.prototype.hide = function () { this.element.disabled = 'disabled' };
    ArrowRight.prototype.show = function () { this.element.disabled = '' };

    return ArrowRight;
})();

function getArrowEventHandler(direction) {
    return function (event) {
        event.stopPropagation();

        centerBarDate = centerBarDate.addDays(direction);

        // arrowRight is inactive if the right-most bar shows yesterdays data 
        if (centerBarDate.addDays(4).dateObject.getTime() == today.dateObject.getTime())
            arrowRight.hide();
        else
            arrowRight.show();

        // Handles sending Ajax request for more activities
        if (centerBarDate.dateString == minDateBeforeFetch.dateString)
            prepareThenSendRequest("left", minDateBeforeFetch);
        else if (centerBarDate.dateString == maxDateBeforeFetch.dateString)
            prepareThenSendRequest("right", maxDateBeforeFetch);

        chart.ready(returnSevenDaysAroundDate(centerBarDate, activities), chart.activity, true);
        startTransition();
    };
}

function prepareThenSendRequest(direction, dateBeforeFetch){
    var multiplier = 1;
    if(direction == "left")
        multiplier = -1;

    var requestDate = centerBarDate.addDays(16 * multiplier);
    
    // Set either the minDateBeforeFetch or the maxDateBeforeFetch to the newDateBeforeFetch
    var newDateBeforeFetch = requestDate.addDays(5 * multiplier);
    dateBeforeFetch.dateObject = newDateBeforeFetch.dateObject;
    dateBeforeFetch.dateString = newDateBeforeFetch.dateString;

    var url = window.location.protocol + "//" + window.location.host + "/" + requestDate.dateString.replace(/-/g, "/");
    requestMoreActivities(url, direction);
}