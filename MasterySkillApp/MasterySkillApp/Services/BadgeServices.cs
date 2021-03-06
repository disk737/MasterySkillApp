﻿using MasterySkillApp.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MasterySkillApp.Services
{
    public class BadgeServices : BaseServices
    {
        // Creo el cliente Http para realizar las peticiones
        private HttpClient client = new HttpClient();

        // GET: Metodo para obtener los atributos basicos desde el servidor
        public async Task<ListBasicAttrModel> GetBasicAttr()
        {
            // Lista para guarda la respuesta del servicio
            ListBasicAttrModel DataResponse = new ListBasicAttrModel();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.GetBasicAttr, ref client);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetBasicAttr");

            // Hago la llamada al WS
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
                DataResponse.BasicAttrs = (JsonConvert.DeserializeObject<ListBasicAttrModel>(content)).BasicAttrs;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return DataResponse;
        }

        // GET: Metodo para obtener los puntos en cada atributo basico
        public async Task<ListAttrPoints> GetAttrPoints()
        {
            // Lista para guarda la respuesta del servicio
            //List<BasicAttrModel> DataResponse = new List<BasicAttrModel>();
            ListAttrPoints DataResponse = new ListAttrPoints();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.GetAttrPoints, ref client);
            
            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetAttrPoints");

            // Hago la llamada al WS
            try
            {
                var response = await client.GetAsync(uri);

                // Guardo la respuesta del servidor
                DataResponse.StatusCode = response.StatusCode;

                // Espero una respuesta positiva del servidor (200)
                if (!response.IsSuccessStatusCode)
                    return DataResponse;

                
                var content = await response.Content.ReadAsStringAsync();
                // listAttrPoints.AttrPoints = JsonConvert.DeserializeObject<List<BasicAttrModel>>(content); -> Alberto

                // Deserializo la respuesta del servidor en un Json
                DataResponse.AttrPoints = (JsonConvert.DeserializeObject<ListAttrPoints>(content)).AttrPoints;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
                //throw new HttpExceptionEx(HttpStatusCode.BadRequest, "", false);
            }

            return  DataResponse; //listAttrPoints;
        }

        // GET: Metodo para obtener los puntos con detalles de cada usuario
        public async Task<ListDetailAttr> GetDetailCount()
        {
            // Lista para guarda la respuesta del servicio
            ListDetailAttr DataResponse = new ListDetailAttr();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.GetDetailCount, ref client);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetDetailCount");

            // Hago la llamada al WS
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
                 DataResponse.AttrDetail = (JsonConvert.DeserializeObject<ListDetailAttr>(content)).AttrDetail;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
            }

            return DataResponse;
        }

        // POST: Metodo para enviar un punto a un usuario
        public async Task<HttpResponseMessage> SendAttrPoint(UserModel argUser, BasicAttrModel argBasicAttr, string argMessage)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            // Construyo la URI a consultar
            var uri = GetUserToken(Constans.SendAttrPoint, ref client);

            // Creo el objeto que voy a Serializar
            var attrPoint = new SendAttrModel(argUser.userUUID, argBasicAttr.basicAttrID, argMessage);

            // Genero el Body de la peticion
            var BodyRequest = new StringContent(JsonConvert.SerializeObject(attrPoint), Encoding.UTF8, Constans.AplicationJson);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion SendAttrPoint");

            // Hago la llamada al WS
            try
            {
                response = await client.PostAsync(uri, BodyRequest);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                Crashes.TrackError(ex);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

       
    }
}
