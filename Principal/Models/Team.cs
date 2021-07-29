namespace Principal.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Team
    {
        public Team()
        {
            this.WorkOrders = new HashSet<WorkOrder>();
        }
    
        public int id { get; set; }
        public string description { get; set; }
        public string url_image { get; set; }
        public string problem { get; set; }
        public DateTime reception { get; set; }
        public int client_id { get; set; }
    
        public virtual Client Client { get; set; }
        
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
