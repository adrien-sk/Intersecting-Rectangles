using System;
using Xunit;
using IntersectingRectangles.Classes;
using System.Collections.Generic;

namespace IntersectingRectangles.Tests
{
	public class RectangleTests
	{
		[Fact]
		public void IsInvolvedInIntersection_ShouldPass()
		{
			Rectangle intersection = new Rectangle(100, 100, 250, 80);
			intersection.AddInvolvedRectangles(new List<int> { 1,3,5 });

			bool actual = intersection.IsInvolvedInIntersection(3);

			Assert.True(actual);
		}

		[Fact]
		public void IsInvolvedInIntersection_ShouldFail()
		{
			Rectangle intersection = new Rectangle(100, 100, 250, 80);
			intersection.AddInvolvedRectangles(new List<int> { 1, 5 });

			bool actual = intersection.IsInvolvedInIntersection(4);

			Assert.False(actual);
		}

		[Fact]
		public void IntersectWith_ShouldPass()
		{
			Rectangle rectangle1 = new Rectangle(100, 100, 250, 80);
			Rectangle rectangle2 = new Rectangle(140, 160, 250, 100);

			bool actual = rectangle1.IntersectWith(rectangle2);

			Assert.True(actual);
		}

		[Fact]
		public void IntersectWith_ShouldFail()
		{
			Rectangle rectangle1 = new Rectangle(100, 100, 250, 80);
			Rectangle rectangle2 = new Rectangle(120, 200, 250, 150);

			bool actual = rectangle1.IntersectWith(rectangle2);

			Assert.False(actual);
		}

		[Fact]
		public void GetIntersection_ShouldEqual()
		{
			Rectangle rectangle1 = new Rectangle(100, 100, 250, 80);
			Rectangle rectangle2 = new Rectangle(140, 160, 250, 100);

			Rectangle expected = new Rectangle(140, 160, 210, 20);
			Rectangle actual = rectangle1.GetIntersection(rectangle2);

			Assert.Equal(actual.x, expected.x);
			Assert.Equal(actual.y, expected.y);
			Assert.Equal(actual.deltaX, expected.deltaX);
			Assert.Equal(actual.deltaY, expected.deltaY);
		}

	}
}
