using CsvHelper;
using System.Security.Cryptography;

namespace CollectionGenerator
{
    public static class GuidHelper
    {
        /// <summary>
        /// Converts an integer to consistent GUID using SHA256
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ConvertIntToGuid(int value)
        {
            byte[] valueBytes = BitConverter.GetBytes(value);
            byte[] hashBytes = SHA256.HashData(valueBytes);
            byte[] guidBytes = new byte[16];
            Array.Copy(hashBytes, guidBytes, 16);
            return new Guid(guidBytes);
        }

        public static Guid GetGuid(this CsvReader csv, string fieldName)
        {
            string? value = csv.GetField<string>(fieldName);
            if (string.IsNullOrWhiteSpace(value))
                throw new FormatException($"ID '{fieldName}' cannot be unset.");
            value = value.Trim('{', '}');

            if (Guid.TryParse(value, out var guid))
                return guid;
            if (int.TryParse(value, out int intValue))
                return ConvertIntToGuid(intValue);
            throw new FormatException($"ID '{fieldName}' has invalid value: '{value}'.");
        }
    }
}
