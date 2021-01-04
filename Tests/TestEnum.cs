namespace Macabresoft.Core.Tests {
    using System.ComponentModel.DataAnnotations;

    public static class TestEnumNames {
        public const string Name1 = "Like a Dragon";
        public const string Name2 = "Skeleton Coast";
    }

    public enum TestEnum {
        [Display(Name = TestEnumNames.Name1)] ValueWithName1,
        [Display(Name = TestEnumNames.Name2)] ValueWithName2,
        ValueWithoutName1,
        ValueWithoutName2
    }
}