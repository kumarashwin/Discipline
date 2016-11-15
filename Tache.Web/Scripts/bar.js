var Bar = (function () {
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

    return Bar;
})();
