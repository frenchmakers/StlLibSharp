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
            Assert.Equal(1000f, Calculator.CalculateVolume(solid.Facets), 2);
            Assert.Equal(solid.GetSignedVolume(), Calculator.CalculateVolume(solid.Facets), 2);

            using (var str = Utils.OpenDataStream("Binary.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(1000f, Calculator.CalculateVolume(solid.Facets), 2);
            Assert.Equal(solid.GetSignedVolume(), Calculator.CalculateVolume(solid.Facets), 2);

            using (var str = Utils.OpenDataStream("block.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(61.02374f, solid.GetSignedVolume(), 5);
            Assert.Equal(solid.GetSignedVolume(), Calculator.CalculateVolume(solid.Facets), 4);
            Assert.Equal(0f, Calculator.CalculateSignedVolume(null));
        }

        [Fact]
        public void TestVolume()
        {

            Solid solid;
            using (var str = Utils.OpenDataStream("ASCII.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(1000f, Calculator.CalculateVolume(solid.Facets), 2);

            using (var str = Utils.OpenDataStream("Binary.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(1000f, Calculator.CalculateVolume(solid.Facets), 2);

            using (var str = Utils.OpenDataStream("block.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(61.02375f, Calculator.CalculateVolume(solid.Facets), 5);
            Assert.Equal(0f, Calculator.CalculateVolume(new Facet[0]));
            Assert.Equal(0f, Calculator.CalculateVolume(null));
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

        [Fact]
        public void TestArea()
        {

            Solid solid;
            using (var str = Utils.OpenDataStream("ASCII.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(600f, Calculator.CalculateSurfaceArea(solid.Facets));

            using (var str = Utils.OpenDataStream("Binary.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(600f, Calculator.CalculateSurfaceArea(solid.Facets));

            using (var str = Utils.OpenDataStream("block.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();
            Assert.Equal(93.00019f, Calculator.CalculateSurfaceArea(solid.Facets));
            Assert.Equal(0f, Calculator.CalculateSurfaceArea(null));
            Assert.Equal(0f, Calculator.CalculateArea(null));
        }

        [Fact]
        public void TestCalculateNormal()
        {
            Facet facet = new Facet(new Vertex(2,4,8), new Vertex[] {
                new Vertex(),
                new Vertex(8,4,2),
                new Vertex(-10,10,0),
            });

            Assert.Equal(new Vertex(-20, -20, 120), Calculator.CalculateNormal(facet));

            Assert.Null(Calculator.CalculateNormal(null));
        }

        [Fact]
        public void TestNormalizeVertex()
        {
            var vertex = new Vertex();
            Calculator.NormalizeVertex(vertex);
            Assert.Equal(new Vertex(), vertex);

            vertex = new Vertex(2, 4, 8);
            Calculator.NormalizeVertex(vertex);
            Assert.Equal(new Vertex(0.2182179f, 0.4364358f, 0.8728716f), vertex);

            vertex = new Vertex(-2, -4, -8);
            Calculator.NormalizeVertex(vertex);
            Assert.Equal(new Vertex(-0.2182179f, -0.4364358f, -0.8728716f), vertex);

            Calculator.NormalizeVertex(null);
        }

    }
}
