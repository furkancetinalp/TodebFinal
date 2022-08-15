using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Helper
{
    public static class CalculateHelper
    {
        public static decimal CalculateCommission(decimal price)
        {
            var commision = price * (decimal)0.06;
            return commision;
        }

        public static decimal CalculateVAT(decimal price, float rate)
        {
            var vat = (price * (decimal)rate) / 240;
            return vat;
        }
    }
}
