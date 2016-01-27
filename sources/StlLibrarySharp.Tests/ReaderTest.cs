using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace STL.Tests
{
    public class ReaderTest
    {

        [Fact]
        public void TestCreateReader()
        {
            using (var str = Utils.OpenDataStream("ASCII.stl"))
            {
                using (var reader = new StlReader(str))
                    reader.ReadSolid();
                Assert.Throws<ObjectDisposedException>(() => str.ReadByte());
            }

            Assert.Throws<ArgumentNullException>(() => new StlReader(null));

        }

        [Fact]
        public void TestReader()
        {
            Solid solid;
            using (var str = Utils.OpenDataStream("ASCII.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();

            Assert.NotNull(solid);
            Assert.Equal(12, solid.Facets.Count);
            foreach (Facet facet in solid.Facets)
                Assert.Equal(3, facet.Vertices.Count);

            using (var str = Utils.OpenDataStream("Binary.stl"))
            using (var reader = new StlReader(str))
                solid = reader.ReadSolid();

            Assert.NotNull(solid);
            Assert.Equal(12, solid.Facets.Count);
            foreach (Facet facet in solid.Facets)
                Assert.Equal(3, facet.Vertices.Count);

            using (var str = new MemoryStream(new byte[4]))
            using (var reader = new StlReader(str))
                Assert.Throws<FormatException>(() => reader.ReadSolid());

        }

        [Fact]
        public void TestReader_ReadFacets()
        {
            IList<Facet> facets;
            using (var str = Utils.OpenDataStream("ASCII.stl"))
            using (var reader = new StlReader(str))
                facets = reader.ReadFacets().ToList();

            Assert.Equal(12, facets.Count);
            foreach (Facet facet in facets)
                Assert.Equal(3, facet.Vertices.Count);

            using (var str = Utils.OpenDataStream("Binary.stl"))
            using (var reader = new StlReader(str))
                facets = reader.ReadFacets().ToList();

            Assert.Equal(12, facets.Count);
            foreach (Facet facet in facets)
                Assert.Equal(3, facet.Vertices.Count);

            using (var str = new MemoryStream(new byte[4]))
            using (var reader = new StlReader(str))
                Assert.Throws<FormatException>(() => reader.ReadFacets().ToList());

        }

        [Fact]
        public void TestTextReader()
        {
            Solid solid;
            using (var str = Utils.OpenDataStream("ASCII.stl"))
            {
                var reader = new StlTextReader(str);
                solid = reader.ReadSolid();
            }

            Assert.NotNull(solid);
            Assert.Equal(12, solid.Facets.Count);
            foreach (Facet facet in solid.Facets)
                Assert.Equal(3, facet.Vertices.Count);

            Assert.Throws<ArgumentNullException>(() => new StlTextReader(null));

            using (var str = new MemoryStream(Consts.FileEncoding.GetBytes("test")))
            {
                var reader = new StlTextReader(str);
                var ex = Assert.Throws<FormatException>(() => reader.ReadSolid());
                Assert.Equal("Invalid text STL file header. Expected 'solid [name]' but 'test' found.", ex.Message);
            }

            using (var str = Utils.MakeStream("solid test\nfacet normal 1 2 3\nouter loop\nvertex 3 2 1\n"))
            {
                var reader = new StlTextReader(str);
                solid = reader.ReadSolid();
                Assert.Equal(0, solid.Facets.Count);
            }

            using (var str = Utils.MakeStream("solid test\nfacet normal 1 2 3\nouter loop\nvertex a 1.2 2\n"))
            {
                var reader = new StlTextReader(str);
                var ex = Assert.Throws<FormatException>(() => reader.ReadSolid());
                Assert.Equal("Could not parse X coordinate 'a' as a decimal.", ex.Message);
            }
            using (var str = Utils.MakeStream("solid test\nfacet normal 1 2 3\nouter loop\nvertex 1.2 a 2\n"))
            {
                var reader = new StlTextReader(str);
                var ex = Assert.Throws<FormatException>(() => reader.ReadSolid());
                Assert.Equal("Could not parse Y coordinate 'a' as a decimal.", ex.Message);
            }
            using (var str = Utils.MakeStream("solid test\nfacet normal 1 2 3\nouter loop\nvertex 1.2 2 a\n"))
            {
                var reader = new StlTextReader(str);
                var ex = Assert.Throws<FormatException>(() => reader.ReadSolid());
                Assert.Equal("Could not parse Z coordinate 'a' as a decimal.", ex.Message);
            }

        }

        [Fact]
        public void TestBinaryReader()
        {
            Solid solid;
            using (var str = Utils.OpenDataStream("Binary.stl"))
            {
                var reader = new StlBinaryReader(str);
                solid = reader.ReadSolid();
            }

            Assert.NotNull(solid);
            Assert.Equal(12, solid.Facets.Count);
            foreach (Facet facet in solid.Facets)
                Assert.Equal(3, facet.Vertices.Count);

            Assert.Throws<ArgumentNullException>(() => new StlBinaryReader(null));
        }

    }
}
