using System.Linq;
using NetTopologySuite.IO.VectorTiles.Tilers;
using Xunit;

namespace NetTopologySuite.IO.VectorTiles.Tests.Tilers
{
    public class SharedTests
    {
        [Fact]
        public void Shared_LineBetween_1XR0YH_Should2Tiles()
        {
            var tiles = Shared.LineBetween(0.1, 0.5, 1.5, 0.9).ToList();
            
            Assert.Equal(2, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_1XL0YH_Should2Tiles()
        {
            var tiles = Shared.LineBetween(1.5, 0.5, 0.1, 0.9).ToList();
            
            Assert.Equal(2, tiles.Count);
            Assert.Equal(1, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(0, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_0XH1YU_Should2Tiles()
        {
            var tiles = Shared.LineBetween(0.5, 0.1, 0.9, 1.5).ToList();
            
            Assert.Equal(2, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(0, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_0XH1YD_Should2Tiles()
        {
            var tiles = Shared.LineBetween(0.5,1.5,0.9,0.1).ToList();
            
            Assert.Equal(2, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(1, tiles[0].Y);
            Assert.Equal(0, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Gradual_1XR1YU_Should3Tiles()
        {
            var tiles = Shared.LineBetween(0.9, 0.5, 1.9, 1.1).ToList();
            
            Assert.Equal(3, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Steep_1XR1YU_Should3Tiles()
        {
            var tiles = Shared.LineBetween(0.5, 0.9,1.1, 1.9).ToList();
            
            Assert.Equal(3, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(0, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Gradual_1XL1YU_Should3Tiles()
        {
            var tiles = Shared.LineBetween(1.9, 1.1, 0.9, 0.5).ToList();
            
            Assert.Equal(3, tiles.Count);
            Assert.Equal(1, tiles[0].X);
            Assert.Equal(1, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
            Assert.Equal(0, tiles[2].X);
            Assert.Equal(0, tiles[2].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Steep_1XL1YD_Should3Tiles()
        {
            var tiles = Shared.LineBetween(1.1, 1.9,0.5, 0.9).ToList();
            
            Assert.Equal(3, tiles.Count);
            Assert.Equal(1, tiles[0].X);
            Assert.Equal(1, tiles[0].Y);
            Assert.Equal(0, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
            Assert.Equal(0, tiles[2].X);
            Assert.Equal(0, tiles[2].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Gradual_2XR1YU_Should4Tiles()
        {
            var tiles = Shared.LineBetween(0.5, 0.5,2.5, 1.5).ToList();
            
            Assert.Equal(4, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
            Assert.Equal(2, tiles[3].X);
            Assert.Equal(1, tiles[3].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Steep_1XR2YU_Should4Tiles()
        {
            var tiles = Shared.LineBetween(0.5, 0.5,1.5, 2.5).ToList();
            
            Assert.Equal(4, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(0, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
            Assert.Equal(1, tiles[3].X);
            Assert.Equal(2, tiles[3].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Gradual_2XL1YD_Should4Tiles()
        {
            var tiles = Shared.LineBetween(2.5, 1.5, 0.5, 0.5).ToList();
            
            Assert.Equal(4, tiles.Count);
            Assert.Equal(2, tiles[0].X);
            Assert.Equal(1, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(0, tiles[2].Y);
            Assert.Equal(0, tiles[3].X);
            Assert.Equal(0, tiles[3].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Steep_1XL2YD_Should4Tiles()
        {
            var tiles = Shared.LineBetween(1.5, 2.5, 0.5, 0.5).ToList();
            
            Assert.Equal(4, tiles.Count);
            Assert.Equal(1, tiles[0].X);
            Assert.Equal(2, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
            Assert.Equal(0, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
            Assert.Equal(0, tiles[3].X);
            Assert.Equal(0, tiles[3].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Gradual_4XR1YU_Should6Tiles()
        {
            var tiles = Shared.LineBetween(0.5, 0.5,4.5, 1.5).ToList();
            
            Assert.Equal(6, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
            Assert.Equal(2, tiles[2].X);
            Assert.Equal(0, tiles[2].Y);
            Assert.Equal(2, tiles[3].X);
            Assert.Equal(1, tiles[3].Y);
            Assert.Equal(3, tiles[4].X);
            Assert.Equal(1, tiles[4].Y);
            Assert.Equal(4, tiles[5].X);
            Assert.Equal(1, tiles[5].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Steep_1XR4YU_Should6Tiles()
        {
            var tiles = Shared.LineBetween(0.5, 0.5,1.5, 4.5).ToList();
            
            Assert.Equal(6, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(0, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
            Assert.Equal(0, tiles[2].X);
            Assert.Equal(2, tiles[2].Y);
            Assert.Equal(1, tiles[3].X);
            Assert.Equal(2, tiles[3].Y);
            Assert.Equal(1, tiles[4].X);
            Assert.Equal(3, tiles[4].Y);
            Assert.Equal(1, tiles[5].X);
            Assert.Equal(4, tiles[5].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Gradual_4XL1YD_Should6Tiles()
        {
            var tiles = Shared.LineBetween(4.5, 1.5, 0.5, 0.5).ToList();
            
            Assert.Equal(6, tiles.Count);
            Assert.Equal(4, tiles[0].X);
            Assert.Equal(1, tiles[0].Y);
            Assert.Equal(3, tiles[1].X);
            Assert.Equal(1, tiles[1].Y);
            Assert.Equal(2, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
            Assert.Equal(2, tiles[3].X);
            Assert.Equal(0, tiles[3].Y);
            Assert.Equal(1, tiles[4].X);
            Assert.Equal(0, tiles[4].Y);
            Assert.Equal(0, tiles[5].X);
            Assert.Equal(0, tiles[5].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_Steep_1XL4YD_Should6Tiles()
        {
            var tiles = Shared.LineBetween(1.5, 4.5, 0.5, 0.5).ToList();
            
            Assert.Equal(6, tiles.Count);
            Assert.Equal(1, tiles[0].X);
            Assert.Equal(4, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(3, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(2, tiles[2].Y);
            Assert.Equal(0, tiles[3].X);
            Assert.Equal(2, tiles[3].Y);
            Assert.Equal(0, tiles[4].X);
            Assert.Equal(1, tiles[4].Y);
            Assert.Equal(0, tiles[5].X);
            Assert.Equal(0, tiles[5].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_2L2U_YChangeAtEnd_Should5Tiles()
        {
            var tiles = Shared.LineBetween(2.75, 0.125, 0.26, 2.29).ToList();
            
            Assert.Equal(5, tiles.Count);
            Assert.Equal(2, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
            Assert.Equal(0, tiles[3].X);
            Assert.Equal(1, tiles[3].Y);
            Assert.Equal(0, tiles[4].X);
            Assert.Equal(2, tiles[4].Y);
        }
        
        [Fact]
        public void Shared_LineBetween_2R2U_YChangeAtEnd_Should5Tiles()
        {
            var tiles = Shared.LineBetween(0.26, 0.125, 2.75, 2.29).ToList();
            
            Assert.Equal(5, tiles.Count);
            Assert.Equal(0, tiles[0].X);
            Assert.Equal(0, tiles[0].Y);
            Assert.Equal(1, tiles[1].X);
            Assert.Equal(0, tiles[1].Y);
            Assert.Equal(1, tiles[2].X);
            Assert.Equal(1, tiles[2].Y);
            Assert.Equal(2, tiles[3].X);
            Assert.Equal(1, tiles[3].Y);
            Assert.Equal(2, tiles[4].X);
            Assert.Equal(2, tiles[4].Y);
        }
    }
}