namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredFieldsAgainn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Speciality", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Speciality", c => c.String(nullable: false));
        }
    }
}
