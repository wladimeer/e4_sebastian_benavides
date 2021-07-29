namespace Principal.Models
{
    using System.Collections.Generic;

    public partial class Client
    {
        public Client()
        {
            this.Teams = new HashSet<Team>();
        }
    
        public int id { get; set; }
        public string rut { get; set; }
        public string name { get; set; }
        public string lastnames { get; set; }
        public string phone { get; set; }
    
        public virtual ICollection<Team> Teams { get; set; }
    }
}
