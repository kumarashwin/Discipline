
document.getElementById("arrow-left")
    .addEventListener("click", getArrowEventHandler(-1));

var arrowRight = document.getElementById("arrow-right");
arrowRight.addEventListener("click", getArrowEventHandler(1));

function getArrowEventHandler(direction) {
    return function (event) {
        event.stopPropagation();

        currentDate = currentDate.addDays(direction);


        if (currentDate.addDays(4).dateObject.getTime() == today.getTime()) {
            arrowRight.style.display = "none";
        } else {
            arrowRight.style.display = "";
        }

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