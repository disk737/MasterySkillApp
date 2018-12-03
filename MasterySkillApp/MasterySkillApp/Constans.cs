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
        public static string UserSignIn = "/mastery/user/signIn";
        public static string GetBasicAttr = "/mastery/user/getBasicAttr";
        public static string GetAllUsers = "/mastery/user/getAllUsers";
        public static string GetAttrPoints = "/mastery/user/getAttrPoints";
        public static string SendAttrPoint = "/mastery/user/sendAttrPoint";
        public static string GetDetailCount = "/mastery/user/getDetailCount";
        public static string GetUserInfo = "/mastery/user/getUserInfo";
        public static string UpdateUserStatus = "/mastery/user/updateStatus";


        // Cadenas que guardan la informacion del usuario
        public static string SaveActive = "active";     // Cadena que se usa para indicar que el usuario pidio guardar las credenciales
        public static string SaveUnactive = "unactive"; // Cadena que se usa para indicar que el usuario pidio no guardar las credenciales
        public static string SaveCredentials = "SaveCredentials"; // Me indica si las credenciales estan activas o no

        // Constantes generales
        public static string AplicationJson = "application/json";
    }
}
