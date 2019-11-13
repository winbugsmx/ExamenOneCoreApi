using System;
using System.Collections.Generic;
using ExamenOneCore.Entity.Models;

namespace ExamenOneCore.Ebl.Interface
{
    public interface IUsuario
    {
        void SetConnectionString(string dbConnection);

        List<UsuarioModel> GetUsuarios();

        UsuarioModel GetUsuario(int id);

        string Delete(int id);

        string Create(UsuarioModel usuarioModel);

        string Update(EditUsuarioModel usuarioModel);

        string UpdatePassword(ChangePasswordModel changePasswordModel);

        string Login(LoginModel loginModel);

        string VerificaUsuario(string username, int userid);

        string VerificaEmail(string email, int userid);
    }
}