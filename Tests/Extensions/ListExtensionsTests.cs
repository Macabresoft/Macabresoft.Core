namespace Macabresoft.Core.Tests.Extensions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using NUnit.Framework;

    [TestFixture]
    public class ListExtensionsTests {
        private static IList<int> GetList(Random random) {
            var list = new List<int>();
            for (var i = 0; i < random.Next(50, 100); i++) {
                list.Add(0);
            }

            return list;
        }

        [Test]
        [Category("Unit Tests")]
        public void InsertOrAdd_ShouldAdd_WhenIndexGreaterThanCount() {
            var random = new Random();
            var list = GetList(random);
            var listCount = list.Count;
            var index = random.Next(listCount + 1, listCount + 100);
            list.InsertOrAdd(index, -1);

            using (new AssertionScope()) {
                list.Count.Should().Be(listCount + 1);
                list.Last().Should().Be(-1);
            }
        }

        [Test]
        [Category("Unit Tests")]
        public void InsertOrAdd_ShouldInsert_WhenIndexInRange() {
            var random = new Random();
            var list = GetList(random);
            var listCount = list.Count;
            var index = random.Next(0, listCount - 1);
            list.InsertOrAdd(index, -1);

            using (new AssertionScope()) {
                list.Count.Should().Be(listCount + 1);
                list[index].Should().Be(-1);
            }
        }

        [Test]
        [Category("Unit Tests")]
        public void InsertOrAdd_ShouldInsertAtZero_WhenIndexBelowZero() {
            var random = new Random();
            var list = GetList(random);
            var listCount = list.Count;
            var index = random.Next(int.MinValue, -1);
            list.InsertOrAdd(index, -1);

            using (new AssertionScope()) {
                list.Count.Should().Be(listCount + 1);
                list[0].Should().Be(-1);
            }
        }
    }
}