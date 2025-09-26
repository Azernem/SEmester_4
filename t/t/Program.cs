// <copyright file="Program.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Work;

    /// <summary>
    /// class with main method.
    /// </summary>
    public static class Operation
    {
        /// <summary>
        /// gets sum of two smallest numbers.
        /// </summary>
        /// <param name="mas">array</param>
        /// <returns>sum of elements</returns>
        /// <exception cref="ArgumentNullException">exception if asray is null</exception>
        /// <exception cref="ArgumentException">exception if array hasnt minimum two elements.</exception>
        public static int SummMin(int[] mas)
        {
            if (mas == null)
            {
                throw new ArgumentNullException("array mustnt be null");
            }

            if (mas.Length < 2)
            {
                throw new ArgumentException("array must contain a minimum two numbers");
            }

            var (min2, min1) = (Math.Min(mas[0], mas[1]), Math.Max(mas[0], mas[1]));
            for (int i = 2; i < mas.Length; i++)
            {
                if (mas[i] < min2)
                {
                    min1 = min2;
                    min2 = mas[i];
                }
                
                else
                {
                    min1 = Math.Min(min1, mas[i]);
                }
            }

            return checked(min1 + min2);
        }
    }
    
    class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }