using System;
using System.Collections.Generic;

namespace ApiPostgress.Modelo;

public partial class Plato
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public decimal Precio { get; set; }

    public bool Disponible { get; set; }

    public string? Descripcion { get; set; }

    public int? Calorias { get; set; }

    public int? Preparacion { get; set; }
}
