var chart = new Chart(document.getElementsByTagName("svg")[0]);
var today = new Date();
today.setHours(0, 0, 0, 0);

if (deactivateRightArrow) arrowRight.hide();

var currentDate = new DateHelper(processedDate);
var minDateBeforeFetch = currentDate.addDays(-5);

var maxDateBeforeFetch = currentDate.addDays(5);

//console.log(activities);
//console.log("min: ", minDateBeforeFetch.dateString);
//console.log("max: ", maxDateBeforeFetch.dateString);

chart.ready(returnSevenDaysAroundDate(currentDate, activities), null, true);
chart.draw();