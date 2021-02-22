using System.Collections.Generic;
using Instadev_06.Models;

namespace Instadev_06.Interfaces
{
    public interface IPublicacao
    {
        //Criação do CRUD

        //Create
        void Create(Publicacao p);

        //Read
        List<Publicacao> ReadAll();

        //Update
        void Update(Publicacao p);
        
        //Delete
        void Delete(int id);
    }
}