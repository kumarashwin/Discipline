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
            var requestDate = currentDate.addDays(-20);
            minDateBeforeFetch = currentDate.addDays(-21);
            var url = window.location.protocol + "//" + window.location.host + "/" + currentDate.addDays(-16).dateString.replace(/-/g, "\\");
            requestMoreActivities(url, "left");
        } else if (currentDate.dateString == maxDateBeforeFetch.dateString) {
            var requestDate = currentDate.addDays(20);
            maxDateBeforeFetch = currentDate.addDays(21);
            var url = window.location.protocol + "//" + window.location.host + "/" + currentDate.addDays(16).dateString.replace(/-/g, "\\");
            requestMoreActivities(url, "right");
        }

        chart.ready(returnSevenDaysAroundDate(currentDate, activities), chart.activity, true);
        chart.svg.parentElement.addEventListener("transitionend", onTransparentFinish);
        chart.svg.parentElement.className = "makeTransparent";
    };
}