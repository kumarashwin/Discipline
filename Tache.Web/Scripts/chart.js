var ActivityColor = {
    2007 : "pink",
    2006 : "yellow",
    2005 : "purple",
    2004 : "blue"
};

function TimeSpan(hours, minutes) {
    this.minutes = minutes;
    this.hours = hours;
}

function Chart(canvas, activities) {
    this.context = canvas.getContext("2d");
    this.day = new Day(activities);
    this.draw();
};

Chart.prototype.draw = function () {
    var width = 50;
    var height = 0;
    var x = 10;
    var y = this.context.canvas.height;

    this.day.bars.forEach(function (bar) {
        height = bar.span * 0.5;
        y -= height;
        this.context.fillStyle = bar.color;
        this.context.fillRect(x, y, width, height);
    }, this);
}


function Day(activities) {
    this.activities = activities;
    this.date = ticksToDate(activities[0]["To"]);
    this.bars = [];

    this.activities.forEach(function(activity) {
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

function parseTicks(ticks){
    return parseInt(/\d+/.exec(ticks));
}

// === MAIN ===
var activities = [
        {
            "Activity": 2007,
            "Duration": 3007,
            "Name": "reading",
            "Description": "*...page flip*",
            "From": "/Date(1477540800000)/",
            "To": "/Date(1477544400000)/"
        },
        {
            "Activity": 2004,
            "Duration": 3008,
            "Name": "sleeping",
            "Description": "I love sleeping!",
            "From": "/Date(1477544401000)/",
            "To": "/Date(1477573200000)/"
        },
        {
            "Activity": 2006,
            "Duration": 3009,
            "Name": "coding",
            "Description": "*klik klak klik klik*",
            "From": "/Date(1477573201000)/",
            "To": "/Date(1477584000000)/"
        },
        {
            "Activity": 2005,
            "Duration": 3010,
            "Name": "eating",
            "Description": "*chomp chomp mnggff!*",
            "From": "/Date(1477584001000)/",
            "To": "/Date(1477587600000)/"
        },
        {
            "Activity": 2006,
            "Duration": 3011,
            "Name": "coding",
            "Description": "*klik klak klik klik*",
            "From": "/Date(1477587601000)/",
            "To": "/Date(1477605600000)/"
        },
        {
            "Activity": 2005,
            "Duration": 3012,
            "Name": "eating",
            "Description": "*chomp chomp mnggff!*",
            "From": "/Date(1477605601000)/",
            "To": "/Date(1477609200000)/"
        },
        {
            "Activity": 2007,
            "Duration": 3013,
            "Name": "reading",
            "Description": "*...page flip*",
            "From": "/Date(1477609201000)/",
            "To": "/Date(1477627199000)/"
        }];

var chart = new Chart(document.getElementById("canvas"), activities);

//function findTimeSpan(fromTicks, toTicks) {
//    var differenceInTicks = toTicks - fromTicks;
//    var totalInMinutes = differenceInTicks / 60000;
//    var hours = Math.floor(totalInMinutes / 60);
//    var minutes = minutes % 60;

//    return new TimeSpan(hours, minutes);
//};

//Chart.prototype.initialize = function () {
//    var carryFoward;
//    for(var durations in this.data){
//        if (Object.hasOwnProperty(activityAndDurations) && durations == "Durations") {
//            var bar = new Bar(durations["From"], durations["To"])
//            this.bars.push(bar);
//            carryFoward = bar.carryForward;
//        }
//    }
//};


// from & to: ticks
//function Bar(from, to) {
//    this.date = ticksToDate(from);
//    this.carryForward = this.calculateCarryForward(from, to);
//    this.timeSpent = findTimeSpan(carryFoward || from, to);
//};

//Bar.prototype.calculateCarryForward = function (fromTicks, toTicks) {
//    from = ticksToDate(fromTicks);
//    to = ticksToDate(toTicks);

//    if (from < to && from.getDay() != to.getDay()) {
//        var midnight = new Date(from.toString().replace(/\d{2}:\d{2}:\d{2}/, "23:59:59"));
//        return (toTicks - (midnight.getTime() + 1000));
//    }
//    return null;
//};

//Bar.prototype.ticksToDate = function(ticks) {
//    return new Date(parseInt(/\d+/.exec(ticks)));
//}

//Bar.prototype.findTimeSpan = function (fromTicks, toTicks) {
//    var differenceInTicks = toTicks - fromTicks;
//    var totalInMinutes = differenceInTicks / 60000;
//    var hours = Math.floor(totalInMinutes / 60);
//    var minutes = minutes % 60;

//    return new TimeSpan(hours, minutes);
//};

