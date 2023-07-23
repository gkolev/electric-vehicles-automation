using TechTalk.SpecFlow;
using Travel.Easy.Electric.Vehicles.Automation.Common;
using Travel.Easy.Electric.Vehicles.Automation.Facades;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.UserModels;

namespace Travel.Easy.Electric.Vehicles.Automation.API.Tests.Definitions
{
    [Binding]
    public class UsersSteps
    {
        private readonly UsersFacade _usersFacade;
        private readonly ScenarioContext _scenarioContext;

        public UsersSteps(UsersFacade usersFacade, ScenarioContext scenarioContext)
        {
            _usersFacade = usersFacade;
            _scenarioContext = scenarioContext;
        }

        [StepDefinition(@"GetUser endpoint is requested")]
        public void GetUserEndpointIsRequested()
        {
            var user = _scenarioContext.Get<User>(Constants.Data.User);
            var response = _usersFacade.GetUser(user.Id);
            _scenarioContext.Set(response, Constants.Data.Response);
        }

        [StepDefinition(@"UserRegisterRequestModel is created")]
        public void UserRegisterRequestModelIsCreated()
        {
            var user = new UserRegisterRequestModel()
            {
                Username = "testUser",
                Password = "securedPassword",
                Email = "test@test.com"
            };

            _scenarioContext.Set(user, Constants.Data.UserRegisterRequestModel);
        }

        [StepDefinition(@"PostRegisterUser endpoint is requested")]
        public void PostRegisterUserEndpointIsRequested()
        {
            var user = _scenarioContext.Get<UserRegisterRequestModel>(Constants.Data.UserRegisterRequestModel);
            var response = _usersFacade.PostRegisterUser(user);
            _scenarioContext.Set(response, Constants.Data.Response);
        }

        [StepDefinition(@"UserLoginRequestModel is created")]
        public void UserLoginRequestModelIsCreated()
        {
            var user = new UserLoginRequestModel()
            {
                Username = "testUser",
                Password = "securedPassword"
            };

            _scenarioContext.Set(user, Constants.Data.UserLoginRequestModel);
        }

        [StepDefinition(@"PostLoginUser endpoint is requested")]
        public void PostLoginUserEndpointIsRequested()
        {
            var user = _scenarioContext.Get<UserLoginRequestModel>(Constants.Data.UserLoginRequestModel);
            var response = _usersFacade.PostLoginUser(user);
            _scenarioContext.Set(response, Constants.Data.Response);
        }
    }
}
