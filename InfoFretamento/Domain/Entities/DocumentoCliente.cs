﻿using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Domain.Entities
{
    [Owned]
    public class DocumentoCliente
    {
        public string Documento { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
    }
}
