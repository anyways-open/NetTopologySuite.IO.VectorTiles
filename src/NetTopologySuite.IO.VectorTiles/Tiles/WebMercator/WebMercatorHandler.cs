using System;

namespace NetTopologySuite.IO.VectorTiles.Tiles.WebMercator
{
    public static class WebMercatorHandler
    {
        // https://gist.github.com/nagasudhirpulla/9b5a192ccaca3c5992e5d4af0d1e6dc4
        
        //Converts given lat/lon in WGS84 Datum to XY in Spherical Mercator EPSG:900913
        public static System.Numerics.Vector2 LatLonToMeters(double lat, double lon)
        {
            var x = lon * TileMath.OriginShift / 180;
            var y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y = y * TileMath.OriginShift / 180;
            return new System.Numerics.Vector2((float)x, (float)y);
        }
        
        //Converts XY point from (Spherical) Web Mercator EPSG:3785 (unofficially EPSG:900913) to lat/lon in WGS84 Datum
        public static System.Numerics.Vector2 MetersToLatLon(System.Numerics.Vector2 m)
        {
            var x = m.X / TileMath.OriginShift * 180;
            var y = m.Y / TileMath.OriginShift * 180;
            y = 180 / Math.PI * (2 * Math.Atan(Math.Exp(y * Math.PI / 180)) - Math.PI / 2);
            return new System.Numerics.Vector2((float)x, (float)y);
        }
        
        //Converts EPSG:900913 to pyramid pixel coordinates in given zoom level
        public static System.Numerics.Vector2 MetersToPixels(System.Numerics.Vector2 m, int zoom, int tileSize)
        {
            var res = Resolution(zoom, tileSize);
            var x = (m.X + TileMath.OriginShift) / res;
            var y = (m.Y + TileMath.OriginShift) / res;
            return new System.Numerics.Vector2((float)x, (float)y);
        }
        
        //Converts pixel coordinates in given zoom level of pyramid to EPSG:900913
        public static System.Numerics.Vector2 PixelsToMeters(System.Numerics.Vector2 p, int zoom, int tileSize)
        {
            var res = Resolution(zoom, tileSize);
            var x = p.X * res - TileMath.OriginShift;
            var y = p.Y * res - TileMath.OriginShift;
            return new System.Numerics.Vector2((float)x, (float)y);
        }
        
        //Resolution (meters/pixel) for given zoom level (measured at Equator)
        public static double Resolution(int zoom, int tileSize)=> InitialResolution(tileSize) / Math.Pow(2, zoom);

       public static double InitialResolution(int tileSize)=> TileMath.PI2EarthRadius / tileSize;
    }
}