// Write your JavaScript code.

// function to remove "hidden" class on div. When "local delivery" clicked, "city" field appears
document.getElementById("localDeliveryCheck").addEventListener("click", function () {
    console.log("button clicked")
    document.getElementsByClassName("hidden").remove()
  
})