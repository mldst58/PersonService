using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonService.Api.Controllers;
using PersonService.Api.DataAccess.Commands;
using PersonService.Api.DataAccess.Queries;
using PersonService.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonService.Api.Models;
using PersonService.Api.UnitTests.ControllerTests;
using Xunit;

namespace PersonService.Api.UnitTests.ControllerTest
{
    public class PersonControllerTests
    {
        [Fact]
        public async Task GetListWithNoParamsReturnsList()
        {
            var mockPersonQuery = new Mock<IPersonQueryService>();
            mockPersonQuery.Setup(q => q.GetAllPersons())
                .Returns(Task.FromResult(new List<PersonContactDTO> { new PersonContactDTO() }));
            var mockPersonCmd = new Mock<IPersonCommandService>();
            var controller = new PersonController(null, mockPersonQuery.Object, mockPersonCmd.Object);

            var result = await controller.GetListAsync();

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<PersonContactDTO>>(((OkObjectResult)result.Result).Value);
            var resultList = (List<PersonContactDTO>)((OkObjectResult)result.Result).Value;
            Assert.Single(resultList);
        }

        [Fact]
        public async Task GetWithInvalidIdReturnsBadRequest()
        {
            var controller = new PersonController(null, null, null);

            var result = await controller.GetAsync(0);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetWithNonexistentIdReturnsNotFound()
        {
            var mockQuery = new Mock<IPersonQueryService>();
            mockQuery.Setup(q => q.GetPersonsByState("NY"))
                .Returns(Task.FromResult(TestData.PersonViewable));
            var mockPersonCmd = new Mock<IPersonCommandService>();
            var controller = new PersonController(null, mockQuery.Object, mockPersonCmd.Object);

            var result = await controller.GetAsync(TestData.FPersonOne.Id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostValidNewPersonReturnsCreated()
        {
            var data = new Person
            {
                FirstName = "James",
                LastName = "Roberts",
                Gender = 1,
                DateOfBirth = new DateTimeOffset(1990, 5, 1, 8, 6, 32,
                    new TimeSpan(1, 0, 0)),
                Deleted = 0,
                PreferredName = "Jim Roberts",
                AddressId = 2,
                CreatedBy = "user"
            };
            var mockValidator = new Mock<IPersonValidator>();
            mockValidator.Setup(v => v.IsPersonValid(It.IsAny<NewPerson>())).Returns(true);
            var mockInsertPerson = new Mock<IPersonCommandService>();
            var mockPersonQuery = new Mock<IPersonQueryService>();
            mockPersonQuery.Setup(q => q.GetPerson(2))
                .Returns(Task.FromResult(TestData.FPersonOne));
            var controller = new PersonController(mockValidator.Object, mockPersonQuery.Object, mockInsertPerson.Object);

            var result = await controller.PostAsync(data);

            Assert.IsType<CreatedResult>(result.Result);
            //mockInsertPerson.Verify(i => i.Insert(It.IsAny<NewPerson>(),"user"),
            //    Times.Once);
        }

        [Fact]
        public async Task PostInvalidNewPersonReturnsCreated()
        {
            var mockValidator = new Mock<IPersonValidator>();
            mockValidator.Setup(v => v.IsPersonValid(It.IsAny<NewPerson>())).Returns(false);
            var mockPersonQuery = new Mock<IPersonQueryService>();
            var mockPersonCmd = new Mock<IPersonCommandService>();
            var controller = new PersonController(mockValidator.Object, mockPersonQuery.Object, mockPersonCmd.Object);

            var result = await controller.PostAsync(TestData.FPersonOne);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PutValidPersonReturnOkResult()
        {
            var person = TestData.FPersonOne;
            var data = new Person
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = "Married",
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                Deleted = person.Deleted,
                PreferredName = person.FirstName + " " + "Married",
                AddressId = 1
            };

            var mockValidator = new Mock<IPersonValidator>();
            mockValidator.Setup(v => v.IsPersonValid(It.IsAny<NewPerson>())).Returns(true);
            var mockInsertPerson = new Mock<IPersonCommandService>();
            var mockPersonQuery = new Mock<IPersonQueryService>();
            mockPersonQuery.Setup(q => q.GetPerson(1))
                .Returns(Task.FromResult(TestData.FPersonOne));
            var controller = new PersonController(mockValidator.Object, mockPersonQuery.Object, mockInsertPerson.Object);

            var result = await controller.PutAsync(person.Id, data);
            
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }

}
