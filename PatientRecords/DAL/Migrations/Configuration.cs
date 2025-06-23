namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            for (int i = 0; i < 10; i++)
            {
                // Generate random DateOfBirth (DOB) within a specific date range (for example, 1980-2000)
                var random = new Random();
                var startDate = new DateTime(1980, 1, 1);
                var endDate = new DateTime(2000, 12, 31);
                var range = endDate - startDate;
                var randomDays = random.Next(range.Days);
                var randomDOB = startDate.AddDays(randomDays);

                context.Patients.AddOrUpdate(
                    p => p.PatientID,  // Specify the property to check for duplicates (here it's PatientID)
                    new Models.Patient
                    {
                        PatientID = i + 1,
                        FirstName = "FirstName" + (i + 1),
                        LastName = "LastName" + (i + 1),
                        DOB = randomDOB,
                        Gender = (i % 2 == 0) ? "Male" : "Female",  // Alternating genders for simplicity
                        ContactNumber = "123-456-789" + (i + 1),
                        Email = "patient" + (i + 1) + "@example.com",
                        Address = "Address " + (i + 1),
                        DateJoined = DateTime.Now
                    });
            }
        }
    }
}
