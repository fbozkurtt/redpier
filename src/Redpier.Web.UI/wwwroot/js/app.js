function focusElement(id) {
    const element = document.getElementById(id);
    element.focus();
}

function disableElement(id) {
    const element = document.getElementById(id);
    element.disabled = true;
}

function enableElement(id) {
    const element = document.getElementById(id);
    element.disabled = false;
}

function showElement(id) {
    const element = document.getElementById(id);
    element.hidden = false;
}

function clearInput(id) {
    const element = document.getElementById(id);
    element.value = "";
}

function fillElement(id, text) {
    const element = document.getElementById(id);
    element.innerText = text;
}

function clearElement(id) {
    const element = document.getElementById(id);
    element.innerText = "";
}

function addConsoleLine(parentId, id) {
    const parent = document.getElementById(parentId);
    let child = document.createElement("input");
    child.setAttribute("id", id);
    child.setAttribute("hidden");
}