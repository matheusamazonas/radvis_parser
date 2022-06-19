namespace RADVisParser.Data;

public readonly struct Vector3<T>
{
    #region Properties

    public T X { get; }
    public T Y { get; }
    public T Z { get; }

    #endregion

    #region Setup

    internal Vector3(T x, T y, T z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #endregion

    #region Public

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    #endregion
}