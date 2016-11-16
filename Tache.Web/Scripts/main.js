var chart = new Chart(document.getElementsByTagName("svg")[0]);
currentDate = window.location.pathname.replace(/\//g, "-").substring(1);
chart.ready(returnSevenDaysAroundDate(currentDate, activities));
chart.draw();