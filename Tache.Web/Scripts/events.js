function startTransition(){
    chart.svg.parentElement.addEventListener("transitionend", onTransparentFinish);
    chart.svg.parentElement.className = "makeTransparent";
}

function onTransparentFinish(event) {
    event.stopPropagation();
    chart.clear();
    chart.draw();
    RemoveTransparentFinish();
}

function RemoveTransparentFinish() {
    chart.svg.parentElement.removeEventListener("transitionend", onTransparentFinish);
    chart.svg.parentElement.addEventListener("transitionend", onOpaqueFinish);
    chart.svg.parentElement.className = "makeOpaque";
}

function onOpaqueFinish(event) {
    event.stopPropagation();
    chart.svg.parentElement.className = "";
    chart.svg.parentElement.removeEventListener("transitionend", onOpaqueFinish);
}