using infoproDigitalTechTask.Models;
using infoproDigitalTechTask.Models.DBContext;

namespace infoproDigitalTechTask.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedData(QuoteManagementDBContext context)
        {
            if (context.Quote.Any()) return;

            var quotes = new List<Quote>
            {
                new Quote
                {
                    CustomerName = "Rafael Nadal", CustomerEmail = "rafael.nadal@rafanadal.com", DateCreated = DateTime.Now,
                    CustomerId = 1, Vrm = "XYZ1234", VehicleDescription = "Toyota Corolla 2020", Mileage = 25000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "ENG001", JobDescription = "Engine Repair", LabourTime = 5.0m, LabourRate = 80.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH001", PartDescription = "Engine Part A",
                                    Quantity = 2, UnitCost = 100.0m, DateCreated = DateTime.Now, PartType = "Mechanical" },
                                new FluidPart { PartNumber = "FLUID001", PartDescription = "Engine Oil",
                                    Quantity = 5000, UnitCost = 20.0m, DateCreated = DateTime.Now, PartType = "Fluid" }
                            }
                        },
                        new Job
                        {
                            JobCode = "BRK001", JobDescription = "Brake Replacement", LabourTime = 3.0m, LabourRate = 70.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH002", PartDescription = "Brake Pads",
                                    Quantity = 4, UnitCost = 50.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                        }
                    }
                },
                new Quote
                {
                    CustomerName = "Novak Djokovic", CustomerEmail = "novak.djokovic@novakdjokovic.com", DateCreated = DateTime.Now,
                    CustomerId = 2, Vrm = "ABC5678", VehicleDescription = "Honda Civic 2018", Mileage = 45000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "SERV001", JobDescription = "Full Service", LabourTime = 4.0m, LabourRate = 85.0m,
                            IsCustomerAuthorized = false, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new FluidPart { PartNumber = "FLUID002", PartDescription = "Coolant Fluid",
                                    Quantity = 2000, UnitCost = 15.0m, DateCreated = DateTime.Now, PartType = "Fluid" }
                            }
                        }
                    }
                },
                new Quote
                {
                    CustomerName = "Carlos Alcaraz", CustomerEmail = "carlos.alcaraz@carlosalcaraz.com", DateCreated = DateTime.Now,
                    CustomerId = 3, Vrm = "LMN2345", VehicleDescription = "Ford Focus 2019", Mileage = 30000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "ELC002", JobDescription = "Battery Replacement", LabourTime = 2.0m, LabourRate = 75.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH003", PartDescription = "Battery",
                                    Quantity = 1, UnitCost = 120.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                        }
                    }
                },
                new Quote
                {
                    CustomerName = "Roger Federer", CustomerEmail = "toger.federer@rogerfederer.com", DateCreated = DateTime.Now,
                    CustomerId = 4, Vrm = "FWE6532", VehicleDescription = "BMW X5 2021", Mileage = 15000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "TRN001", JobDescription = "Transmission Check", LabourTime = 6.0m, LabourRate = 100.0m,
                            IsCustomerAuthorized = false, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new FluidPart { PartNumber = "FLUID003", PartDescription = "Transmission Fluid",
                                    Quantity = 4000, UnitCost = 25.0m, DateCreated = DateTime.Now, PartType = "Fluid" }
                            }
                        },
                        new Job
                        {
                            JobCode = "ELC002", JobDescription = "Battery Replacement", LabourTime = 2.0m, LabourRate = 75.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH003", PartDescription = "Battery",
                                    Quantity = 1, UnitCost = 120.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                        }
                    }
                },
                new Quote
                {
                    CustomerName = "David Ferrer", CustomerEmail = "david.ferrer@davidferrer.com", DateCreated = DateTime.Now,
                    CustomerId = 5, Vrm = "EFG6789", VehicleDescription = "Seat Panda 1990", Mileage = 236000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "TRN001", JobDescription = "Transmission Check", LabourTime = 6.0m, LabourRate = 100.0m,
                            IsCustomerAuthorized = false, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new FluidPart { PartNumber = "FLUID003", PartDescription = "Transmission Fluid",
                                    Quantity = 4000, UnitCost = 25.0m, DateCreated = DateTime.Now, PartType = "Fluid" }
                            }
                        },
                        new Job
                        {
                            JobCode = "BRK001", JobDescription = "Brake Replacement", LabourTime = 3.0m, LabourRate = 70.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now, PriceOverride = 250,
                            Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH002", PartDescription = "Brake Pads",
                                    Quantity = 4, UnitCost = 50.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                        }
                    }
                },
                new Quote
                {
                    CustomerName = "David Ferrer", CustomerEmail = "david.ferrer@davidferrer.com", DateCreated = DateTime.Now,
                    CustomerId = 5, Vrm = "EFG6789", VehicleDescription = "Seat Panda 1990", Mileage = 236000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "TRN001", JobDescription = "Transmission Check", LabourTime = 6.0m, LabourRate = 100.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new FluidPart { PartNumber = "FLUID003", PartDescription = "Transmission Fluid",
                                    Quantity = 4000, UnitCost = 25.0m, DateCreated = DateTime.Now, PartType = "Fluid" }
                            }
                        }
                    }
                }
            };

            context.Quote.AddRange(quotes);
            context.SaveChanges();
        }
    }
}

