using Pidgin;
using RADVisParser.Data;
using static Pidgin.Parser;

namespace RADVisParser.Parsers;

internal static class CommonParser
{
    #region Internal

    internal static Parser<char, string> ParseLine()
    {
        return AnyCharExcept('\n', '\r')
            .ManyString()
            .Before(ParseLineEnding());
    }

    internal static Parser<char, Unit> ParseWhitespaces()
    {
        return Char(' ').Many().IgnoreResult();
    }

    internal static Parser<char, Unit> ParseLineEnding()
    {
        return Try(String("\r\n"))
            .Or(String("\n"))
            .IgnoreResult();
    }

    internal static Parser<char, Vector3<int>> ParseIntVector()
    {
        return from x in Int(10)
                .Before(SkipWhitespaces)
            from y in Int(10)
                .Before(SkipWhitespaces)
            from z in Int(10)
            select new Vector3<int>(x, y, z);
    }

    internal static Parser<char, double[]> ParseDoubleArray(int count)
    {
        return Real
            .Before(SkipWhitespaces)
            .Repeat(count)
            .Select(doubles => doubles.ToArray());
    }

    #endregion
}