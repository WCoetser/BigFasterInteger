using ObjectLifeCycleManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMP {

    /// <summary>
    /// Wrapper class for GMP multi-precision integers.
    /// </summary>
    public partial class GmpInteger : IDisposable {

        /// <summary>
        /// Prevent multiple disposals.
        /// </summary>
        private bool _IsDisposed = false;

        /// <summary>
        /// Struct with information for the actual integer, links back to the GMP library.
        /// Note: "readonly" field cannot be passed by ref.
        /// </summary>
        private mpz_t _Storage;

        /// <summary>
        /// Initialize a zero valued integer.
        /// </summary>
        public GmpInteger() {
            Interop.mpz_init(ref _Storage);
            DisposeContext.AddDisposeObject(this);
        }

        /// <summary>
        /// Creates an integer from a string.
        /// </summary>
        public GmpInteger(string initialValue) {
            if (string.IsNullOrWhiteSpace(initialValue)) throw new ArgumentException("initialValue");
            Interop.mpz_set_str(ref _Storage, initialValue, 10);
            DisposeContext.AddDisposeObject(this);
        }

        /// <summary>
        /// Create a new integer, importing it from an array of input values and setting the sign.
        /// The most significant byte is given first.
        /// This option is here for saving and loading the integer.
        /// The sign is specified seperately because GMP uses magnitude/sign format.
        /// </summary>
        public GmpInteger(byte[] initialValue, bool isNegative = false) {
            if (initialValue == null) throw new ArgumentException("initialValue");
            Interop.mpz_import(ref _Storage, initialValue.Length, 1, 1, 0, 0, initialValue);
            if (isNegative) {
                mpz_t negStorage = new mpz_t();
                Interop.mpz_neg(ref negStorage, ref _Storage);
                Interop.mpz_clear(ref _Storage);
                _Storage = negStorage;
            }
            DisposeContext.AddDisposeObject(this);
        }

        /// <summary>
        /// Gets the number as a byte[] for saving purposes. The sign is returned seperately because GMP uses sign/magniture format.
        /// Data is exported with most signaficant byte first.
        /// </summary>
        public void Export(out byte[] data, out bool isNegative) {
            // Calculate buffer size based on information from https://gmplib.org/manual/Integer-Import-and-Export.html.
            const int numb = 8; // * size - nail;
            int count = (Interop.mpz_sizeinbase(ref _Storage, 2) + numb - 1) / numb;
            data = new byte[count];
            int dataCount = 0;
            Interop.mpz_export(data, ref dataCount, 1, 1, 0, 0, ref _Storage);
            GmpInteger zero = new GmpInteger();
            isNegative = zero.CompareTo(this) > 0;
        }

        /// <summary>
        /// De-allocate unmanaged memory held by the GMP library.
        /// </summary>
        public void Dispose() {
            if (_IsDisposed) return;
            Interop.mpz_clear(ref _Storage);
            _IsDisposed = true;
        }

        /// <summary>
        /// Converts the GMP integer to a base-10 string.
        /// </summary>
        public override string ToString() {
            int length = Interop.mpz_sizeinbase(ref _Storage, 10) + 2; // add 2 for the sign and \0
            StringBuilder strVal = new StringBuilder(length);
            Interop.mpz_get_str(strVal, 10, ref this._Storage);
            return strVal.ToString();
        }
    }
}
