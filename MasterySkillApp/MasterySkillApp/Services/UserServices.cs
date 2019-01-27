using MasterySkillApp.Entitys;
using MasterySkillApp.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterySkillApp.Services
{
    public class UserServices : BaseServices
    {

        // Creo el cliente Http para realizar las peticiones
        private HttpClient client = new HttpClient();

        // Metodo para crear una cuenta nueva
        public async Task<UserToken> UserSignUp(string argUserName, 
                                                string argUserLastName, 
                                                string argUserEmail, 
                                                string argUserPassword, 
                                                string argCodeGroup, 
                                                string argInstallationID)
        {
            var user = new UserModel
            {
                userEmail = argUserEmail,
                userPassword = argUserPassword,
                userName = argUserName,
                userLastName = argUserLastName,
                userCodeGroup = argCodeGroup,
                userDevice = argInstallationID
            };

            // Creo el contenedor del Token que voy a retornar
            UserToken Token = new UserToken();

            // Genero el Body de la peticion
            var BodyRequest = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, Constans.AplicationJson);

            // Construyo la URI que voy a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.UserSignUp));

            // Hago la llamada al servicio
            try
            {
                var response = await client.PostAsync(uri, BodyRequest);

                // Leo la cadena en la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Aqui voy a recibir el token o el mensaje de error dependiendo del caso
                Token = JsonConvert.DeserializeObject<UserToken>(content);
                Token.StatusCode = response.StatusCode;
                Token.IsSuccessStatusCode = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
            }

            return Token;
        }


        // Metodo para obtener el Token de Logueo del usuario
        public async Task<UserToken> UserSignIn(string argUserEmail, string argUserPassword, string argInstallationID)
        {
            // Creo un onjeto para guardar la info del usuario
            var user = new UserModel
            {
                userEmail = argUserEmail,
                userPassword = argUserPassword,
                userDevice = argInstallationID
            };

            // Creo el contenedor del Token que voy a retornar
            UserToken Token = new UserToken();

            // Genero el Body de la peticion
            var BodyRequest = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, Constans.AplicationJson);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.UserSignIn));

            // Hago la llamada al servicio
            try
            {
                var response = await client.PostAsync(uri, BodyRequest);

                // Leo la cadena de la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Aqui voy a recibir el token o el mensaje de error dependiendo del caso
                Token = JsonConvert.DeserializeObject<UserToken>(content);
                Token.StatusCode = response.StatusCode;
                Token.IsSuccessStatusCode = response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
            }

            return Token;
        }

        // Metodo para hacer Logout y borrar el token del telefono
        public void UserLogout()
        {
            // Verifico que la Key exista dentro del telefono
            if (Application.Current.Properties.ContainsKey(Constans.SaveCredentials))
            {
                // Cambio el estado de SaveCredentials a Inactivo
                Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveUnactive;
            }

            // Si el Token se encuentra en el telefono lo debo de borrar
            if (Application.Current.Properties.ContainsKey(Constans.UserTokenString))
            {
                // Borro el contenido de la variable que contiene el Token
                Application.Current.Properties.Remove(Constans.UserTokenString);
            }

        }

        // GET: Obtengo todos los usuarios que estan dentro de la aplicacion
        public async Task<ListUserModel> GetUserModels()
        {
            // Lista para guarda la respuesta del servicio
            ListUserModel DataResponse = new ListUserModel();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.GetAllUsers, ref client);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetUserModels");

            try
            {
                var response = await client.GetAsync(uri);
                // Guardo la respuesta del servidor
                DataResponse.StatusCode = response.StatusCode;

                // Espero una respuesta positiva del servidor (200)
                if (!response.IsSuccessStatusCode)
                    return DataResponse;


                var content = await response.Content.ReadAsStringAsync();

                // Deserializo la respuesta del servidor en un Json
                DataResponse.UserModels = (JsonConvert.DeserializeObject<ListUserModel>(content)).UserModels;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
            }

            return DataResponse;
        }

        // GET: Obtengo la informacion del usuario logueado
        public async Task<ListUserModel> GetUserInfo()
        {
            // Lista para guarda la respuesta del servicio
            ListUserModel DataResponse = new ListUserModel();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.GetUserInfo, ref client);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetUserInfo");

            try
            {
                var response = await client.GetAsync(uri);
                // Guardo la respuesta del servidor
                DataResponse.StatusCode = response.StatusCode;

                // Espero una respuesta positiva del servidor (200)
                if (!response.IsSuccessStatusCode)
                    return DataResponse;


                var content = await response.Content.ReadAsStringAsync();

                // Deserializo la respuesta del servidor en un Json
                DataResponse.UserModels = (JsonConvert.DeserializeObject<ListUserModel>(content)).UserModels;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
            }

            return DataResponse;
        }

        // PUT: Metodo para cambiar el estado del usuario
        public async Task<string> UpdateUserStatus(string argMessage)
        {
            // Debo agregar un Handler cuando las respuesas del servidor son fallidas
            // Solucion temporal 
            string resFail = "";

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.UpdateUserStatus, ref client);

            // Creo el objeto que voy a Serializar
            var userStatus = new SendUserStatus(argMessage);

            // Genero el Body de la peticion
            var BodyRequest = new StringContent(JsonConvert.SerializeObject(userStatus), Encoding.UTF8, Constans.AplicationJson);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion UpdateUserStatus");

            // Hago la llamada al WS
            try
            {
                var response = await client.PutAsync(uri, BodyRequest);

                // Debo agregar un Handler cuando las respuesas del servidor son fallidas

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
                resFail = ex.Message;
            }

            return resFail;
        }

        // PUT: Metodo para cambiar la contraseña de un usuario
        public async Task<BaseResponse> UpdateUserPassword(string argOldPassword, string argNewPassword)
        {
            // Debo agregar un Handler cuando las respuesas del servidor son fallidas
            // Solucion temporal 
            BaseResponse responseServer = new BaseResponse();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.UpdateUserPassword, ref client);

            // Creo el objeto que voy a Serializar
            var userPassword = new SendUserPassword(argOldPassword, argNewPassword);

            // Genero el Body de la peticion
            var BodyRequest = new StringContent(JsonConvert.SerializeObject(userPassword), Encoding.UTF8, Constans.AplicationJson);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion UpdateUserPassword");

            // Hago la llamada al WS
            try
            {
                var response = await client.PutAsync(uri, BodyRequest);

                // Leo la cadena de la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Deserializo el contenido del mensaje
                responseServer = JsonConvert.DeserializeObject<BaseResponse>(content);

                responseServer.StatusCode = response.StatusCode;
                responseServer.IsSuccessStatusCode = response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
                responseServer.IsSuccessStatusCode = false;
                responseServer.message = ex.Message;
            }

            return responseServer;
        }

        // GET: Obtengo la lista de los Grupos creados
        public async Task<ListGroupsModel> GetGroups()
        {
            // Lista para guarda la respuesta del servicio
            ListGroupsModel DataResponse = new ListGroupsModel();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.GetGroups, ref client);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetGroups");

            try
            {
                var response = await client.GetAsync(uri);
                // Guardo la respuesta del servidor
                DataResponse.StatusCode = response.StatusCode;

                // Espero una respuesta positiva del servidor (200)
                if (!response.IsSuccessStatusCode)
                    return DataResponse;


                var content = await response.Content.ReadAsStringAsync();

                // Deserializo la respuesta del servidor en un Json
                DataResponse.GroupModel = (JsonConvert.DeserializeObject<ListGroupsModel>(content)).GroupModel;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
            }

            return DataResponse;
        }

    }
}
