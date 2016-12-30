var Chart = ( function () {
    function Chart(svg) {
        this.svg = svg;
        this.setInitialValues();
    }

    Chart.prototype.setInitialValues = function(){
        //Edit these if needed:
        this.padding = 15;
        this.barWidth = 55;

        this.mode = "calendar";
        this.height = this.svg.height.baseVal.value;
        this.labels = document.getElementById("dates");
        this.svg.addEventListener("click", rectClickHandler);
    }

    Chart.prototype.ready = function (days, activity, redrawLabels) {
        this.activity = activity;
        this.redrawLabels = redrawLabels;

        if (days) this.days = Object.keys(days).map(function (day, index, array) {
            return new Day(day, days[day], this.barWidth, this.height, this.svg);
        }, this);
    };

    Chart.prototype.clear = function () {
        while (this.svg.firstChild)
            this.svg.removeChild(this.svg.firstChild);
    };

    Chart.prototype.draw = function () {
        if (this.days){
            if (this.redrawLabels) this.clearLabels();

            this.days.forEach(function (day, index) {
                if (this.redrawLabels) this.drawLabel(new DateHelper(day.date));
                var x = this.padding + ((this.barWidth + this.padding) * index);
                day.draw(x, this.activity);
            }, this);
        } 
    };

    Chart.prototype.drawLabel = function (helper) {

        var dayOfTheWeek = document.createElement('p');
        dayOfTheWeek.appendChild(document.createTextNode(helper.getDayOfTheWeek()));

        var date = document.createElement('p');
        date.appendChild(document.createTextNode(helper.dateString));

        var li = document.createElement('li');
        li.appendChild(dayOfTheWeek);
        li.appendChild(date);

        this.labels.appendChild(li);
    };

    Chart.prototype.clearLabels = function () {
        while (this.labels.firstChild) {
            this.labels.removeChild(this.labels.firstChild);
        }
    };
    
    return Chart;
})();