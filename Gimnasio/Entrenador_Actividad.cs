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
    
    public partial class Entrenador_Actividad
    {
        public int entre_act_id { get; set; }
        public int actividad_id { get; set; }
        public int entrenador_id { get; set; }
    
        public virtual Actividad Actividad { get; set; }
        public virtual Entrenador Entrenador { get; set; }
    }
}