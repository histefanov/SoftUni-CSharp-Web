using System;
using System.Linq;

namespace MergeSortAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var sortedArray = MergeSort(array);

            Console.WriteLine(string.Join(" ", sortedArray));
        }

        private static int[] MergeSort(int[] a)
        {
            var n = a.Length;

            if (n == 1) return a;

            var mid = n / 2;
            int[] left;
            int[] right;

            left = new int[mid];

            if (n % 2 == 0)
            {
                right = new int[mid];
            }
            else
            {
                right = new int[mid + 1];
            }

            for (int i = 0; i < mid; i++)
            {
                left[i] = a[i];
            }

            int j = 0;

            for (int i = mid; i < n; i++)
            {
                right[j++] = a[i];
            }

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        private static int[] Merge(int[] a, int[] b)
        {
            int[] c = new int[a.Length + b.Length];

            int i = 0;
            int j = 0;
            int k = 0;

            while (i < a.Length && j < b.Length)
            {
                if (a[i] < b[j])
                {
                    c[k++] = a[i++];
                }
                else
                {
                    c[k++] = b[j++];
                }
            }

            while (i < a.Length)
            {
                c[k++] = a[i++];
            }

            while (j < b.Length)
            {
                c[k++] = b[j++];
            }

            return c;
        }
    }
}
