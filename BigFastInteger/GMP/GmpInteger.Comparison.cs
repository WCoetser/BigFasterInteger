using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GMP
{
    public partial class GmpInteger : IComparable, IComparable<GmpInteger> {

        /// <summary>
        /// NB: This must change if _Storage change.
        /// </summary>
        private int? _HashCode;

        public int CompareTo(object obj) {
            return CompareTo((GmpInteger)obj);
        }

        /// <summary>
        /// Return a positive value if (this) &gt; (other), zero if (this) = (other), or a negative value if (this) &lt; (other). 
        /// </summary>
        public int CompareTo(GmpInteger other) {
            // Avoid infinite recurtion by using ReferenceEquals
            if (ReferenceEquals(other, null)) {
                throw new Exception("NULL values are not allowed for reference type comprison. Everything is regarded as greater than NULL.");
            }
            else {
                return Interop.mpz_cmp(ref _Storage, ref other._Storage);
            }
        }

        public override bool Equals(object obj) {
            var other = obj as GmpInteger;
            return other != null && this == other;
        }

        public override int GetHashCode() {
            if (_HashCode.HasValue) {
                return _HashCode.Value;
            }
            else {
                byte[] data = null;
                bool isNegative = false;
                Export(out data, out isNegative);
                _HashCode = BitConverter.ToInt32(new MD5CryptoServiceProvider().ComputeHash(data), 0);
                return _HashCode.Value;
            }
        }
    }
}
