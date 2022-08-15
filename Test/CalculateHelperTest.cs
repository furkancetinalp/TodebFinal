using Business.Configuration.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class CalculateHelperTest
    {
        [Fact]
        public void CalculateCommission_Success()
        {
            // arrange
            decimal price = 100;

            //act
            var response = CalculateHelper.CalculateCommission(price);

            //assert
            Assert.Equal(response, (decimal)2);

        }
    }
}
