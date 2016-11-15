function rectClickHandler(event) {
    var timeInterval = 15;
    if (event.target.nodeName == "rect" && chart.mode == "calendar") {
        var opacity = 1;
        var interval = setInterval(function () {
            chart.svg.parentElement.style.opacity = opacity -= 0.1;
            if (opacity <= 0) {
                chart.clear();
                chart.mode = "budget";
                chart.draw(null, event.target.getAttribute("data-activity-id"));
                var interval2 = setInterval(function () {
                    chart.svg.parentElement.style.opacity = opacity += 0.1;
                    if (opacity >= 1)
                        clearInterval(interval2);
                }, timeInterval);
                clearInterval(interval);
            }
        }, timeInterval);
    } else if (chart.mode == "budget") {
        var opacity = 1;
        var interval = setInterval(function () {
            chart.svg.parentElement.style.opacity = opacity -= 0.1;
            if (opacity <= 0) {
                chart.clear();
                chart.draw();
                chart.mode = "calendar";
                var interval2 = setInterval(function () {
                    chart.svg.parentElement.style.opacity = opacity += 0.1;
                    if (opacity >= 1)
                        clearInterval(interval2);
                }, timeInterval);
                clearInterval(interval);
            }
        }, timeInterval);
    }
}
