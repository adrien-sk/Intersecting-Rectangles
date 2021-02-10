using IntersectingRectangles.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace IntersectingRectangles.Tests
{
	public class IntersectionFinderTests
	{
		[Fact]
		public void LoadJson_ValidData()
		{
			string jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"Data", @"IntersectionFinder", @"valid.json");
			IntersectionFinder intersectionFinderObject = IntersectionFinder.LoadJson(jsonFilePath);

			Assert.NotNull(intersectionFinderObject);
		}

		[Fact]
		public void LoadJson_SingleItem()
		{
			string jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"Data", @"IntersectionFinder", @"single_item.json");
			IntersectionFinder intersectionFinderObject = IntersectionFinder.LoadJson(jsonFilePath);

			Assert.Null(intersectionFinderObject);
		}

		[Fact]
		public void LoadJson_MoreThanTenShouldContainsTen()
		{
			string jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"Data", @"IntersectionFinder", @"more_than_ten.json");
			IntersectionFinder intersectionFinderObject = IntersectionFinder.LoadJson(jsonFilePath);
			int expected = 10;
			int actual = intersectionFinderObject.rectangles.Count();

			Assert.NotNull(intersectionFinderObject);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void LoadJson_NullAndNegativeValuesShouldContainsFive()
		{
			string jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"Data", @"IntersectionFinder", @"null_and_negative.json");
			IntersectionFinder intersectionFinderObject = IntersectionFinder.LoadJson(jsonFilePath);
			int expected = 5;
			int actual = intersectionFinderObject.rectangles.Count();

			Assert.NotNull(intersectionFinderObject);
			Assert.Equal(expected, actual);
		}
	}
}
