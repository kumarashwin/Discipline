
// This is a really rudimentary request function; should be changed
function requestMoreActivities(url, direction) {
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
            console.log(activities);
        }
            
    });
    req.send(null);
}

function removeListItem(id) {
    return function(data, status, xhr) {
        if (data != false) {
            var selector = "li[activity-id=" + id + "]";
            var li = document.getElementById("activitiesDropDown").querySelector(selector);
            li.parentElement.removeChild(li);
        }
    }
}