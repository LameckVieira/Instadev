using System.Collections.Generic;
using Instadev_06.Models;

namespace Instadev_06
{
    public interface IUsuario
    {
         //Cração do CRUD

         //Create
         void Create(Usuario u);

         //Read
         List<Usuario> ReadAll();

         //Update
         void Update(Usuario u);

         //Delete
         void Delete(int id);
    }
}