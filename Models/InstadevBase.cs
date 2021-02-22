using System.Collections.Generic;
using System.IO;

namespace Instadev_06.Models
{
    public class InstadevBase
    {
        public void CreateFolderAndFile(string path)
        {
            string folder = path.Split("/")[0];

            //Verfica se existe o diretório do path
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            //Verfica se existe o arquivo do path
            if(!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> linhas = new List<string>();

            using(StreamReader file = new StreamReader(path))
            {
                string linha;

                //Percorrer as linhas com um laço while
                while ((linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        public void RewriteCSV(string path, List<string> linhas)
        {
            using(StreamWriter output = new StreamWriter(path))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
    }
}