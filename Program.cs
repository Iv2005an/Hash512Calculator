using System.Security.Cryptography;
using System.Text;

Console.Write("Введите постоянную соль(Enter для пропуска): ");
string constantSalt = Console.ReadLine()!;

Console.Write("Введите соль(Enter для генерации): ");
string salt = Console.ReadLine()!;
if (string.IsNullOrEmpty(salt)) salt = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

Console.Write("Введите пароль: ");
string password = Console.ReadLine()!;

byte[] strBytes = Encoding.UTF8.GetBytes(password ?? "");
byte[] constantSaltBytes = Encoding.UTF8.GetBytes(constantSalt ?? "");
byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
byte[] saltedStrBytes = new byte[strBytes.Length + constantSaltBytes.Length + saltBytes.Length];

strBytes.CopyTo(saltedStrBytes, 0);
constantSaltBytes.CopyTo(saltedStrBytes, strBytes.Length);
saltBytes.CopyTo(saltedStrBytes, strBytes.Length + constantSaltBytes.Length);

Console.WriteLine("Постоянная соль: " + constantSalt);
Console.WriteLine("Соль: " + salt);
Console.WriteLine("Хеш пароля(SHA512): " + Convert.ToHexString(SHA512.HashData(saltedStrBytes)));
