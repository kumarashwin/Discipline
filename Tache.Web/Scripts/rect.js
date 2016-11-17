function rectClickHandler(event) {
    event.stopPropagation();

    // If in calendar mode, clicking on the background of the svg does nothing;
    if (chart.mode == "calendar" && event.target.nodeName != "rect")
        return;

    if (chart.mode == "calendar") {
        chart.ready(null, event.target.getAttribute("data-activity-id"), false);
        chart.mode = "budget";
    } else if (chart.mode == "budget") {
        chart.ready(null, null, false);
        chart.mode = "calendar";
    }

    chart.svg.parentElement.addEventListener("transitionend", onTransparentFinish);
    chart.svg.parentElement.className = "makeTransparent";
}