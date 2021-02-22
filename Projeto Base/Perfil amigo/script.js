let botao = document.querySelector(".btn1");
let seguindo = false;

botao.addEventListener("click", function() {

    if(seguindo == false) {
        this.innerText = "Seguindo";
        seguindo = true;
    } else {
        this.innerText = "Seguir";
        seguindo = false;
    }

    this.classList.toggle("btnfct");
});

let like = "unliked";

function curtir(id) {
    let heart = document.getElementById(id);
    console.log(id);

    if(like == "unliked") {
        heart.src="./images/like-painted.svg";
        like = "liked";
        console.log(heart);
    } else {
        heart.src="./images/like.svg";
        like = "unliked";
    }
};