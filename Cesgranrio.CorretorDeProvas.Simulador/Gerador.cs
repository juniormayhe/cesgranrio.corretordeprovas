﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.Simulador
{
    /// <summary>
    /// Utilitários
    /// </summary>
    internal static class Util
    {

        /// <summary>
        /// Converte imagem para byte array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ConverterParaArray(Image img) {
            
            byte[] arrImagem;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arrImagem = ms.ToArray();
            }
            return arrImagem;
        }

        private static Random rnd = new Random();

        /// <summary>
        /// Gera uma nota
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static double GeraNota(double minValue, double maxValue)
        {
            var next = rnd.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }

        /// <summary>
        /// Gera CPF válido
        /// </summary>
        /// <returns></returns>
        public static String GerarCPF(int sequencia)
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            
            string cpf = sequencia.ToString().PadLeft(9,'0');

            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            cpf = cpf + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            cpf = cpf + resto;
            return cpf;
        }

        /// <summary>
        /// Gera palavras simulando uma resposta
        /// </summary>
        /// <returns></returns>
        public static string GerarPalavras() {
            StringBuilder palavras = new StringBuilder();

            int num_letters = 5;
            int totalPalavras = 3;

            char[] letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Make a random number generator.
            

            for (int i = 1; i <= totalPalavras; i++)
            {
                string palavra = "";
                for (int j = 1; j <= num_letters; j++)
                {
                    int posicao = rnd.Next(0, letras.Length - 1);
                    palavra += letras[posicao];
                }
                
                palavras.Append(palavra);
            }
            return palavras.ToString();
        }
    }


}
