function resetPage() {
    setupActivitiesCRUD();
    main();
}

function setupActivitiesCRUD() {
    var activitiesCRUD = document.getElementById("activities-CRUD");
    var activitiesUL = document.getElementsByClassName("activities-list")[0].querySelector("ul");
    var newButton = document.getElementById("new-activity");
    var deleteButton = activitiesCRUD.querySelector("form.delete");

    // Sets the userDateTime input value when one of the activities <li> is clicked
    activitiesUL.addEventListener("submit", function (event) {
        event.target.querySelector('input[name=userDateTime]').value = new Date().toISOString();
    });

    // Only works when right-clicking the list of activities, or left-clicking the 'new' button
    activitiesUL.addEventListener("contextmenu", activityCRUDEventHandler);
    newButton.addEventListener("click", activityCRUDEventHandler);

    function activityCRUDEventHandler(event) {
        if (event.target.nodeName == "BUTTON") {
            deleteButton.style.display = "none";
            var inputs = activitiesCRUD.querySelectorAll("input");
            inputs.forEach(function (input) { input.value = ""; });

            if (event.target.type == "submit") {
                inputs.forEach(function (input) {
                    input.value = event.target.getAttribute("data-activity-" + input.id.toLowerCase());
                });
                deleteButton.style.display = "block";
            }
            event.preventDefault();
            activitiesCRUD.style.display = "block";
        }
    }
}

// Initial call;
setupActivitiesCRUD();