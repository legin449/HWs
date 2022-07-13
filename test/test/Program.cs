using System;

namespace Homework4_2
{
    /// <summary>
    /// Построение треугольника Паскаля
    /// </summary>
    class Program
    {
        public const int cellWidth = 3;
        static void Main()
        {
            int[][] TriangleofPascale = new int[15][]; //Создаем зубчатый массив для красивого вывода, здесь же указываем количество строк
            int row = TriangleofPascale.Length; //Количество элементов в строке
            int col = row * cellWidth; //Отступ на кол-во элементов каждые три ячейки
            for (int i = 0; i < row; i++) //отрисовка и подсчет каждого элемента в массиве
            {
                TriangleofPascale[i] = new int[i + 1]; //Подсчет количества элементов в строке
                for (int k = 0; k < TriangleofPascale[i].Length; k++)
                {
                    if (k == 0 || k == TriangleofPascale[i].Length - 1) //Каждый первый и последний элемент в строке равен единице
                    {
                        TriangleofPascale[i][k] = 1;
                    }
                    else if (k != 0 && i > 1 && k != TriangleofPascale[i].Length - 1) // начиная с 3 строки ведем подсчет каждого элемента
                    {
                        TriangleofPascale[i][k] = TriangleofPascale[i - 1][k - 1] + TriangleofPascale[i - 1][k]; // элемент равен сумме двух вышестоящих элементов
                    }
                    Console.SetCursorPosition(col, i);
                    Console.Write($"{TriangleofPascale[i][k],cellWidth}");
                    col += cellWidth * 2;
                }
                Console.WriteLine();
                col = cellWidth * row - cellWidth * (i + 1);
            }
            Console.ReadKey();
        }
    }
}