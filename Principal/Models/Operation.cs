namespace Principal.Models
{
    using System.Collections.Generic;

    public partial class Operation
    {
        public Operation()
        {
            this.RoleOperations = new HashSet<RoleOperation>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int module_id { get; set; }
    
        public virtual Module Module { get; set; }

        public virtual ICollection<RoleOperation> RoleOperations { get; set; }
    }
}
