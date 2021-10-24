using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Newtonsoft.Json;
using pruebaBantrabBE.Models;
using pruebaBantrabBE.Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace pruebaBantrabBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        consumo servicio = new consumo();

        //GET  Obtener todas las empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imagen>>> getImagenes()
        {
            List<Imagen> lista = new List<Imagen>();
            lista = await servicio.getListaImagenes();

            return lista;

        }

        //Post  Agregar Una Empresa
        [HttpPost]
        public ImagenUp Post([FromForm] ImagenUp value)
        {
            /*
             *  Metodo enviando la Base64 solo como un string bcuscul 
            Imagenes nImagen = new Imagenes();

            nImagen = value;

            Imagenes responseObj = servicio.PostInfo(nImagen).Result;

            return responseObj;*/


            ImagenUp nImagen = new ImagenUp();

            nImagen = value;

            var postedFileExtension = Path.GetExtension(nImagen.base64.FileName);

            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                nImagen.mensaje = "Solo se permite imagenes";
                return nImagen;
            }

            string sb64 = "";
            if (nImagen.base64.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    nImagen.base64.CopyTo(ms);
                    var fb = ms.ToArray();
                    sb64 = Convert.ToBase64String(fb);
                }
            }

            Imagen gImagen = new Imagen();
            gImagen.nombre = nImagen.nombre;
            gImagen.base64 = sb64;

            Imagen responseObj = servicio.PostImage(gImagen).Result;

            nImagen.mensaje = "Imagen Guardada Exitosamente";
            return nImagen;


        }

        //GET  Obtener todas las empresas
        [HttpGet("{id}")]
        public async Task<ActionResult<Imagen>> getImagenById(int id)
        {

            Imagen objImagen = new Imagen();
         
            objImagen = await servicio.getImagenById(id);

            if (objImagen == null)
            {
                return NotFound();
            }

            return objImagen;

        }


        //Post  Agregar Una Empresa
        [HttpPut("{id}")]
        public ImagenUp Put([FromForm] ImagenUp value)
        {
            ImagenUp nImagen = new ImagenUp();

            nImagen = value;

            var postedFileExtension = Path.GetExtension(nImagen.base64.FileName);

            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                nImagen.mensaje = "Solo se permite imagenes";
                return nImagen;
            }

            string sb64 = "";
            if (nImagen.base64.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    nImagen.base64.CopyTo(ms);
                    var fb = ms.ToArray();
                    sb64 = Convert.ToBase64String(fb);
                }
            }

            Imagen gImagen = new Imagen();
            gImagen.nombre = nImagen.nombre;
            gImagen.base64 = sb64;

            Imagen responseObj = servicio.PutImage(gImagen).Result;

            nImagen.mensaje = "Imagen Modificada Exitosamente";
            return nImagen;

        }

    }
}
