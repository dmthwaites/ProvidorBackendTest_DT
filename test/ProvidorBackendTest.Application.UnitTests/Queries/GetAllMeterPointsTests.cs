using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProvidorBackendTest.Application.Queries;
using ProvidorBackendTest.Domain.Entities;
using ProvidorBackendTest.Persistance.Repositories;
using Xunit;

namespace ProvidorBackendTest.Application.UnitTests.Queries
{
    public class GetAllMeterPointsTests
    {
        private readonly Fixture _autoFixture;
        private readonly Mock<ILogger<GetAllMeterPoints.Handler>> _mockLogger;
        private readonly Mock<IMeterPointRepository> _mockRepository;

        public GetAllMeterPointsTests()
        {
            _autoFixture = new Fixture();
            _mockLogger = new Mock<ILogger<GetAllMeterPoints.Handler>>();
            _mockRepository = new Mock<IMeterPointRepository>();
        }

        [Fact]
        public async Task ReturnsMeterPoints_When_MeterPointsFound()
        {
            // Arrange
            var meterPoints = _autoFixture.CreateMany<MeterPoint>(3);
            var query = new GetAllMeterPoints.Query();

            _mockRepository.Setup(_ => _.GetAllMeterPoints()).Returns(meterPoints.AsQueryable());

            var sut = new GetAllMeterPoints.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(meterPoints);
        }

        [Fact]
        public async Task ReturnsEmpty_When_NoMeterPointsFound()
        {
            // Arrange
            var query = new GetAllMeterPoints.Query();

            _mockRepository.Setup(_ => _.GetAllMeterPoints()).Returns(new List<MeterPoint>().AsQueryable());

            var sut = new GetAllMeterPoints.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeEmpty();
        }
    }
}