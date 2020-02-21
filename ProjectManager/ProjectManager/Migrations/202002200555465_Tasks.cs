namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskDesc = c.String(),
                        DateCreated = c.String(),
                        DateFinished = c.String(),
                        Status = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectDesc = c.String(),
                        DateCreated = c.String(),
                        DateFinished = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.TaskUsers",
                c => new
                    {
                        Task_TaskId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_TaskId, t.User_UserId })
                .ForeignKey("dbo.Tasks", t => t.Task_TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Task_TaskId)
                .Index(t => t.User_UserId);
            
            AddColumn("dbo.Users", "Project_ProjectId", c => c.Int());
            CreateIndex("dbo.Users", "Project_ProjectId");
            AddForeignKey("dbo.Users", "Project_ProjectId", "dbo.Projects", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.TaskUsers", "Task_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Users", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.TaskUsers", new[] { "User_UserId" });
            DropIndex("dbo.TaskUsers", new[] { "Task_TaskId" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropIndex("dbo.Users", new[] { "Project_ProjectId" });
            DropColumn("dbo.Users", "Project_ProjectId");
            DropTable("dbo.TaskUsers");
            DropTable("dbo.Projects");
            DropTable("dbo.Tasks");
        }
    }
}
