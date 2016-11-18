var chart = new Chart(document.getElementsByTagName("svg")[0]);
var today = new Date();
today.setHours(0, 0, 0, 0);
var currentDate = new DateHelper(window.location.pathname.replace(/\//g, "-").substring(1));;
var minDateBeforeFetch = currentDate.addDays(-5);
var maxDateBeforeFetch = currentDate.addDays(5);

console.log(activities);
//console.log("min: ", minDateBeforeFetch.dateString);
//console.log("max: ", maxDateBeforeFetch.dateString);

chart.ready(returnSevenDaysAroundDate(currentDate, activities), null, true);
chart.draw();