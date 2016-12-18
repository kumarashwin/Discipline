var Day = (function () {
    function Day(date, activities, width, height, svg) {
        this.date = date;
        this.activities = activities || [];
        this.width = width;
        this.height = height;
        this.svg = svg;
    }

    Day.prototype.draw = function (x, activity) {
        if (this.activities.length) {
            if (activity) {
                this.drawDayInBudgetFormat(x, activity);
            } else
                this.drawDayInCalendarFormat(x);
        }
    };

    Day.prototype.drawDayInBudgetFormat = function(x, activity){
        var height = 0;
        var y = this.height;

        var activities = this.activities.filter(function (current) {
            return current["Activity"] == activity; 
        }, this);

        activities.forEach(function (activity) {
            var diffInMinutes = calcDiffInMinutes(activity["From"], activity["To"]);
            activity.Minutes = diffInMinutes % 60;
            activity.Hours = (diffInMinutes - activity.Minutes)/60;
            height += diffInMinutes * 0.25; // Minutes to Pixels
        }, this);
        y -= height;
        
        // Being that this is about a single activity which will, across the chart,
        // have the same color and budget line, we can set it here as the first activity
        // in the day activities array for each day; only the spatial values change
        // Note, however, that the rect should not display the 'From' and 'To' fields
        var dayActivity = activities[0];
        dayActivity.To = dayActivity.From = null;
        this.svg.appendChild(makeSvgRect(x, y, this.width, height, dayActivity));

        // The following is to add the budget line, if one exists for the selected activity;
        // default color is black
        var ticks = budgets[activity];
        if (ticks) {
            this.svg.appendChild(makeSvgLine(
                x - 5,                                  // x
                x + 5 + this.width,                     // width
                this.height - ticksToPixels(ticks)));}  // height 
    };

    Day.prototype.drawDayInCalendarFormat = function(x){
        var height = 0;
        var y = this.height;
        var gElem = document.createElementNS("http://www.w3.org/2000/svg", "g");

        this.activities.forEach(function (activity) {
            var diffInMinutes = calcDiffInMinutes(activity["From"], activity["To"]);
            activity.Minutes = diffInMinutes % 60;
            activity.Hours = (diffInMinutes - activity.Minutes)/60;
            height = diffInMinutes * 0.25; // Minutes to Pixels
            y -= height;
            gElem.appendChild(makeSvgRect(x, y, this.width, height, activity));
        }, this);

        this.svg.appendChild(gElem);
    };

    function ticksToPixels(ticks) {
        return (((ticks/10000)/1000)/60) * 0.25; }

    function calcDiffInMinutes(from, to) {
        return calcMinutesRegex(to) - calcMinutesRegex(from); }

    function calcMinutesRegex(time) {
        var match = /(\d{1,2}):(\d{1,2}):(\d{1,2})/.exec(time);
        return (Number(match[1]) * 60) + Number(match[2]) + (Number(match[3]) > 30 ? 1 : 0);
    }

    function makeSvgRect(x, y, width, height, activity) {
        var svgElem = document.createElementNS("http://www.w3.org/2000/svg", "rect");
        svgElem.setAttributeNS(null, "x", x);
        svgElem.setAttributeNS(null, "y", y);
        svgElem.setAttributeNS(null, "width", width);
        svgElem.setAttributeNS(null, "height", height);
        svgElem.setAttributeNS(null, "fill", activity.Color);
        svgElem.setAttributeNS(null, "data-activity-id", activity.Activity);
        svgElem.setAttributeNS(null, "data-activity-name", activity.Name);
        svgElem.setAttributeNS(null, "data-activity-description", activity.Description);
        svgElem.setAttributeNS(null, "data-activity-hours", activity.Hours);
        svgElem.setAttributeNS(null, "data-activity-minutes", activity.Minutes);
        if (activity.From && activity.To) {
            svgElem.setAttributeNS(null, "data-activity-from", activity.From);
            svgElem.setAttributeNS(null, "data-activity-to", activity.To);
        }
        return svgElem;
    }

    function makeSvgLine(x, width, height) {
        var elem = document.createElementNS("http://www.w3.org/2000/svg", "line");
        elem.setAttribute("x1", x);
        elem.setAttribute("x2", width);
        elem.setAttribute("y1", height);
        elem.setAttribute("y2", height);
        elem.setAttribute("stroke", "black");
        elem.setAttribute("stroke-width", 3);
        return elem;
    }

    return Day;
})();