var Day = (function () {
    function Day(date, activities, width, height, svg) {
        this.date = date;
        this.activities = activities || [];
        this.width = width;
        this.height = height;
        this.svg = svg;
    }

    Day.prototype.draw = function (x, activity) {
        var y = this.height;
        var height = 0;
        var activities = this.activities
        if (this.activities.length) {
            if (activity) {
                activities = activities.filter(function (current) { return current["Activity"] == activity; }, this);
                activities.forEach(function (activity) { height += calcDiffInMinutes(activity["From"], activity["To"]) * 0.25; }, this);
                y -= height;

                var color = activities[0]["Color"];

                this.svg.appendChild(svgElemFactory(activity, x, y, this.width, height, color));

                var ticks = budgets[activity];
                if (ticks) {
                    var budgetLineHeight = this.height - ticksToPixels(ticks);
                    this.svg.appendChild(budgetLine(x - 5, x + 5 + this.width, budgetLineHeight, color));
                }
                
            } else {
                this.gElem = document.createElementNS("http://www.w3.org/2000/svg", "g");
                activities.forEach(function (activity) {
                    height = calcDiffInMinutes(activity["From"], activity["To"]) * 0.25;
                    y -= height;
                    this.gElem.appendChild(svgElemFactory(activity["Activity"], x, y, this.width, height, activity["Color"]));
                }, this);
                this.svg.appendChild(this.gElem);
            }
        }
    };
    
    function budgetLine(x, width, height, color) {
        var elem = document.createElementNS("http://www.w3.org/2000/svg", "line");
        elem.setAttribute("x1", x);
        elem.setAttribute("x2", width);
        elem.setAttribute("y1", height);
        elem.setAttribute("y2", height);
        elem.setAttribute("stroke", "black");
        elem.setAttribute("stroke-width", 3);
        return elem;
    }

    function ticksToPixels(ticks) {
        return (((ticks/10000)/1000)/60) * 0.25
    }

    function calcDiffInMinutes(from, to) {
        return calcMinutesRegex(to) - calcMinutesRegex(from);
    }

    function calcMinutesRegex(time) {
        var match = /(\d{1,2}):(\d{1,2}):(\d{1,2})/.exec(time);
        return (Number(match[1]) * 60) + Number(match[2]) + (Number(match[3]) > 30 ? 1 : 0);
    }

    function svgElemFactory(activityId, x, y, width, height, fill) {
        
        var svgElem = document.createElementNS("http://www.w3.org/2000/svg", "rect");
        svgElem.setAttributeNS(null, "data-activity-id", activityId);
        svgElem.setAttributeNS(null, "x", x);
        svgElem.setAttributeNS(null, "y", y);
        svgElem.setAttributeNS(null, "width", width);
        svgElem.setAttributeNS(null, "height", height);
        svgElem.setAttributeNS(null, "fill", fill);
        return svgElem;
    }

    return Day;
})();