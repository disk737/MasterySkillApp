using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MasterySkillApp.Services
{
    public static class GeneralServices
    {
        // Metodo para guardar el token en el dispositivo movil. Este metodo tambien es llamado por CreateAccountView
        static public void SaveCredentialsMethod(string token, bool SaveCredencials)
        {
            // Guardo el token generado para el usuario
            Application.Current.Properties[Constans.UserTokenString] = token;

            // Si el usuario decidio guardar sus credenciales, procedo a guardarlas
            if (SaveCredencials == true)
                Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveActive;
            else
                Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveUnactive;


        }
    }
}
