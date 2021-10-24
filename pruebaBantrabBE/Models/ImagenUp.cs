using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pruebaBantrabBE.Models
{
    public class ImagenUp
    {
        [Key]
        public int id { get; set; }

        public string nombre { get; set; }

        public IFormFile base64 { get; set; }

        public string mensaje { get; set; }

        public string created_at { get; set; }

        public string updated_at { get; set; }
    }
}
