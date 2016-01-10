using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMP {

    public partial class GmpInteger {
            
        /// <summary>
        /// Returns lhs + rhs as a new object.
        /// </summary>
        public static GmpInteger operator + (GmpInteger lhs, GmpInteger rhs) {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_add(ref retVal._Storage, ref lhs._Storage, ref rhs._Storage);
            return retVal;
        }

        /// <summary>
        /// Returns lhs - rhs. 
        /// </summary>
        public static GmpInteger operator - (GmpInteger lhs, GmpInteger rhs) {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_sub(ref retVal._Storage, ref lhs._Storage, ref rhs._Storage);
            return retVal;
        }

        /// <summary>
        /// Returns lhs * rhs. 
        /// </summary>
        public static GmpInteger operator * (GmpInteger lhs, GmpInteger rhs) {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_mul(ref retVal._Storage, ref lhs._Storage, ref rhs._Storage);
            return retVal;
        }

        /// <summary>
        /// Returns lhs % rhs. (ie. modulo)
        /// </summary>
        public static GmpInteger operator % (GmpInteger lhs, GmpInteger rhs) {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_mod(ref retVal._Storage, ref lhs._Storage, ref rhs._Storage);
            return retVal;
        }

        /// <summary>
        /// Returns lhs / rhs.
        /// </summary>
        public static GmpInteger operator / (GmpInteger lhs, GmpInteger rhs) {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_tdiv_q(ref retVal._Storage, ref lhs._Storage, ref rhs._Storage);
            return retVal;
        }

        /// <summary>
        /// Gets negated value of the op.
        /// </summary>
        public static GmpInteger operator - (GmpInteger op) {
            GmpInteger retVal = new GmpInteger();
            Interop.mpz_neg(ref retVal._Storage, ref op._Storage);
            return retVal;
        }

        public static bool operator == (GmpInteger lhs, GmpInteger rhs) {
            // Use ReferenceEquals to avoid infinitae recursion
            bool lhsNull = ReferenceEquals(lhs, null);
            bool rhsNull = ReferenceEquals(rhs, null);
            if (lhsNull && rhsNull) return true;
            else if (lhsNull ^ rhsNull) return false;
            else return lhs.CompareTo(rhs) == 0;
        }

        public static bool operator != (GmpInteger lhs, GmpInteger rhs) {
            return !(lhs == rhs);
        }

        public static bool operator < (GmpInteger lhs, GmpInteger rhs) {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator > (GmpInteger lhs, GmpInteger rhs) {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator <= (GmpInteger lhs, GmpInteger rhs) {
            int cmp = lhs.CompareTo(rhs);
            return cmp < 0 || cmp == 0;
        }

        public static bool operator >= (GmpInteger lhs, GmpInteger rhs) {
            int cmp = lhs.CompareTo(rhs);
            return cmp > 0 || cmp == 0;
        }
    }
}
