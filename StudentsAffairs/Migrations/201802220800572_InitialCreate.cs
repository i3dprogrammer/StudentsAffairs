namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Sex = c.Int(nullable: false),
                        Nationality = c.String(nullable: false),
                        Religion = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        BirthPlace = c.String(nullable: false),
                        PersonalCardId = c.String(nullable: false),
                        CivilRegistry = c.String(nullable: false),
                        AcademicQualificationAndDate = c.String(nullable: false),
                        Total = c.Double(nullable: false),
                        Speciality = c.String(nullable: false),
                        StatusOfConstraint = c.String(nullable: false),
                        ContantMethod = c.String(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        PlaceOfResidence = c.String(nullable: false),
                        HomeNumber = c.Int(nullable: false),
                        StreetNumber = c.String(nullable: false),
                        OtherNotes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
