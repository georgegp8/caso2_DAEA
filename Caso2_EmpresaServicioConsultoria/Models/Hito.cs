using System;
using System.Collections.Generic;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class Hito
{
    public Guid Id { get; set; }

    public Guid ProyectoId { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaPlanificada { get; set; }

    public bool? Completado { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
