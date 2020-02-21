namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullsystem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.Users", new[] { "Project_ProjectId" });
            DropColumn("dbo.Users", "Project_ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Project_ProjectId", c => c.Int());
            CreateIndex("dbo.Users", "Project_ProjectId");
            AddForeignKey("dbo.Users", "Project_ProjectId", "dbo.Projects", "ProjectId");
        }
    }
}
