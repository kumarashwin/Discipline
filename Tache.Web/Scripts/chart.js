function TimeSpan(hours, minutes) {
    this.minutes = minutes;
    this.hours = hours;
}

function Chart(canvas, data) {
    this.context = canvas.getContext('2d');
    this.data = data;
    this.bars = [];
};

Chart.prototype.initialize = function () {
    var carryFoward;
    for(var durations in this.data){
        if (Object.hasOwnProperty(activityAndDurations) && durations == "Durations") {
            var bar = new Bar(durations["From"], durations["To"])
            this.bars.push(bar);
            carryFoward = bar.carryForward;
        }
    }
};


// from & to: ticks
function Bar(from, to) {
    this.date = ticksToDate(from);
    this.carryForward = this.calculateCarryForward(from, to);
    this.timeSpent = findTimeSpan(carryFoward || from, to);
};

Bar.prototype.calculateCarryForward = function (fromTicks, toTicks) {
    from = ticksToDate(fromTicks);
    to = ticksToDate(toTicks);

    if (from < to && from.getDay() != to.getDay()) {
        var midnight = new Date(from.toString().replace(/\d{2}:\d{2}:\d{2}/, "23:59:59"));
        return (toTicks - (midnight.getTime() + 1000));
    }
    return null;
};

Bar.prototype.ticksToDate = function(ticks) {
    return new Date(parseInt(/\d+/.exec(ticks)));
}

Bar.prototype.findTimeSpan = function (fromTicks, toTicks) {
    var differenceInTicks = toTicks - fromTicks;
    var totalInMinutes = differenceInTicks / 60000;
    var hours = Math.floor(totalInMinutes / 60);
    var minutes = minutes % 60;

    return new TimeSpan(hours, minutes);
};

