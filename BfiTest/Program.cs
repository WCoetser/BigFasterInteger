//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Please Note: 
//
//     Build 64-bit otherwise there will be problems with the imported assembly.
//     The gmp library was build for 64 bit version of windows and uses 64 bit "limbs" for integer representation.
//     Also: Check that libgmp-10.dll copied from the GmpInteger project reference if it is missing.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using ObjectLifeCycleManagement;
using GMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BfiTest {
    class Program {

        static void Main(string[] args) {
            

            using (new DisposeContext()) {

                // Arithmatic
                GmpInteger lhs = new GmpInteger("111111111111111111111111111111111111111111111111111111111111111111111111111");
                GmpInteger rhs = new GmpInteger("111111111111111111111111111111111111111111111111111111111111111111111111111");
                GmpInteger one = new GmpInteger("1");
                GmpInteger add = lhs + rhs;
                GmpInteger sub = lhs - rhs;
                GmpInteger neg = -lhs;
                GmpInteger abs = neg.Abs();
                GmpInteger pow = one.Pow2(64);
                GmpInteger lhsPlus1 = lhs + one;
                GmpInteger mod = lhsPlus1 % lhs;
                GmpInteger mul = lhs * rhs;
                GmpInteger div = lhs / pow;

                Console.WriteLine($"lhs = {lhs}");
                Console.WriteLine($"rhs = {rhs}");
                Console.WriteLine($"one = {one}");
                Console.WriteLine($"lhs + rhs = {add}");
                Console.WriteLine($"lhs - rhs = {sub}");
                Console.WriteLine($"neg(lhs) = {neg}");
                Console.WriteLine($"abs(neg(lhs)) = {abs}");
                Console.WriteLine($"pow(1,64) = {pow}");
                Console.WriteLine($"lhs * rhs = {mul}");
                Console.WriteLine($"(lhs + 1) % lhs = {mod}");
                Console.WriteLine($"lhs / pow(1,65) = {div}");
                Console.WriteLine($"lhs = rhs: {lhs == rhs}");
                Console.WriteLine($"lhs + rhs > 1: {add > one}");
                Console.WriteLine($"lhs + rhs < 1: {add < one}");
                Console.WriteLine($"lhs + rhs >= 1: {add >= one}");
                Console.WriteLine($"lhs + rhs <= 1: {add <= one}");
                Console.WriteLine($"lhs == null: {lhs == null}");
                Console.WriteLine($"null == lhs: {null == lhs}");                

                byte[] exportedValue = null;
                bool isNegative = false;
                pow.Export(out exportedValue, out isNegative);
                Console.WriteLine($"export pow(1,64) = {BitConverter.ToString(exportedValue)}");
                Console.WriteLine($"export pow(1,64) is negative = {isNegative}");
                
                // Import and export
                GmpInteger impTest = new GmpInteger(new byte[] { 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                Console.WriteLine($"new byte[] {{ 0x1,0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 }} = {impTest}");

                HashSet<GmpInteger> set = new HashSet<GmpInteger>();
                set.Add(lhs);
                set.Add(lhs);
                set.Add(new GmpInteger("1111"));
                set.Add(new GmpInteger("1111"));
                set.Add(new GmpInteger("11111111"));
                Console.WriteLine($"HashSet<GmpInteger> len = {set.Count}");

            }
        }
    }
}
