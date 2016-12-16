// == ENTRY POINT FOR GENERATING THE CHART OF ACTIVITIES ACROSS TIME ==

// GLOBAL VARIABLES
var today, centerBarDate, minDateBeforeFetch, maxDateBeforeFetch, arrowRight;

// Body onLoad Event Handler
// Reruns every time the bodyDiv changes
function main(event) {
    // This variable is used to determine when to hide the right arrow
    today = (new DateHelper()).today();
    centerBarDate = today.addDays(-4);

    // Limits before Ajax requests for more activities are sent
    minDateBeforeFetch = centerBarDate.addDays(-5);
    maxDateBeforeFetch = centerBarDate.addDays(5);

    // Left arrow doesn't yet need any special functionality
    document.getElementById("arrow-left").addEventListener("click", getArrowEventHandler(-1));
    arrowRight = new ArrowRight(document.getElementById("arrow-right"));
}

// Upon 'show-chart' being clicked, the request date is set to client's date - 4 days
function prepareAnchorForChartRequest() {
    var showChart = document.getElementById("show-chart");
    showChart.href = centerBarDate.dateObject.getFullYear() + "/" + (centerBarDate.dateObject.getMonth() + 1) + "/" + centerBarDate.dateObject.getDate();
}

// GLOBAL VARIABLES
// Variables which are vital to the chart, which will be populated continously through AJAX requests;
var activities, budgets, chart;

// When the Ajax response for the chart data is returned to the client, the following goes through
function resolveAjaxReponseForChart(data, status, xhr) {
    if (status == "success") {

        // Code to show the svgDiv
        document.getElementById("chartDiv").style.display = "block";

        // Activities and budgets are set
        activities = data.activities;
        budgets = data.budgets;

        // The chart object is found and readied with the necessary data; then drawn
        chart = new Chart(document.getElementsByTagName("svg")[0]);
        chart.ready(returnSevenDaysAroundDate(centerBarDate, activities), null, true);
        chart.clear();
        chart.draw();
    }
}

function returnSevenDaysAroundDate(dateHelper, data) {
    var result = {};
    for (var i = -3; i <= 3; i++) {
        var currDate = dateHelper.addDays(i);
        result[currDate.dateString] = data[currDate.dateString];
    }
    return result;
}

var hoverOnRectActivityInfo = document.getElementById('hover-on-rect-activity-info');
var svg = document.getElementsByTagName("svg")[0];
svg.addEventListener('contextmenu', showActivityInfo);
function showActivityInfo(event) {
    if (event.target.nodeName == 'rect') {
        event.preventDefault();
        hoverOnRectActivityInfo.querySelector('h5#activity-name').innerHTML = event.target.getAttribute('data-activity-name');
        hoverOnRectActivityInfo.style.left = event.pageX + "px";
        hoverOnRectActivityInfo.style.top = event.pageY + "px";
        hoverOnRectActivityInfo.style.display = "block";
    }
}
