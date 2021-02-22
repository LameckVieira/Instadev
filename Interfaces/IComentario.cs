using System.Collections.Generic;
using Instadev_06.Models;

namespace Instadev_06.Interfaces
{
    public interface IComentario
    {
        //Criação do CRUD

        //Create
        void Create(Comentario c);

        //Read
        List<Comentario> ReadAll();

        //Update
        void Update(Comentario c);
        
        //Delete
        void Delete(int id); 
    }
}