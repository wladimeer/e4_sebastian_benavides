namespace Principal.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class AdvanceWorkOrder
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Ingresa una Descripción.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "La Descripción Debe Tener de 5 a 250 Caracteres.")]
        public string description { get; set; }

        [Required(ErrorMessage = "Ingresa una Fecha.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public int work_order_id { get; set; }
    
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
