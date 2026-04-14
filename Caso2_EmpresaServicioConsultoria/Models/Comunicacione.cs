using System;
using System.Collections.Generic;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class Comunicacione
{
    public Guid Id { get; set; }

    public Guid ProyectoId { get; set; }

    public Guid? EmpleadoId { get; set; }

    public string? Tipo { get; set; }

    public string? Asunto { get; set; }

    public string Contenido { get; set; } = null!;

    public DateTime? FechaHora { get; set; }

    public virtual Empleado? Empleado { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
