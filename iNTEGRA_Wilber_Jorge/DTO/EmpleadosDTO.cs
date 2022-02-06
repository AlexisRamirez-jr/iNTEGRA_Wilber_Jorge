using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iNTEGRA_Wilber_Jorge.DTO
{
    public class EmpleadosDTO
    {
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "Debe ingresar los apellidos del empleado")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Nombres { get; set; }

        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "Debe ingresar el número de teléfono")]
        [RegularExpression(@"^(\+0?1\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Ingrese un numero de telefono valido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Debe ingresar correo electrónico")]
        [EmailAddress(ErrorMessage = "Direccion de correo no valida")]
        public string Correo { get; set; }


        [Required(ErrorMessage = "Debe ingresar una imagen")]
        public IFormFile Foto { get; set; }

        [DisplayName("Fecha de contratación")]
        [Required(ErrorMessage = "Debe ingresar la fecha de contratación del empleado")]
        [DataType(DataType.DateTime, ErrorMessage = "Valor invalido")]
        //Formato solicitado por el cliente
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaContratacion { get; set; }
        public string FormatoFecha { get; set; }

        public string RutaImg { get; set; }
    }
}
