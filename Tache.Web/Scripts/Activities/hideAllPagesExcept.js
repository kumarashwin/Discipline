function hideAllPagesExcept(pageId) {
    document.querySelectorAll('div.page').forEach(function (page) { page.style.display = 'none' });
    document.querySelector('button.active').classList.remove('active')

    // If the '+' button is clicked, clear the inputs except the verification token
    pageToShow = document.getElementById(pageId);
    if (pageId == 'activity-form')
        pageToShow.querySelectorAll('input').forEach(function (input) {
            if (input.name != '__RequestVerificationToken')
                input.value = '';
        });

    document.querySelector('button[value=' + pageId + ']').classList.add('active');
    pageToShow.style.display = '';
}