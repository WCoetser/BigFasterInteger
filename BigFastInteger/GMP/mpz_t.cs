using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GMP {

    /// <summary>
    /// Main storage for integers, where mp_limb_t is the basic machine word used to store a grouping of bits.
    /// On a 64-bit computer this should be a 64 bit word.
    /// 
    /// see __mpz_struct and typedef __mpz_struct *mpz_ptr in gmp-h.in.
    /// In this cross-compilation output the limbs are 8 bytes long
    /// 
    /// Assumptions: int = 32 bits, long = 64 bits
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct mpz_t {

        /// <summary>
        /// Number of *limbs* allocated and pointed
        /// to by the _mp_d field.
        /// </summary>
        public int _mp_alloc;

        /// <summary>
        /// abs(_mp_size) is the number of limbs the
        /// last field points to.  If _mp_size is
        /// negative this is a negative number.  */
        /// </summary>
        public int _mp_size;

        /// <summary>
        /// Pointer to the limbs.
        /// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        //ulong[] _mp_d;
        public IntPtr _mp_d;
    }

}

