using System;
using System.Collections.Generic;
using System.IO;
using Instadev_06.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instadev_06.Controllers
{
    [Route("EditarPerfil")]
    public class EditarPerfilController : Controller
    {
        Usuario usuarioModel = new Usuario();
        Publicacao publicacaoModel = new Publicacao();
        
        public IActionResult Index()
        {
            ViewBag.UserLogado = MostrarUsuario();
            return View();
        }
        
        [Route("EditarPerfil-Usuario")]
        public Usuario MostrarUsuario()
        {
            var userid = HttpContext.Session.GetString("_UserId");
            Usuario userLogado = usuarioModel.ObterUsuarioDaSessao(int.Parse(userid));

            return userLogado;
        }
        
        [Route("EditarPerfil-Alterar-dados")]
        public IActionResult AlterarDados(IFormCollection form)
        {            
            Usuario novoUsuario = MostrarUsuario();
            novoUsuario.Nome = form["Nome"];
            novoUsuario.Foto = form["Foto"];
            
            if(form.Files.Count > 0){

                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/perfil");

                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using(var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                
                novoUsuario.Foto = file.FileName;           
            } else {
                //Obter foto do usuario logado
                novoUsuario.Foto = FotoUsuarioLogado();
            }

            novoUsuario.DataNascimento = DateTime.Parse(form["DataNascimento"]);
            novoUsuario.Email = form["Email"];
            novoUsuario.Username = form["Username"];
            
            usuarioModel.Update(novoUsuario);

            ViewBag.UsuarioAtualizado = novoUsuario;

            //Alterar posts - início
            List<string> posts = publicacaoModel.ReadAllLinesCSV(publicacaoModel._PATH);

            var pub = posts.FindAll(x => x.Split(";")[3] == novoUsuario.IdUsuario.ToString());
            
            foreach (string item in pub)
            {
                string[] linha = item.Split(";");

                Publicacao publicacao = new Publicacao();
                publicacao.IdPublicacao = int.Parse(linha[0]);
                publicacao.Imagem = linha[1];
                publicacao.Legenda = linha[2];
                publicacao.IdUsuario = int.Parse(linha[3]);
                publicacao.Likes = int.Parse(linha[4]);
                if(form.Files.Count > 0) {
                    var file = form.Files[0];
                    publicacao.FotoUsuario = file.FileName;
                } else {
                    publicacao.FotoUsuario = FotoUsuarioLogado();
                }
                publicacao.Username = novoUsuario.Username;

                publicacaoModel.Update(publicacao);
            }
            //Alterar posts - início

            return LocalRedirect("~/EditarPerfil");
        }

        [Route("ExcluirPerfil")]
        public IActionResult Excluir(int id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (userId == id.ToString())
            {
                usuarioModel.Delete(id);
            }

            List<string> posts = publicacaoModel.ReadAllLinesCSV(publicacaoModel._PATH);

            var pub = posts.FindAll(x => x.Split(";")[3] == userId);

            foreach (string item in pub)
            {
                string[] linha = item.Split(";");

                publicacaoModel.Delete(int.Parse(linha[0]));
            }

            return LocalRedirect("~/Login");
        }

        [Route("FotoUsuarioLogado")]
        public string FotoUsuarioLogado()
        {
            var userId = HttpContext.Session.GetString("_UserId");

            List<string> usuarios = usuarioModel.ReadAllLinesCSV(usuarioModel._PATH);

            var usuario = usuarios.Find(x => x.Split(";")[0] == userId);

            var atributo = usuario.Split(";");

            return atributo[2];
        }
    }
}