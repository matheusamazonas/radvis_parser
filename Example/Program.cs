using RADVisParser.Parsers.VTK;

var parser = new VTKParser("/Users/matheus/Development/radvis-parser/Example/Inputs/track_yz.vtk");
var header = parser.Parse();
Console.WriteLine($"Header:{header}");