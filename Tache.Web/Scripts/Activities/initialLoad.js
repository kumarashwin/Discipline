function initialLoad(event) {
    // Initializes the color picker
    $('#Color').colorpicker({color:'transparent', format:'hex'});

    // Attaches the necessary event handlers to the next-activity list
    attachEventsToActivities();

    // Loads the chart framework
    loadChart();
    
    hideAllPagesExcept('current-activity');
}