using System;
using Xunit;
using IntersectingRectangles.Helpers;
using System.IO;

namespace IntersectingRectangles.Tests
{
	public class JsonFileVerificationsTests
	{
		[Fact]
		public void IsJsonFileValid_CorrectFile()
		{
			string path = Path.Combine(Environment.CurrentDirectory, @"Data", "data.json");
			bool actual = JsonFileVerifications.IsJsonFileValid(path);

			Assert.True(actual);
		}

		[Fact]
		public void IsJsonFileValid_NullPath()
		{
			bool actual = JsonFileVerifications.IsJsonFileValid(null);

			Assert.False(actual);
		}

		[Fact]
		public void IsJsonFileValid_EmptyPath()
		{
			bool actual = JsonFileVerifications.IsJsonFileValid("");

			Assert.False(actual);
		}

		[Fact]
		public void IsJsonFileValid_IncorrectPath()
		{
			string path = Path.Combine(Environment.CurrentDirectory, @"incorrect_path", "data.json");
			bool actual = JsonFileVerifications.IsJsonFileValid(path);
			Assert.False(actual);
		}

		[Fact]
		public void IsJsonFileValid_IncorrectFileType()
		{
			string path = Path.Combine(Environment.CurrentDirectory, @"Data", "data.txt");
			bool actual = JsonFileVerifications.IsJsonFileValid(path);
			Assert.False(actual);
		}

		[Fact]
		public void IsJsonFileValid_IncorrectJsonStructure()
		{
			string path = Path.Combine(Environment.CurrentDirectory, @"Data", "incorrect.json");
			bool actual = JsonFileVerifications.IsJsonFileValid(path);
			Assert.False(actual);
		}
	}
}
