using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesgranrio.CorretorDeProvas.Simulador
{
    internal sealed class Imagem
    {

        public static Image GerarFolha(string numeroQuestao, string nomeDoCandidato)
        {
            const int larguraCanvas = 600;
            const int alturaCanvas = 1300;
            const int totalLinhas = 30;
            int linha = 20;
            Font fonte = new Font("Segoe UI", 10, FontStyle.Bold);
            

            Image img = new Bitmap(larguraCanvas, alturaCanvas);
            Graphics grafico = Graphics.FromImage(img);
            SizeF textSize = grafico.MeasureString(nomeDoCandidato, fonte);
            img.Dispose();
            grafico.Dispose();

            //nosso canvas com fundo branco
            img = new Bitmap(larguraCanvas, alturaCanvas);
            grafico = Graphics.FromImage(img);
            grafico.Clear(Color.White);

            
            Brush canetaPreta = new SolidBrush(Color.Black);
            

            //separador
            grafico.DrawString("FOLHA DE REDAÇÃO", fonte, canetaPreta, 200, 20);
            
            linha += 40;
            for (int i = 1; i < totalLinhas+1; i++) {
                grafico.DrawRectangle(new Pen(Color.Black), 10, linha, 40, 40);
                grafico.DrawRectangle(new Pen(Color.Black), 10, linha, larguraCanvas-20, 40);
                grafico.DrawString($"{i:d2} ", fonte, canetaPreta, 20, linha + 8);
                linha += 40;    
            }

            fonte = new Font("Segoe UI", 16, FontStyle.Bold);
            Brush canetaVermelha = new SolidBrush(Color.Red);
            grafico.DrawString($"Questão: {numeroQuestao}", fonte, canetaVermelha, 50, 60);
            grafico.DrawString($"Candidato: {nomeDoCandidato}", fonte, canetaVermelha, 50, 100);

            grafico.Save();

            canetaPreta.Dispose();
            grafico.Dispose();

            return img;

        }
    }
}
