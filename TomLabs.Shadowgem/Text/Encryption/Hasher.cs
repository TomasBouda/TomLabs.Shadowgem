﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace TomLabs.Shadowgem.Text.Encryption
{
	/// <summary>
	/// Provides methods for string hashing
	/// </summary>
	public static class Hasher
	{
		/// <summary>
		/// Supported hash algorithms
		/// </summary>
		public enum EHashType
		{
			/// <summary>
			/// Keyed-hash Message Authentication Code
			/// </summary>
			HMAC,

			/// <summary>
			/// Hash-based Message Authentication Code (HMAC) by using the MD5 hash function
			/// </summary>
			HMACMD5,

			/// <summary>
			/// Hash-based Message Authentication Code (HMAC) using the SHA1 hash function
			/// </summary>
			HMACSHA1,

			/// <summary>
			/// Hash-based Message Authentication Code (HMAC) by using the SHA256 hash function
			/// </summary>
			HMACSHA256,

			/// <summary>
			/// Hash-based Message Authentication Code (HMAC) by using the SHA384 hash function
			/// </summary>
			HMACSHA384,

			/// <summary>
			/// Hash-based Message Authentication Code (HMAC) by using the SHA512 hash function
			/// </summary>
			HMACSHA512,

			/// <summary>
			/// Message Authentication Code (MAC) using TripleDES
			/// </summary>
			MACTripleDES,

			/// <summary>
			/// MD160 hash algorithm
			/// </summary>
			RIPEMD160,

			/// <summary>
			/// Secure Hash Algorithm 256
			/// </summary>
			SHA256,

			/// <summary>
			/// Secure Hash Algorithm 384
			/// </summary>
			SHA384,

			/// <summary>
			/// Secure Hash Algorithm 512
			/// </summary>
			SHA512,

			/// <summary>
			/// Should not be used.
			/// </summary>
			[Obsolete("Has cryptographic weaknesses", false)]
			MD5,

			/// <summary>
			/// Should not be used.
			/// <para>
			/// https://www.howtogeek.com/238705/what-is-sha-1-and-why-will-retiring-it-kick-thousands-off-the-internet/
			/// </para>
			/// </summary>
			[Obsolete("Has cryptographic weaknesses", false)]
			SHA1,
		}

		private static byte[] GetHash(string input, EHashType hash)
		{
			byte[] inputBytes = Encoding.ASCII.GetBytes(input);

			switch (hash)
			{
				case EHashType.HMAC:
					return HMAC.Create().ComputeHash(inputBytes);

				case EHashType.HMACMD5:
					return HMACMD5.Create().ComputeHash(inputBytes);

				case EHashType.HMACSHA1:
					return HMACSHA1.Create().ComputeHash(inputBytes);

				case EHashType.HMACSHA256:
					return HMACSHA256.Create().ComputeHash(inputBytes);

				case EHashType.HMACSHA384:
					return HMACSHA384.Create().ComputeHash(inputBytes);

				case EHashType.HMACSHA512:
					return HMACSHA512.Create().ComputeHash(inputBytes);

#pragma warning disable CS0618 // Type or member is obsolete
				case EHashType.MD5:
#pragma warning restore CS0618 // Type or member is obsolete
					return MD5.Create().ComputeHash(inputBytes);

#pragma warning disable CS0618 // Type or member is obsolete
				case EHashType.SHA1:
#pragma warning restore CS0618 // Type or member is obsolete
					return SHA1.Create().ComputeHash(inputBytes);

				case EHashType.SHA256:
					return SHA256.Create().ComputeHash(inputBytes);

				case EHashType.SHA384:
					return SHA384.Create().ComputeHash(inputBytes);

				case EHashType.SHA512:
					return SHA512.Create().ComputeHash(inputBytes);

				default:
					return inputBytes;
			}
		}

		/// <summary>
		/// Computes the hash of the string using a specified hash algorithm
		/// </summary>
		/// <param name="input">The string to hash</param>
		/// <param name="hashType">The hash algorithm to use</param>
		/// <returns>The resulting hash or an empty string on error</returns>
		public static string ComputeHash(this string input, EHashType hashType)
		{
			try
			{
				byte[] hash = GetHash(input, hashType);
				var ret = new StringBuilder();

				foreach (var h in hash)
				{
					ret.Append(h.ToString("x2"));
				}

				return ret.ToString();
			}
			catch
			{
				return string.Empty;
			}
		}
	}
}