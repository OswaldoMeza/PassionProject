namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Test", c => c.String());
        }
    }
}
