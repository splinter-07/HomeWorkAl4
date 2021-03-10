using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace HomeWorkAl4
{
    

    class Program
    {   
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadLine();
        }
        
    }

    public class BechmarkClass {
        public static  HashSet<string> hashSet = new HashSet<string>();
        public static string[] stringArray = GetArray(10000);
        

        public static string[] GetArray(int n)
        {
            string[] array = new string[n];
            string word;
            for (int i = 0; i < array.Length; i++)
            {
                word = GetWorld(n);
                hashSet.Add(word);
                array[i] = word; 
                
            }

            return array;
        }
        public static string GetWorld(int n)
        {
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ absdefghijklmnopqrstuvwxyz".ToCharArray();
            string word = "";
            Random rand = new Random();
            for (int j = 0; j < n; j++)
            {
                int letter_num = rand.Next(0, letters.Length - 1);

                word += letters[letter_num];
            }
            
            return word;
        }


        public bool SearchInHashSet(HashSet<string> hashSet)
        {
            var searchString = "HomeWorkAl";
            if (hashSet.Contains(searchString))
                return true;
            return false;
        }

        public bool SearchInArray(string[] array) 
        {
            var searchString = "HomeWorkAl";
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == searchString)
                {
                    return true;
                }

            }
            return false;
        }

        [Benchmark]
        public void TestSearchInHashSet() 
        {
            SearchInHashSet(hashSet);
        }
        [Benchmark]
        public void TestSearchInArray()
        {
            SearchInArray(stringArray);
        }
    }
    
}
