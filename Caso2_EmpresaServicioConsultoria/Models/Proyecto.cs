using System;
using System.Collections.Generic;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class Proyecto
{
    public Guid Id { get; set; }

    public Guid ClienteId { get; set; }

    public Guid ResponsableId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFinEstimada { get; set; }

    public decimal? PresupuestoTotal { get; set; }

    public string? Estado { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Comunicacione> Comunicaciones { get; set; } = new List<Comunicacione>();

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual ICollection<Hito> Hitos { get; set; } = new List<Hito>();

    public virtual Empleado Responsable { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
