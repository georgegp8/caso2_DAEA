using System;
using System.Collections.Generic;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class Empleado
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Cargo { get; set; }

    public string? Rol { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Comunicacione> Comunicaciones { get; set; } = new List<Comunicacione>();

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
