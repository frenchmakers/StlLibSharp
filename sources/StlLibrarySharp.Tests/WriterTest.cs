using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace STL.Tests
{
    public class WriterTest
    {

        [Fact]
        public void TestWriter()
        {
            using (MemoryStream stream = new MemoryStream())
            using (var writer = new StlTextWriter(stream))
            {
                Assert.Throws<ArgumentNullException>(() => writer.WriteSolid(null));
            }

            Assert.Throws<ArgumentNullException>(() => new StlTextWriter(null));
        }

        [Fact]
        public void TestTextWriter()
        {
            Solid solid1 = new Solid("test", new List<Facet>()
            {
                new Facet(new Vertex( 0.23f, 0, 1), new Vertex[]
                {
                    new Vertex( 0, 0, 0),
                    new Vertex(-10.123f, -10, 0),
                    new Vertex(-10.123f, 0, 0)
                }, 0)
            });

            byte[] data;
            string dataString1;

            using (MemoryStream stream = new MemoryStream())
            using (var writer = new StlTextWriter(stream))
            {
                writer.WriteSolid(solid1);
                data = stream.ToArray();
                dataString1 = Consts.FileEncoding.GetString(data);
            }

            Solid solid2;

            using (MemoryStream stream = new MemoryStream(data))
            using (var reader = new StlReader(stream))
            {
                solid2 = reader.ReadSolid();
            }

            Assert.Equal(solid1.Name, solid2.Name);
            Assert.Equal(solid1.Facets.Count, solid2.Facets.Count);
            for (int i = 0; i < solid1.Facets.Count; i++)
                Assert.True(solid1.Facets[i].Equals(solid2.Facets[i]));

        }

        [Fact]
        public void TestBinaryWriter()
        {
            Solid solid1 = new Solid("test", new Facet[]
            {
                new Facet(new Vertex( 0, 0, 1), new Vertex[]
                {
                    new Vertex( 0, 0, 0),
                    new Vertex(-10, -10, 0),
                    new Vertex(-10, 0, 0)
                }, 0)
            });

            byte[] data;

            using (MemoryStream stream = new MemoryStream())
            using (var writer = new StlBinaryWriter(stream))
            {
                writer.WriteSolid(solid1);
                data = stream.ToArray();
            }

            Solid solid2;

            using (MemoryStream stream = new MemoryStream(data))
            using (var reader = new StlReader(stream))
            {
                solid2 = reader.ReadSolid();
            }

            Assert.NotEqual(solid1.Name, solid2.Name);
            Assert.Null(solid2.Name);
            Assert.Equal(solid1.Facets.Count, solid2.Facets.Count);
            for (int i = 0; i < solid1.Facets.Count; i++)
                Assert.True(solid1.Facets[i].Equals(solid2.Facets[i]));

        }

    }
}
