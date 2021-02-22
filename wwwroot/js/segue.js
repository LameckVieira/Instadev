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