using System;
using System.Collections.Generic;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class Gasto
{
    public Guid Id { get; set; }

    public Guid ProyectoId { get; set; }

    public string Concepto { get; set; } = null!;

    public decimal Monto { get; set; }

    public string? Categoria { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
