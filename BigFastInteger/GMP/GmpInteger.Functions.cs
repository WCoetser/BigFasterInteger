using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMP {
    
    public partial class GmpInteger  {
        
        /// <summary>
        /// Gets this value, times 2 raised to v.
        /// </summary>
        public GmpInteger Pow2(uint v) {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_mul_2exp(ref retVal._Storage, ref _Storage, v);
            return retVal;
        }

        /// <summary>
        /// Gets absolute value of this value. Returns it in a new instance of BigFastInteger.
        /// </summary>
        public GmpInteger Abs() {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_abs(ref retVal._Storage, ref _Storage);
            return retVal;
        }
    }
}
