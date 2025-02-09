﻿using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters;
using TagCloudTest.CloudLayouter.SpiralCloudLayouters.Infrastructure;

namespace TagCloudTest.CloudLayouter.SpiralCloudLayouters;

public class CircularCloudLayouterTest
{
    private Point center;
    private SpiralCloudLayouterSettings? settings;
    private Result<List<Rectangle>> rectangles;
    private SpiralCloudLayouter? sut;

    [OneTimeSetUp]
    public void StartTests()
    {
        center = new Point(200, 100);
        settings = new SpiralCloudLayouterSettings(center, 0.1, 0.1);
        sut = new SpiralCloudLayouter();
    }

    [Test]
    public void PutNextRectangle_FirstGotRectangle_ShouldContainsCenter()
    {
        rectangles = sut!.GetLaidRectangles(new[] { new Size(10, 5) }, settings!);
        rectangles.IsSuccess.Should().BeTrue();
        rectangles.GetValueOrThrow()!.First().Contains(center).Should().Be(true);
    }

    [TestCase(2)]
    [TestCase(10)]
    [TestCase(40)]
    [TestCase(100)]
    public void PutNextRectangle_ShouldReturnRectangles_WithoutIntersections(int count)
    {
        var size = new Size(20, 5);
        var sizes = Enumerable.Range(0, count).Select(i => size);
        rectangles = sut!.GetLaidRectangles(sizes, settings!);
        rectangles.IsSuccess.Should().BeTrue();
        for (var i = 0; i < count; i++)
        {
            var rect = rectangles.GetValueOrThrow()![i];
            for (var j = 0; j < count; j++)
                if (i != j)
                    rect.IntersectsWith(rectangles.GetValueOrThrow()![j]).Should().Be(false);
        }
    }

    [TearDown]
    public void SaveImage_OnFailTest()
    {
        var testContext = TestContext.CurrentContext;
        if (testContext.Result.FailCount == 0)
            return;
        if (!rectangles.IsSuccess)
            return;
        var filename =
            $"Failed test {TestContext.CurrentContext.Test.Name} image at {DateTime.Now:dd-MM-yyyy HH_mm_ss}.jpg";
        var bitmap = TagCloudDrawer.DrawWithAutoSize(rectangles.GetValueOrThrow()!.ToArray(),
            Color.Black, Color.DarkOrange,
            true, true);
        bitmap.Save(filename);
        var path = AppContext.BaseDirectory;
        TestContext.Error.Write($"Tag cloud visualization saved to file {path + filename}");
    }
}