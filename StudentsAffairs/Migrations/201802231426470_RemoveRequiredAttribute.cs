namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "ContantMethod", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "ContantMethod", c => c.String(nullable: false));
        }
    }
}
