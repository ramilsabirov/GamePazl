namespace LabaPoTexProgrammirovanii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaveDatas", "second", c => c.Int(nullable: false));
            AddColumn("dbo.SaveDatas", "minute", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaveDatas", "minute");
            DropColumn("dbo.SaveDatas", "second");
        }
    }
}
