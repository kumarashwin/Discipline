var activitiesCRUD = document.getElementById("activities-CRUD");
var backgroundDiv = document.getElementById("background-cover");

var activitiesList = document.getElementsByClassName("activities-list")[0].querySelector("ul");

activitiesList.addEventListener("contextmenu", function (event) {
    if (event.target.nodeName == "BUTTON") {
        event.preventDefault();
        activitiesCRUD.querySelector("#Id").value = event.target.getAttribute("data-activity-id")   
        activitiesCRUD.querySelector("#Name").value = event.target.getAttribute("data-activity-name")   
        activitiesCRUD.querySelector("#Description").value = event.target.getAttribute("data-activity-description")   
        activitiesCRUD.querySelector("#Color").value = event.target.getAttribute("data-activity-color")   
        activitiesCRUD.querySelector("#BudgetHours").value = event.target.getAttribute("data-activity-hours")
        activitiesCRUD.querySelector("#BudgetMinutes").value = event.target.getAttribute("data-activity-minutes")
        activitiesCRUD.style.display = "block";
    }
});
