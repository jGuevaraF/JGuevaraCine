using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public byte[] Password { get; set; } = null!;
}
