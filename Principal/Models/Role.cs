namespace Principal.Models
{
    using System.Collections.Generic;

    public partial class Role
    {
        public Role()
        {
            this.RoleOperations = new HashSet<RoleOperation>();
            this.Users = new HashSet<User>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<RoleOperation> RoleOperations { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
