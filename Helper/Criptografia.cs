using System.Security.Cryptography;
using System.Text;

namespace ControleDeContatos.Helper;

public static class Criptografia
{
    public static string GerarHash(this string valor)
    {
        SHA1 hash = SHA1.Create();
        var encoding = new ASCIIEncoding();

        byte[] array = encoding.GetBytes(valor);
        array = hash.ComputeHash(array);

        var strHexa = new StringBuilder();

        foreach (byte item in array)
        {
            strHexa.Append(item.ToString("x2"));
        }

        return strHexa.ToString();
    }
}