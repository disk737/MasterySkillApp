using MasterySkillApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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

        // GET: Metodo para obtener los atributos basicos desde el servidor
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

                    // Deserializo la respuesta del servidor en un Json
                    DataResponse = (JsonConvert.DeserializeObject<ListBasicAttrModel>(content)).BasicAttrs;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return DataResponse;
        }

        // GET: Metodo para obtener los puntos en cada atributo basico
        public async Task<List<BasicAttrModel>> GetAttrPoints()
        //public async Task<ListAttrPoints> GetAttrPoints()
        {
            // Lista para guarda la respuesta del servicio
            List<BasicAttrModel> DataResponse = new List<BasicAttrModel>();
            //ListAttrPoints listAttrPoints = new ListAttrPoints();

            // Capturo el Token guardado en la aplicacion
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Construyo la URI a consultar
            //var uri = new Uri(string.Format(Constans.RestUrl + Constans.GetAttrPoints));
            var uri = new Uri(string.Format(Constans.RestUrl + "/mastery/privateFail"));

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetAttrPoints");

            // Hago la llamada al WS
            try
            {
                var response = await client.GetAsync(uri);

                // Espero una respuesta positiva del servidor (200)
                if (!response.IsSuccessStatusCode)
                {                    
                    throw new HttpExceptionEx(response.StatusCode, "Fallo en el servidor", false);
                }
                
                var content = await response.Content.ReadAsStringAsync();
                // listAttrPoints.AttrPoints = JsonConvert.DeserializeObject<List<BasicAttrModel>>(content); -> Alberto
                
                // Deserializo la respuesta del servidor en un Json
                DataResponse = (JsonConvert.DeserializeObject<ListAttrPoints>(content)).AttrPoints;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw new HttpExceptionEx(HttpStatusCode.BadRequest, "", false);
            }
            //finally
            //{

            //}

            return  DataResponse; //listAttrPoints;
        }

        // GET: Metodo para obtener los puntos con detalles de cada usuario
        public async Task<List<DetailAttrModel>> GetDetailCount()
        {
            // Lista para guarda la respuesta del servicio
            List<DetailAttrModel> DataResponse = new List<DetailAttrModel>();

            // Capturo el Token guardado en la aplicacion
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.getDetailCount));

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetDetailCount");

            // Hago la llamada al WS
            try
            {
                var response = await client.GetAsync(uri);

                // Espero una respuesta positiva del servidor (200)
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserializo la respuesta del servidor en un Json
                    DataResponse = (JsonConvert.DeserializeObject<ListDetailAttr>(content)).AttrDetail;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return DataResponse;
        }

        // POST: Metodo para enviar un punto a un usuario
        public async Task<string> SendAttrPoint(UserModel argUser, BasicAttrModel argBasicAttr)
        {
            // Debo agregar un Handler cuando las respuesas del servidor son fallidas
            // Solucion temporal 
            string resFail = "";

            // Capturo el Token guardado en la aplicacion
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.SendAttrPoint));

            // Creo el objeto que voy a Serializar
            var attrPoint = new SendAttrModel(argUser.userUUID, argBasicAttr.basicAttrID);

            // Genero el Body de la peticion
            var BodyRequest = new StringContent(JsonConvert.SerializeObject(attrPoint), Encoding.UTF8, Constans.AplicationJson);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion SendAttrPoint");

            // Hago la llamada al WS
            try
            {
                var response = await client.PostAsync(uri, BodyRequest);

                // Debo agregar un Handler cuando las respuesas del servidor son fallidas

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                resFail = ex.Message;
            }

            return resFail;
        }
    }
}
