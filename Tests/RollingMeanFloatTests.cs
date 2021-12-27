namespace Macabresoft.Core.Tests;

using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

[TestFixture]
public class RollingMeanFloatTests {
    [Test]
    [Category("Unit Tests")]
    public void Clear_Should_SetMeanValueToZero() {
        var random = new Random();
        var size = random.Next(3, 10);
        var rollingMean = new RollingMeanFloat(size);

        for (var i = 0; i < size; i++) {
            rollingMean.Add(random.Next(3, 10));
        }

        using (new AssertionScope()) {
            rollingMean.MeanValue.Should().BeGreaterThan(0f);
            rollingMean.Clear();
            rollingMean.MeanValue.Should().Be(0f);
        }
    }
}