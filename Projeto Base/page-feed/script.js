var barraDeBusca = document.querySelector(".busca-container");

document.addEventListener("click", function(event) {
    if(event.target.closest("#busca")) {
        barraDeBusca.classList.add("buscando");
        return
    }
    barraDeBusca.classList.remove("buscando");
})

let like = "unliked";

function mudarImagem(id) {
    let heart = document.getElementById(id);
    console.log(id);

    if(like == "unliked") {
        heart.src="./img/heart-2-pintado1.svg";
        like = "liked";
        console.log(heart);
    } else {
        heart.src="./img/Vectors2.svg";
        like = "unliked";
    }
};