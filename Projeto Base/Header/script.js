var barraDeBusca = document.querySelector(".search-container");

document.addEventListener("click", function(event) {
    if(event.target.closest("#search")) {
        barraDeBusca.classList.add("searching");
        return
    }
    barraDeBusca.classList.remove("searching");
})