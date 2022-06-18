namespace RADVisParser.Data.VTK;

public readonly struct Header
{
    #region Properties

    public Version Version { get; }
    public string Name { get; }
    public FileFormat Format { get; }
    public DataType Topology { get; }

    #endregion

    #region Setup

    internal Header(Version version, string name, FileFormat format, DataType topology)
    {
        Version = version;
        Name = name;
        Format = format;
        Topology = topology;
    }

    #endregion

    #region Setup

    public override string ToString()
    {
        return $"Version: {Version}, Name: {Name}, Format: {Format}, Structure: {Topology}";
    }

    #endregion
}