using System;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Infrastructure.Identity;
using MyTested.AspNetCore.Mvc;

namespace DistanceCalculator.Web.IntegrationTests
{
    public class TestData
    {
        public static DateTime TestNow => new DateTime(3000, 10, 10);

        public static object[] Locations
            => new object[]
            {
                new Location("Test Title 1")
                {
                    Id = 1,
                },
                new Location("Test Title 2")
                {
                    Id = 2
                },
                new User
                {
                    Id = TestUser.Identifier,
                    UserName = TestUser.Username
                }
            };
    }
}
