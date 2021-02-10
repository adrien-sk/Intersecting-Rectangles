using IntersectingRectangles.Classes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IntersectingRectangles.Helpers
{
	/// <summary>
	/// JsonFileVerifications Class allows to validate a Json file according :
	///		- A valid path
	///		- A valid json type
	///		- A valid structure following a specific format
	/// </summary>
	class JsonFileVerifications
	{
		//	Json Schema used to validate the Json structure
		static JSchema jsonSchema = JSchema.Parse(@"
			{
				'type': 'object',
				'properties': {
					'rects': {
						'description': 'The set of rectangles',
						'type': 'array', 
						'items': {
						  'x': {
							'type': 'integer'
						  },
						  'y': {
							'type': 'integer'
						  }, 
						  'delta_x': {
							'type': 'integer'
						  }, 
						  'delta_y': {
							'type': 'integer'
						  }
						}
					}
				},
				'required': ['rects'], 
				'additionalProperties': false
			}");

		//	Json example to display in case of a structure error
		static string jsonExample = 
		"{"+ System.Environment.NewLine+
			"\t\"rects\": ["+ System.Environment.NewLine+
				"\t\t{\"x\": 100, \"y\": 100, \"delta_x\": 250, \"delta_y\": 80 }," + System.Environment.NewLine+
				"\t\t{\"x\": 120, \"y\": 200, \"delta_x\": 250, \"delta_y\": 150 }," +System.Environment.NewLine+
				"\t\t{\"x\": 140, \"y\": 160, \"delta_x\": 250, \"delta_y\": 100 }," +System.Environment.NewLine+
				"\t\t{\"x\": 160, \"y\": 140, \"delta_x\": 350, \"delta_y\": 190 }" +System.Environment.NewLine+
			"\t]" +System.Environment.NewLine+
		"}";


		/// <summary>
		/// Public method to validate a Json file for a given path
		/// </summary>
		/// <param name="path">String absolute path</param>
		/// <returns>True if file is valid</returns>
		public static bool IsJsonFileValid(string path)
		{
			if (!IsPathValid(path))
				return false;

			if (!IsJsonTypeValid(path))
				return false;
			
			return true;
		}

		/// <summary>
		/// Path validation for a given absolute path
		/// </summary>
		/// <param name="path"></param>
		/// <returns>True if the path is valid</returns>
		private static bool IsPathValid(string path)
		{
			try
			{
				if (String.IsNullOrWhiteSpace(path))
				{
					Console.WriteLine("Provided path is empty. Please ensure you enter the correct path.");
					return false;
				}

				var json = File.ReadAllText(path);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("File not found : Please ensure you enter the correct path.");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Json file type and structure validation
		/// </summary>
		/// <param name="path"></param>
		/// <returns>True if the file is a Json, and it follow the defined Schema</returns>
		private static bool IsJsonTypeValid(string path)
		{
			try {
				if (Path.GetExtension(path) != ".json")
				{
					Console.WriteLine("File type error : The file need to be \".json\"");
					return false;
				}

				var json = File.ReadAllText(path);
				var parsedObject = JObject.Parse(json);

				if (!parsedObject.IsValid(jsonSchema))
				{
					Console.WriteLine("JSON Schema error : The provided JSON doesn't follow the correct schema.");
					Console.WriteLine();
					Console.WriteLine("Format example :");
					Console.WriteLine(jsonExample);
					return false;
				}
			}
			catch (Exception)
			{
				throw;
			}
			return true;
		}
	}
}
