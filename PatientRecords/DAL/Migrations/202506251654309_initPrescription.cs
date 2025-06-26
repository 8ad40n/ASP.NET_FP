namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initPrescription : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Prescriptions", "PatientID");
            CreateIndex("dbo.Prescriptions", "DoctorID");
            AddForeignKey("dbo.Prescriptions", "DoctorID", "dbo.Doctors", "DoctorID", cascadeDelete: true);
            AddForeignKey("dbo.Prescriptions", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prescriptions", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Prescriptions", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.Prescriptions", new[] { "DoctorID" });
            DropIndex("dbo.Prescriptions", new[] { "PatientID" });
        }
    }
}
