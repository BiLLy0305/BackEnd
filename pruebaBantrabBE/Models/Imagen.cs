using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pruebaBantrabBE.Models
{
    public class Imagen
    {
        [Key]
        public int id { get; set; }
      
        public string nombre { get; set; }

        public string  base64 { get; set; }

        public string created_at { get; set; }

        public string updated_at { get; set; }

    }
}
