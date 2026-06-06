using System;
using System.Collections.Generic;

namespace Models;

public partial class Notificacione
{
    public int IdUsuario { get; set; }

    public int IdNotificacion { get; set; }

    public string? Mensaje { get; set; }

    public bool Leida { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
