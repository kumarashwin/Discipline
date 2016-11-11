function rectClickHandler(event) {
    var timeInterval = 15;
    if (!chart.activityId) {
        if (event.target.nodeName == "rect") {
            var opacity = 1;
            var interval = setInterval(function () {
                chart.svg.parentElement.style.opacity = opacity -= 0.1;
                if (opacity <= 0) {
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

function TimeSpan(hours, minutes) {
    this.minutes = minutes;
    this.hours = hours;
}

function Chart(svg, days) {
    this.activityId; // Only for redrawing chart

    this.svg = svg;
    svg.addEventListener("click", rectClickHandler);

    this.days = days;
    this.datesList = document.getElementById("dates");

    this.draw();
}

// e.g.: From - 8:5:00; To - 10:20:00
function calcDiffInMinutes(from, to) {
    from = calcMinutesRegex(from);
    to = calcMinutesRegex(to);

    return to - from;
}

function calcMinutesRegex(time) {
    var match = /(\d{1,2}):(\d{1,2}):(\d{1,2})/.exec(time);
    return (Number(match[1]) * 60) + Number(match[2]) + (Number(match[3]) > 30 ? 1 : 0);
}

Chart.prototype.clear = function () {
    while (this.svg.firstChild)
        this.svg.removeChild(this.svg.firstChild);
};

Chart.prototype.draw = function (activityId) {

    this.activityId = activityId;

    var width = 70;
    var leftPadding = 15;
    var x = leftPadding;

    Object.keys(this.days).forEach(function (day, index) {
        var currentListElement = this.datesList.children[index];
        if (typeof (activityId) === 'undefined')
            currentListElement.firstChild.appendChild(document.createTextNode(day));
        var className = currentListElement.className;

        var color;
        var height = 0;
        var y = this.svg.height.baseVal.value;
        var bar = this.days[day];

        for (var i in bar) {
            var activity = bar[i];

            if (activityId) {
                if (activity["Activity"] == activityId) {
                    if (!color) color = activity["Color"];
                    height += calcDiffInMinutes(activity["From"], activity["To"]) * 0.25;
                }
                continue;
            } else {
                color = activity["Color"];
                height = calcDiffInMinutes(activity["From"], activity["To"]) * 0.25;
                y -= height;
                this.svg.appendChild(svgElemFactory(className, activity["Activity"], x, y, width, height, color));
            }
        }
        if (activityId) {
            y -= height;
            this.svg.appendChild(svgElemFactory(className, activity["Activity"], x, y, width, height, color));
        }
        x += leftPadding + width;
    }, this);
};

function svgElemFactory(className, activityId, x, y, width, height, fill) {
    var svgElem = document.createElementNS("http://www.w3.org/2000/svg", "rect");
    svgElem.setAttributeNS(null, "class", className);
    svgElem.setAttributeNS(null, "data-activity-id", activityId);
    svgElem.setAttributeNS(null, "x", x);
    svgElem.setAttributeNS(null, "y", y);
    svgElem.setAttributeNS(null, "width", width);
    svgElem.setAttributeNS(null, "height", height);
    svgElem.setAttributeNS(null, "fill", fill);
    return svgElem;
}

function Day(activities) {
    this.activities = activities;
    this.date = ticksToDate(activities[0]["To"]);
    this.bars = [];

    this.activities.forEach(function (activity) {
        this.bars.push(new Bar(activity));
    }, this);
}

function Bar(activity) {
    this.color = ActivityColor[activity["Activity"]];

    this.start = parseTicks(activity["From"]);
    this.end = parseTicks(activity["To"]);

    // Minutes
    this.span = (this.end - this.start) / 60000;

    this.start = ticksToDate(this.start);
    this.end = ticksToDate(this.end);
}

function ticksToDate(ticks) {
    return new Date(parseTicks(ticks));
}

function parseTicks(ticks) {
    return parseInt(/\d+/.exec(ticks));
}

// === MAIN ===
var chart = new Chart(document.getElementsByTagName("svg")[0], activities);