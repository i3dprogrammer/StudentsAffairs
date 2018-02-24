namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "BirthPlace", c => c.String());
            AlterColumn("dbo.Students", "StatusOfConstraint", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "StatusOfConstraint", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "BirthPlace", c => c.String(nullable: false));
        }
    }
}
