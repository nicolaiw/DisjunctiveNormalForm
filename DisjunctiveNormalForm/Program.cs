using System;
using static DisjunctiveNormalForm.DnfTest;

namespace DisjunctiveNormalForm
{
    /*
     * 
     *      Given the following truth table.
     *      
     *      Input:          a, b, c
     *      Desired output: s
     *      
     *      ^ / * = konjunction
     *      v / + = disjunction
     *      - = negation
     *      
     *       a | b | c | s
     *  1.   0 | 0 | 0 | 1      t1 = -a-b-c (same as: -a ^ -b ^ -c)     
     *  2.   0 | 0 | 1 | 0  
     *  3.   0 | 1 | 0 | 0  
     *  4.   0 | 1 | 1 | 0  
     *  5.   1 | 0 | 0 | 1      t2 = a-b-c
     *  6.   1 | 0 | 1 | 1      t3 = a-bc
     *  7.   1 | 1 | 0 | 1      t4 = ab-c
     *  8.   1 | 1 | 1 | 1      t5 = abc
     *  
     *  Equivalence transformation of the DNF:
     *  
     *  s = -a-b-c + a-b-c + a-bc + ab-c + abc
     *    = -a-b-c + a-b(-c + c) + ab(-c + c)
     *    = -a-b-c + a-b + ab
     *    = -a-b-c + a(-b + b)
     *    = -a-b-c + a
     *    
     *    It must now be proved that:
     *    
     *    -a-b-c + a-b-c + a-bc + ab-c + abc == -a-b-c + a
     *
     */

    static class DnfTest
    {
        // "before" refers to BEFOR equivalence transformation
        public static bool Dnf_before(bool a, bool b, bool c)
        {
            // Parantheses just for readebility

            //      -a-b-c        +   a-b-c        +   a-bc        +   ab-c        +   abc          
            return (!a & !b & !c) || (a & !b & !c) || (a & !b & c) || (a & b & !c) || (a & b & c);
        }


        // "after" refers to AFTER equivalence transformation
        public static bool Dnf_after(bool a, bool b, bool c)
        {
            //      -a-b-c        + a
            return (!a & !b & !c) || a;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            var truthTable = new bool[,]
            {
                { false, false, false }, // -a-b-c = t1;
                { false, false, true },
                { false, true, false },
                { false, true, true },
                { true, false, false },  // a-b-c = t2
                { true, false, true },   // a-bc = t3
                { true, true, false },   // ab-c = t4
                { true, true, true }     // abc = t5
            };

            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                var a = truthTable[i, 0];
                var b = truthTable[i, 1];
                var c = truthTable[i, 2];

                // Proving
                if (Dnf_before(a, b, c) == Dnf_after(a, b, c))
                {
                    Console.WriteLine($"{i+1}. Same value");
                }
                else
                {
                    Console.WriteLine($"{i+1}. Not the same value");
                }
            }

            Console.ReadLine();
        }
    }
}
