using System;
using System.Collections.Generic;
using System.IO;

namespace Instadev_06.Models
{
    public class Usuario : InstadevBase , IUsuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public DateTime DataNascimento { get; set; }
        public int[] Seguidos { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }

        public const string PATH = "Database/Usuarios.csv";

        public string _PATH {
            get{return PATH;}
        }

        Random numRandom = new Random();
        
        public Usuario() {
            //Cria a pasta e o arquivos caso ainda não esteja criado
            CreateFolderAndFile(PATH);
        }

        public string PrepareLineCSV(Usuario u)
        {
            //Transformamos o objeto Usuario em uma linha de arquivo CSV
            return $"{u.IdUsuario};{u.Nome};{u.Foto};{u.DataNascimento};{u.Username};{u.Email};{u.Senha}";
        }

        //CRUD - Início

        public void Create(Usuario u)
        {
            string[] linhas = {PrepareLineCSV(u)};

            File.AppendAllLines(PATH, linhas);
        }

        public List<Usuario> ReadAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            string[] linhas = File.ReadAllLines(PATH);

            foreach (string item in linhas)
            {
                string[] linha = item.Split(";");

                Usuario novoUsuario = new Usuario();
                novoUsuario.IdUsuario = int.Parse(linha[0]);
                novoUsuario.Nome = linha[1];
                novoUsuario.Foto = linha[2];
                novoUsuario.DataNascimento = DateTime.Parse(linha[3]);
                novoUsuario.Username = linha[4];
                novoUsuario.Email = linha[5];
                novoUsuario.Senha = linha[6];

                usuarios.Add(novoUsuario);
            }

            return usuarios;
        }

        public void Update(Usuario u)
        {
            //Lemos todas as linhas do CSV
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removemos as linhas com o código comparado
            linhas.RemoveAll(x => x.Split(";")[0] == u.IdUsuario.ToString());

            //Adicionamos na lista a equipe alterada
            linhas.Add(PrepareLineCSV(u));

            //Reescrevemos o csv com a lista alterada
            RewriteCSV(PATH, linhas);
        }

        public void Delete(int id)
        {
            //Lemos todas as linhas do CSV
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removemos as linhas com o id comparado
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());

            //Reescrevemos o csv com a lista alterada
            RewriteCSV(PATH, linhas);
        }

        //CRUD - Término
                
        public int GerarId()
        {
            // List<string> linhas = ReadAllLinesCSV(PATH);

            // int numero = linhas.Count() + 1;

            return numRandom.Next();
        }

        public Usuario ObterUsuarioDaSessao(int userId)
        {
            List<string> usuarios = ReadAllLinesCSV(PATH);

            // Traz os dados do usuário logado. Exemplo: 1;Nome;Foto;DataNascimento;Username;Email;Senha
            var userLogado = usuarios.Find(x => x.Split(";")[0] == userId.ToString());

            string[] atributo = userLogado.Split(";");
            // var formato = new CultureInfo("en-US");

            Usuario novoUsuario = new Usuario();
            novoUsuario.IdUsuario = int.Parse(atributo[0]);
            novoUsuario.Nome = atributo[1];
            novoUsuario.Foto = atributo[2];
            // novoUsuario.DataNascimento = DateTime.ParseExact(atributo[3],"g",formato);
            novoUsuario.DataNascimento = DateTime.Parse(atributo[3]);
            novoUsuario.Username = atributo[4];
            novoUsuario.Email = atributo[5];
            novoUsuario.Senha = atributo[6];

            return novoUsuario;
        }
    }
}