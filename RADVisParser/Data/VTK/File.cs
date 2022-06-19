namespace RADVisParser.Data.VTK;

public sealed class File
{
    #region Properties

    public Header Header { get; }
    public Vector3<int> Dimensions { get; }
    public Vector3<double>[] Positions { get; }

    #endregion

    #region Setup

    internal File(Header header, Vector3<int> dimensions, Vector3<double>[] positions)
    {
        Header = header;
        Dimensions = dimensions;
        Positions = positions;
    }

    #endregion
}