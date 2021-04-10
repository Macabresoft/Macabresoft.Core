namespace Macabresoft.Core.Tests.Utilities {
    using System.ComponentModel.DataAnnotations;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using NUnit.Framework;

    [TestFixture]
    public static class Tests {
        private const string ClassName = "All These Governors";
        private const string Property1Name = "The Hero of Kvatch!";
        private const string Property2Name = "Tarhiel";
        
        [Test]
        [Category("Unit Tests")]
        public static void GetEnumDisplayName_Should_UseDisplayAttribute() {
            using (new AssertionScope()) {
                TestEnum.ValueWithName1.GetEnumDisplayName().Should().Be(TestEnumNames.Name1);
                TestEnum.ValueWithName2.GetEnumDisplayName().Should().Be(TestEnumNames.Name2);
                TestEnum.ValueWithoutName1.GetEnumDisplayName().Should().Be(TestEnum.ValueWithoutName1.ToString());
                TestEnum.ValueWithoutName2.GetEnumDisplayName().Should().Be(TestEnum.ValueWithoutName2.ToString());
            }
        }

        [Test]
        [Category("Unit Tests")]
        public static void GetTypeDisplayName_Should_UseDisplayAttribute() {
            using (new AssertionScope()) {
                typeof(DisplayNameTestClass).GetTypeDisplayName().Should().Be(ClassName);
                typeof(NonDisplayNameTestClass).GetTypeDisplayName().Should().Be(nameof(NonDisplayNameTestClass));
            }
        }
        
        [Test]
        [Category("Unit Tests")]
        public static void GetPropertyDisplayName_Should_UseDisplayAttribute() {
            using (new AssertionScope()) {
                typeof(DisplayNameTestClass).GetPropertyDisplayName(nameof(DisplayNameTestClass.Property1)).Should().Be(Property1Name);
                typeof(DisplayNameTestClass).GetPropertyDisplayName(nameof(DisplayNameTestClass.Property2)).Should().Be(Property2Name);
                typeof(NonDisplayNameTestClass).GetPropertyDisplayName(nameof(NonDisplayNameTestClass.Property1)).Should().Be(nameof(NonDisplayNameTestClass.Property1));
                typeof(NonDisplayNameTestClass).GetPropertyDisplayName(nameof(NonDisplayNameTestClass.Property2)).Should().Be(nameof(NonDisplayNameTestClass.Property2));
            }
        }

        [Display(Name = ClassName)]
        private class DisplayNameTestClass {
            [Display(Name = Property1Name)]
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public float Property1 { get; }
            
            [Display(Name = Property2Name)]
            public byte Property2 { get; set; }
        }

        private class NonDisplayNameTestClass {
            public object Property1 { get; private set; }
            
            public object Property2 {
                set => Property1 = value;
            }
        }
    }
}