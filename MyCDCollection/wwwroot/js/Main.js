//* Event listener for the hamburgermenu
document.getElementById("Hambugermenu").addEventListener("click", function () {
    this.classList.toggle("is-active");
    let menu = document.querySelector(".Mobile-container");
    menu.classList.toggle("is-active");
});
