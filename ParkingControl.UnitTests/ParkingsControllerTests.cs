using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingControl.Api.Controllers;
using ParkingControl.Api.DTOs;
using ParkingControl.Api.Entities;
using ParkingControl.Api.Repositories;
using Xunit;

namespace ParkingControl.UnitTests
{
  public class ParkingsControllerTests
  {
    private readonly Mock<IParkingsRepository> repositoryStub = new();
    private readonly Random rand = new();

    [Fact]
    public async Task GetParkingAsync_WithUnexistingParking_ReturnsNotFound()
    {
      // Arrange
      repositoryStub.Setup(
          repo => repo
          .GetParkingAsync(It.IsAny<int>()))
          .ReturnsAsync((Parking)null);

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var result = await controller.GetParkingAsync(rand.Next(1, 1000));

      // Assert
      result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetParkingAsync_WithExistingParking_ReturnsExpectedParking()
    {
      // Arrange
      var expectedParking = CreateRandomParking();

      repositoryStub.Setup(
          repo => repo
          .GetParkingAsync(It.IsAny<int>()))
          .ReturnsAsync(expectedParking);

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var result = await controller.GetParkingAsync(rand.Next(1, 1000));

      //Assert
      result.Value.Should().BeEquivalentTo(expectedParking);
    }

    [Fact]
    public async Task GetParkingsAsync_WithExistingParkings_ReturnsAllParkings()
    {
      // Arrange
      var expectedParkings = new[] {
            CreateRandomParking(),
            CreateRandomParking(),
            CreateRandomParking()
        };

      repositoryStub.Setup(repo => repo.GetParkingsAsync()).ReturnsAsync(expectedParkings);

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var actualParkings = await controller.GetParkingsAsync();

      // Assert
      actualParkings.Should().BeEquivalentTo(expectedParkings);
    }

    [Fact]
    public async Task GetParkingsAsync_WithMatchingPlates_ReturnsMatchingParkings()
    {
      // Arrange
      var allParkings = new[] {
            new Parking(){ Plate = "ABC-1234"},
            new Parking(){ Plate = "DEF-5678"},
            new Parking(){ Plate = "ABC-1234"}
        };

      var plateToMatch = "ABC-1234";

      repositoryStub.Setup(repo => repo.GetParkingsAsync()).ReturnsAsync(allParkings);

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      IEnumerable<ParkingDTO> foundParkings = await controller.GetParkingsAsync(plateToMatch);

      // Assert
      foundParkings.Should().OnlyContain(
        parking => parking.Plate == allParkings[0].Plate || parking.Plate == allParkings[2].Plate
      );
    }

    [Fact]
    public void GetParkings_WithExistingParkings_ReturnsAllParkings()
    {
      // Arrange
      var expectedParkings = new[] {
            CreateRandomParking(),
            CreateRandomParking(),
            CreateRandomParking()
        };

      repositoryStub.Setup(repo => repo.GetParkings()).Returns(expectedParkings);

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var actualParkings = controller.GetParkings();

      // Assert
      actualParkings.Should().BeEquivalentTo(
          expectedParkings,
          options => options.ComparingByMembers<Parking>());
    }

    [Fact]
    public async Task CreateParkingAsync_WithParkingToCreate_ReturnsCreatedParking()
    {
      // Arrange
      var parkingToCreate = new CreateParkingDTO("ABC-1234");

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var result = await controller.CreateParkingAsync(parkingToCreate);

      // Assert
      var createdParking = (result.Result as CreatedAtActionResult).Value as ParkingDTO;
      parkingToCreate.Should().BeEquivalentTo(
          createdParking,
          options => options.ComparingByMembers<ParkingDTO>().ExcludingMissingMembers());
    }

    [Fact]
    public async Task UpdateParkingOutAsync_WithExistingParking_ReturnsNoContent()
    {
      // Arrange
      var existingParking = CreateRandomParking();

      repositoryStub.Setup(
          repo => repo
          .GetParkingAsync(It.IsAny<int>()))
          .ReturnsAsync(existingParking);

      var parkingId = existingParking.Id;
      var parkingToUpdateOut = new UpdateParkingOutDTO() { };

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var result = await controller.UpdateParkingOutAsync(parkingId, parkingToUpdateOut);

      // Assert
      result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateParkingOutAsync_WithUnexistingParking_ReturnsNotFound()
    {
      // Arrange
      var existingParking = CreateRandomParking();

      repositoryStub.Setup(
          repo => repo
          .GetParkingAsync(It.IsAny<int>()))
          .ReturnsAsync((Parking)null);

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var result = await controller.GetParkingAsync(rand.Next(1, 1000));

      // Assert
      result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateParkingPayAsync_WithExistingParking_ReturnsNoContent()
    {
      // Arrange
      var existingParking = CreateRandomParking();

      repositoryStub.Setup(
          repo => repo
          .GetParkingAsync(It.IsAny<int>()))
          .ReturnsAsync(existingParking);

      var parkingId = existingParking.Id;
      var parkingToUpdatePay = new UpdateParkingPayDTO();

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var result = await controller.UpdateParkingPayAsync(parkingId, parkingToUpdatePay);

      // Assert
      result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateParkingPayAsync_WithUnexistingParking_ReturnsNotFound()
    {
      // Arrange
      var existingParking = CreateRandomParking();

      repositoryStub.Setup(
          repo => repo
          .GetParkingAsync(It.IsAny<int>()))
          .ReturnsAsync((Parking)null);

      var controller = new ParkingsController(repositoryStub.Object);

      // Act
      var result = await controller.GetParkingAsync(rand.Next(1, 1000));

      // Assert
      result.Result.Should().BeOfType<NotFoundResult>();
    }

    private Parking CreateRandomParking()
    {
      return new()
      {
        Id = rand.Next(1, 1000),
        Plate = "ABC-1234",
        EntryDate = DateTimeOffset.UtcNow,
        ExitDate = DateTimeOffset.UtcNow,
        Paid = true,
        Left = true
      };
    }
  }
}
