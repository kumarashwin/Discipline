using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Moq;
using Discipline.Domain.Entities;
using System.Linq;
using Discipline.Domain.Concrete;
using System.Collections.Generic;
using System;

namespace Discipline.Tests {

    [TestClass]
    public class CurrentActivityRepoTests {
        private IList<Duration> durationsList = new List<Duration> { };
        private IList<CurrentActivity> currentActivitiesList = (new List<CurrentActivity> {
                new CurrentActivity {  Id = 1, ActivityId = 101, Start = DateTime.Now.AddHours(-1)},
                new CurrentActivity {  Id = 2, ActivityId = 102, Start = DateTime.Now.AddHours(-2)}});

        private Mock<DbSet<T>> MockDbSetFactory<T>(IList<T> list) where T : class {
            var listAsQueryable = list.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(listAsQueryable.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(listAsQueryable.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(listAsQueryable.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => listAsQueryable.GetEnumerator());
            mockDbSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(list.Add);
            mockDbSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(t => list.Remove(t));

            return mockDbSet;
        }
        
        private CurrentActivityRepository RepoFactory() {

            var mockCurrentActivityDbSet = MockDbSetFactory<CurrentActivity>(currentActivitiesList);
            var mockDurationsDbSet = MockDbSetFactory<Duration>(durationsList);

            var mockContext = new Mock<Domain.Concrete.DbContext>();
            mockContext.Setup(m => m.CurrentActivities).Returns(mockCurrentActivityDbSet.Object);
            mockContext.Setup(m => m.Durations).Returns(mockDurationsDbSet.Object);

            return new CurrentActivityRepository(mockContext.Object);
        }

        [TestMethod]
        public void RetrieveAllCurrentActivities() {
            // Arrange
            var repo = RepoFactory();

            // Act
            var actual = repo.CurrentActivities;

            // Assert
            Assert.IsInstanceOfType(actual, typeof(IQueryable<CurrentActivity>));
            Assert.AreEqual(2, actual.ToList().Count());
        }

        [TestMethod]
        public void AddDurationsForStart() {
            // Arrange
            var repo = RepoFactory();

            // Act
            repo.Start(new Activity { Id = 103 }, DateTime.Now);

            // Assert
            Assert.AreEqual(3, currentActivitiesList.Count);
            Assert.IsTrue(currentActivitiesList.Any(ca => ca.ActivityId == 103));
        }

        [TestMethod]
        public void AddDurationsForStopWithinSameDay() {
            // Arrange
            var repo = RepoFactory();

            // Act
            repo.Stop(new Activity { Id = 101 }, DateTime.Now);

            // Assert
            Assert.AreEqual(1, currentActivitiesList.Count);
            Assert.IsFalse(currentActivitiesList.Any(ca => ca.ActivityId == 101));
            Assert.AreEqual(1, durationsList.Count);
            Assert.AreEqual(101, durationsList.First().ActivityId);
        }

        [TestMethod]
        public void AddDurationsForStopAfterMultipleDays() {
            // Arrange
            var repo = RepoFactory();

            // Act
            repo.Stop(new Activity { Id = 101 }, DateTime.Now.AddDays(2));

            // Assert
            Assert.AreEqual(3, durationsList.Count);
        }
    }
}
