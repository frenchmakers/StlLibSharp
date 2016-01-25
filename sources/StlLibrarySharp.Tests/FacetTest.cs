using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StlLibrarySharp.Tests
{
    public class FacetTest
    {
        [Fact]
        public void TestCreate()
        {
            var facet = new Facet();
            Assert.NotNull(facet.Normal);
            Assert.NotNull(facet.Vertices);
            Assert.Equal(0, facet.AttributeByteCount);
            Assert.Equal(0, facet.Vertices.Count);

            var vn = new Vertex(1, 2, 3);

            facet = new Facet(vn, null);
            Assert.Same(vn, facet.Normal);
            Assert.NotNull(facet.Vertices);
            Assert.Equal(0, facet.AttributeByteCount);
            Assert.Equal(0, facet.Vertices.Count);

            facet = new Facet(null, new Vertex[] { new Vertex(), new Vertex(3, 2, 1) }, 23);
            Assert.NotNull(facet.Normal);
            Assert.NotNull(facet.Vertices);
            Assert.Equal(23, facet.AttributeByteCount);
            Assert.Equal(2, facet.Vertices.Count);
        }

        [Fact]
        public void TestToString()
        {
            var vn = new Vertex(1.2f, -3.4f, 5);
            var facet = new Facet(vn, null);
            Assert.Equal("facet([1.2, -3.4, 5])", facet.ToString());
        }

        [Fact]
        public void TestEquals()
        {
            var vn = new Vertex(12.34f, -98.7f, 54);
            var vertices = new Vertex[] { new Vertex(), vn, new Vertex(3, 2, 1) };
            var facet = new Facet(vn, vertices);

            Assert.False(facet.Equals((object)null));
            Assert.False(facet.Equals(12345));

            var facet2 = new Facet();
            Assert.False(facet.Equals((object)facet2));

            facet2 = new Facet(new Vertex(12.34f, -98.7f, 54), null);
            Assert.False(facet.Equals((object)facet2));

            facet2 = new Facet(new Vertex(12.34f, -98.7f, 54), vertices);
            Assert.True(facet.Equals((object)facet2));

            Assert.True(facet.Equals((object)facet));
        }

        [Fact]
        public void TestEquatable()
        {
            var vn = new Vertex(12.34f, -98.7f, 54);
            var vertices = new Vertex[] { new Vertex(), vn, new Vertex(3, 2, 1) };
            var facet = new Facet(vn, vertices);

            IEquatable<Facet> ieqFacet = facet;

            Assert.False(ieqFacet.Equals((Facet)null));

            var facet2 = new Facet();
            Assert.False(ieqFacet.Equals(facet2));

            facet2 = new Facet(new Vertex(12.34f, -98.7f, 54), null);
            Assert.False(ieqFacet.Equals(facet2));

            facet2 = new Facet() { Normal = null };
            Assert.False(ieqFacet.Equals(facet2));

            facet2 = new Facet() { Vertices = null };
            Assert.False(ieqFacet.Equals(facet2));

            facet2 = new Facet(new Vertex(12.34f, -98.7f, 54), vertices);
            Assert.True(ieqFacet.Equals(facet2));

            Assert.True(ieqFacet.Equals(facet));
        }

    }
}
