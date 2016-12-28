function initialLoad(event) {
    // Initializes the color picker
    $('#Color').colorpicker({color:'transparent', format:'hex'});

    // Loads the chart framework
    loadChart();
    
    hideAllPagesExcept('current-activity');
}