namespace Principal.Models
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class RepairEverythingEntities : DbContext
    {
        public RepairEverythingEntities() : base("name=RepairEverythingEntities")
        {}
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdvanceWorkOrder> AdvanceWorkOrders { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleOperation> RoleOperations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
    }
}
