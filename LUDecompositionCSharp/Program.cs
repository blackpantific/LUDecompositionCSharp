using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUDecompositionCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var length = 0;
            byte[] array;

            using (FileStream fs = File.OpenRead("matrix.txt"))
            {
                array = new byte[fs.Length];
                // считываем данные
                fs.Read(array, 0, array.Length);



            }

			int rowsCount = 0;
			string tempString = "";
			int pointer = 0;

			while (rowsCount == 0)
			{
				
				if (array[pointer] != ' ' && (int)array[pointer] != 13 && (int)array[pointer] != 10 && array[pointer] != '\0')
				{
					tempString += Char.GetNumericValue((char)array[pointer]);
					pointer++;
				}
				else
				{
					rowsCount =Convert.ToInt32(tempString);
					pointer+= 2;
				}
			}

			tempString = "";

			// выделяем память для двухмерного массива
			double[][] table = new double[rowsCount][];
			//rows = (float*) malloc(sizeof(float) * rowscount);

			for (int i = 0; i < rowsCount; i++)
			{
				table[i] = new double[rowsCount];//table[i] - это сам указатель на будущий массив под элементы

				int j = 0;
				while (j != rowsCount)
				{
					if ((int)array[pointer] != 32 && (int)array[pointer] != 13 && (int)array[pointer] != 10 && array[pointer] != '\0')
					{
						if (array[pointer] == 46)
						{
							tempString += ",";
							pointer++;
						}
                        else
                        {
							tempString += Char.GetNumericValue((char)array[pointer]);
							pointer++;
						}
						

					}
					else
					{
						table[i][j] = Convert.ToDouble(tempString);
						j += 1;
						pointer++;
						tempString = "";
					}
					if (j == rowsCount)
					{
						pointer++;
					}

				}
			}

			int n = rowsCount;



			//переменная n(размерность иходной ''квадратной'' матрицы) должна получить значение до этого момента
			double[,] matrix = new double[n, n];

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
					matrix[i, j] = table[i][j];
                }
            }


			//double[,] L = new double[n, n];
			//double[,] U = new double[n, n];
			//до этого момента массив A должен быть полностью определен
			//for (int i = 0; i < n; i++)
			//{
			//	for (int j = 0; j < n; j++)
			//	{
			//		U[0, i] = A[0, i];

			//		L[i, 0] = A[i, 0] / U[0, 0];
			//		double sum = 0;
			//		for (int k = 0; k < i; k++)
			//		{
			//			sum += L[i, k] * U[k, j];
			//		}
			//		U[i, j] = A[i, j] - sum;
			//		if (i > j)
			//		{
			//			L[j, i] = 0;
			//		}
			//		else
			//		{
			//			sum = 0;
			//			for (int k = 0; k < i; k++)
			//			{
			//				sum += L[j, k] * U[k, i];
			//			}
			//			L[j, i] = (A[j, i] - sum) / U[i, i];
			//		}
			//	}
			//}


			double[,] lu = new double[n, n];
			double sum = 0;
			for (int i = 0; i < n; i++)
			{
				for (int j = i; j < n; j++)
				{
					sum = 0;
					for (int k = 0; k < i; k++)
						 sum += lu[i, k] * lu[k, j];
					lu[i, j] = matrix[i, j] - sum;
				}
				for (int j = i + 1; j < n; j++)
				{
					sum = 0;
					for (int k = 0; k < i; k++)
						sum += lu[j, k] * lu[k, i];
					lu[j, i] = (1 / lu[i, i]) * (matrix[j, i] - sum);
				}
			}



		}
    }
}
