let like = "unliked";

function curtir(id) {
    let heart = document.getElementById(id);
    console.log(id);

    if(like == "unliked") {
        heart.src="./img/img-perfil/like-painted.svg";
        like = "liked";
        console.log(heart);
    } else {
        heart.src="./img/img-perfil/like.svg";
        like = "unliked";
    }
};