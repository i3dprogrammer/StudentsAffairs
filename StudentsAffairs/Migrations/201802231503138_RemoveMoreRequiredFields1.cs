namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMoreRequiredFields1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "StreetNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "StreetNumber", c => c.String(nullable: false));
        }
    }
}
