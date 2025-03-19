using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq; // Для SequenceEqual

namespace practika // Замените на имя вашего проекта, если оно другое
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;     // Размер соли в байтах (128 бит)
        private const int HashSize = 20;     // !!! РАЗМЕР ХЕША ДЛЯ SHA-1 (20 байт) !!!
        private const int Iterations = 10000; // Количество итераций хеширования

        public static (string hashedPassword, string salt) HashPassword(string password)
        {
            // 1. Генерация случайной соли
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // 2. Хеширование пароля с использованием соли
            byte[] hash;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                hash = deriveBytes.GetBytes(HashSize); // Используем HashSize (20 байт для SHA-1)
            }

            // 3. Объединяем соль и хеш в один массив байт
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // 4. Возвращаем хеш (вместе с солью) и соль (отдельно) в виде строк Base64
            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(salt));
        }

        public static bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            // 1. Извлекаем соль и хеш из объединенной строки Base64
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // ПРОВЕРЯЕМ ДЛИНУ!
            if (hashBytes.Length != SaltSize + HashSize)
            {
                //  throw new ArgumentException("Invalid hash length", nameof(hashedPassword)); // Лучше выбросить исключение
                return false; // Или вернуть false
            }

            byte[] saltBytes = new byte[SaltSize];
            Array.Copy(hashBytes, 0, saltBytes, 0, SaltSize); // Копируем соль

            byte[] storedHash = new byte[HashSize];
            Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize); // Копируем хеш

            // 2. Хешируем введенный пароль с использованием той же соли и тех же параметров
            byte[] hash;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
            {
                hash = deriveBytes.GetBytes(HashSize); // Используем HashSize (20 байт)
            }

            // 3. Сравниваем полученный хеш с хешем, хранящимся в базе данных
            return storedHash.SequenceEqual(hash);
        }
    }
}