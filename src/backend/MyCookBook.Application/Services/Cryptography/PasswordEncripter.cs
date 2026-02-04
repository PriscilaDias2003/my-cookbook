using System.Security.Cryptography;
using System.Text;

namespace MyCookBook.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        private readonly string _additionalKey;
        public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;

        public string Encrypt(string password)
        {
            var newPassword = $"{password}{_additionalKey}";

            // Converter a senha para um array de bytes
            var bytes = Encoding.UTF8.GetBytes(newPassword);

            // Calcular o hash SHA-512 dos bytes da senha
            var hashBytes = SHA512.HashData(bytes);

            return ConvertToHexString(hashBytes);
        }

        private string ConvertToHexString(byte[] bytes)
        {
            // Converter o array de bytes para uma string hexadecimal
            var sb = new StringBuilder();

            // Percorrer cada byte e convertê-lo para hexadecimal
            foreach (byte b in bytes)
            {
                // "x2" formata o byte como um valor hexadecimal de dois dígitos
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
