using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GMP {

    /// <summary>
    /// Functions for custom allocations and random number generation.
    /// </summary>
    internal static class Interop {

        /// <summary>
        /// Initialize x, and set its value to 0.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_init")]
        public static extern bool mpz_init(ref mpz_t x);

        /// <summary>
        /// Set the number based on a input string.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_set_str")]
        public static extern int mpz_set_str(ref mpz_t rop, string str, int base_);

        /// <summary>
        /// Free the space occupied by x.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_clear")]
        public static extern bool mpz_clear(ref mpz_t x);

        /// <summary>
        /// w = u + v
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_add")]
        public static extern void mpz_add(ref mpz_t w, ref mpz_t u, ref mpz_t v);

        /// <summary>
        /// w = u - v
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_sub")]
        public static extern void mpz_sub(ref mpz_t w, ref mpz_t u, ref mpz_t v);

        /// <summary>
        /// Gets absolute value.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_abs")]
        public static extern void mpz_abs(ref mpz_t absV, ref mpz_t v);

        /// <summary>
        /// Negates v and returns it in negV.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_neg")]
        public static extern void mpz_neg(ref mpz_t negV, ref mpz_t v);

        /// <summary>
        /// Set rop to op1 times 2 raised to op2. This operation can also be defined as a left shift by op2 bits.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_mul_2exp")]
        public static extern void mpz_mul_2exp(ref mpz_t rop, ref mpz_t op1, uint op2);

        /// <summary>
        /// rop = op1 * op2. This invokes mpz_mul in the GMP library which uses fast multiplication algorithms based on the size of the operands.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_mul")]
        public static extern void mpz_mul(ref mpz_t rop, ref mpz_t op1, ref mpz_t op2);

        /// <summary>
        /// Set rop = op1 mod op2
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_mod")]
        public static extern void mpz_mod(ref mpz_t rop, ref mpz_t op1, ref mpz_t op2);

        /// <summary>
        /// Set q = n div d. rop is rounded to 0.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_tdiv_q")]
        public static extern void mpz_tdiv_q(ref mpz_t q, ref mpz_t n, ref mpz_t d);

        /// <summary>
        /// Compare op1 and op2. Return a positive value if op1 &gt; op2, zero if op1 = op2, or a negative value if op1 &lt; op2. 
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_cmp")]
        public static extern int mpz_cmp(ref mpz_t op1, ref mpz_t op2);

        /// <summary>
        /// Import a number from an array of ulongs.
        /// </summary>
        /// <param name="rop">Output value.</param>
        /// <param name="count">Number of input words (limbs)</param>
        /// <param name="endian">From the manual: "1 for most significant byte first, -1 for least significant first, or 0 for the native endianness of the host CPU"</param>
        /// <param name="size">Word size. For a byte[] this is 1.</param>
        /// <param name="nails">The most significant nails bits of each word are skipped, this can be 0 to use the full words.</param>
        /// <param name="order">Order can be 1 for most significant word first or -1 for least significant first.</param>
        /// <param name="op">Input data as an array of ulong values.</param>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_import")]
        public static extern void mpz_import(ref mpz_t rop, int count, int order, int size, int endian, int nails, byte[] op);

        /// <summary>
        /// Gets the buffer size needed to represent op in base_.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_sizeinbase")]
        public static extern int mpz_sizeinbase(ref mpz_t op, int base_);

        /// <summary>
        /// Convert op to a string of digits in base base. The base argument may vary from 2 to 62 or from -2 to -36.
        /// For base in the range 2..36, digits and lower-case letters are used; for -2..-36, digits and upper-case letters are used; for 37..62, digits, upper-case letters, and lower-case letters(in that significance order) are used.
        /// If str is NULL, the result string is allocated using the current allocation function(see Custom Allocation). The block will be strlen(str)+1 bytes, that being exactly enough for the string and null-terminator.
        /// If str is not NULL, it should point to a block of storage large enough for the result, that being mpz_sizeinbase(op, base) + 2. The two extra bytes are for a possible minus sign, and the null-terminator.
        /// A pointer to the result string is returned, being either the allocated block, or the given str.
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_get_str")]
        public static extern IntPtr mpz_get_str([MarshalAs(UnmanagedType.LPStr)] StringBuilder str, int base_, ref mpz_t op);

        /// <summary>
        /// Export the data in op. Only the magnitude part is exported.
        /// <param name="rop">The output data.</param>
        /// <param name="countp">The number of types output.</param>
        /// <param name="order">Order can be 1 for most significant word first or -1 for least significant first. </param>
        /// <param name="size">Size in bytes for rop. For byte[] this should be 1.</param>
        /// <param name="endian">Within each word endian can be 1 for most significant byte first, -1 for least significant first, or 0 for the native endianness of the host CPU.</param>
        /// <param name="nails">0 padding inside words.</param>
        /// <param name="op">The number to export.</param>
        /// </summary>
        [DllImport("libgmp-10.dll", EntryPoint = "__gmpz_export")]
        public static extern IntPtr mpz_export(byte[] rop, ref int countp, int order, int size, int endian, int nails, ref mpz_t op);

    }
}
