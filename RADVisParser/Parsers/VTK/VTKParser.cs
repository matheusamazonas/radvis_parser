using Pidgin;
using RADVisParser.Data;
using static Pidgin.Parser;
using static RADVisParser.Parsers.CommonParser;
using File = RADVisParser.Data.VTK.File;

namespace RADVisParser.Parsers.VTK;

public class VTKParser
{
    #region Fields

    private readonly string _filePath;

    #endregion

    #region Setup

    public VTKParser(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
            throw new FileNotFoundException($"File to be parsed not found: {filePath}");

        _filePath = filePath;
    }

    #endregion

    #region Public

    public File Parse()
    {
        using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
        using var readStream = new StreamReader(fileStream);
        var parser = 
            from header in HeaderParser.Parse()
            from dimensions in ParseDimensions()
            from positions in ParsePositions()
            select new File(header, dimensions, positions);
        return parser.ParseOrThrow(readStream);
    }

    #endregion

    #region Private

    private static Parser<char, Vector3<int>> ParseDimensions()
    {
        return String("DIMENSIONS")
            .Before(SkipWhitespaces)
            .Then(ParseIntVector())
            .Before(ParseLineEnding());
    }

    private static Parser<char, Vector3<double>[]> ParsePositions()
    {
        return
            from xCoordinates in ParseFloatCoordinates("X_COORDINATES")
            from yCoordinates in ParseFloatCoordinates("Y_COORDINATES")
            from zCoordinates in ParseFloatCoordinates("Z_COORDINATES")
            select Compose(xCoordinates, yCoordinates, zCoordinates);

        Vector3<double>[] Compose(IReadOnlyList<double> xs, IReadOnlyList<double> ys, IReadOnlyList<double> zs)
        {
            if (xs.Count != ys.Count || xs.Count != zs.Count)
                throw new InvalidDataException("Number of coordinates don't match");
            
            var length = xs.Count;
            var positions = new Vector3<double>[length];

            for (var i = 0; i < length; i++)
                positions[i] = new Vector3<double>(xs[i], ys[i], zs[i]);

            return positions;
        }
    }

    private static Parser<char, double[]> ParseFloatCoordinates(string identifier)
    {
        return 
            from count in String(identifier)
                .Then(SkipWhitespaces)
                .Then(Int(10))
            from numbers in SkipWhitespaces
                .Then(String("float"))
                .Then(SkipWhitespaces)
                .Then(ParseDoubleArray(count))
            select numbers.ToArray();
    }

    #endregion
}