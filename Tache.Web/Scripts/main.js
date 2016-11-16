var chart = new Chart(document.getElementsByTagName("svg")[0]);
currentDate = window.location.pathname.replace(/\//g, "-").substring(1);
chart.draw(returnSevenDaysAroundDate(currentDate, activities));