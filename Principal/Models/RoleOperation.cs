namespace Principal.Models
{

    public partial class RoleOperation
    {
        public int id { get; set; }
        public int role_id { get; set; }
        public int operation_id { get; set; }
    
        public virtual Operation Operation { get; set; }
        public virtual Role Role { get; set; }
    }
}
