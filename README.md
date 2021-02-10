
# Intersecting Rectangles

This is a Console application written with C# .Net Core, used to output the list of intersections between a given list of rectangles.

The application will request to input the path of a Json file containing a formatted list of rectangles coordinates.
It will calculate each possible intersections between all the provided rectangles, and then display the rectangles and their intersections.

## Json example
```json
{  
	"rects": [  
		{"x": 100, "y": 100, "delta_x": 250, "delta_y": 80 },  
		{"x": 120, "y": 200, "delta_x": 250, "delta_y": 150 },  
		{"x": 140, "y": 160, "delta_x": 250, "delta_y": 100 },  
		{"x": 160, "y": 140, "delta_x": 350, "delta_y": 190 }  
	]  
}
```
## Output result
```sh
Input:
        1: Rectangle  at (100,100), delta_x=250, delta_y=80.
        2: Rectangle  at (120,200), delta_x=250, delta_y=150.
        3: Rectangle  at (140,160), delta_x=250, delta_y=100.
        4: Rectangle  at (160,140), delta_x=350, delta_y=190.

Intersections:
        1: Between rectangle 1, 3 at (140,160), delta_x=210, delta_y=20.
        2: Between rectangle 1, 4 at (160,140), delta_x=190, delta_y=40.
        3: Between rectangle 2, 3 at (140,200), delta_x=230, delta_y=60.
        4: Between rectangle 2, 4 at (160,200), delta_x=210, delta_y=130.
        5: Between rectangle 3, 4 at (160,160), delta_x=230, delta_y=100.
        6: Between rectangle 1, 3, 4 at (160,160), delta_x=190, delta_y=20.
        7: Between rectangle 2, 3, 4 at (160,200), delta_x=210, delta_y=60.
```

## With Visual Studio
If you have Visual Studio installed, you should be able to easily do the following from Visual Studio directly :
- Explore the code.
- Build the application.
- Run the "IntersectingRectangles" application project.
- Run the "IntersectingRectangles.Tests" Unit tests project.

## Without Visual Studio

Clone this repository and open the folder
```sh
git clone https://github.com/adrien-sk/Intersecting-Rectangles.git
```
```sh
cd Intersecting-Rectangles
```

### Run the application

```sh
dotnet run --project src/IntersectingRectangles
```

### Run tests

```sh
dotnet test src/IntersectingRectangles.Tests
```
