namespace Macabresoft.Core.Tests.Utilities {
    using System.ComponentModel.DataAnnotations;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using NUnit.Framework;

    [TestFixture]
    public static class Tests {
        private const string ClassName = "All These Governors";

        [Test]
        [Category("Unit Tests")]
        public static void GetEnumDisplayName_Should_UseDisplayAttribute() {
            using (new AssertionScope()) {
                DisplayNameHelper.GetEnumDisplayName(TestEnum.ValueWithName1).Should().Be(TestEnumNames.Name1);
                DisplayNameHelper.GetEnumDisplayName(TestEnum.ValueWithName2).Should().Be(TestEnumNames.Name2);
                DisplayNameHelper.GetEnumDisplayName(TestEnum.ValueWithoutName1).Should().Be(TestEnum.ValueWithoutName1.ToString());
                DisplayNameHelper.GetEnumDisplayName(TestEnum.ValueWithoutName2).Should().Be(TestEnum.ValueWithoutName2.ToString());
            }
        }

        [Test]
        [Category("Unit Tests")]
        public static void GetTypeDisplayName_Should_UseDisplayAttribute() {
            using (new AssertionScope()) {
                DisplayNameHelper.GetTypeDisplayName(typeof(DisplayNameTestClass)).Should().Be(ClassName);
                DisplayNameHelper.GetTypeDisplayName(typeof(NonDisplayNameTestClass)).Should().Be(nameof(NonDisplayNameTestClass));
            }
        }

        [Display(Name = ClassName)]
        private class DisplayNameTestClass {
        }

        private class NonDisplayNameTestClass {
        }
    }
}