var chart = new Chart(document.getElementsByTagName("svg")[0]);
currentDate = new DateHelper(window.location.pathname.replace(/\//g, "-").substring(1));
chart.ready(returnSevenDaysAroundDate(currentDate, activities), null, true);
chart.draw();