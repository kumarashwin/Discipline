function Chart(canvas, data) {
    this.context = canvas.getContext('2d');
    this.data = data;
    this.bars = [];
};

Chart.prototype.initialize = function () {
    for(var activityAndDurations in data){
        if (Object.hasOwnProperty(activityAndDurations) && activityAndDurations != "Activity") {
            
        }
    }
};

function Bar(to, from) {
    this.timeSpent = this.FindTimeSpan(from, to);
};

Bar.prototype.FindTimeSpan = function (from, to) {
    from = parseInt(/\d+/.exec(from));
    to = parseInt(/\d+/.exec(to));

    var remainingTime = this.IsItInTheNextDay(from, to);

    var differenceInMilliSeconds = to - from;
    var totalInMinutes = differenceInMilliSeconds / 60000;
    var hours = Math.floor(totalInMinutes / 60);
    var minutes = minutes % 60;

    return new TimeSpan(hours, minutes);
};

Bar.prototype.IsItInTheNextDay(from, to){
    from = new Date(from);
    to = new Date(to);

    //TO DO: Finish implementation
    if(from < to && from.getDay() != to.getDay()){
        var midnight = new Date(from.toString().replace(/\d{2}:\d{2}:\d{2}/, "23:59:59"));

    } else {

    }
};

function TimeSpan(hours, minutes) {
    this.minutes = minutes;
    this.hours = hours;
}