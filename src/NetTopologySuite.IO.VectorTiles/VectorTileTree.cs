using System.Collections;
using System.Collections.Generic;

namespace NetTopologySuite.IO.VectorTiles
{
    /// <summary>
    /// A vector tile tree.
    /// </summary>
    public class VectorTileTree : IEnumerable<ulong>, IEnumerable<VectorTile>
    {
        private readonly IDictionary<ulong, VectorTile> _tiles = new Dictionary<ulong, VectorTile>();

        /// <summary>
        /// All contained <see cref="VectorTiles"/>
        /// </summary>
        public IEnumerable<VectorTile> Tiles => _tiles.Values;

        /// <summary>
        /// Tries to get the given tile.
        /// </summary>
        /// <param name="tileId">The tile id.</param>
        /// <param name="vectorTile">The resulting tile (if any).</param>
        /// <returns>True if the tile exists.</returns>
        public bool TryGet(ulong tileId, out VectorTile vectorTile)=> _tiles.TryGetValue(tileId, out vectorTile);

        /// <summary>
        /// Gets a <see cref="VectorTile"/> by id
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public VectorTile this[ulong tileId]
        {
            get => _tiles[tileId];
            set => _tiles[tileId] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ulong> GetEnumerator()=> _tiles.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<VectorTile> IEnumerable<VectorTile>.GetEnumerator()=> Tiles.GetEnumerator();
    }
}