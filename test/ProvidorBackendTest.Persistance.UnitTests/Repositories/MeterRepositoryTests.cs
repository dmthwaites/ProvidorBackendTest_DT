using System.Linq;
using AutoFixture;
using FluentAssertions;
using ProvidorBackendTest.Domain.Entities;
using ProvidorBackendTest.Persistance.Repositories;
using Xunit;

namespace ProvidorBackendTest.Persistance.UnitTests.Repositories
{
    public class MeterRepositoryTests
    {
        private readonly Fixture _autoFixture;

        public MeterRepositoryTests()
        {
            _autoFixture = new Fixture();
        }

        [Fact]
        public void GetAllMetersReturnsMeters_When_MetersAvailable()
        {
            // Arrange
            var meters = _autoFixture.CreateMany<Meter>();

            var repository = new MeterRepository();
            repository.InitialiseData(meters);

            // Act
            var result = repository.GetAllMeters();

            // Assert
            result.Should().BeEquivalentTo(meters);
        }

        [Fact]
        public void GetAllMetersReturnsEmpty_When_NoMetersFound()
        {
            // Arrange
            var repository = new MeterRepository();

            // Act
            var result = repository.GetAllMeters();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetMeterReturnsMeter_When_MeterTypeFound()
        {
            // Arrange
            var meters = _autoFixture.CreateMany<Meter>();
            var meter = meters.First();

            var repository = new MeterRepository();
            repository.InitialiseData(meters);

            // Act
            var result = repository.GetMeter(meter.MeterType);

            // Assert
            result.Should().BeEquivalentTo(meter);
        }

        [Fact]
        public void GetMeterReturnsNull_When_MeterTypeNotFound()
        {
            // Arrange
            var meters = _autoFixture.CreateMany<Meter>();

            var repository = new MeterRepository();
            repository.InitialiseData(meters);

            // Act
            var result = repository.GetMeter(_autoFixture.Create<string>());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetMeterReturnsMeter_When_MeterTypeCaseDoesntMatch()
        {
            // Arrange
            var meters = _autoFixture.CreateMany<Meter>().ToList();
            var meter = new Meter("type", _autoFixture.Create<string>(), _autoFixture.Create<string>());
            meters.Add(meter);

            var repository = new MeterRepository();
            repository.InitialiseData(meters);

            // Act
            var result = repository.GetMeter("TYPE");

            // Assert
            result.Should().BeEquivalentTo(meter);
        }
    }
}
