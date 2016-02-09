# BigFasterInteger
A C# wrapper for the GMP library. The aim of this wrapper is to give C# programmers access to faster integer operations. This library is compiled for 64-bit Intel processors running on Windows. [Update: Does not work on Intel i7 6700]

# Motivation
I created this wrapper because I want to play around with images represented as large integers.

GMP is a great library (https://gmplib.org/), but it is implemented in C and this makes it difficult to access. Therefore I created this wrapper.

# Using the BigFasterInteger library
Please note the following:

* Code *must* be compiled for 64-bit. If it is not there will be a format mismatch with the unmanaged DLL.
* The GMP library have been compiled into *libgmp-10.dll* which is part of the *BigFastInteger* project.
* If *libgmp-10.dll* is missing in the compile output, check that it copied through from *BigFastInteger*.
* GMP 6.1 was used to create *libgmp-10.dll*.
* It is recommended that all *GmpInteger* instances are wrapped in using statements with *DisposeContext* to avoid memory leaks.

# Usage sample code
See the BfiTest console application.

# Possible improvements
Further improvements can be made:

* Overload more operators. Add bit-wise operators.
* Also expose the other functions and types (ex. multi-precision rational number.)
