// This is a really rudimentary request function; should be changed
function requestMoreActivities(url, direction) {
    if (!direction)
        direction = "right";
    
    var req = new XMLHttpRequest();
    req.open("GET", url, true);
    req.setRequestHeader("accept", "application/json");
    req.addEventListener("load", function () {
        if (req.status < 400) {
            switch (direction) {
                case "right":
                    Object.assign(activities, JSON.parse(req.responseText));
                    break;
                case "left":
                    newActivities = JSON.parse(req.responseText);
                    Object.assign(newActivities, activities);
                    activities = newActivities;
                    break;
            }
        }
    });
    req.send(null);
}