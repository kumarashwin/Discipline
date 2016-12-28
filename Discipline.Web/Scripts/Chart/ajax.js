// This is a really rudimentary request function; should be changed
function requestMoreActivities(url, direction) {
    if (!direction)
        direction = "right";
    
    var req = new XMLHttpRequest();
    req.open("GET", url, true);
    req.setRequestHeader("accept", "application/json");
    req.addEventListener("load", function () {
        if (req.status < 400) {
            var response = JSON.parse(req.responseText);
            switch (direction) {
                case "right":
                    Object.assign(activities, response.activities);
                    break;
                case "left":
                    Object.assign(response.activities, activities);
                    activities = response.activities;
                    break;
            }
        }
    });
    req.send(null);
}