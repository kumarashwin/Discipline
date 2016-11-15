var Day = (function () {
    function Day(activities) {
        this.activities = activities;
        //this.date = ticksToDate(activities[0]["To"]);
        this.bars = [];

        this.activities.forEach(function (activity) {
            this.bars.push(new Bar(activity));
        }, this);
    }
    return Day;
})();