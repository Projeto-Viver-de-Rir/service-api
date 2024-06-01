using Institutional.Api.IntegrationTests.Common;
using System;
using System.Net;
using FluentAssertions;
using Institutional.Application.Common.Responses;
using Institutional.Application.Features.Volunteers;
using Institutional.Application.Features.Volunteers.CreateVolunteer;
using Institutional.Application.Features.Volunteers.GetAllVolunteers;
using Institutional.Application.Features.Volunteers.UpdateVolunteer;
using Institutional.Domain.Entities.Common;
using System.Net.Http.Json;

namespace Institutional.Api.IntegrationTests;

public class VolunteerControllerTests : BaseTest
{
    
    public VolunteerControllerTests(CustomWebApplicationFactory apiFactory) : base(apiFactory)
    {
    }
    
    // #region GET
    //
    // [Fact]
    // public async Task Get_AllVolunteers_ReturnsOk()
    // {
    //     // Act
    //     var response = await GetAsync<PaginatedList<GetVolunteerResponse>>("/api/Volunteer");
    //
    //     // Assert
    //     response.Should().NotBeNull();
    //     response!.Result.Should().OnlyHaveUniqueItems();
    //     response.Result.Should().HaveCount(3);
    //     response.CurrentPage.Should().Be(1);
    //     response.TotalItems.Should().Be(3);
    //     response.TotalPages.Should().Be(1);
    // }
    //
    // [Fact]
    // public async Task Get_AllVolunteersWithPaginationFilter_ReturnsOk()
    // {
    //     // Act
    //     var response = await GetAsync<PaginatedList<GetVolunteerResponse>>("/api/Volunteer", 
    //         new GetAllVolunteersRequest(null, null, 1, 1));
    //
    //     // Assert
    //     response.Should().NotBeNull();
    //     response!.Result.Should().OnlyHaveUniqueItems();
    //     response.Result.Should().HaveCount(1);
    //     response.CurrentPage.Should().Be(1);
    //     response.TotalItems.Should().Be(3);
    //     response.TotalPages.Should().Be(3);
    // }
    //
    // [Fact]
    // public async Task Get_AllVolunteersWithNegativePageSize_ReturnsOk()
    // {
    //     // Act
    //     var response = await GetAsync<PaginatedList<GetVolunteerResponse>>("/api/Volunteer", new GetAllVolunteersRequest(
    //         null, null,  1, -1));
    //
    //     // Assert
    //     response.Should().NotBeNull();
    //     response!.Result.Should().OnlyHaveUniqueItems();
    //     response.Result.Should().HaveCount(3);
    //     response.CurrentPage.Should().Be(1);
    //     response.TotalItems.Should().Be(3);
    //     response.TotalPages.Should().Be(1);
    // }
    //
    // [Fact]
    // public async Task Get_AllVolunteersWithNegativeCurrentPage_ReturnsOk()
    // {
    //     // Act
    //     var response = await GetAsync<PaginatedList<GetVolunteerResponse>>("/api/Volunteer", 
    //         new GetAllVolunteersRequest(null, null, -1, 15));
    //
    //     // Assert
    //     response.Should().NotBeNull();
    //     response!.Result.Should().OnlyHaveUniqueItems();
    //     response.Result.Should().HaveCount(3);
    //     response.CurrentPage.Should().Be(1);
    //     response.TotalItems.Should().Be(3);
    //     response.TotalPages.Should().Be(1);
    // }
    //
    // [Fact]
    // public async Task Get_ExistingVolunteersWithFilter_ReturnsOk()
    // {
    //     // Act
    //     var response = await GetAsync<PaginatedList<GetVolunteerResponse>>("/api/Volunteer", new GetAllVolunteersRequest("Corban", null, 1, 10)
    //     {
    //         Name = "Corban"
    //     });        
    //
    //     // Assert
    //     response.Should().NotBeNull();
    //     response!.Result.Should().OnlyHaveUniqueItems();
    //     response.Result.Should().HaveCount(1);
    //     response.CurrentPage.Should().Be(1);
    //     response.TotalItems.Should().Be(1);
    //     response.TotalPages.Should().Be(1);
    // }
    //
    //
    // [Fact]
    // public async Task Get_NonExistingVolunteersWithFilter_ReturnsOk()
    // {
    //
    //     // Act
    //     var response = await GetAsync<PaginatedList<GetVolunteerResponse>>("/api/Volunteer", new GetAllVolunteersRequest()
    //     {
    //         Name = "asdsadsadsadsadasdsasadsa"
    //     });
    //
    //     // Assert
    //     response.Should().NotBeNull();
    //     response!.Result.Should().BeEmpty();
    //     response.CurrentPage.Should().Be(1);
    //     response.TotalItems.Should().Be(0);
    //     response.TotalPages.Should().Be(0);
    // }
    //
    // [Fact]
    // public async Task GetById_ExistingVolunteer_ReturnsOk()
    // {
    //     // Act
    //     var response = await GetAsync<GetVolunteerResponse>("/api/Volunteer/824a7a65-b769-4b70-bccb-91f880b6ddf1");
    //
    //     // Assert
    //     response.Should().NotBeNull();
    //     response!.Id.Should().NotBe(VolunteerId.Empty);
    //     response.Name.Should().NotBeNull();
    //     // response.VolunteerType.Should().NotBeNull();
    // }
    //
    // [Fact]
    // public async Task GetById_ExistingVolunteer_ReturnsNotFound()
    // {
    //     // Act
    //     var response = await GetAsync($"/api/Volunteer/{Guid.NewGuid()}");
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    // }
    //
    // #endregion
    //
    // #region POST
    //
    // [Fact]
    // public async Task Post_ValidVolunteer_ReturnsCreated()
    // {
    //     // Act
    //     var newVolunteer = new CreateVolunteerRequest()
    //     {
    //         Name = "Name hero success",
    //         // VolunteerType = VolunteerType.Student,
    //         // Individuality = "all for one"
    //     };
    //     var response = await PostAsync("/api/Volunteer", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    //     var json = await response.Content.ReadFromJsonAsync<GetVolunteerResponse>();
    //     json.Should().NotBeNull();
    //     json!.Id.Should().NotBe(VolunteerId.Empty);
    //     json.Name.Should().NotBeNull();
    //     // json.VolunteerType.Should().NotBeNull();
    // }
    //
    // [Fact]
    // public async Task Post_NamelessVolunteer_ReturnsBadRequest()
    // {
    //     // Act
    //     var newVolunteer = new CreateVolunteerRequest()
    //     {
    //         // Individuality = "Individuality hero badrequest",
    //         
    //     };
    //     var response = await PostAsync("/api/Volunteer", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    // }
    //
    // [Fact]
    // public async Task Post_Negative_Age_Volunteer_ReturnsBadRequest()
    // {
    //     // Act
    //     var newVolunteer = new CreateVolunteerRequest()
    //     {
    //         // Individuality = "Individuality hero badrequest",
    //         Name = "Test hero",
    //         // Age = -1
    //         
    //     };
    //     var response = await PostAsync("/api/Volunteer", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    // }  
    //
    // [Fact]
    // public async Task Post_EmptyVolunteer_ReturnsBadRequest()
    // {
    //     // Act
    //     var newVolunteer = new CreateVolunteerRequest();
    //     var response = await PostAsync("/api/Volunteer", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    // }
    //
    // #endregion
    //
    // #region PUT
    //
    // [Fact]
    // public async Task Update_ValidVolunteer_Should_Return_Ok()
    // {
    //     // Arrange
    //     
    //
    //     // Act
    //     var newVolunteer = new UpdateVolunteerRequest()
    //     {
    //         Name = "Name hero success",
    //         // VolunteerType = VolunteerType.Villain,
    //         // Individuality = "Invisibility"
    //     };
    //     var response = await PutAsync("/api/Volunteer/824a7a65-b769-4b70-bccb-91f880b6ddf1", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    // }
    //
    //
    // [Fact]
    // public async Task Put_NamelessVolunteer_ReturnsBadRequest()
    // {
    //     // Act
    //     var newVolunteer = new UpdateVolunteerRequest()
    //     {
    //         // VolunteerType = VolunteerType.Student
    //     };
    //     var response = await PutAsync("/api/Volunteer/824a7a65-b769-4b70-bccb-91f880b6ddf1", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    // }
    //
    // [Fact]
    // public async Task Put_Individualityless_ReturnsBadRequest()
    // {
    //     // Act
    //     var newVolunteer = new UpdateVolunteerRequest()
    //     {
    //         Name = "Name hero badrequest"
    //     };
    //     var response = await PutAsync("/api/Volunteer/824a7a65-b769-4b70-bccb-91f880b6ddf1", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    // }
    //
    // [Fact]
    // public async Task Put_EmptyVolunteer_ReturnsBadRequest()
    // {
    //     // Act
    //     var newVolunteer = new UpdateVolunteerRequest();
    //     var response = await PutAsync("/api/Volunteer/824a7a65-b769-4b70-bccb-91f880b6ddf1", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    // }
    //
    // [Fact]
    // public async Task Put_InvalidVolunteerId_ReturnsNotFound()
    // {
    //     // Act
    //     var newVolunteer = new UpdateVolunteerRequest()
    //     {
    //         Name = "Name hero not found",
    //         // VolunteerType = VolunteerType.Teacher,
    //         // Individuality = "one for all"
    //     };
    //     var response = await PutAsync($"/api/Volunteer/{Guid.NewGuid()}", newVolunteer);
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    // }
    //
    // #endregion
    //
    // #region DELETE
    //
    // [Fact]
    // public async Task Delete_ValidVolunteer_Returns_NoContent()
    // {
    //     // Arrange
    //     var response = await DeleteAsync("/api/Volunteer/824a7a65-b769-4b70-bccb-91f880b6ddf1");
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    // }
    //
    // [Fact]
    // public async Task DeleteVolunteer_EmptyId_Should_Return_BadRequest()
    // {
    //     // Arrange
    //     var response = await DeleteAsync($"/api/Volunteer/{Guid.Empty}");
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    // }
    //
    // [Fact]
    // public async Task Delete_InvalidVolunteer_ReturnsNotFound()
    // {
    //     // Arrange
    //     var response = await DeleteAsync($"/api/Volunteer/{Guid.NewGuid()}");
    //
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    // }
    //
    // #endregion
}