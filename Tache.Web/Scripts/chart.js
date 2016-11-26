var Chart = ( function () {
    function Chart(svg) {
        this.svg = svg;
        this.setInitialValues();
    }

    Chart.prototype.setInitialValues = function(){
        //Edit these if needed:
        this.padding = 15;
        this.barWidth = 70;

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

            //var x = this.padding;
            this.days.forEach(function (day, index) {
                if (this.redrawLabels) this.drawLabel(day.date);
                var x = this.padding + ((this.barWidth + this.padding) * index); // TRY THIS OUT!!
                day.draw(x, this.activity);
                //x += this.padding + this.barWidth;
            }, this);
        } 
    };

    Chart.prototype.drawLabel = function (label) {

        var span = document.createElement("span");
        span.appendChild(document.createTextNode(label));

        var li = document.createElement("li");
        li.appendChild(span);

        this.labels.appendChild(li);
    };

    Chart.prototype.clearLabels = function () {
        while (this.labels.firstChild) {
            this.labels.removeChild(this.labels.firstChild);
        }
    };
    
    return Chart;
})();