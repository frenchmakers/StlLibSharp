using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace STL.Tests
{
    public class VertexTest
    {
        [Fact]
        public void TestCreate()
        {
            var vertex = new Vertex();
            Assert.Equal(0f, vertex.X);
            Assert.Equal(0f, vertex.Y);
            Assert.Equal(0f, vertex.Z);

            vertex = new Vertex(12.34f, -98.7f, 54);
            Assert.Equal(12.34f, vertex.X);
            Assert.Equal(-98.7f, vertex.Y);
            Assert.Equal(54.00f, vertex.Z);
        }

        [Fact]
        public void TestToString()
        {
            var vertex = new Vertex(12.34f, -98.7f, 54);
            Assert.Equal("[12.34, -98.7, 54]", vertex.ToString());
        }

        [Fact]
        public void TestGetHashCode()
        {
            var vertex = new Vertex(12.34f, -98.7f, 54);
            Assert.Equal(
                (12.34f).GetHashCode() ^ (-98.7f).GetHashCode() ^ (54f).GetHashCode()
                , vertex.GetHashCode()
            );
        }

        [Fact]
        public void TestEquals()
        {
            var vertex = new Vertex(12.34f, -98.7f, 54);

            Assert.False(vertex.Equals((object)null));
            Assert.False(vertex.Equals(12345));

            var vertex2 = new Vertex();
            Assert.False(vertex.Equals((object)vertex2));

            vertex2 = new Vertex(12.34f, -98.7f, 54);
            Assert.True(vertex.Equals((object)vertex2));

            Assert.True(vertex.Equals((object)vertex));
        }

        [Fact]
        public void TestEquatable()
        {
            var vertex = new Vertex(12.34f, -98.7f, 54);

            IEquatable<Vertex> ieqVertex = vertex;

            Assert.False(ieqVertex.Equals((Vertex)null));

            var vertex2 = new Vertex();
            Assert.False(ieqVertex.Equals(vertex2));

            vertex2 = new Vertex(12.34f, -98.7f, 54);
            Assert.True(ieqVertex.Equals(vertex2));

            Assert.True(ieqVertex.Equals(vertex));
        }

    }
}
