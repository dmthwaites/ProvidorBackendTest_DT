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
    public class GetAllMetersTests
    {
        private readonly Fixture _autoFixture;
        private readonly Mock<ILogger<GetAllMeters.Handler>> _mockLogger;
        private readonly Mock<IMeterRepository> _mockRepository;

        public GetAllMetersTests()
        {
            _autoFixture = new Fixture();
            _mockLogger = new Mock<ILogger<GetAllMeters.Handler>>();
            _mockRepository = new Mock<IMeterRepository>();
        }

        [Fact]
        public async Task ReturnsMeters_When_MetersFound()
        {
            // Arrange
            var meters = _autoFixture.CreateMany<Meter>(3);
            var query = new GetAllMeters.Query();

            _mockRepository.Setup(_ => _.GetAllMeters()).Returns(meters.AsQueryable());

            var sut = new GetAllMeters.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(meters);
        }

        [Fact]
        public async Task ReturnsEmpty_When_NoMetersFound()
        {
            // Arrange
            var query = new GetAllMeters.Query();

            _mockRepository.Setup(_ => _.GetAllMeters()).Returns(new List<Meter>().AsQueryable());

            var sut = new GetAllMeters.Handler(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = await sut.Handle(query, default);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
