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
		/// <summary>
		/// 
		/// Main Application : 
		/// - The application takes a Json file as input : You need to copy/paste the absolute path of a Json file in the console.
		/// - It will display the list of the valid rectangles from the Json, and the intersections between all the rectangles.
		/// 
		/// Different steps :
		/// - First : Checking if the Json file is valid and can be used by the program.
		/// - Second : Serialization of the Json in an IntersectionFinder object.
		/// - Third : Request the object to find all intersections for the given rectangles, and display the output.
		/// 
		/// </summary>
		static void Main(string[] args)
		{
			try
			{
				// Console input from user for the file path
				Console.WriteLine("Please enter/paste the path of your file :");
				Console.WriteLine("(If you don't have a file, You can use example data by typing : Data/data.json)");
				var jsonFilePath = @"" + Console.ReadLine();
				Console.WriteLine();

				// Verification of the Json file validity
				if (!JsonFileVerifications.IsJsonFileValid(jsonFilePath))
					return;

				// Creation of an IntersectionFinder Object by loading the validated Json path
				IntersectionFinder intersectionFinderObject = IntersectionFinder.LoadJson(jsonFilePath);

				// If there is no issue : Find and Display all the intersections
				if (intersectionFinderObject != null)
					Console.WriteLine(intersectionFinderObject.GetIntersections());
				else
					return;

			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
