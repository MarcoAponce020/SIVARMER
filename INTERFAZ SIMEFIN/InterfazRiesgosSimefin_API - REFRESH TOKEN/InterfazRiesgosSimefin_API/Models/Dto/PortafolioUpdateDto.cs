﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterfazRiesgosSimefin_API.Models.Dto
{
    public class PortafolioUpdateDto
    {
        [Required]
        public int IdPortafolio { get; set; }
        public string F_Posicion { get; set; }

        [Required]
        [MaxLength(30)]
        public string NombrePortafolio { get; set; }
        [Required]
        public string SubPortafolio { get; set; }

        public string listaDatos { get; set; }

        public int No_Envio { get; set; }
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

    }
}
