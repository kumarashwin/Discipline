var arrowLeft = document.getElementById("arrow-left");
arrowLeft.addEventListener("click", function (event) {
    event.stopPropagation();
    chart.move(0);
})

var arrowRight = document.getElementById("arrow-right");
arrowRight.addEventListener("click", function (event) {
    event.stopPropagation();
    chart.move(1);
})