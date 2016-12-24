function setupActivitiesCRUD() {
    //var activitiesCRUD = document.getElementById("activities-CRUD");
    var activitiesForm = document.getElementById('activity-form');
    var activitiesButtonGroup = document.getElementById('activities-button-group');
    //var deleteButton = activitiesCRUD.querySelector("form.delete");

    // Sets the clientRequestTime input value when one of the activities is clicked on
    activitiesButtonGroup.addEventListener("submit", function (event) {
        event.target.querySelector('input[name=clientRequestTime]').value = new Date().toISOString();
    });

    // Add the event handler to the edit button beside each activity
    activitiesButtonGroup.addEventListener("click", activityEditEventHandler);

    // Initializes the color picker
    $('#Color').colorpicker({color:'transparent', format:'hex'});

    function activityEditEventHandler(event) {
        if (event.target.type == 'button') {
            var allInputs = activitiesForm.querySelectorAll('input');

            // Clearning all input fields;
            allInputs.forEach(function (input) { input.value = ''; });

            // Populating the value fields with the data-activity attribute in the Edit button
            allInputs.forEach(function (input) { 
                var attribute = 'data-activity-' + input.id.toLowerCase();
                input.value = event.target.getAttribute(attribute);
            });
            
            // Sets the value of the color picker
            $('#Color').colorpicker('setValue', event.target.getAttribute("data-activity-color")); 

            // To Do: add code to show the form?
        }
    }
}