namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAllRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Students", "Nationality", c => c.String());
            AlterColumn("dbo.Students", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.Students", "AcademicQualificationAndDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "AcademicQualificationAndDate", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "Nationality", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
