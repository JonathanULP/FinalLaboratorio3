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
    
    public partial class Inscripcion
    {
        public int inscripcion_id { get; set; }
        public int cliente_id { get; set; }
        public int actividad_id { get; set; }
        public int plan_id { get; set; }
        public System.DateTime fecha_inicio { get; set; }
        public Nullable<int> cant_dias { get; set; }
        public bool activo { get; set; }
        public Nullable<bool> borrado_logico { get; set; }
    
        public virtual Actividad Actividad { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Plaan Plaan { get; set; }

        // los atributos activo y borrado_logico cumplen la misma funcion. Solo utilizare activo
    }
}
