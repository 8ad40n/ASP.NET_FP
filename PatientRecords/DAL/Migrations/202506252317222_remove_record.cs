namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_record : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalHistories", "PatientID", "dbo.Patients");
            DropIndex("dbo.MedicalHistories", new[] { "PatientID" });
            DropTable("dbo.MedicalHistories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedicalHistories",
                c => new
                    {
                        HistoryID = c.Int(nullable: false, identity: true),
                        Details = c.String(nullable: false, maxLength: 500),
                        HistoryDate = c.DateTime(nullable: false),
                        PatientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID);
            
            CreateIndex("dbo.MedicalHistories", "PatientID");
            AddForeignKey("dbo.MedicalHistories", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
        }
    }
}
