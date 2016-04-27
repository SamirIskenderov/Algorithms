﻿using Algorithms.BigNumber;
using Algorithms.Library;
using System;

namespace Algorithms.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            string test1Str = "123456123456789123456789123456789123456789123456789,123456789123456789123450000";

            string test2Str = "123456123456789123456789123456789,123456789123456789123456789123450000";

            //                 123456123456789123580245246913578246913578246913578,246913578246913578246906789123450000
            //                 123456123456789123580245246913578246913578246913578,246913578246913578246906789123450000

            string test3Str = "123456123456789123456789123456789123456789123456789,123456789123456789123450000";
            string test4Str = "-123456123456789123456789123456789,123456789123456789123456789123450000";
            //                 123456123456789123333332999999999999999999999999999,999999999999999999999993211123450000
            //                 123456123456789123333332999999999999999999999999999,999999999999999999999993211123450000

            //             -000123456123456789123333332999999999999999999999999999,999999999999999999999993211123450000

            string test6Str = "123456123456789123456789123456789,123456789123456789123456789123450000";
            string test5Str = "-123456123456789123456789123456789123456789123456789,123456789123456789123450000";
            //                 -123456123456789123333332999999999999999999999999999,999999999999999999999993211123450000
            //              -000123456123456789876666667000000000000000000000000000,000000000000000000000006789123450000

            string test7Str = "-7848,0";
            string test8Str = "000000000" +
                              "000000000" +
                              "100000000" +
                              "000000000" +
                              "000000000" +
                              "000000000" +
                              "000000000" +
                              "000000000" +
                              "100000000" +
                              "100000000," +
                              "000000000" +
                              "000000001" +
                              "000000000" +
                              "000000000" +
                              "000000000";

            string test9Str = "0";
            string test10Str = "0";

            BigNumberDS mybig1 = new BigNumberDS(test1Str);
            BigNumberDS mybig2 = new BigNumberDS(test2Str);

            BigNumberDS mybig3 = new BigNumberDS(test3Str);
            BigNumberDS mybig4 = new BigNumberDS(test4Str);

            BigNumberDS mybig5 = new BigNumberDS(test5Str);
            BigNumberDS mybig6 = new BigNumberDS(test6Str);

            BigNumberDS mybig7 = new BigNumberDS(test7Str);

            BigNumberDS mybig9 = new BigNumberDS(test9Str);
            BigNumberDS mybig10 = new BigNumberDS(test10Str);

            var test = mybig1 + mybig2;

            var test2 = mybig3 + mybig4;

            var test3 = mybig5 + mybig6;

            var test4 = mybig9 + mybig10;

            BigNumberDS lhs = new BigNumberDS("123456789123456789,000000001");
            BigNumberDS rhs = new BigNumberDS("223456789123456789,1");

            BigNumberDS mybig8 = new BigNumberDS(test8Str);

            Console.WriteLine(lhs.CompareTo(rhs));

            Console.ReadLine();
        }
    }
}