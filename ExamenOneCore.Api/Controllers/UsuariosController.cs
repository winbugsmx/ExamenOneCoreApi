using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamenOneCore.Entity.Models;
using ExamenOneCore.Ebl.Interface;
using ExamenOneCore.Api.App_Code;

namespace ExamenOneCore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UsuariosController : ControllerBase
    {
        private IUsuario usuarioEbl { get; set; }
        private string ConnectionString = ConfigurationManager.AppSetting["MySettings:ConnectionString"];

        public UsuariosController(IUsuario _usuarioEbl)
        {
            this.usuarioEbl = _usuarioEbl;
            usuarioEbl.SetConnectionString(ConnectionString);
        }

        // GET: api/Usuarios
        [HttpGet("Get", Name = "Get")]
        public IEnumerable<UsuarioModel> Get()
        {
            return usuarioEbl.GetUsuarios();
        }

        // POST: api/Usuarios/CreateUser
        [HttpPost("CreateUser", Name = "CreateUser")]
        [Route("CreateUser")]
        public string CreateUser([FromBody] UsuarioModel usuarioModel)
        {
            return usuarioEbl.Create(usuarioModel);
        }

        // POST: api/Usuarios/UpdateUser
        [HttpPost]
        [Route("UpdateUser")]
        public string UpdateUser([FromBody] EditUsuarioModel usuarioModel)
        {
            return usuarioEbl.Update(usuarioModel);
        }

        // POST: api/Usuarios/DeleteUser
        [HttpPost]
        [Route("DeleteUser")]
        public string DeleteUser([FromBody] DeleteUsuarioModel usuarioModel)
        {
            return usuarioEbl.Delete(usuarioModel.Id);
        }

        // POST: api/Usuarios/ChangePassword
        [HttpPost]
        [Route("ChangePassword")]
        public string ChangePassword([FromBody] ChangePasswordModel model)
        {
            return usuarioEbl.UpdatePassword(model);
        }

        // POST: api/Usuarios/VerificaUsuario
        [HttpPost]
        [Route("VerificaUsuario")]
        public string VerificaUsuario([FromBody] EditUsuarioModel usuarioModel)
        {
            return usuarioEbl.VerificaUsuario(usuarioModel.Username, usuarioModel.UserId);
        }

        // POST: api/Usuarios/VerificaEmail
        [HttpPost]
        [Route("VerificaEmail")]
        public string VerificaEmail([FromBody] EditUsuarioModel usuarioModel)
        {
            return usuarioEbl.VerificaEmail(usuarioModel.Email, usuarioModel.UserId);
        }

        // POST: api/Usuarios/Login
        [HttpPost]
        [Route("Login")]
        public string Login([FromBody] LoginModel model)
        {
            return usuarioEbl.Login(model);
        }
    }
}