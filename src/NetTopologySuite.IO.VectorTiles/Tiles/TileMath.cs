namespace NetTopologySuite.IO.VectorTiles.Tiles
{
    internal static class TileMath
    {
        internal const int EarthRadius = 6378137;
        internal const double PI2 = System.Math.PI * 2;
        internal const double OriginShift = PI2 * EarthRadius / 2;
        internal const double PI2EarthRadius = PI2 * EarthRadius;
        internal const double OneRadian = 180.0 / System.Math.PI;
        internal const double OneDegree = System.Math.PI / 180.0;
    }
}
