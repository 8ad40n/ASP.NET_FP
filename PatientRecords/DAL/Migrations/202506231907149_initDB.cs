namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentID = c.Int(nullable: false, identity: true),
                        AppointmentDate = c.DateTime(nullable: false),
                        Reason = c.String(nullable: false, maxLength: 200),
                        Status = c.String(nullable: false, maxLength: 20),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Specialization = c.String(nullable: false, maxLength: 100),
                        ContactNumber = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.DoctorID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 10),
                        ContactNumber = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 255),
                        DateJoined = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PatientID);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        DiagnosisID = c.Int(nullable: false, identity: true),
                        DiagnosisDate = c.DateTime(nullable: false),
                        DiagnosisDetails = c.String(nullable: false, maxLength: 500),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.MedicalHistories",
                c => new
                    {
                        HistoryID = c.Int(nullable: false, identity: true),
                        Details = c.String(nullable: false, maxLength: 500),
                        HistoryDate = c.DateTime(nullable: false),
                        PatientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationID = c.Int(nullable: false, identity: true),
                        MedicationName = c.String(nullable: false, maxLength: 100),
                        Dosage = c.String(nullable: false, maxLength: 20),
                        Duration = c.String(nullable: false, maxLength: 50),
                        PrescriptionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicationID)
                .ForeignKey("dbo.Prescriptions", t => t.PrescriptionID, cascadeDelete: true)
                .Index(t => t.PrescriptionID);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        PrescriptionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PrescriptionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medications", "PrescriptionID", "dbo.Prescriptions");
            DropForeignKey("dbo.MedicalHistories", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Diagnosis", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Diagnosis", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.Medications", new[] { "PrescriptionID" });
            DropIndex("dbo.MedicalHistories", new[] { "PatientID" });
            DropIndex("dbo.Diagnosis", new[] { "DoctorID" });
            DropIndex("dbo.Diagnosis", new[] { "PatientID" });
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Medications");
            DropTable("dbo.MedicalHistories");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Appointments");
        }
    }
}
