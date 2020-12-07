using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NetTopologySuite.IO.VectorTiles.Tests")]
namespace NetTopologySuite.IO.VectorTiles.Tiles
{
    /// <summary>
    /// Represents a tile.
    /// </summary>
    public class Tile
    {
        private readonly ulong _id;

        /// <summary>
        /// Creates a new tile from a given id.
        /// </summary>
        /// <param name="id"></param>
        public Tile(ulong id)
        {
            var vec = Tile.CalculateTile(_id = id);
            (this.X, this.Y, this.Zoom) = ((int)vec.X, (int)vec.Y, (int)vec.Z);

            this.CalculateBounds();
        }

        /// <summary>
        /// Creates a new tile.
        /// </summary>
        public Tile(int x, int y, int zoom)
        {
            _id = Tile.CalculateTileId(this.Zoom = zoom, this.X = x, this.Y = y);

            this.CalculateBounds();
        }

        private void CalculateBounds()
        {
            var zoomSquare = Math.Pow(2.0, this.Zoom);
            var n = Math.PI - (TileMath.PI2 * this.Y / zoomSquare);
            this.Left = (double)((this.X / zoomSquare * 360.0) - 180.0);
            this.Top = (double)(TileMath.OneRadian * Math.Atan(Math.Sinh(n)));

            n = Math.PI - (TileMath.PI2 * (this.Y + 1) / zoomSquare);
            this.Right = (double)(((this.X + 1) / zoomSquare * 360.0) - 180.0);
            this.Bottom = (double)(TileMath.OneRadian * Math.Atan(Math.Sinh(n)));

            this.CenterLat = (double)((this.Top + this.Bottom) / 2.0);
            this.CenterLon = (double)((this.Left + this.Right) / 2.0);
        }

        /// <summary>
        /// The X position of the tile.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// The Y position of the tile.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// The zoom level for this tile.
        /// </summary>
        public int Zoom { get; private set; }

        /// <summary>
        /// Gets the top.
        /// </summary>
        public double Top { get; private set; }

        /// <summary>
        /// Get the bottom.
        /// </summary>
        public double Bottom { get; private set; }

        /// <summary>
        /// Get the left.
        /// </summary>
        public double Left { get; private set; }

        /// <summary>
        /// Gets the right.
        /// </summary>
        public double Right { get; private set; }

        /// <summary>
        /// Gets the center lat.
        /// </summary>
        public double CenterLat { get; private set; }

        /// <summary>
        /// Gets the center lon.
        /// </summary>
        public double CenterLon { get; private set; }

        /// <summary>
        /// Gets the parent tile.
        /// </summary>
        public Tile Parent => new Tile(this.X / 2, this.Y / 2, this.Zoom - 1);

        /// <summary>
        /// Returns a hashcode for this tile position.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Zoom.GetHashCode());

        /// <summary>
        /// Returns true if the given object represents the same tile.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is Tile other)
            {
                return Equals(other);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Tile other)
        {
            return other.X == this.X &&
                       other.Y == this.Y &&
                       other.Zoom == this.Zoom;
        }

        /// <summary>
        /// Returns a description for this tile.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{this.X}x-{this.Y}y@{this.Zoom}z";

        /// <summary>
        /// Returns true if the given tiles are direct neighbours.
        /// </summary>
        /// <param name="tileId1">The first tile id.</param>
        /// <param name="tileId2">The second tile id.</param>
        /// <returns></returns>
        public static bool IsDirectNeighbour(ulong tileId1, ulong tileId2)
        {
            if (tileId1 == tileId2) return false;
            
            var tile1 = Tile.CalculateTile(tileId1);
            var tile2 = Tile.CalculateTile(tileId2);

            if (tile1.Z != tile2.Z)
            {
                return false;
            }

            if (tile1.X == tile2.X)
            {
                return (tile1.Y == tile2.Y + 1) ||
                       (tile1.Y == tile2.Y - 1);
            }
            else if (tile1.Y == tile2.Y)
            {
                return (tile1.X == tile2.X + 1) ||
                       (tile1.X == tile2.X - 1);
            }

            return false;
        }

        /// <summary>
        /// Calculates the tile id of the tile at position (0, 0) for the given zoom.
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        private static ulong CalculateTileId(int zoom)
        {
            switch (zoom)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 5;
                case 3: return 21;
                case 4: return 85;
                case 5: return 341;
                case 6: return 1365;
                case 7: return 5461;
                case 8: return 21845;
                case 9: return 87381;
                case 10: return 349525;
                case 11: return 1398101;
                case 12: return 5592405;
                case 13: return 22369621;
                case 14: return 89478485;
                case 15: return 357913941;
                case 16: return 1431655765;
                case 17: return 5726623061;
                case 18: return 22906492245;
                default:
                    {
                        var size = (ulong)System.Math.Pow(2, 2 * (zoom - 1));
                        var tileId = Tile.CalculateTileId(zoom - 1) + size;
                        return tileId;
                    }
            }
        }
        internal static ulong CalculateTileId(int zoom, System.Numerics.Vector2 v) => CalculateTileId(zoom, (int)v.X, (int)v.Y);

        /// <summary>
        /// Calculates the tile id of the tile at position (x, y) for the given zoom.
        /// </summary>
        /// <param name="zoom"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static ulong CalculateTileId(int zoom, int x, int y)
        {
            var id = Tile.CalculateTileId(zoom);
            var width = (long)System.Math.Pow(2, zoom);
            return id + (ulong) x + (ulong) (y * width);
        }

        /// <summary>
        /// Calculate the tile given the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static System.Numerics.Vector3 CalculateTile(ulong id)
        {
            // find out the zoom level first.
            var zoom = 0;
            if (id > 0)
            {
                // only if the id is at least at zoom level 1.
                while (id >= Tile.CalculateTileId(zoom))
                {
                    // move to the next zoom level and keep searching.
                    ++zoom;
                }

                --zoom;
            }

            // calculate the x-y.
            var local = id - Tile.CalculateTileId(zoom);
            var width = (ulong)System.Math.Pow(2, zoom);
            var x = (int) (local % width);
            var y = (int) (local / width);

            return new(x, y, zoom);
        }

        /// <summary>
        /// Returns the id of this tile.
        /// </summary>
        public ulong Id => _id;

        /// <summary>
        /// Returns true if this tile is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.X < 0 || this.Y < 0 || this.Zoom < 0) return false; // some are negative.
                var size = System.Math.Pow(2, this.Zoom);
                return this.X < size && this.Y < size;
            }
        }

        /// <summary>
        /// Returns the tile at the given location at the given zoom.
        /// </summary>
        public static Tile? CreateAroundLocation(double lat, double lon, int zoom)
        {
            if (!Tile.CreateAroundLocation(lat, lon, zoom, out var x, out var y))
            {
                return null;
            }

            return new Tile(x, y, zoom);
        }

        /// <summary>
        /// Returns the tile at the given location at the given zoom.
        /// </summary>
        public static ulong CreateAroundLocationId(double lat, double lon, int zoom)
        {
            if (!Tile.CreateAroundLocation(lat, lon, zoom, out var x, out var y))
            {
                return ulong.MaxValue;
            }

            return Tile.CalculateTileId(zoom, x, y);
        }

        /// <summary>
        /// A fast method of calculating x-y without creating a tile object.
        /// </summary>
        public static bool CreateAroundLocation(double lat, double lon, int zoom, out int x, out int y)
        {
            if (lon == 180)
            {
                lon = lon - 0.000001;
            }

            if (lat > 85.0511 || lat < -85.0511)
            {
                x = 0;
                y = 0;
                return false;
            }

            x = (int) ((lon + 180.0) / 360.0 * (1 << zoom));

            var latRad = lat * TileMath.OneDegree;

            y = (int) ((1.0 - Math.Log(Math.Tan(latRad) +
                                       1.0 / Math.Cos(latRad)) / Math.PI) / 2.0 * (1 << zoom));
            return true;
        }

        /// <summary>
        /// Gets the tile id the given lat/lon belongs to one zoom level lower.
        /// </summary>
        public ulong GetSubTileIdFor(double lat, double lon)
        {
            const int factor = 2;
            var zoom = this.Zoom + 1;
            int x = 0, y = 0;
            if (lat >= this.CenterLat && lon < this.CenterLon)
            {
                x = this.X * factor;
                y = this.Y * factor;
            }
            else if (lat >= this.CenterLat && lon >= this.CenterLon)
            {
                x = this.X * factor + factor - 1;
                y = this.Y * factor;
            }
            else if (lat < this.CenterLat && lon < this.CenterLon)
            {
                x = this.X * factor;
                y = this.Y * factor + factor - 1;
            }
            else if (lat < this.CenterLat && lon >= this.CenterLon)
            {
                x = this.X * factor + factor - 1;
                y = this.Y * factor + factor - 1;
            }

            return Tile.CalculateTileId(zoom, x, y);
        }

        /// <summary>
        /// Returns the subtiles of this tile at the given zoom.
        /// </summary>
        public TileRange GetSubTiles(int zoom)
        {
            if (this.Zoom > zoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom),
                    "Subtiles can only be calculated for higher zooms.");
            }

            if (this.Zoom == zoom)
            {
                // just return a range of one tile.
                return new TileRange(this.X, this.Y, this.X, this.Y, this.Zoom);
            }

            var factor = 1 << (zoom - this.Zoom);

            return new TileRange(
                this.X * factor,
                this.Y * factor,
                this.X * factor + factor - 1,
                this.Y * factor + factor - 1,
                zoom);
        }

        /// <summary>
        /// Inverts the X-coordinate.
        /// </summary>
        /// <returns></returns>
        public Tile InvertX()
        {
            var n = (int)System.Math.Floor(System.Math.Pow(2, this.Zoom));

            return new Tile(n - this.X - 1, this.Y, this.Zoom);
        }

        /// <summary>
        /// Inverts the Y-coordinate.
        /// </summary>
        /// <returns></returns>
        public Tile InvertY()
        {
            var n = (int)System.Math.Floor(System.Math.Pow(2, this.Zoom));

            return new Tile(this.X, n - this.Y - 1, this.Zoom);
        }

        internal System.Numerics.Vector2 SubCoordinates(double lat, double lon)
        {
            var leftOffset = lon - this.Left;
            var bottomOffset = lat - this.Bottom;

            return new (this.X + (float)(leftOffset / (this.Right - this.Left)),
                this.Y + (float)(bottomOffset / (this.Top - this.Bottom)));
        }

        public string ToGeoJson()
        {
            return $@"{{
                ""type"": ""Polygon"",
                ""coordinates"": [
                  [
                    [
                      {this.Left},
                      {this.Bottom}
                    ],
                    [
                      {this.Right},
                      {this.Bottom}
                    ],
                    [
                      {this.Right},
                      {this.Top}
                    ],
                    [
                      {this.Left},
                      {this.Top}
                    ],
                    [
                      {this.Left},
                      {this.Bottom}
                    ]
                  ]
                ]
              }}";
        }
    }
}