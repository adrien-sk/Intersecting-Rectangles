using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using IntersectingRectangles.Classes;
using IntersectingRectangles.Helpers;

namespace IntersectingRectangles
{
	class Program
	{
		static void Main(string[] args)
		{
			IntersectionFinder intersectionFinder = IntersectionFinder.LoadJson();

			Console.WriteLine(intersectionFinder.FindIntersections());
		}
	}
}
