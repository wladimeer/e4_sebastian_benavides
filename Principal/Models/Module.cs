namespace Principal.Models
{
    using System.Collections.Generic;

    public partial class Module
    {
        public Module()
        {
            this.Operations = new HashSet<Operation>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
           
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
