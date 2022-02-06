using iNTEGRA_Wilber_Jorge.Data;
using iNTEGRA_Wilber_Jorge.DTO;
using iNTEGRA_Wilber_Jorge.Interfaces;
using iNTEGRA_Wilber_Jorge.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace iNTEGRA_Wilber_Jorge.Services
{
    public class EmpleadosService : IEmpleadosService
    {
        private readonly Prueba_IntegraContext _context;
        private readonly IWebHostEnvironment _web;
        private string PathFile = string.Empty;
        private readonly string[] ExtensionsFoto = new string[] { ".jpg", ".png", ".jpeg" };
        private string mensaje = string.Empty;

        public EmpleadosService(Prueba_IntegraContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _web = webHostEnvironment;
        }
        #region CONSULTAS EMPLEADOS
        public async Task<List<Empleados>> ObtenerEmpleados()
        {
            return await _context.Empleados.ToListAsync();
        }
        public async Task<List<Empleados>> ObtenerEmpleados_Email_Apellido(string campo)
        {
            return await _context.Empleados
                .Where(x => x.Apellidos.Contains(campo) || x.Correo.Contains(campo)).ToListAsync();
        }
        public Empleados EmpleadoExiste(EmpleadosDTO emp)
        {
            return _context.Empleados
                .Where(x =>
                (x.Nombres == emp.Nombres && x.Apellidos == emp.Apellidos) ||
                x.Telefono == emp.Telefono || x.Correo == emp.Correo).FirstOrDefault();
        }
        public async Task<bool> EmpleadoExistsAsync(int id_emplead)
        {
            return await Task.FromResult(_context.Empleados.Any(e => e.IdEmpleado == id_emplead));
        }
        public Empleados EmpleadoExisteActualizar(EmpleadosDTO emp)
        {
            return _context.Empleados
                .Where(x =>
                ((x.Nombres == emp.Nombres && x.Apellidos == emp.Apellidos) ||
                x.Telefono == emp.Telefono || x.Correo == emp.Correo) && x.IdEmpleado != emp.IdEmpleado).FirstOrDefault();
        }
        public async Task<Empleados> RegistroEmpleadoAsync(int? id_emplead)
        {
            return await _context.Empleados.FindAsync(id_emplead);
        }
        #endregion

        #region FUNCIONES EMPLEADO
        public async Task<string> GuardarEmpleadoAsync(EmpleadosDTO empleados)
        {
            PathFile = Path.Combine(_web.WebRootPath, "img\\Empleado\\" + empleados.Foto.FileName);
            mensaje = await ComprobarArchivo(empleados.Foto);
            if (!string.IsNullOrEmpty(mensaje))
            {
                return await Task.FromResult(mensaje);
            }
            try
            {
                Empleados emp = new Empleados()
                {
                    Apellidos = empleados.Apellidos,
                    Nombres = empleados.Nombres,
                    Telefono = empleados.Telefono,
                    Correo = empleados.Correo,
                    Foto = empleados.Foto.FileName,
                    FechaContratacion = empleados.FechaContratacion
                };
                _context.Add(emp);
                await _context.SaveChangesAsync();
                mensaje = string.Empty;
            }
            catch (Exception)
            {
                mensaje = "Algo salio mal";
            }
            return await Task.FromResult(mensaje);
        }
        public async Task<string> ActualizarEmpleadoAsync(EmpleadosDTO model)
        {
            if (model.Foto != null)
            {
                await EliminarFotoAsync(model.RutaImg);
                PathFile = Path.Combine(_web.WebRootPath, "img\\Empleado\\" + model.Foto.FileName);
                mensaje = await ComprobarArchivo(model.Foto);
                model.RutaImg = model.Foto.FileName;
            }
            if (!string.IsNullOrEmpty(mensaje))
            {
                return await Task.FromResult(mensaje);
            }
            try
            {
                Empleados emp = new()
                {
                    IdEmpleado = model.IdEmpleado,
                    Apellidos = model.Apellidos,
                    Nombres = model.Nombres,
                    Telefono = model.Telefono,
                    Correo = model.Correo,
                    Foto = model.RutaImg,
                    FechaContratacion = model.FechaContratacion
                };
                _context.Update(emp);
                await _context.SaveChangesAsync();
                mensaje = string.Empty;
            }
            catch (Exception)
            {
                mensaje = "Algo salio mal";
            }
            return await Task.FromResult(mensaje);
        }
        public async Task<string> EliminarEmpleadoAsync(Empleados model)
        {
            try
            {
                _context.Empleados.Remove(model);
                await _context.SaveChangesAsync();
                await EliminarFotoAsync(model.Foto);
                mensaje = string.Empty;
            }
            catch (Exception)
            {
                mensaje = "Algo salio mal intentalo mas tarde";
            }
            return await Task.FromResult(mensaje);
        }
        #endregion

        #region VALIDA ARCHIVO
        public Task<bool> SubirArchivo(IFormFile archivo)
        {
            bool subir = false;

            if (archivo != null)
            {
                using FileStream fileStream = File.Create(PathFile);
                archivo.CopyTo(fileStream);
                fileStream.Flush();
                subir = true;
            }
            return Task.FromResult(subir);
        }
        public Task<bool> ValidarFotoEmpleado(string Foto)
        {
            bool valida = false;
            if (!ExtensionsFoto.Contains(Path.GetExtension(Foto)))
                valida = true;
            return Task.FromResult(valida);
        }
        public Task<bool> ExisteArchivo(string archivo)
        {
            bool existe = false;
            if (!string.IsNullOrEmpty(archivo) && File.Exists(archivo))
                existe = true;
            return Task.FromResult(existe);
        }
        public async Task<string> ComprobarArchivo(IFormFile foto)
        {
            if (await ValidarFotoEmpleado(PathFile))
            {
                mensaje = "El formato de la imagen debe ser .jpg | .jpeg | .png";
            }
            if (await ExisteArchivo(PathFile))
            {
                mensaje = "Ya existe una foto con este Nombres";
            }
            if (!await SubirArchivo(foto))
            {
                mensaje = "La imagen no fue cargada o no existe la ruta";
            }
            return await Task.FromResult(mensaje);
        }

        public async Task<bool> EliminarFotoAsync(string NombreFoto)
        {
            bool result = true;
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\Empleado", NombreFoto);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return await Task.FromResult(result);
        }
        #endregion
    }
}

