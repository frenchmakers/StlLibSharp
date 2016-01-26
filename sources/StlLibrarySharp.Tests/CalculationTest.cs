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

        [Fact]
        public void TestSize()
        {

            Solid solid;
            using (var str = Utils.OpenDataStream("block.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();

            SolidSize size = solid.GetSize();
            Assert.Equal(new Vertex(-1.968504f, -1.968504f, -1.968504f), size.Min);
            Assert.Equal(new Vertex(1.968504f, 1.968504f, 1.968504f), size.Max);
            Assert.Equal(new Vertex(3.937008f, 3.937008f, 3.937008f), size.Size);
            Assert.Equal(6.81909771961959, size.BoudingDiameter, 14);

            size = Calculator.CalculateSize(null);
            Assert.NotNull(size);
            Assert.Equal(new Vertex(), size.Min);
            Assert.Equal(new Vertex(), size.Max);
            Assert.Equal(new Vertex(), size.Size);
            Assert.Equal(0, size.BoudingDiameter, 14);

            size = Calculator.CalculateSize(new Facet[0]);
            Assert.NotNull(size);
            Assert.Equal(new Vertex(), size.Min);
            Assert.Equal(new Vertex(), size.Max);
            Assert.Equal(new Vertex(), size.Size);
            Assert.Equal(0, size.BoudingDiameter, 14);

        }
    }
}
