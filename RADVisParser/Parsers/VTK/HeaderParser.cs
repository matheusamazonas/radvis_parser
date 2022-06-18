using Pidgin;
using static Pidgin.Parser;
using static RADVisParser.Parsers.CommonParser;
using RADVisParser.Data.VTK;
using Version = RADVisParser.Data.VTK.Version;

namespace RADVisParser.Parsers.VTK;

internal static class HeaderParser
{
    #region Internal

    internal static Parser<char, Header> Parse()
    {
        return from version in ParseVersion()
            from name in ParseLine()
            from format in ParseFormat()
            from topology in ParseTopology()
            select new Header(version, name, format, topology);
    }
    
    #endregion

    #region Private

    private static Parser<char, Version> ParseVersion()
    {
        return from major in String("# vtk DataFile Version ").Then(UnsignedInt(10)) 
            from minor in Char('.').Then(UnsignedInt(10)).Before(ParseLineEnding())
            select new Version(major, minor);
    }
    
    private static Parser<char, FileFormat> ParseFormat()
    {
        return ParseLine()
            .Map(ToFileFormat);

        FileFormat ToFileFormat(string format)
        {
            return format switch
            {
                "ASCII" => FileFormat.Ascii,
                "BINARY" => FileFormat.Binary,
                _ => throw new NotImplementedException($"File format not implemented: {format}")
            };
        }
    }

    private static Parser<char, DataType> ParseTopology()
    {
        return String("DATASET ")
            .Then(ParseDataType());
    }
    
    private static Parser<char, DataType> ParseDataType()
    {
        return ParseLine()
            .Map(ToDataType);
        
        DataType ToDataType(string datasetStructure)
        {
            return datasetStructure switch
            {
                "STRUCTURED_POINTS" => DataType.StructuredPoints,
                "STRUCTURED_GRID" => DataType.StructuredGrid,
                "UNSTRUCTURED_GRID" => DataType.UnstructuredGrid,
                "POLYDATA" => DataType.Polydata,
                "RECTILINEAR_GRID" => DataType.RectilinearGrid,
                "FIELD" => DataType.Field,
                _ => throw new NotImplementedException($"Data structure not implemented: {datasetStructure}")
            };
        }
    }
    
    #endregion
}