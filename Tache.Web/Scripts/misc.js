// == ENTRY POINT FOR GENERATING THE CHART OF ACTIVITIES ACROSS TIME ==

// Variables which are vital to the chart, which will be populated continously through AJAX requests;
var deactivateRightArrow, processedDate, activities, budgets, currentDate, minDateBeforeFetch, maxDateBeforeFetch, today, chart;

function showChart(data, status, xhr) {
    if (status == "success") {
        // Code to show the svgDiv

        if (deactivateRightArrow = data.deactivateRightArrow) arrowRight.hide();
        processedDate = data.processedDate;
        activities = data.activities;
        budgets = data.budgets;

        currentDate = new DateHelper(processedDate);
        minDateBeforeFetch = currentDate.addDays(-5);
        maxDateBeforeFetch = currentDate.addDays(5);
        (today = new Date()).setHours(0, 0, 0, 0);

        chart = new Chart(document.getElementsByTagName("svg")[0]);
        chart.ready(returnSevenDaysAroundDate(currentDate, activities), null, true);
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