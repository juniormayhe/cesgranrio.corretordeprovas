using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Cesgranrio.CorretorDeProvas.Util
{
    /// <summary>
    /// Funcoes comuns para validacao e manipulacao de string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converte texto para formato md5
        /// </summary>
        /// <param name="texto">texto a ser convertido</param>
        /// <returns></returns>
        public static string ConverterParaMD5(this string texto)
        {
            var md5 = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(texto));

            for (int i = 0; i < bytes.Length; i++)
            {
                md5.Append(bytes[i].ToString("x2"));
            }
            return md5.ToString();
        }
        
        /// <summary>
        /// Converte um numero para o formato CPF
        /// </summary>
        /// <param name="numero"></param>
        /// <returns>cpf formatado</returns>
        public static string FormatarCPF(this string numero)
        {
            return Convert.ToUInt64(numero.RetirarFormato()).ToString(@"000\.000\.000\-00");
        }
        /// <summary>
        /// Retirar formatação retornando somente digitos
        /// </summary>
        /// <param name="textoFormatado">texto formatado a ser limpo</param>
        /// <returns>string sem formato</returns>
        public static string RetirarFormato(this string textoFormatado)
        {
            return Regex.Replace(textoFormatado, "[^0-9]+", "");
        }

        /// <summary>
        /// Indica se CPF é válido
        /// </summary>
        /// <param name="cpfInformado"></param>
        /// <returns>retorna true se CPF é válido</returns>
        public static bool ÉCPFVálido(this string cpfInformado) {
        {
                int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                const int totalDigitos = 11;
                string parteSemDigito;
                string digitoVerificador;
                int soma;
                int resto;
                
                cpfInformado = cpfInformado.RetirarFormato();

                if (cpfInformado.Length != totalDigitos)
                    return false;

                parteSemDigito = cpfInformado.Substring(0, 9);
                soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += int.Parse(parteSemDigito[i].ToString()) * mt1[i];

                resto = soma % totalDigitos;
                if (resto < 2)
                    resto = 0;
                else
                    resto = totalDigitos - resto;

                digitoVerificador = resto.ToString();
                parteSemDigito = parteSemDigito + digitoVerificador;
                soma = 0;
                for (int i = 0; i < (totalDigitos-1); i++)
                    soma += int.Parse(parteSemDigito[i].ToString()) * mt2[i];

                resto = soma % totalDigitos;
                if (resto < 2)
                    resto = 0;
                else
                    resto = totalDigitos - resto;

                digitoVerificador = digitoVerificador + resto.ToString();

                return cpfInformado.EndsWith(digitoVerificador);
            }
        }
    }
}
