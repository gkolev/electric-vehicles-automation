using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow;
using TravelEasy.EV.DataLayer;

namespace Travel.Easy.Electric.Vehicles.Automation.DataSetup.AfterScenarios
{
    [Binding]
    public class TearDown
    {
        private readonly ElectricVehiclesContext _dbContext;

        public TearDown(ElectricVehiclesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [AfterScenario]
        public void DeleteFromDatabase()
        {

            _dbContext.Database.ExecuteSqlRaw(@"DELETE FROM ElectricVehicles
                                                DELETE FROM Users");
        }
    }
}
