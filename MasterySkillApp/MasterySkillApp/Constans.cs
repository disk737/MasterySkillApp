using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp
{
    class Constans
    {
        public static string RestUrl = "https://masteryskillapp.herokuapp.com";

        // Cadena que guarda el Token de seguridad
        public static string UserTokenString = "UserToken";

        // API para crear e ingresar usuarios
        public static string UserSignIn = "/mastery/user/ingresarUsuario";

        // Cadenas que guardan la informacion del usuario
        public static string SaveActive = "active";     // Cadena que se usa para indicar que el usuario pidio guardar las credenciales
        public static string SaveUnactive = "unactive"; // Cadena que se usa para indicar que el usuario pidio no guardar las credenciales
        public static string SaveCredentials = "SaveCredentials"; // Me indica si las credenciales estan activas o no
    }
}
