var Chart = ( function () {
    function Chart(svg) {
        this.svg = svg;
        this.svg.addEventListener("click", rectClickHandler);

        this.barWidth = 70;
        this.height = svg.height.baseVal.value;
        this.padding = 15;
        this.labels = document.getElementById("dates");

        this.mode = "calendar";
    }

    Chart.prototype.clear = function () {
        while (this.svg.firstChild)
            this.svg.removeChild(this.svg.firstChild);
    };

    Chart.prototype.draw = function (days, activity) {
        if (!days && !this.days) {
            return;
        } else if (days) {
            this.days = Object.keys(days).map(function (day, index, array) { return new Day(day, days[day], this.barWidth, this.height, this.svg); }, this);
        }

        this.clearLabels();
        var x = this.padding;
        this.days.forEach(function (day, index) {
            this.drawLabel(day.date);
            day.draw(x, activity);
            x += this.padding + this.barWidth;
        }, this);
    };

    Chart.prototype.drawLabel = function (label) {
        var li = document.createElement("li");
        var span = document.createElement("span");

        span.appendChild(document.createTextNode(label));
        li.appendChild(span);

        this.labels.appendChild(li);
    };

    Chart.prototype.clearLabels = function () {
        while (this.labels.firstChild) {
            this.labels.removeChild(this.labels.firstChild);
        }
    };

    // right: 1
    // left: 0
    Chart.prototype.move = function (right) {
        var x;

        if (right === 1)
            x = 5;
        else if (right === 0)
            x = -5
        else
            return;

        var interval = setInterval((function () {
            for (var i = 0; i < this.svg.childNodes.length; i++) {
                this.svg.childNodes[i].setAttribute("transform", "translate(" + x + ",0)");
            }
            if (right === 1 && x < 85){
                x += 5;
            } else if (right === 0 && x > -85){
                x -= 5;
            } else {
                clearInterval(interval);
            }
        }).bind(this), 10);
    };

    Chart.prototype.moveRight = function () {
        var x = 5;
        var interval = setInterval((function () {
            for (var i = 0; i < this.svg.childNodes.length; i++) {
                this.svg.childNodes[i].setAttribute("transform", "translate(" + x + ",0)");
            }
            if (x < 85)
                x += 5;
            else
                clearInterval(interval);
        }).bind(this), 10);
    };

    Chart.prototype.moveLeft = function () {
        var x = 5;
        var interval = setInterval((function () {
            for (var i = 0; i < this.svg.childNodes.length; i++) {
                this.svg.childNodes[i].setAttribute("transform", "translate(" + x + ",0)");
            }
            if (x > -85)
                x -= 5;
            else
                clearInterval(interval);
        }).bind(this), 10);
    };

    return Chart;
})();