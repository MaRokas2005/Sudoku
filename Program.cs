using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string filePath = @"./../../U3.txt";
            Sudoku sudoku = new Sudoku();
            sudoku.Read(filePath);
            sudoku.Solve();
            Console.WriteLine(sudoku.Answer());
        }
    }
    class Sudoku
    {
        private int[,] lenta;
        private int[,] answer;
        public Sudoku()
        {
            lenta = new int[6, 6];
        }
        public void Solve()
        {
            List<(int x, int y)> nuliai = Zeroes(lenta);
            List<List<int>> skaiciai = new List<List<int>>();
            GenerateNumbers(nuliai.Count(), new List<int>(), 0, skaiciai);
            List<int[,]> lentos = Join(lenta, nuliai, skaiciai);
            Check(lentos);
        }
        public void Read(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int eil = 0;
            foreach (string line in lines)
            {
                string[] nums = line.Trim().Split();
                for (int stul = 0; stul < 6; stul++)
                {
                    lenta[eil, stul] = int.Parse(nums[stul]);
                }
                eil++;
            }
        }
        public string Answer()
        {
            if (answer == null)
            {
                return "Nėra galimo atsakymo!";
            }
            string s = "";
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    s += answer[i, j] + " ";
                }
                s += '\n';
            }
            return s;
        }
        private List<int[,]> Join(int[,] lenta, List<(int x, int y)> nuliai, List<List<int>> skaiciai)
        {
            List<int[,]> lentos = new List<int[,]>();

            foreach(var skaic in skaiciai)
            {
                int[,] tempLenta = DeepCopy(lenta);
                for(int i = 0; i < nuliai.Count(); i++)
                {
                    int x = nuliai[i].x;
                    int y = nuliai[i].y;
                    tempLenta[x, y] = skaic[i];
                }
                lentos.Add(tempLenta);
            }
            return lentos;
        }
        private void GenerateNumbers(int n, List<int> currentNumbers, int index, List<List<int>> numbers)
        {
            if (index == n)
            {
                numbers.Add(new List<int>(currentNumbers));
                return;
            }
            for(int i = 1; i <= 6; i++)
            {
                currentNumbers.Add(i);
                GenerateNumbers(n, currentNumbers, index + 1, numbers);
                currentNumbers.RemoveAt(currentNumbers.Count - 1);
            }
            return;
        }
        private static int[,] DeepCopy(int[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            int[,] copy = new int[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    copy[i, j] = array[i, j];
                }
            }

            return copy;
        }
        private List<(int x, int y)> Zeroes(int[,] lenta)
        {
            List<(int x, int y)> nuliai = new List<(int x, int y)>();
            for(int eil = 0; eil < 6; eil++)
            {
                for(int stul = 0; stul < 6; stul++)
                {
                    if (lenta[eil, stul] == 0) nuliai.Add((eil, stul));
                }
            }
            return nuliai;
        }
        private bool Check(int[,] lenta)
        {
            int[] eilutes    = new int[6];
            int[] stulpeliai = new int[6];
            int[] dezutes = new int[6];
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    if (lenta[i, j] == 0) return false;
                    #region checkSum
                    int a = i;
                    int b = j;
                    int c = i / 2 * 2 + j / 3;
                    eilutes[a] += (lenta[i, j]);
                    stulpeliai[b] += (lenta[i, j]);
                    dezutes[c] += (lenta[i, j]);
                    #endregion
                }
            }
            for(int i = 0; i < 6; i++)
            {
                if (eilutes[i] != 21) return false;
                if (stulpeliai[i] != 21) return false;
                if (dezutes[i] != 21) return false;
            }
            return true;
        }
        private void Check(List<int[,]> lentos)
        {
            foreach (var lenta in lentos)
            {
                if (Check(lenta)) { 
                    answer = lenta;
                    return; 
                }
            }
        }
    }
}