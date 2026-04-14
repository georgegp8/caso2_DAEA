using System;
using System.Collections.Generic;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class Tarea
{
    public Guid Id { get; set; }

    public Guid ProyectoId { get; set; }

    public Guid? AsignadoA { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Prioridad { get; set; }

    public short? ProgresoPct { get; set; }

    public string? Estado { get; set; }

    public virtual Empleado? AsignadoANavigation { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
