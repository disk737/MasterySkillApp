using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace MasterySkillApp.Services
{
    public class BaseServices
    {
        // Metodo encargado de extraer el token de la memoria y construir la URI
        public Uri GetUserToken(string argApi, ref HttpClient client)
        {
            // Capturo el Token guardado en la aplicacion
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + argApi));
            return uri;
        }

    }
}
