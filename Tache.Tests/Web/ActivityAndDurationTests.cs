using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;
using Tache.Models;

namespace Tache.Tests {
    [TestClass]
    public class ActivityAndDurationTests {

        //private ActivityAndDurationsRepository ActivityAndDurationRepoFactory() {

        //    var mockActivityRepo = new Mock<IActivityRepository>();
        //    var mockDurationRepo = new Mock<IDurationRepository>();

        //    mockActivityRepo.Setup(m => m.Activities).Returns((new List<Activity>() {
        //        new Activity() { Id = 1, Name = "first", Description = "First Description" },
        //        new Activity() { Id = 2, Name = "second", Description = "Second Description" },
        //        new Activity() { Id = 3, Name = "third", Description = "Third Description" }
        //    }).AsQueryable());

        //    mockDurationRepo.Setup(m => m.Durations).Returns((new List<Duration>() {
        //        new Duration() { Id = 101, ActivityId = 1 },
        //        new Duration() { Id = 102, ActivityId = 2 },
        //        new Duration() { Id = 103, ActivityId = 3 },
        //        new Duration() { Id = 104, ActivityId = 1 }
        //    }).AsQueryable());

        //    return new ActivityAndDurationsRepository(mockActivityRepo.Object, mockDurationRepo.Object);
        //}

        //[TestMethod]
        //public void ModelPropReturnsSingleViewModelForInt() {
        //    // Arrange
        //    var activityAndDurationRepo = ActivityAndDurationRepoFactory();

        //    // Act
        //    var actual = activityAndDurationRepo.For("1").Model();

        //    // Assert
        //    Assert.IsInstanceOfType(actual.First(), typeof(ActivityAndDurationsViewModel));
        //    Assert.AreEqual(1, actual.Count);
        //    Assert.AreEqual(1, actual.First().Activity.Id);
        //    Assert.AreEqual(2, actual.First().Durations.Count());
        //    Assert.IsTrue(actual.First().Durations.All(d => d.ActivityId == 1));
        //}

        //[TestMethod]
        //public void ModelPropReturnsSingleViewModelForActivityName() {
        //    // Arrange
        //    var activityAndDurationRepo = ActivityAndDurationRepoFactory();

        //    // Act
        //    var actual = activityAndDurationRepo.For("First").Model();

        //    // Assert
        //    Assert.IsInstanceOfType(actual.First(), typeof(ActivityAndDurationsViewModel));
        //    Assert.AreEqual(1, actual.Count);
        //    Assert.AreEqual(1, actual.First().Activity.Id);
        //    Assert.AreEqual(2, actual.First().Durations.Count());
        //    Assert.IsTrue(actual.First().Durations.All(d => d.ActivityId == 1));
        //}

        //[TestMethod]
        //public void ModelPropReturnsListOfViewModelsByDefault() {
        //    // Arrange
        //    var activityAndDurationRepo = ActivityAndDurationRepoFactory();

        //    // Act
        //    var actual = activityAndDurationRepo.Model();

        //    // Assert
        //    Assert.IsInstanceOfType(actual, typeof(ICollection<ActivityAndDurationsViewModel>));
        //    Assert.AreEqual(3, actual.Count);
        //}
    }
}
