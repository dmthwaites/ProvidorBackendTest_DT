using System.Linq;
using AutoFixture;
using FluentAssertions;
using ProvidorBackendTest.Domain.Entities;
using ProvidorBackendTest.Persistance.Repositories;
using Xunit;

namespace ProvidorBackendTest.Persistance.UnitTests.Repositories
{
    public class MeterPointRepositoryTests
    {
        private readonly Fixture _autoFixture;

        public MeterPointRepositoryTests()
        {
            _autoFixture = new Fixture();
        }

        [Fact]
        public void GetAllMeterPointsReturnsMeterPoints_When_MeterPointsAvailable()
        {
            // Arrange
            var meterPoints = _autoFixture.CreateMany<MeterPoint>(3);

            var repository = new MeterPointRepository();
            repository.InitialiseData(meterPoints);

            // Act
            var result = repository.GetAllMeterPoints();

            // Assert
            result.Should().BeEquivalentTo(meterPoints);
        }

        [Fact]
        public void GetAllMeterPointsReturnsEmpty_When_NoMeterPoints()
        {
            // Arrange
            var repository = new MeterPointRepository();

            // Act
            var result = repository.GetAllMeterPoints();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetMeterPointReturnsMeterPoint_When_MpanFound()
        {
            // Arrange
            var meterPoints = _autoFixture.CreateMany<MeterPoint>(3);
            var meterPoint = meterPoints.First();

            var repository = new MeterPointRepository();
            repository.InitialiseData(meterPoints);

            // Act
            var result = repository.GetMeterPoint(meterPoint.Mpan);

            // Assert
            result.Should().BeEquivalentTo(meterPoint);
        }

        [Fact]
        public void GetMeterPointReturnsNull_When_MpanNotFound()
        {
            // Arrange
            var meterPoints = _autoFixture.CreateMany<MeterPoint>(3);

            var repository = new MeterPointRepository();
            repository.InitialiseData(meterPoints);

            // Act
            var result = repository.GetMeterPoint(_autoFixture.Create<string>());

            // Assert
            result.Should().BeNull();
        }
    }
}
