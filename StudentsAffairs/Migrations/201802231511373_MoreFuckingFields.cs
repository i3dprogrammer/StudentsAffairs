namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreFuckingFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Total", c => c.Double());
            AlterColumn("dbo.Students", "JoinDate", c => c.DateTime());
            AlterColumn("dbo.Students", "HomeNumber", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "HomeNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "JoinDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "Total", c => c.Double(nullable: false));
        }
    }
}
