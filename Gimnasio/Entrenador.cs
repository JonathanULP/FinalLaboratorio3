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
    
    public partial class Entrenador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entrenador()
        {
            this.Entrenador_Actividad = new HashSet<Entrenador_Actividad>();
        }
    
        public int entrenador_id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dni { get; set; }
        public System.DateTime fecha { get; set; }
        public string sexo { get; set; }
        public string titulo { get; set; }
        public Nullable<bool> borrado_logico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entrenador_Actividad> Entrenador_Actividad { get; set; }
    }
}