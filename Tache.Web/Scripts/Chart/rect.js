function rectClickHandler(event) {
    event.stopPropagation();

    switch(chart.mode) {
        case "calendar" :
            if(event.target.nodeName == "rect"){
                chart.ready(null, event.target.getAttribute("data-activity-id"), false);
                chart.mode = "budget";
                break;
            } else // If in calendar mode, only the rect elements should respond; 
                return;
        case "budget" :
            chart.ready(null, null, false);
            chart.mode = "calendar";
            break;
        default:
            break;
    }

    chart.svg.parentElement.addEventListener("transitionend", onTransparentFinish);
    chart.svg.parentElement.className = "makeTransparent";
}