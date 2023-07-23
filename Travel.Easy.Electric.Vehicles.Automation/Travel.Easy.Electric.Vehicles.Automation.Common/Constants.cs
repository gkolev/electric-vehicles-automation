namespace Travel.Easy.Electric.Vehicles.Automation.Common
{
    public class Constants
    {
        public static class Endpoints
        {
        }

        public static class Data
        {
            public const string Response = "Response";
            public const string Car = "Car";
            public const string Cars = "Cars";
            public const string User = "User";
            public const string UserRegisterRequestModel = "UserRegisterRequestModel";
            public const string UserLoginRequestModel = "UserLoginRequestModel";
        }

        public static class CarModels
        {
            public static readonly List<string> Brands = new List<string>()
            {
                "VW",
                "Mercedes",
                "BMW",
                "Skoda",
                "Audi"
            };

            public static readonly List<string> Models = new List<string>()
            {
                "V50",
                "R100",
                "J1000",
                "M40",
                "C900"
            };
        }
    }
}
