var arrowLeft = document.getElementById("arrow-left");

arrowLeft.addEventListener("click", function (event) {
    backgroundReadFile(window.location.protocol + "//" + window.location.host + "/" + "2016/11/9", console.log);
});

function backgroundReadFile(url, callback) {
    var req = new XMLHttpRequest();
    req.open("GET", url, true);
    req.addEventListener("load", function () {
        if (req.status < 400)
            callback(req.responseText);
    });
    req.send(null);
}