using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Travel.Easy.Electric.Vehicles.Automation.Common;
using Travel.Easy.Electric.Vehicles.Automation.Facades;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.EVModels;
using TravelEasy.EV.DataLayer;

namespace Travel.Easy.Electric.Vehicles.Automation.API.Tests.Definitions
{
    [Binding]
    public class CarsSteps
    {
        private readonly CarsFacade _carsFacade;
        private readonly ScenarioContext _scenarioContext;
        private readonly ElectricVehiclesContext _dbContext;

        public CarsSteps(CarsFacade carsFacade, ScenarioContext scenarioContext, ElectricVehiclesContext dbContext)
        {
            _carsFacade = carsFacade;
            _scenarioContext = scenarioContext;
            _dbContext = dbContext;
        }

        [StepDefinition(@"(\d+) cars are created and saved into db")]
        public void CarsAreCreatedAndSavedIntoDb(int carsNumber)
        {
            var brands = Constants.CarModels.Brands;
            var models = Constants.CarModels.Models;
            var cars = new List<ElectricVehicle>();
            Random random = new Random();

            for (int i = 0; i < carsNumber; i++)
            {
                int brandIndex = random.Next(0, brands.Count - 1);
                int modelIndex = random.Next(0, models.Count - 1);

                var car = new ElectricVehicle()
                {
                    Brand = brands[brandIndex],
                    HorsePowers = random.Next(100, 200),
                    Model = models[modelIndex],
                    PricePerDay = random.Next(50, 120),
                    Range = random.Next(100, 500)
                };

                cars.Add(car);
            }

            _dbContext.ElectricVehicles.AddRange(cars);
            _dbContext.SaveChanges();

            _scenarioContext.Set(cars.First(), Constants.Data.Car);
            _scenarioContext.Set(cars, Constants.Data.Cars);
        }

        [StepDefinition(@"Response should contain the requested cars")]
        public void ResponseShouldContainTheRequestedCars()
        {
            var expectedCars = _scenarioContext.Get<List<ElectricVehicle>>(Constants.Data.Cars);
            var response = _scenarioContext.Get<HttpResponseMessage>(Constants.Data.Response);
            var content = response.Content.ReadAsStringAsync().Result;
            var actualCars = JsonConvert.DeserializeObject<List<AllEVResponseModel>>(content);

            Assert.IsNotNull(actualCars, "Cars should be returned");
            CollectionAssert.AreEquivalent(expectedCars.Select(x => x.Brand), actualCars.Select(x => x.Brand), "Brads are incorrect");
            CollectionAssert.AreEquivalent(expectedCars.Select(x => x.Model), actualCars.Select(x => x.Model), "Model are incorrect");
            CollectionAssert.AreEquivalent(expectedCars.Select(x => x.PricePerDay), actualCars.Select(x => x.PricePerDay), "PricePerDay are incorrect");
        }

        [StepDefinition(@"Response should contain the requested car")]
        public void ResponseShouldContainTheRequestedCar()
        {
            var expectedCar = _scenarioContext.Get<ElectricVehicle>(Constants.Data.Car);
            var response = _scenarioContext.Get<HttpResponseMessage>(Constants.Data.Response);
            var content = response.Content.ReadAsStringAsync().Result;
            var actualCar = JsonConvert.DeserializeObject<AllEVResponseModel>(content);

            Assert.IsNotNull(actualCar, "Cars should be returned");
            Assert.AreEqual(expectedCar.Brand, actualCar.Brand, "Brads are incorrect");
            Assert.AreEqual(expectedCar.Model, actualCar.Model, "Model are incorrect");
            Assert.AreEqual(expectedCar.PricePerDay, actualCar.PricePerDay, "Brads are incorrect");
        }

        [StepDefinition(@"GetCars endpoint is requested")]
        public void GetCarsEndpointIsRequested()
        {
            var user = _scenarioContext.Get<User>(Constants.Data.User);
            var response = _carsFacade.GetCars(user.Id);
            _scenarioContext.Set(response, Constants.Data.Response);
        }

        [StepDefinition(@"GetCars endpoint is requested with wrong user")]
        public void GetCarsEndpointIsRequestedWithWrongUser()
        {
            Random random = new Random();
            var randomNumber = random.Next(1, 100);
            var response = _carsFacade.GetCars(randomNumber);
            _scenarioContext.Set(response, Constants.Data.Response);
        }

        [StepDefinition(@"GetCar endpoint is requested")]
        public void GetCarEndpointIsRequested()
        {
            //var carId = _scenarioContext.Get<EVRequestModel>(Constants.Data.Car).Id;
            var user = _scenarioContext.Get<User>(Constants.Data.User);
            var carId = _scenarioContext.Get<ElectricVehicle>(Constants.Data.Car).Id;
            var response = _carsFacade.GetCar(carId, user.Id);
            _scenarioContext.Set(response, Constants.Data.Response);
        }

        [StepDefinition(@"The status code of the response should be (\d+)")]
        public void TheStatusCodeOfTheResponseShouldBe(int statusCode)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>(Constants.Data.Response);
            int actualStatusCode = (int)response.StatusCode;
            int expectedStatusCode = statusCode;

            if (!response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                TestContext.WriteLine(content);
            }

            Assert.AreEqual(expectedStatusCode, actualStatusCode, "Response status code is incorrect");
        }
    }
}
