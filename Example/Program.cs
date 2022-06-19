using RADVisParser.Parsers.VTK;

var parser = new VTKParser("/Users/matheus/Development/radvis-parser/Example/Inputs/track_yz.vtk");
var file = parser.Parse();
Console.WriteLine($"Header:{file.Header}. Dimensions: {file.Dimensions}. Positions: {file.Positions.Length}");