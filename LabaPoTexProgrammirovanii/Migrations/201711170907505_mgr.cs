namespace LabaPoTexProgrammirovanii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reytings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.SaveDatas",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Nw = c.Int(nullable: false),
            //            Nh = c.Int(nullable: false),
            //            Ex = c.Int(nullable: false),
            //            Ey = c.Int(nullable: false),
            //            Picture = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SaveDatas");
            DropTable("dbo.Reytings");
        }
    }
}
