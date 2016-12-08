function setupActivitiesCRUD() {
    var activitiesCRUD = document.getElementById("activities-CRUD");
    var backgroundDiv = document.getElementById("background-cover");

    var activitiesList = document.getElementsByClassName("activities-list")[0].querySelector("ul");
    var newButton = document.getElementById("new-activity");

    activitiesList.addEventListener("contextmenu", activityCRUDEventHandler);
    newButton.addEventListener("click", activityCRUDEventHandler);

    function activityCRUDEventHandler(event) {
        if (event.target.nodeName == "BUTTON") {
            var inputs = activitiesCRUD.querySelectorAll("input");
            inputs.forEach(function (input) { input.value = ""; });

            if (event.target.type == "submit") {
                inputs.forEach(function (input) {
                    input.value = event.target.getAttribute("data-activity-" + input.id.toLowerCase());
                });
            }
            event.preventDefault();
            activitiesCRUD.style.display = "block";
        }
    }
}

// Initial call;
setupActivitiesCRUD();