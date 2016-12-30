function nextActivitySubmit(event) {
    event.target.querySelector('input[name=clientRequestTime]').value = new Date().toISOString();
}