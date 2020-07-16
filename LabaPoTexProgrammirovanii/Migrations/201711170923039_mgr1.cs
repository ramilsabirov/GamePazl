namespace LabaPoTexProgrammirovanii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reytings", "Time", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reytings", "Time", c => c.String());
        }
    }
}
