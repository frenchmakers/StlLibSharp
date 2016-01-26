using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StlLibrarySharp.Tests
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
    }
}
