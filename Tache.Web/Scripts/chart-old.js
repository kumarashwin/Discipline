var Chart = (function () {
    function Chart(svg, days) {
        this.activityId; // Only for redrawing chart

        this.svg = svg;
        this.svg.addEventListener("click", rectClickHandler);

        
        var test = Object.keys(days).map(function (day, index, array) {
            return new Day(day, days[day]);
        }, this);

        this.days = days;
        this.datesList = document.getElementById("dates");

        this.draw();
    }



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

            if (bar.length) {
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
            }

            x += leftPadding + width;
        }, this);
    };

    Chart.prototype.clear = function () {
        while (this.svg.firstChild)
            this.svg.removeChild(this.svg.firstChild);
    };

    // e.g.: From - 8:5:00; To - 10:20:00
    function calcDiffInMinutes(from, to) {
        return calcMinutesRegex(to) - calcMinutesRegex(from);
    }

    function calcMinutesRegex(time) {
        var match = /(\d{1,2}):(\d{1,2}):(\d{1,2})/.exec(time);
        return (Number(match[1]) * 60) + Number(match[2]) + (Number(match[3]) > 30 ? 1 : 0);
    }

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

    return Chart;
})();