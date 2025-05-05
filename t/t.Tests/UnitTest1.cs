// <copyright file="t.Tests.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace t.Tests;

using NUnit.Framework.Internal.Execution;
using Work;

public class Tests
{
    /// <summary>
    /// Get Result Of Correct Array
    /// </summary>
    [Test]
    public void GetResultOfCorrectArray()
    {
        var array = new int[] {5, 4, 3, 2, 1};
        var res = Operation.SummMin(array);
        var expected = 3;
        Assert.That(res, Is.EqualTo(expected));
    }

    /// <summary>
    /// Checks Empty Array
    /// </summary>
    [Test]
    public void ChecksEmptyArray()
    {
        var array = new int[] {};
        Assert.Throws<ArgumentException>(() => Operation.SummMin(array));
    }

    /// <summary>
    /// Tests Few Arguments For Function
    /// </summary>
    [Test]
    public void TestsFewArgumentsForFunction()
    {
        var array = new int[] {3};
        Assert.Throws<ArgumentException>(() => Operation.SummMin(array));
    }

    /// <summary>
    /// Get Result Of Second Correct Array
    /// </summary>
    [Test]
    public void GetResultOfSecondCorrectArray()
    {
        var array = new int[] {343, 3, 12, 12};
        var res = Operation.SummMin(array);
        var expected = 15;
        Assert.That(res, Is.EqualTo(expected));
    }

    /// <summary>
    /// Get Result Of Correct Big Array
    /// </summary>
    [Test]
    public void GetResultOfCorrectBigArray()
    {
        var array = Enumerable.Range(0, 1000).Select(i => i * i).ToArray();
        var res = Operation.SummMin(array);
        var expected = 1;
        Assert.That(res, Is.EqualTo(expected));
    }
}