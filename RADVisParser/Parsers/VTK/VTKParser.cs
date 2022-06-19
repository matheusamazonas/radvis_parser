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
        var parser = from header in HeaderParser.Parse()
            from dimensions in ParseDimensions()
            select new File(header, dimensions);
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

    #endregion
}