using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pruebaBantrabBE.Models;
using pruebaBantrabBE.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pruebaBantrabBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        consumo servicio = new consumo();

        [HttpPost]
        public ImagenUp Post([FromForm] ImagenUp value)
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

            string s = "";
            if (nImagen.base64.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    nImagen.base64.CopyTo(ms);
                    var fb = ms.ToArray();
                    s = Convert.ToBase64String(fb);
                    // act on the Base64 data
                }
            }

            Imagen gImagen = new Imagen();
            gImagen.nombre = nImagen.nombre;
            gImagen.base64 = s;

            Imagen responseObj = servicio.PostImage(gImagen).Result;


            // Imagenes responseObj = servicio.PostInfo(nImagen).Result;

            return nImagen;

        }



    }
}
