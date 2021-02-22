//Animação da barra de pesquisa - Início
var barraDeBusca = document.querySelector(".search-container");

document.addEventListener("click", function(event) {
    if(event.target.closest("#search")) {
        barraDeBusca.classList.add("searching");
        return
    }
    barraDeBusca.classList.remove("searching");
})
//Animação da barra de pesquisa - Final


//Pesquisar usuário/posts - Início
let campoFiltro = document.querySelector("#buscar");

campoFiltro.addEventListener("input", function() {
    console.log(this.value);
    let posts = document.querySelectorAll(".post-feed");
    let stories = document.querySelectorAll(".friends");

    if (campoFiltro.value.length != 0){
        for (let i = 0; i < posts.length; i++) {
            let post = posts[i];
            let postNome = post.querySelector(".nome-publicacao");
            let nome = postNome.textContent;

            var expressao = new RegExp(this.value, "i");
            if(!expressao.test(nome)) {
                post.classList.add("invisivel");
            } else {
                post.classList.remove("invisivel");
            }
        }

        for (let i = 0; i < stories.length; i++) {
            let story = stories[i];
            let storyNome = story.querySelector(".nome-stories");
            let nome = storyNome.textContent;

            if(!expressao.test(nome)) {
                story.classList.add("invisivel");
            } else {
                story.classList.remove("invisivel");
            }
        }
    } else {
        for (let i = 0; i < posts.length; i++) {
            let post = posts[i];
            post.classList.remove("invisivel");
            
        }

        for (let i = 0; i < stories.length; i++) {
            let story = stories[i];
            story.classList.remove("invisivel");
            
        }
    }
});