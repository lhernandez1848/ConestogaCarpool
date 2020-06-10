using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ConestogaCarpool
{
    public class PasswordHash
    {
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20;
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        public static string HashPassword(string Password)
        {
            byte[] salt;
            byte[] hashBuffer;

            if (Password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, 0x10, 0x3e8))
            {
                salt = deriveBytes.Salt;
                hashBuffer = deriveBytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(hashBuffer, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool ValidatePassword(string HashedPassword, string Password)
        {
            byte[] validateBuffer1;

            if (HashedPassword == null)
            {
                return false;
            }
            byte[] source = Convert.FromBase64String(HashedPassword);

            if ((source.Length != 0x31) || (source[0] != 0))
            {
                return false;
            }

            byte[] dst = new byte[0x10];

            Buffer.BlockCopy(source, 1, dst, 0, 0x10);

            byte[] validateBuffer2 = new byte[0x20];

            Buffer.BlockCopy(source, 0x11, validateBuffer2, 0, 0x20);

            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, dst, 0x3e8))
            {
                validateBuffer1 = deriveBytes.GetBytes(0x20);
            }

            return Equals(validateBuffer2, validateBuffer1);
        }

        private static bool Equals(byte[] a, byte[] b)
        {
            var difference = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                difference |= (uint)(a[i] ^ b[i]);
            }
            return difference == 0;
        }
    }
}
