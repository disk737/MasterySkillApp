using MasterySkillApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterySkillApp.Services
{
    public class BadgeServices
    {
        // Creo el cliente Http para realizar las peticiones
        private HttpClient client = new HttpClient();

        // Metodo para obtener los atributos basicos desde el servidor
        public async Task<List<BasicAttrModel>> GetBasicAttr()
        {
            // Lista para guarda la respuesta del servicio
            List<BasicAttrModel> DataResponse = new List<BasicAttrModel>();

            // Capturo el Token guardado en la aplicacion
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.GetBasicAttr));

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetBasicAttr");

            // Hago la llamada al WS
            try
            {
                var response = await client.GetAsync(uri);

                // Espero una respuesta positiva del servidor (200)
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    //DataResponse = ((ListBasicAttrModel)JsonConvert.DeserializeObject<ListBasicAttrModel>(content)).BasicAttrs;
                    DataResponse = (JsonConvert.DeserializeObject<ListBasicAttrModel>(content)).BasicAttrs;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return DataResponse;
        }

        public List<BadgeModel> GetBadgeModels2()
        {
            List<BadgeModel> BadgeList = new List<BadgeModel>
            {
                new BadgeModel {badgeName = "Tecnica", badgeStatus = "26 Tokens", badgeText = "La maestria sobre la herramienta"},
                new BadgeModel {badgeName = "Colaboración", badgeStatus = "15 Tokens", badgeText = "Solo unidos tendremos exito"},
                new BadgeModel {badgeName = "Comunicación", badgeStatus = "18 Tokens", badgeText = "Mi lenguaje es universal"},
                new BadgeModel {badgeName = "Soluciones", badgeStatus = "26 Tokens", badgeText = "Las buenas ideas se explican solas"},
                new BadgeModel {badgeName = "Pasion", badgeStatus = "20 Tokens", badgeText = "El motor que mueve el mundo"}
            };

            return BadgeList ;
        }
    }
}
