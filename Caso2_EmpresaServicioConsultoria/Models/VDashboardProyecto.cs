using System;
using System.Collections.Generic;

namespace Caso2_EmpresaServicioConsultoria.Models;

public partial class VDashboardProyecto
{
    public Guid? Id { get; set; }

    public string? Proyecto { get; set; }

    public string? Cliente { get; set; }

    public string? LiderProyecto { get; set; }

    public decimal? Presupuesto { get; set; }

    public decimal? GastoEjecutado { get; set; }

    public decimal? Saldo { get; set; }

    public decimal? AvanceTareasPct { get; set; }

    public string? Estado { get; set; }
}
