namespace Principal.Models
{
    using System;
    using System.Collections.Generic;

    public partial class User
    {
        public User()
        {
            this.WorkOrders = new HashSet<WorkOrder>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime date { get; set; }
        public int role_id { get; set; }
    
        public virtual Role Role { get; set; }
        
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
