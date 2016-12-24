function resetPage() {
    // Initializes the color picker
    $('#Color').colorpicker({color:'transparent', format:'hex'});

    attachEventsToActivities();

    // Setup Chart Window
    //main();
}