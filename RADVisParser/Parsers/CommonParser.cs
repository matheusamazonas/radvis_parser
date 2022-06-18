using Pidgin;
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

    internal static Parser<char, Unit> ParseLineEnding()
    {
        return Try(String("\r\n"))
            .Or(String("\n"))
            .IgnoreResult();
    }

    #endregion
}