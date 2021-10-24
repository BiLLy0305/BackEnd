using Microsoft.Graph;
using Newtonsoft.Json;
using pruebaBantrabBE.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pruebaBantrabBE.Servicios
{
    public class consumo
    {

        //Metodo para consultar las imagenes GET
        public async Task<List<Imagen>> getListaImagenes()
        {
            HttpClient cliente = new HttpClient
            {
                BaseAddress = new Uri("https://apitest-bt.herokuapp.com/api/v1/imagenes")
            };

            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Add("user", "User123");
            cliente.DefaultRequestHeaders.Add("password", "Password123");

            List<Imagen> lImagenes = new List<Imagen>();

            try
            {
                HttpResponseMessage response = await cliente.GetAsync("https://apitest-bt.herokuapp.com/api/v1/imagenes");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var images = System.Text.Json.JsonSerializer.Deserialize<List<Imagen>>(json);
                    lImagenes = images;
                }

                foreach (var im in lImagenes) {
                    if (!IsBase64(im.base64)) {
                        lImagenes.Remove(im);
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return lImagenes;
        }

        //Metodo para insertar una nueva imagen POST
        public async Task<Imagen> PostImage(Imagen requestObj)
        {
            // Initialization.  
            Imagen responseObj = new Imagen();

            try
            {
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("https://apitest-bt.herokuapp.com/api/v1/imagenes");

                    // Setting content type.                   
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("user", "User123");
                    client.DefaultRequestHeaders.Add("password", "Password123");

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    var myContent = JsonConvert.SerializeObject(requestObj);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    // HTTP POST  
                    response = await client.PostAsync("https://apitest-bt.herokuapp.com/api/v1/imagenes", byteContent).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<Imagen>(result);
                    }
                }
                return responseObj;
            }
            catch (Exception ex)
            {

            }
            return responseObj;
        }

        //Metodo para consultar las imagenes GET BY ID
        public async Task<Imagen> getImagenById(int id)
        {
            HttpClient cliente = new HttpClient
            {
               // BaseAddress = new Uri("https://apitest-bt.herokuapp.com/api/v1/imagenes/"+id)
            };

            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Add("user", "User123");
            cliente.DefaultRequestHeaders.Add("password", "Password123");

            Imagen objImagen = new Imagen();

            try
            {
                HttpResponseMessage response = await cliente.GetAsync("https://apitest-bt.herokuapp.com/api/v1/imagenes/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var images = System.Text.Json.JsonSerializer.Deserialize<Imagen>(json);
                    objImagen = images;
                }
            }
            catch (Exception ex)
            {

            }
            return objImagen;
        }

        //Metodo para modificar una imagen PUT
        public async Task<Imagen> PutImage(Imagen requestObj)
        {
            // Initialization.  
            Imagen responseObj = new Imagen();

            try
            {
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("https://apitest-bt.herokuapp.com/api/v1/imagenes");

                    // Setting content type.                   
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("user", "User123");
                    client.DefaultRequestHeaders.Add("password", "Password123");

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    var myContent = JsonConvert.SerializeObject(requestObj);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    // HTTP POST  
                    response = await client.PutAsync("https://apitest-bt.herokuapp.com/api/v1/imagenes/"+requestObj.id, byteContent).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<Imagen>(result);
                    }
                }
                return responseObj;
            }
            catch (Exception ex)
            {

            }
            return responseObj;
        }

        public bool IsBase64(string base64String)
        {
            if (base64String.Replace(" ", "").Length % 4 != 0) { return false; }
            try { Convert.FromBase64String(base64String); return true; }
            catch (Exception exception)
            {
            }
            return false;
        }

    }
}
