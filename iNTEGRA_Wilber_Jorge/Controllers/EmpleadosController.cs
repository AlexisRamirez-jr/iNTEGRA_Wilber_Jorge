using iNTEGRA_Wilber_Jorge.DTO;
using iNTEGRA_Wilber_Jorge.Interfaces;
using iNTEGRA_Wilber_Jorge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iNTEGRA_Wilber_Jorge.Controllers
{
    public class EmpleadosController : Controller
    {
        readonly IEmpleadosService _empleados;

        public EmpleadosController(IEmpleadosService empleado)
        {
            _empleados = empleado;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            List<EmpleadosDTO> model = new();
            var empleado = await _empleados.ObtenerEmpleados();

            foreach (var item in empleado)
            {
                model.Add(new EmpleadosDTO()
                {
                    IdEmpleado = item.IdEmpleado,
                    Apellidos = item.Apellidos,
                    Nombres = item.Nombres,
                    Telefono = item.Telefono,
                    Correo = item.Correo,
                    RutaImg = item.Foto,
                    FormatoFecha = item.FechaContratacion.ToString("MM/dd/yyyy")
                });
            }
            return View(model);
        }

        public async Task<IActionResult> EmpleadosPartialAsync(string valor)
        {

            
                List<EmpleadosDTO> model = new List<EmpleadosDTO>();
            List<Empleados> empleado = await _empleados.ObtenerEmpleados_Email_Apellido(valor);
           
                foreach (var item in empleado)
                {

                    model.Add(new EmpleadosDTO()
                    {
                        IdEmpleado = item.IdEmpleado,
                        Apellidos = item.Apellidos,
                        Nombres = item.Nombres,
                        Telefono = item.Telefono,
                        Correo = item.Correo,
                        RutaImg = item.Foto,
                        FormatoFecha = item.FechaContratacion.ToString("MM/dd/yyyy")
                    });
            }
            return PartialView("EmpleadosPartial", model);
        
        }

        // GET: EmpleadosController/Details/5
      
       public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empleados registro = await _empleados.RegistroEmpleadoAsync(id);
            EmpleadosDTO model = new EmpleadosDTO()
            {
                IdEmpleado = registro.IdEmpleado,
                Nombres = registro.Nombres,
                Apellidos = registro.Apellidos,
                Telefono = registro.Telefono,
                Correo = registro.Correo,
                RutaImg = registro.Foto,
                FormatoFecha = registro.FechaContratacion.ToString("MM/dd/yyyy")
            };
            return View(model);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Create([Bind("IdEmpleado,Apellidos,Nombres,Telefono,Correo,Foto,FechaContratacion")] EmpleadosDTO empleado)
        {
            if (ModelState.IsValid)
            {
                if (_empleados.EmpleadoExiste(empleado) == null)
                {
                    string result = await _empleados.GuardarEmpleadoAsync(empleado);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ModelState.AddModelError("CustomError", result);
                        return View(empleado);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("CustomError", "El empleado ya existe");
                }
            }
            return View(empleado);
        }
        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Empleados registro = await _empleados.RegistroEmpleadoAsync(id);
            EmpleadosDTO model = new EmpleadosDTO()
            {
                IdEmpleado = registro.IdEmpleado,
                Nombres = registro.Nombres,
                Apellidos = registro.Apellidos,
                Telefono = registro.Telefono,
                Correo = registro.Correo,
                RutaImg = registro.Foto,
                FechaContratacion = registro.FechaContratacion
            };
            return View(model);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,Apellidos,Nombres,Telefono,Correo,Foto,RutaImg,FechaContratacion")] EmpleadosDTO empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                return NotFound();
            }
            if (empleado.Foto == null && !string.IsNullOrEmpty(empleado.RutaImg) && ModelState.ErrorCount == 1)
            {
                ModelState.Clear();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_empleados.EmpleadoExisteActualizar(empleado) == null)
                    {
                        string result = await _empleados.ActualizarEmpleadoAsync(empleado);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ModelState.AddModelError("CustomError", result);
                            return View(empleado);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "El empleado ya existe");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _empleados.EmpleadoExistsAsync(empleado.IdEmpleado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }                
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empleado = await _empleados.RegistroEmpleadoAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _empleados.RegistroEmpleadoAsync(id);
            string result = await _empleados.EliminarEmpleadoAsync(empleado);
            if (!string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError("CustomError", result);
                return View(empleado);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
