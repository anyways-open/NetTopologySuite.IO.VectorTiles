using System.Collections.Generic;
using System.IO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace NetTopologySuite.IO.VectorTiles.Tests.Functional
{
    class Program
    {
        static IEnumerable<(IFeature feature, int zoom, string layerName)> ConfigureFeature(IFeature feature)
        {
            for (var z = 12; z <= 14; ++z)
            {
                if (feature.Geometry is LineString)
                {
                    yield return (feature, z, "cyclenetwork");
                }
                else if (feature.Geometry is Polygon)
                {
                    yield return (feature, z, "polygons");
                }
                else
                {
                    yield return (feature, z, "cyclenodes");
                }
            }
        }

        static void Main(string[] args)
        {
            var JsonSerializerOptions = new System.Text.Json.JsonSerializerOptions();

            JsonSerializerOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());

            var features = System.Text.Json.JsonSerializer.Deserialize<FeatureCollection>(File.ReadAllText("test.geojson"), JsonSerializerOptions);

            // build the vector tile tree.
            var tree = new VectorTileTree();

            foreach (var feature in features!) tree.Add(ConfigureFeature(feature));

            // write the tiles to disk as mvt.
            Mapbox.MapboxTileWriter.Write(tree, "tiles");

            // write the tiles to disk as geojson.
            GeoJson.GeoJsonTileWriter.Write(tree, "tiles");
        }
    }
}