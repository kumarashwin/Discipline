
document.getElementById("arrow-left")
    .addEventListener("click", getArrowEventHandler(-1));

document.getElementById("arrow-right")
     .addEventListener("click", getArrowEventHandler(1));

function getArrowEventHandler(direction) {
    return function (event) {
        event.stopPropagation();
        currentDate = new Date(currentDate);
        currentDate = currentDate.setDate(currentDate.getDate() + direction);
        currentDate = convertToViableDateString(currentDate);
        chart.clear();
        chart.draw(returnSevenDaysAroundDate(currentDate, activities));
    };
}