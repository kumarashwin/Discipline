var ActivityColor = {
    2007: "#99CCCC",
    2006: "#666666",
    2005: "#6600FF",
    2004: "#6699CC",
    3004: "yellow"
};

function TimeSpan(hours, minutes) {
    this.minutes = minutes;
    this.hours = hours;
}

function Chart(svg, days) {
    this.svg = svg;
    this.days = days;
    this.draw();
};

// e.g.: From - 8:5:00; To - 10:20:00
function calcDiffInMinutes(from, to) {
    from = calcMinutesRegex(from);
    to = calcMinutesRegex(to);

    return to - from;
}

function calcMinutesRegex(time) {
    var match = /(\d{1,2}):(\d{1,2}):(\d{1,2})/.exec(time);
    return ((Number(match[1]) * 60) + Number(match[2]) + (Number(match[3]) > 30 ? 1 : 0));
}

Chart.prototype.draw = function () {
    var width = 70;
    var leftPadding = 15;
    var x = leftPadding;

    Object.keys(this.days).forEach(function (key, index) {
        
        var height = 0;
        var y = this.svg.height.baseVal.value;
        
        var xmlns = "http://www.w3.org/2000/svg"
        var bar = this.days[key];

        bar.forEach(function (activity) {
            height = calcDiffInMinutes(activity["From"], activity["To"]) * 0.25;
            y -= height;
            var svgElem = document.createElementNS(xmlns, "rect");
            svgElem.setAttributeNS(null, "x", x);
            svgElem.setAttributeNS(null, "y", y);
            svgElem.setAttributeNS(null, "width", width);
            svgElem.setAttributeNS(null, "height", height);
            svgElem.setAttributeNS(null, "fill", ActivityColor[activity["Activity"]] );
            
            this.svg.appendChild(svgElem);
        }, this);

        x += leftPadding + width;
    }, this);
};

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

//var activities = {
//    "2016-10-23": [],
//    "2016-10-24": [],
//    "2016-10-25": [],
//    "2016-10-26": [
//        {
//            "Activity": 2007,
//            "Duration": 3006,
//            "Name": "reading",
//            "Description": "*...page flip*",
//            "From": "21:0:0",
//            "To": "23:59:59"
//        }
//    ],
//    "2016-10-27": [
//        {
//            "Activity": 2007,
//            "Duration": 3007,
//            "Name": "reading",
//            "Description": "*...page flip*",
//            "From": "0:0:0",
//            "To": "1:0:0"
//        },
//        {
//            "Activity": 2004,
//            "Duration": 3008,
//            "Name": "sleeping",
//            "Description": "I love sleeping!",
//            "From": "1:0:1",
//            "To": "9:0:0"
//        },
//        {
//            "Activity": 2005,
//            "Duration": 3010,
//            "Name": "eating",
//            "Description": "*chomp chomp mnggff!*",
//            "From": "12:0:1",
//            "To": "13:0:0"
//        },
//        {
//            "Activity": 2006,
//            "Duration": 3011,
//            "Name": "coding",
//            "Description": "*klik klak klik klik*",
//            "From": "13:0:1",
//            "To": "18:0:0"
//        },
//        {
//            "Activity": 2005,
//            "Duration": 3012,
//            "Name": "eating",
//            "Description": "*chomp chomp mnggff!*",
//            "From": "18:0:1",
//            "To": "19:0:0"
//        },
//        {
//            "Activity": 2007,
//            "Duration": 3013,
//            "Name": "reading",
//            "Description": "*...page flip*",
//            "From": "19:0:1",
//            "To": "23:59:59"
//        },
//        {
//            "Activity": 2006,
//            "Duration": 3009,
//            "Name": "coding",
//            "Description": "*klik klak klik klik*",
//            "From": "9:0:1",
//            "To": "12:0:0"
//        }
//    ],
//    "2016-10-28": [
//        {
//            "Activity": 2007,
//            "Duration": 3014,
//            "Name": "reading",
//            "Description": "*...page flip*",
//            "From": "0:0:0",
//            "To": "0:30:0"
//        },
//        {
//            "Activity": 2004,
//            "Duration": 3015,
//            "Name": "sleeping",
//            "Description": "I love sleeping!",
//            "From": "0:30:1",
//            "To": "8:30:0"
//        }
//    ],
//    "2016-10-29": []
//};

var chart = new Chart(document.getElementsByTagName("svg")[0], activities);