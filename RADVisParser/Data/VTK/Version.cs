namespace RADVisParser.Data.VTK;

public readonly struct Version
{
    #region Properties

    public int Major { get; }
    public int Minor { get; }

    #endregion

    #region Setup

    internal Version(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }

    #endregion

    #region Public

    public override string ToString()
    {
        return $"{Major}.{Minor}";
    }

    #endregion
}