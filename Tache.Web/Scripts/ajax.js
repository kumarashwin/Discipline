function requestMoreActivities(url, direction) {
    var req = new XMLHttpRequest();
    req.open("GET", url, true);
    req.setRequestHeader("accept", "application/json");
    req.addEventListener("load", function () {
        if (req.status < 400) {
            switch (direction) {
                case "right":
                    Object.assign(activities, JSON.parse(req.responseText));
                    console.log("max: ", maxDateBeforeFetch.dateString);
                    break;
                case "left":
                    newActivities = JSON.parse(req.responseText);
                    Object.assign(newActivities, activities);
                    activities = newActivities;
                    console.log("min: ", minDateBeforeFetch.dateString);
                    break;
            }
            console.log(activities);
        }
            
    });
    req.send(null);
}