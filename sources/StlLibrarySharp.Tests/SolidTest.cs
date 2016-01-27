using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace STL.Tests
{
    public class SolidTest
    {

        [Fact]
        public void TestCreate()
        {
            var solid = new Solid();
            Assert.Null(solid.Name);
            Assert.NotNull(solid.Facets);
            Assert.Equal(0, solid.Facets.Count);

            solid = new Solid("Test", new Facet[] { new Facet(), new Facet() });
            Assert.Equal("Test", solid.Name);
            Assert.NotNull(solid.Facets);
            Assert.Equal(2, solid.Facets.Count);
        }

    }
}
