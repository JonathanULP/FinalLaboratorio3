//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gimnasio
{
    using System;
    using System.Collections.Generic;
    
    public partial class Registro
    {
        public int registro_id { get; set; }
        public System.DateTime dia_ingreso { get; set; }
        public int cliente_id { get; set; }
        public Nullable<bool> borrado_logico { get; set; }
        public string hora_ingreso { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}
