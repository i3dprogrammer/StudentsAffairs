namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMoreRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "PersonalCardId", c => c.String());
            AlterColumn("dbo.Students", "CivilRegistry", c => c.String());
            AlterColumn("dbo.Students", "PlaceOfResidence", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "PlaceOfResidence", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "CivilRegistry", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "PersonalCardId", c => c.String(nullable: false));
        }
    }
}
