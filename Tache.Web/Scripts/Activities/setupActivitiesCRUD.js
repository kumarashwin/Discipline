function setupActivitiesCRUD() {
    //var activitiesCRUD = document.getElementById("activities-CRUD");
    var activitiesButtonGroup = document.getElementById('activities-button-group');
    //var newButton = document.getElementById("new-activity");
    //var deleteButton = activitiesCRUD.querySelector("form.delete");

    // Attachs the color picker to the colorpicker div 
    //var colorField = document.getElementById('Color');
    //var colorPicker = document.getElementById('colorpicker');
    //$(colorPicker).farbtastic(colorField);
    //colorField.addEventListener('focus', function (event) { colorPicker.style.display = 'block'; });
    //colorField.addEventListener('blur', function (event) { colorPicker.style.display = 'none'; });

    // Sets the clientRequestTime input value when one of the activities is clicked on
    activitiesButtonGroup.addEventListener("submit", function (event) {
        event.target.querySelector('input[name=clientRequestTime]').value = new Date().toISOString();
    });

    // Only works when right-clicking the list of activities, or left-clicking the 'new' button
    //activitiesButtonGroup.addEventListener("contextmenu", activityCRUDEventHandler);
    //newButton.addEventListener("click", activityCRUDEventHandler);

    //function activityCRUDEventHandler(event) {
    //    if (event.target.nodeName == "BUTTON") {
    //        event.preventDefault();
    //        var inputs = activitiesCRUD.querySelectorAll("input");

    //        // Clearning all input fields;
    //        inputs.forEach(function (input) { input.value = ""; });

    //        // By default i.e. if it's a new activity, delete will be hidden;
    //        deleteButton.style.display = "none";

    //        // Populating the Activities Crud window with data from the activity list item;
    //        // in case it's an update of a currently existing activity
    //        if (event.target.type == "submit") {
    //            inputs.forEach(function (input) {
    //                input.value = event.target.getAttribute("data-activity-" + input.id.toLowerCase());
    //            });

    //            // The delete button is only important in the case of updates
    //            deleteButton.style.display = "block";
    //        }

    //        // Set up the color picker:
    //        // If we are in the process of creating a new activity,
    //        // we would populate the blank color field with white, by default 
    //        var colorField = document.getElementById('Color');
    //        if (!colorField.value) colorField.value = '#ffffff';
    //        $.farbtastic('#colorpicker').setColor(colorField.value);

    //        activitiesCRUD.style.display = "block";
    //    }
    //}
}

// Initial call;
//setupActivitiesCRUD();