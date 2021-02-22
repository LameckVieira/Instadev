using System;
using Instadev_06.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instadev_06.Controllers
{
    [Route("Cadastro")]
    public class CadastroController : Controller
    {
        Usuario usuarioModel = new Usuario();
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("Cadastro-Usuario")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Usuario novoUsuario = new Usuario();
            novoUsuario.IdUsuario = novoUsuario.GerarId();
            novoUsuario.Nome = form["Nome"];
            novoUsuario.Foto = "padrao.png";
            novoUsuario.DataNascimento = DateTime.Parse(form["DataNascimento"]);
            novoUsuario.Email = form["Email"];
            novoUsuario.Username = form["Username"];
            novoUsuario.Senha = form["Senha"];

            usuarioModel.Create(novoUsuario);

            return LocalRedirect("~/Login");
        }       
    }
 }