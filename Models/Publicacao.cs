using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Instadev_06.Interfaces;

namespace Instadev_06.Models
{
    public class Publicacao : InstadevBase , IPublicacao
    {
        public int IdPublicacao { get; set; }
        public string Imagem { get; set; }
        public string Legenda { get; set; }
        public int IdUsuario { get; set; }
        public int Likes { get; set; }
        public string FotoUsuario { get; set; }
        public string Username { get; set; }
        
        public const string PATH = "Database/Publicacao.csv";

        public string _PATH {
            get{return PATH;}
        }

        Random numRandom = new Random();
        
        public Publicacao(){
            //Cria a pasta e o arquivos caso ainda não esteja criado
            CreateFolderAndFile(PATH);
        }

        public string PrepareLineCSV(Publicacao p)
        {
            //Transforma o objeto Publicação em uma linha de arquivo CSV
            return $"{p.IdPublicacao};{p.Imagem};{p.Legenda};{p.IdUsuario};{p.Likes};{p.FotoUsuario};{p.Username}";
        }

        // Implementado a Interface com o CRUD

        public void Create(Publicacao p)
        {
            string[] linhas = {PrepareLineCSV(p)};

            File.AppendAllLines(PATH, linhas);
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

        public List<Publicacao> ReadAll()
        {
            List<Publicacao> publicacoes = new List<Publicacao>();

            string[] linhas = File.ReadAllLines(PATH);

            foreach (string item in linhas)
            {
                string[] linha = item.Split(";");

                Publicacao novaPublicacao = new Publicacao();
                novaPublicacao.IdPublicacao = int.Parse(linha[0]);
                novaPublicacao.Imagem = linha[1];
                novaPublicacao.Legenda = linha[2];
                novaPublicacao.IdUsuario = int.Parse(linha[3]);
                novaPublicacao.Likes = int.Parse(linha[4]);
                novaPublicacao.FotoUsuario = linha[5];
                novaPublicacao.Username = linha[6];
                
                publicacoes.Add(novaPublicacao);
            }

            return publicacoes;
        }

        public void Update(Publicacao p)
        {
           //Lemos todas as linhas do CSV
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removemos as linhas com o código comparado
            // Nesse caso o usuário só consegue alterar a sua publicação e não os comentarios/likes;
            linhas.RemoveAll(x => x.Split(";")[0] == p.IdPublicacao.ToString());

            //Adicionamos a lista alterada
            linhas.Add(PrepareLineCSV(p));

            //Reescrevemos o csv com a lista alterada
            RewriteCSV(PATH, linhas);
        }

        public int GerarIdPublicacao()
        {
            return numRandom.Next();
        }

        public void Curtir(int id)
        {
            List<string> posts = ReadAllLinesCSV(PATH);

            var pub = posts.Find(x => x.Split(";")[0] == id.ToString());
            System.Console.WriteLine(pub);

            string[] linha = pub.Split(";");

            Publicacao publicacao = new Publicacao();
            publicacao.IdPublicacao = id;
            publicacao.Imagem = linha[1];
            publicacao.Legenda = linha[2];
            publicacao.IdUsuario = int.Parse(linha[3]);
            publicacao.Likes = int.Parse(linha[4]) + 1;
            publicacao.FotoUsuario = linha[5]; 
            publicacao.Username = linha[6];

            Update(publicacao);
        }
    }
}