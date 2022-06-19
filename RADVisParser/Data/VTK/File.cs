namespace RADVisParser.Data.VTK;

public sealed class File
{
    #region Properties

    public Header Header { get; }
    public Vector3<int> Dimensions { get; }

    #endregion

    #region Setup

    internal File(Header header, Vector3<int> dimensions)
    {
        Header = header;
        Dimensions = dimensions;
    }

    #endregion
}