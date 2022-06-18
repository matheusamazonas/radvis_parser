using Pidgin;
using RADVisParser.Data.VTK;

namespace RADVisParser.Parsers.VTK;

public class VTKParser
{
    #region Fields

    private readonly string _filePath;

    #endregion

    #region Setup

    public VTKParser(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File to be parsed not found: {filePath}");

        _filePath = filePath;
    }

    #endregion

    #region Public

    public Header Parse()
    {
        using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
        using var readStream = new StreamReader(fileStream);
        return HeaderParser.Parse().ParseOrThrow(readStream);
    }

    #endregion
}