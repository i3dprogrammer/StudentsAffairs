namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentAndGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Department", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Group", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Group");
            DropColumn("dbo.Students", "Department");
        }
    }
}
