﻿// Left arrow doesn't yet need any special functionality
document.getElementById("arrow-left").addEventListener("click", getArrowEventHandler(-1));

var ArrowRight = (function () {
    function ArrowRight(element) {
        this.element = element;
        this.element.addEventListener("click", getArrowEventHandler(1));
    }

    ArrowRight.prototype.hide = function () { this.element.style.display = "none" };
    ArrowRight.prototype.show = function () { this.element.style.display = "" };

    return ArrowRight;
})();
var arrowRight = new ArrowRight(document.getElementById("arrow-right"));

function getArrowEventHandler(direction) {
    return function (event) {
        event.stopPropagation();

        currentDate = currentDate.addDays(direction);

        if (currentDate.addDays(4).dateObject.getTime() == today.getTime())
            arrowRight.hide();
        else
            arrowRight.show();

        if (currentDate.dateString == minDateBeforeFetch.dateString) {
            prepareThenSendRequest("left", minDateBeforeFetch);
        } else if (currentDate.dateString == maxDateBeforeFetch.dateString) {
            prepareThenSendRequest("right", maxDateBeforeFetch);
        }

        chart.ready(returnSevenDaysAroundDate(currentDate, activities), chart.activity, true);

        startTransition();
    };
}

function prepareThenSendRequest(direction, dateBeforeFetch){
    var multiplier = 1;
    if(direction == "left")
        multiplier = -1;

    var requestDate = currentDate.addDays(20 * multiplier);
    var newDateBeforeFetch = currentDate.addDays(21 * multiplier);
    dateBeforeFetch.dateObject = newDateBeforeFetch.dateObject;
    dateBeforeFetch.dateString = newDateBeforeFetch.dateString;
    var url = window.location.protocol + "//" + window.location.host + "/" + currentDate.addDays(16 * multiplier).dateString.replace(/-/g, "\\");
    requestMoreActivities(url, direction);
}