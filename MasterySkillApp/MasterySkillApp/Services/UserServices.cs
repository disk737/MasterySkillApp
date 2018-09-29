﻿using MasterySkillApp.Models;
using Microsoft.AppCenter.Crashes;
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
    public class UserServices : BaseServices
    {

        // Creo el cliente Http para realizar las peticiones
        private HttpClient client = new HttpClient();

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

                if (response.IsSuccessStatusCode)
                {
                    // Aqui voy a recibir el token o el mensaje de error dependiendo del caso
                    Token = JsonConvert.DeserializeObject<UserToken>(content);
                }

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
    }
}
