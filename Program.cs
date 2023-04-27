using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Sudoku()
        {
            lenta = new int[6, 6];
        }
        public void Solve()
        {

        }
        public void Read(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int a = 0;
            foreach (string line in lines)
            {
                string[] nums = line.Trim().Split();
                for (int i = 0; i < 6; i++)
                {
                    lenta[a, i] = int.Parse(nums[i]);
                }
                a++;
            }
        }
        public string Answer()
        {
            return "";
        }
    }
}