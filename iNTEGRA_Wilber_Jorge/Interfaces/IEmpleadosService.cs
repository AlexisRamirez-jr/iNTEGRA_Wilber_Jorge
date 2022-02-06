using iNTEGRA_Wilber_Jorge.DTO;
using iNTEGRA_Wilber_Jorge.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iNTEGRA_Wilber_Jorge.Interfaces
{
    public interface IEmpleadosService
    {

        /// <summary>Interface <c>EmpleadoExistsAsync</c> 
        /// Listado de empleados
        /// .</summary>
        Task<List<Empleados>> ObtenerEmpleados();
        Task<List<Empleados>> ObtenerEmpleados_Email_Apellido(string campo);
  
        Empleados EmpleadoExiste(EmpleadosDTO empleado);
        /// <summary>Interface <c>EmpleadoExistsAsync</c> 
        /// Verificar si el empleado esta registado
        /// .</summary>
        Task<bool> EmpleadoExistsAsync(int id);
        /// <summary>Interface <c>RegistroEmpleadoAsync</c> 
        /// Obtiene los datos de un empleado por medio de su codigo
        /// .</summary>
        Task<Empleados> RegistroEmpleadoAsync(int? id);
        Empleados EmpleadoExisteActualizar(EmpleadosDTO empleado);
        /// <summary>Función <c>GuardarEmpleadoAsync</c> 
        /// Registra los datos de un empleados nuevo.
        /// .</summary>
        Task<string> GuardarEmpleadoAsync(EmpleadosDTO empleados);
        /// <summary>Función <c>ActualizarEmpleadoAsync</c> 
        /// Actualiza datos del empleados.
        /// Elimina foto que será sustituida.
        /// .</summary>
        Task<string> ActualizarEmpleadoAsync(EmpleadosDTO empleados);
        Task<bool> SubirArchivo(IFormFile archivo);
        /// <summary>Función <c>EliminarEmpleadoAsync</c> 
        /// Elimina el registro del empleados y su foto.
        /// .</summary>
        Task<string> EliminarEmpleadoAsync(Empleados model);
        Task<bool> ValidarFotoEmpleado(string Foto);
        /// <summary>Función <c>ExisteArchivo</c>
        /// Verifica la existencia de un archivo
        /// .</summary>
        Task<bool> ExisteArchivo(string archivo);
        /// <summary>Función <c>EliminarFotoAsync</c> 
        /// Elimina una foto con ingresar su nombre y extensión
        /// .</summary>
        Task<bool> EliminarFotoAsync(string NombreFoto);
    }
}
