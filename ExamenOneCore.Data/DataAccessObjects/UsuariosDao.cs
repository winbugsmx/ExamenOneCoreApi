using System;
using System.Collections.Generic;
using System.Text;
using ExamenOneCore.Entity.Models;
using ExamenOneCore.Data.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using ExamenOneCore.Data.AppCode;

namespace ExamenOneCore.Data.DataAccessObjects
{
    public class UsuariosDao
    {
        private string _dbConnection = string.Empty;

        public UsuariosDao(string dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<UsuarioModel> GetUsuariosActivos()
        {
            List<UsuarioModel> usersList = new List<UsuarioModel>();

            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                usersList = context.Usuario.ToList();
            }

            return usersList;
        }

        public UsuarioModel GetUsuario(int userId)
        {
            UsuarioModel response = new UsuarioModel();
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    response = context.Usuario.Where(select => select.UserId == userId).FirstOrDefault();
                }
                catch (Exception)
                {
                    response = new UsuarioModel();
                }
            }
            return response;
        }

        public string Delete(int userId)
        {
            string response = string.Empty;
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    UsuarioModel usuarioModel = context.Usuario.Where(select => select.UserId == userId).FirstOrDefault();
                    usuarioModel.Estatus = false;
                    usuarioModel.FechaActualizacion = DateTime.Now;
                    context.Update<UsuarioModel>(usuarioModel);
                    context.SaveChanges();
                    response = string.Format("Usuario Eliminado");
                }
                catch (Exception ex)
                {
                    response = string.Format("Error: ", ex.Message);
                }
            }
            return response;
        }

        public string Create(UsuarioModel usuarioModel)
        {
            string response = string.Empty;
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    context.Database.ExecuteSqlCommand("spCreateUsuario @p0, @p1, @p2, @p3, @p4", parameters: new[] {
                        usuarioModel.Username.ToString(),
                        Security.Encrypt(usuarioModel.Password.ToString()),
                        usuarioModel.Email.ToString(),
                        usuarioModel.Sexo.ToString(),
                        usuarioModel.Estatus.ToString()
                    });

                    response = string.Format("Usuario Creado");
                }
                catch (Exception ex)
                {
                    response = string.Format("Error: ", ex.Message);
                }
            }
            return response;
        }

        public string Update(EditUsuarioModel usuarioModel)
        {
            string response = string.Empty;
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    context.Database.ExecuteSqlCommand("spUpdateUsuario @p0, @p1, @p2, @p3, @p4", parameters: new[] {
                        usuarioModel.UserId.ToString(),
                        usuarioModel.Username.ToString(),
                        usuarioModel.Email.ToString(),
                        usuarioModel.Sexo.ToString(),
                        usuarioModel.Estatus.ToString()
                    });

                    response = string.Format("Usuario Actualizado");
                }
                catch (Exception ex)
                {
                    response = string.Format("Error: ", ex.Message);
                }
            }
            return response;
        }

        public string UpdatePassword(ChangePasswordModel changePasswordModel)
        {
            string response = string.Empty;
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    context.Database.ExecuteSqlCommand("spUpdatePassword @p0, @p1", parameters: new[] {
                        changePasswordModel.UserId.ToString(),
                        Security.Encrypt(changePasswordModel.Password.ToString())
                    });

                    response = string.Format("Contraseña Actualizada");
                }
                catch (Exception ex)
                {
                    response = string.Format("Error: ", ex.Message);
                }
            }
            return response;
        }

        public string Login(LoginModel loginModel)
        {
            string response = string.Empty;
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    UsuarioModel usuarioModel = context.Usuario.Where(select => select.Username.Equals(loginModel.Username) && select.Password.Equals(Security.Encrypt(loginModel.Password.ToString()))).FirstOrDefault();
                    if (usuarioModel != null)
                    {
                        if (usuarioModel.UserId > byte.MinValue)
                        {
                            response = string.Format("OK");
                        }
                        else
                        {
                            response = string.Format("FAIL");
                        }
                    }
                    else
                    {
                        response = string.Format("FAIL");
                    }
                }
                catch (Exception ex)
                {
                    response = string.Format("FAIL");
                }
            }
            return response;
        }

        public string VerificaUsuario(string username, int userid)
        {
            string response = string.Empty;
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    UsuarioModel usuarioModel = context.Usuario.Where(select => select.Username.Equals(username) && select.UserId != userid).FirstOrDefault();
                    if (usuarioModel != null)
                    {
                        response = !string.IsNullOrWhiteSpace(usuarioModel.Username) ? "El usuario ya existe" : "Usuario disponible";
                    }
                    else
                    {
                        response = "Usuario disponible";
                    }
                }
                catch (Exception ex)
                {
                    response = string.Format("Error: ", ex.Message);
                }
            }
            return response;
        }

        public string VerificaEmail(string email, int userid)
        {
            string response = string.Empty;
            using (var context = new ExamenOneCoreBDContext(_dbConnection))
            {
                try
                {
                    UsuarioModel usuarioModel = context.Usuario.Where(select => select.Email.Equals(email) && select.UserId != userid).FirstOrDefault();
                    if (usuarioModel != null)
                    {
                        response = !string.IsNullOrWhiteSpace(usuarioModel.Email) ? "El correo electrónico ya existe" : "Correo electrónico disponible";
                    }
                    else
                    {
                        response = "Correo electrónico disponible";
                    }
                }
                catch (Exception ex)
                {
                    response = string.Format("Error: ", ex.Message);
                }
            }
            return response;
        }
    }
}