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
            Array.Sort(mas);

            if (mas == null)
            {
                throw new ArgumentNullException("array mustnt be null");
            }

            if (mas.Length < 2)
            {
                throw new ArgumentException("array must contain a minimum two numbers");
            }

            return mas[0] + mas[1];
        }
    }
    
    class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }