// Sets the clientRequestTime input value when one of the activities is clicked on
function nextActivitySubmitEventHandler(event) {
    event.target.querySelector('input[name=clientRequestTime]').value = new Date().toISOString();
}