using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StlLibrarySharp.Tests
{
    public class CalculationTest
    {
        [Fact]
        public void TestSignedVolume()
        {
            Solid solid;
            using (var str = Utils.OpenDataStream("ASCII.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(1000f, solid.GetSignedVolume());

            using (var str = Utils.OpenDataStream("Binary.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(1000f, solid.GetSignedVolume());

            using (var str = Utils.OpenDataStream("block.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(61.023746f, solid.GetSignedVolume(), 5);
            Assert.Equal(0f, Calculator.CalculateSignedVolume(null));

        }
    }
}
