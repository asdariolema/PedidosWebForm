using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
 public  class Encriptado
    {
        public  string Encrypt(string plainText , string clave)
        {
            string key = clave; // Debe tener 16, 24 o 32 caracteres
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }

            // Codifica en Base64 y lo hace seguro para URL
            return Convert.ToBase64String(array)
                         .Replace("+", "-")
                         .Replace("/", "_")
                         .Replace("=", "");
        }

    

    public  string Decrypt(string encryptedText, string clave)
        {
            string key = clave;
            byte[] iv = new byte[16];

            // Revertir codificación URL-safe
            string base64 = encryptedText.Replace("-", "+").Replace("_", "/");
            while (base64.Length % 4 != 0)
            {
                base64 += "=";
            }

            byte[] buffer = Convert.FromBase64String(base64);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }


    }
}