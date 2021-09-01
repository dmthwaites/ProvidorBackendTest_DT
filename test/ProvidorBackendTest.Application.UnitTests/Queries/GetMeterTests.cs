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
    public class GetMeterTests
    {
        private readonly Fixture _autoFixture;
        private readonly Mock<ILogger<GetMeter.Handler>> _mockLogger;
        private readonly Mock<IMeterRepository> _mockRepository;

        public GetMeterTests()
        {
            _autoFixture = new Fixture();
            _mockLogger = new Mock<ILogger<GetMeter.Handler>>();
            _mockRepository = new Mock<IMeterRepository>();
        }

        [Fact]
        public async Task ReturnsMeter_When_MeterTypeFound()
        {
            // Arrange
            var meter = _autoFixture.Create<Meter>();

            var query = new GetMeter.Query(meter.MeterType);

            _mockRepository.Setup(_ => _.GetMeter(query.MeterType)).Returns(meter);

            var sut = new GetMeter.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(meter);
        }

        [Fact]
        public async Task ReturnsNull_When_MeterTypeNotFound()
        {
            // Arrange
            var query = new GetMeter.Query(_autoFixture.Create<string>());

            _mockRepository.Setup(_ => _.GetMeter(query.MeterType)).Returns<Meter>(null);

            var sut = new GetMeter.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeNull();
        }
    }
}
