function hideAllButActive(event) {
    if (event.target.nodeName == 'button'.toUpperCase()) {
        document.querySelectorAll('div.page').forEach(function (page) { page.style.display = 'none' });
        document.querySelector('button.active').classList.remove('active');

        // If the '+' button is clicked, clear the inputs
        var pageToShow = document.getElementById(event.target.value);
        if (pageToShow.id == 'activity-form')
            pageToShow.querySelectorAll('input').forEach(function (input) { input.value = ''; });

        event.target.classList.add('active');
        pageToShow.style.display = '';
    }
}

function hideFormShowNextActivity(event){
    if (event.target.nodeName == 'button'.toUpperCase()) {
        document.querySelectorAll('div.page').forEach(function (page) { page.style.display = 'none' });
        document.querySelector('button.active').classList.remove('active');
        document.querySelector('button[value=next-activity]').classList.add('active');
        document.getElementById('next-activity').style.display = '';
    }
}