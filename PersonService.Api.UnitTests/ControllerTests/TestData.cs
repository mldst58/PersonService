using System;
using System.Collections.Generic;
using PersonService.Api.DTOs;
using PersonService.Api.Models;

namespace PersonService.Api.UnitTests.ControllerTests
{
    public static class TestData
    {
        public static readonly Person FPersonOne = new Person
        {
            Id = 1,
            FirstName = "Megan",
            LastName = "Chipps",
            PreferredName = "Megan Chipps",
            Gender = 2,
            DateOfBirth = new DateTimeOffset(1980, 5, 1, 8, 6, 32,
                new TimeSpan(1, 0, 0)),
            AddressId = 1
        };

        public static readonly Person MPersonOne = new Person
        {
            Id = 2,
            FirstName = "James",
            LastName = "Roberts",
            PreferredName = "Jim Roberts",
            Gender = 1,
            DateOfBirth = new DateTimeOffset(1990, 5, 1, 8, 6, 32,
                new TimeSpan(1, 0, 0)),
            AddressId = 2
        };

        public static readonly Person MPersonTwo = new Person
        {
            Id = 3,
            FirstName = "Samuel",
            LastName = "Smith",
            PreferredName = "Sam Smith",
            Gender = 1,
            DateOfBirth = new DateTimeOffset(1975, 5, 1, 8, 6, 32,
                new TimeSpan(1, 0, 0)),
            AddressId = 3
        };

        public static readonly List<Person> PersonTable =
            new List<Person> { FPersonOne, MPersonOne, MPersonTwo };

        public static readonly PersonContactDTO PersonOne = new PersonContactDTO
        {
            Id = 1,
            AddressId = 1,
            FirstName = "Megan",
            LastName = "Chipps",
            PreferredName = "Megan Chipps",
            Gender = "Female",
            DateOfBirth = new DateTimeOffset(1980, 5, 1, 8, 6, 32,
                new TimeSpan(1, 0, 0)),
            Address = "133 Test Dr",
            Address2 = "Apt3",
            City = "Cranberry",
            ZipCode = "16066",
            State = "PA"
        };

        public static readonly PersonContactDTO PersonTwo = new PersonContactDTO
        {
            Id = 2,
            AddressId = 2,
            FirstName = "James",
            LastName = "Roberts",
            PreferredName = "Jim Roberts",
            Gender = "Male",
            DateOfBirth = new DateTimeOffset(1990, 5, 1, 8, 6, 32,
                new TimeSpan(1, 0, 0)),
            Address = "1478 Test Ave",
            City = "Pittsburgh",
            ZipCode = "15107",
            State = "PA"
        };

        public static readonly PersonContactDTO PersonThree = new PersonContactDTO
        {
            Id = 3,
            AddressId = 3,
            FirstName = "Samuel",
            LastName = "Smith",
            PreferredName = "Sam Smith",
            Gender = "Male",
            DateOfBirth = new DateTimeOffset(1975, 5, 1, 8, 6, 32,
                new TimeSpan(1, 0, 0)),
            Address = "55 Test Street",
            City = "Chicago",
            ZipCode = "60615",
            State = "IL"
        };

        public static readonly List<PersonContactDTO> PersonViewable =
            new List<PersonContactDTO> { PersonOne, PersonTwo, PersonThree };


        public static readonly Address TestDriveAddress = new Address
        {
            Id = 1,
            Address = "133 Test Dr",
            Address2 = "Apt3",
            City = "Cranberry",
            ZipCode = "16066",
            State = "PA"
        };

        public static readonly Address TestAveAddress = new Address
        {
            Id = 2,
            Address = "1478 Test Ave",
            City = "Pittsburgh",
            ZipCode = "15107",
            State = "PA"
        };

        public static readonly Address TestStreetAddress = new Address
        {
            Id = 3,
            Address = "55 Test Street",
            City = "Chicago",
            ZipCode = "60615",
            State = "IL"
        };
    }
}