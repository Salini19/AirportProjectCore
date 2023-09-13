using AirportProjectCore.Controllers;
using AirportProjectCore.Models;
using AirportProjectCore.Repository;
using AirportProjectCore.Services;
using AirportProjectCore.Services.Implementation;
using Moq;
using System.Reflection;

namespace AirportFinder.Test
{
    public class AirportServiceTest
    {
        private readonly Mock<ICityService> _cityService;
        private readonly Mock<IRepository<AirportInfo>> _airrepo;
        public AirportServiceTest()
        {          
            _cityService = new Mock<ICityService>();
            _airrepo = new Mock<IRepository<AirportInfo>>();
        }

        [Fact]
        public void AirportListTest_Should_Return_Success()
        {

            //Arrange
            _airrepo.Setup(x => x.Get()).Returns(GetAirportsList());
            _cityService.Setup(x => x.Get()).Returns(GetCityInfoList());

            //Act
            AirportServices service = new AirportServices(_airrepo.Object,_cityService.Object);        
            var k = service.GetAirportList();

            //Assert
            Assert.True(k.Count() > 0);
        }

        [Theory]
        [InlineData("Chennai", "Salem")]
        public void GetAirportsBetweenCities_Return_Success(string from, string to)
        {
            //Arrange
            _airrepo.Setup(x => x.Get()).Returns(GetAirportsList());
            _cityService.Setup(x => x.Get()).Returns(GetCityInfoList());

            //Act
            AirportServices list = new AirportServices(_airrepo.Object, _cityService.Object);
            var k = list.GetAirportsBetweenCities(from,to);

            //Assert
            //Assert.NotNull(k);
            Assert.True(k.Count() > 0);

        }

        [Theory]
        [InlineData("TamilNadu")]
        public void GetAirportsByState(string id)
        {
            //Arrange
            _airrepo.Setup(x => x.Get()).Returns(GetAirportsList());
            _cityService.Setup(x => x.Get()).Returns(GetCityInfoList());
            //Act
            AirportServices list = new AirportServices(_airrepo.Object, _cityService.Object);
            var k = list.GetAirportsByState(id);

            //Assert
            Assert.True(k.Count() > 0);
        }

        private List<AirportInfo> GetAirportsList()
        {
            return new List<AirportInfo>
            {
                new AirportInfo(){AirportName="Chennai International Airport", City="Chennai",IataCode="MAA",State="TamilNadu",Lat=12.99000,Long=80.169296},               
                new AirportInfo(){AirportName="Coimbatore International Airport", City="Coimbatore",IataCode="CJB",State="TamilNadu",Lat=11.02999973,Long=77.04340363},
                new AirportInfo(){AirportName="Madurai Airport",City="Madurai",IataCode="IXM",State="TamilNadu",Lat=9.83450985,Long=78.09339905},
                new AirportInfo(){AirportName="Kovilpatti Airport",City="Nallatinputhur",IataCode="KPI",State="TamilNadu",Lat=9.15389,Long=77.821198},
                new AirportInfo(){AirportName="Neyveli Airport",City="Neyveli",IataCode="NVY",State="TamilNadu",Lat=11.61295605,Long=79.5273819},
                new AirportInfo(){AirportName="Tiruchirappalli International Airport",City="Tiruchirappalli",IataCode="TRZ",State="TamilNadu",Lat=10.766223,Long=78.71774}

            };
        }
        private List<CityInfo> GetCityInfoList()
        {
            return new List<CityInfo> {
            new CityInfo(){ City="Chennai",Lat=13.0836939,Long=80.270186 },
            new CityInfo(){ City="Salem",Lat=11.65212,Long=78.157982 }
            };
        }
       
    }
}