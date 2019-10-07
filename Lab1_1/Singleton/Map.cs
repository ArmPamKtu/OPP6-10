using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Map
    {
        private static int counter = 0;
        private static readonly object Instancelock = new object();
        private Map()
        {
            //generate map here
            counter++;
            Console.WriteLine("Counter Value " + counter.ToString());
        }
        private static Map instance = null;

        public static Map GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new Map();
                        }
                    }
                }
                return instance;
            }
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }

    }
}
