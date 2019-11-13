using System;
using System.Collections.Generic;
using System.Text;
using ExamenOneCore.Ebl.Interface;
using ExamenOneCore.Entity.Models;
using ExamenOneCore.Data.DataAccessObjects;

namespace ExamenOneCore.Ebl.Implementaciones
{
    public class UsuarioEbl : IUsuario
    {
        private UsuariosDao usuarioDao;

        public void SetConnectionString(string dbConnection)
        {
            usuarioDao = new UsuariosDao(dbConnection);
        }

        public List<UsuarioModel> GetUsuarios()
        {
            return usuarioDao.GetUsuariosActivos();
        }

        public UsuarioModel GetUsuario(int id)
        {
            return usuarioDao.GetUsuario(id);
        }

        public string Delete(int id)
        {
            return usuarioDao.Delete(id);
        }

        public string Create(UsuarioModel usuarioModel)
        {
            return usuarioDao.Create(usuarioModel);
        }

        public string Update(EditUsuarioModel usuarioModel)
        {
            return usuarioDao.Update(usuarioModel);
        }

        public string UpdatePassword(ChangePasswordModel changePasswordModel)
        {
            return usuarioDao.UpdatePassword(changePasswordModel);
        }

        public string Login(LoginModel loginModel)
        {
            return usuarioDao.Login(loginModel);
        }

        public string VerificaUsuario(string username, int userid)
        {
            return usuarioDao.VerificaUsuario(username, userid);
        }

        public string VerificaEmail(string email, int userid)
        {
            return usuarioDao.VerificaEmail(email, userid);
        }
    }
}