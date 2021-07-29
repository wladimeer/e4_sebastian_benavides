namespace Principal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class WorkOrder
    {
        public WorkOrder()
        {
            this.AdvanceWorkOrders = new HashSet<AdvanceWorkOrder>();
        }
    
        public int id { get; set; }

        [Required(ErrorMessage = "Ingresa un Tipo.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El Tipo Debe Tener de 5 a 50 Caracteres.")]
        public string type { get; set; }

        [Required(ErrorMessage = "Ingresa una Descripción.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "La Descripción Debe Tener de 5 a 250 Caracteres.")]
        public string description { get; set; }

        [Required(ErrorMessage = "Selecciona un Equipo.")]
        public int team_id { get; set; }

        public int user_id { get; set; }

        [Required(ErrorMessage = "Ingresa una Fecha.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public Nullable<bool> state { get; set; }
    
        public virtual ICollection<AdvanceWorkOrder> AdvanceWorkOrders { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
