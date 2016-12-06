var activitiesCRUD = document.getElementById("activities-CRUD");
var backgroundDiv = document.getElementById("background-cover");

var activitiesList = document.getElementsByClassName("activities-list")[0].querySelector("ul");

activitiesList.addEventListener("contextmenu", function (event) {
    event.stopPropagation();
    event.preventDefault();
    activitiesCRUD.style.display = "block";
});
