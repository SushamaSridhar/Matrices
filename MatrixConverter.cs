using System.Security.Cryptography;
using System.Text;
namespace Matrices
{
    public class MatrixConverter
    {
        //private int[,] _data;

        //public Matrix(int[,] data)
        //    {
        //    _data = data;
        //}

        public static int[] GetRow(int[,] matrix, int rowIndex)
        {
            int cols = matrix.GetLength(1);
            int[] row = new int[cols];
            for (int i = 0; i < cols; i++)
            {
                row[i] = matrix[rowIndex, i];
            }
            return row;
        }

        public static int[] GetColumn(int[,] matrix, int colIndex)
        {
            int rows = matrix.GetLength(0);
            int[] col = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                col[i] = matrix[i, colIndex];
            }
            return col;
        }
        public static int[,] StringToMtarix(string matrixString)
        {
            //split the string by rows
            string[] rows = matrixString.Split(";");

            //Determine the number of rows and coulumns
            int rowsCount = rows.Length;
            int colCount = rows[0].Split(",").Length;

            //Intilize the mtraix 
            int[,] matrix = new int[rowsCount, colCount];

            //Fill the matrix with parsed values
            for (int i =0; i <rowsCount; i++)
            {
                string[] cols = rows[i].Split(",");
                for (int j= 0; j < colCount; j++)
                {
                    matrix[i, j] = int.Parse(cols[j]);
                }
            }
            return matrix;
        }

        public static string MatrixToString(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j=0; j< cols; j++)
                {
                    sb.Append(matrix[i, j]);
                    if (j < cols - 1)
                    {
                        sb.Append(",");
                    }
                }
                if (i < rows - 1)
                {
                    sb.Append(";");
                }
            }
            return sb.ToString();         
           
            }

        public static string ComputeMD5Hash(string matrix)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[]  inputBytes = Encoding.ASCII.GetBytes(matrix);
                byte[] hashbytes = md5.ComputeHash(inputBytes);

                //Convert the byte array to hxadecimal string
                StringBuilder sb = new StringBuilder();
                //for (int i = 0; i< hashbytes.Length; i++)
                //{
                //    sb.Append(hashbytes[i].ToString("x2"));
                //}
                //return sb.ToString();

                return BitConverter.ToString(hashbytes).Replace("_", "").ToLower();

            }
        }

        public static int[,] Multiply(int[,] A, int[,] B)
        {
            if (A.GetLength(1) != B.GetLength(0))
                throw new InvalidOperationException("Matrix dimentsion do not match for multiplication");

            int rows = A.GetLength(0);
            int cols = B.GetLength(1);
            int common = A.GetLength(1);

            int[,] matrixResult = new int[rows, cols];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j =0; j < cols; j++)
                {
                    matrixResult[i, j] = 0;
                    for (int k = 0; k < common; k++)
                    {
                        matrixResult[i, j] += A[i, k] * B[k, j];

                    }
                }
            }
            return matrixResult;
        }


    }
}
