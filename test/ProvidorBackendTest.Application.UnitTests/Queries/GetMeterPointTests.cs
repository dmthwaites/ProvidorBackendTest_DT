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
    public class GetMeterPointTests
    {
        private readonly Fixture _autoFixture;
        private readonly Mock<ILogger<GetMeterPoint.Handler>> _mockLogger;
        private readonly Mock<IMeterPointRepository> _mockRepository;

        public GetMeterPointTests()
        {
            _autoFixture = new Fixture();
            _mockLogger = new Mock<ILogger<GetMeterPoint.Handler>>();
            _mockRepository = new Mock<IMeterPointRepository>();
        }

        [Fact]
        public async Task ReturnsMeterPoint_When_MpanFound()
        {
            // Arrange
            var meterPoint = _autoFixture.Create<MeterPoint>();

            var query = new GetMeterPoint.Query(meterPoint.Mpan);

            _mockRepository.Setup(_ => _.GetMeterPoint(query.Mpan)).Returns(meterPoint);

            var sut = new GetMeterPoint.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(meterPoint);
        }

        [Fact]
        public async Task ReturnsNull_When_MpanNotFound()
        {
            // Arrange
            var query = new GetMeterPoint.Query(_autoFixture.Create<string>());

            _mockRepository.Setup(_ => _.GetMeterPoint(query.Mpan)).Returns<MeterPoint>(null);

            var sut = new GetMeterPoint.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeNull();
        }
    }
}
