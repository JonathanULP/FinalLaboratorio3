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
    
    public partial class Plaan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plaan()
        {
            this.Inscripcion = new HashSet<Inscripcion>();
        }
    
        public int plan_id { get; set; }
        public int cant_dias { get; set; }
        public Nullable<System.DateTime> fecha_limite { get; set; }
        public Nullable<bool> borrado_logico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inscripcion> Inscripcion { get; set; }
    }
}
