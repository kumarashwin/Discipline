function rectClickHandler(event) {
    var timeInterval = 15;
    if (!chart.activityId) {
        if (event.target.nodeName == "rect") {
            var opacity = 1;
            var interval = setInterval(function () {
                chart.svg.parentElement.style.opacity = opacity -= 0.1;
                if (opacity <= 0.5) {
                    chart.clear();
                    chart.draw(event.target.getAttribute("data-activity-id"));
                    var interval2 = setInterval(function () {
                        chart.svg.parentElement.style.opacity = opacity += 0.1;
                        if (opacity >= 1)
                            clearInterval(interval2);
                    }, timeInterval);
                    clearInterval(interval);
                }
            }, timeInterval);
        }
    } else {
        var opacity = 1;
        var interval = setInterval(function () {
            chart.svg.parentElement.style.opacity = opacity -= 0.1;
            if (opacity <= 0) {
                chart.clear();
                chart.draw(null);
                chart.activityId = null;
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
