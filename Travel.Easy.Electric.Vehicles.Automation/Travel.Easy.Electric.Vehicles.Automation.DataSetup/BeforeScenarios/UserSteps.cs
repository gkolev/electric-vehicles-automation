using TechTalk.SpecFlow;
using Travel.Easy.Electric.Vehicles.Automation.Common;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;

namespace Travel.Easy.Electric.Vehicles.Automation.DataSetup.BeforeScenarios
{
    [Binding]
    public class UserSteps
    {
        private readonly ElectricVehiclesContext _dbContext;
        private readonly ScenarioContext _scenarioContext;
        public UserSteps(ElectricVehiclesContext dbContext, ScenarioContext scenarioContext)
        {
            _dbContext = dbContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario(Constants.Data.User)]
        public void CreateUser()
        {
            var user = new User()
            {
                Username = "TestUser",
                Password = "password",
                Email = "test@test.com"
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            _scenarioContext.Set(user, Constants.Data.User);
        }
    }
}
