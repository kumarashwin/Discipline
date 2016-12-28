function editClickEventHandler(event) {
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