function attachEventsToActivities(event) {
    var activitiesButtonGroup = document.getElementById('activities-button-group');

    // Sets the clientRequestTime input value when one of the activities is clicked on
    activitiesButtonGroup.addEventListener("submit", function (event) {
        if(event.target.name == 'next-activity')
            event.target.querySelector('input[name=clientRequestTime]').value = new Date().toISOString();
    });

    // Add the event handler to the edit button beside each activity
    activitiesButtonGroup.addEventListener("click", activityEditEventHandler);

    function activityEditEventHandler(event) {
        if (event.target.name == 'edit') {
            var activityForm = document.getElementById('activity-form');
            var allInputs = activityForm.querySelectorAll('input');

            // Clearning all input fields except the verification token
            allInputs.forEach(function (input) {
                if (input.name != '__RequestVerificationToken')
                    input.value = '';
            });

            // Populating the value fields with the data-activity attribute in the Edit button
            allInputs.forEach(function (input) { 
                if (input.name != '__RequestVerificationToken') {
                    var attribute = 'data-activity-' + input.id.toLowerCase();
                    input.value = event.target.getAttribute(attribute);
                }
            });
            
            // Sets the value of the color picker
            $('#Color').colorpicker('setValue', event.target.getAttribute("data-activity-color")); 

            document.querySelectorAll('div.page').forEach(function (page) { page.style.display = 'none' });
            activityForm.style.display = '';
        }
    }
}