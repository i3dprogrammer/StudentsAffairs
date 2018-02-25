namespace StudentsAffairs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Sex = c.Int(nullable: false),
                        Nationality = c.String(),
                        Religion = c.Int(nullable: false),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(),
                        PersonalCardId = c.String(),
                        CivilRegistry = c.String(),
                        AcademicQualificationAndDate = c.String(),
                        Total = c.Double(),
                        Speciality = c.String(),
                        StatusOfConstraint = c.String(),
                        ContantMethod = c.String(),
                        JoinDate = c.DateTime(),
                        PlaceOfResidence = c.String(),
                        HomeNumber = c.Int(),
                        StreetNumber = c.String(),
                        OtherNotes = c.String(),
                        Department = c.Int(nullable: false),
                        Group = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
