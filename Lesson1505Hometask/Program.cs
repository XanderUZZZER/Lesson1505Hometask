﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1505Hometask
{
    public class Person
    {
        public int PersonId;
        public string FullName;
    }

    public class Car
    {
        public int CarId;
        public string Model;
        public int PersonId;
        public string Color;
        public DateTime CreationDate;
    }

    class VowelsComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string vowels = "aeiouy";
            int xTotalVolwes = x.ToLower().Count(c => vowels.Contains(c));
            int yTotalVolwes = y.ToLower().Count(c => vowels.Contains(c));
            if (xTotalVolwes > yTotalVolwes)
            {
                return 1;
            }
            if (xTotalVolwes < yTotalVolwes)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    class UniqueCarsEqualityComparer : IEqualityComparer<Car>
    {
        public bool Equals(Car x, Car y)
        {
            return (x.Model == y.Model && x.Color == y.Color);
        }

        public int GetHashCode(Car obj)
        {
            return obj.Color.GetHashCode() + obj.Model.GetHashCode();
        }
    }

    class Progarmm
    {
        static void Main()
        {
            Console.WriteLine("HomeTask LINQ 1505");

            List<string> firstList = new List<string>() { "alsdfk", "asdfgs", "testq", "adgdfk", "aabcqw", "aabck" };
            List<string> secondList = new List<string>() { "al123fk", "asd456fgs", "testq", "ad12gdfk", "aabc23qw", "aabck" };
            Console.WriteLine("1 First list");
            Console.WriteLine(string.Join(" ", firstList) + "\n");
            Console.WriteLine("2 Second list");
            Console.WriteLine(string.Join(" ", secondList) + "\n");

            Console.WriteLine("---- 1.1 Begins with A, ends with K, on even positions (position starts from 1)");
            Console.WriteLine(string.Join(" | ", firstList.Where((x, i) => x.StartsWith("a") && x.EndsWith("k") && (i + 1) % 2 == 0)) + "\n");

            Console.WriteLine("---- 1.2 Number of chars in strings start with AA");
            Console.WriteLine(string.Join(" | ", firstList.Where(x => x.StartsWith("aa")).Select(x => x.Length)) + "\n");

            Console.WriteLine("---- 1.3 All strings and number of chars in the strings");
            Console.WriteLine(string.Join(" | ", firstList.Select(x => x + " - " + x.Length.ToString())) + "\n");

            Console.WriteLine("---- 1.4 All chars in all strings are dispayed with a space");
            Console.WriteLine(string.Join(" ", firstList.SelectMany(x => x)) + "\n");

            Console.WriteLine("---- 1.5 All first elements starting with a");
            Console.WriteLine(string.Join(" | ", firstList.TakeWhile(x => x.StartsWith("a"))) + "\n");

            Console.WriteLine("---- 1.6 All strings containing AABC substring");
            Console.WriteLine(string.Join(" | ", firstList.Where(x => x.ToUpper().Contains("AABC"))) + "\n");

            Console.WriteLine("__________________________________________________________________________________\n");

            Console.WriteLine("---- 2.1 Combine the first and second list in one sequence starting with the second elements");
            Console.WriteLine(string.Join(" | ", firstList.Skip(1).Concat(secondList.Skip(1))) + "\n");

            Console.WriteLine("---- 2.2 Find duplicate strings in two lists");
            Console.WriteLine(string.Join(" | ", firstList.Intersect(secondList)) + "\n");

            Console.WriteLine("---- 2.3 Find unique strings in two lists");
            Console.WriteLine(string.Join(" | ", firstList.Except(secondList).Union(secondList.Except(firstList))) + "\n");

            Console.WriteLine("---- 2.4 Sort the combined list by the number of vowels");
            Console.WriteLine(string.Join(" | ", firstList.Concat(secondList).OrderBy(x => x, new VowelsComparer())) + "\n");

            Console.WriteLine("__________________________________________________________________________________\n");

            List<Person> persons = new List<Person>()
            {
                new Person { PersonId = 1, FullName = "user1" },
                new Person { PersonId = 2, FullName = "user2" },
                new Person { PersonId = 3, FullName = "user3" },
                new Person { PersonId = 4, FullName = "user4" }
            };

            List<Car> cars = new List<Car>()
            {
                new Car { CarId = 1, Color = "Green", Model = "Tesla S", PersonId = 1, CreationDate = new DateTime(2015, 4, 1) },
                new Car { CarId = 2, Color = "Blue", Model = "BMW 5", PersonId = 1, CreationDate = new DateTime(2010, 4, 1) },
                new Car { CarId = 3, Color = "Green", Model = "Tesla S", PersonId = 2, CreationDate = new DateTime(2015, 2, 2) },
                new Car { CarId = 4, Color = "Green", Model = "BMW 3", PersonId = 3, CreationDate = new DateTime(2013, 4, 1) },
                new Car { CarId = 5, Color = "White", Model = "Tesla S", PersonId = 3, CreationDate = new DateTime(2012, 4, 1) }
            };

            Console.WriteLine("---- 3.1 Display the owner name and his cars (name repeat is possible)");
            Console.WriteLine(string.Join("\n", persons.Join(cars,
                                                                p => p.PersonId,
                                                                c => c.PersonId,
                                                                (p, c) => $"{p.FullName} has {c.Model}"
                                                             )) + "\n");

            Console.WriteLine("---- 3.2 Display the owner name and number of his cars");
            Console.WriteLine(string.Join("\n", persons.GroupJoin(cars,
                                                                    p => p.PersonId,
                                                                    c => c.PersonId,
                                                                    (p, c) => $"{p.FullName} has {c.Select(x => x.Model).Count()} cars"
                                                                 )) + "\n");

            Console.WriteLine("---- 3.3 Display the owners of blue cars");
            Console.WriteLine(string.Join("\n", persons.Join(cars,
                                                                p => p.PersonId,
                                                                c => c.PersonId,
                                                                (p, c) => $"{p.FullName} has {c.Color} car"
                                                             )
                                                             .Where(x => x.Contains("Blue"))) + "\n");

            Console.WriteLine("---- 3.4 Display the owners of uniq cars (uniq model and color)");
            Console.WriteLine(string.Join("\n", cars.Distinct(new UniqueCarsEqualityComparer()).GroupJoin(persons,
                                                                                                    c => c.PersonId,
                                                                                                    p => p.PersonId,
                                                                                                    (c, p) =>
                                                                                                    $"{string.Join("", p.Select(x => x.FullName))} has unique car, it is {c.Color} {c.Model}"
                                                                                                    )) + "\n");

            //1111111111111111
            var tempM1 = cars.GroupBy(x => new { x.Color, x.Model });
            var temp1 = tempM1.Select(x => string.Join("\n", x.Select(z => $"{z.Color}, {z.Model}")));

            //222222222222222222222
            var tempM2 = tempM1.Where(x => x.Count() == 1);
            var temp2 = tempM2.Select(x => string.Join("\n", x.Select(z => $"{z.Color}, {z.Model}")));

            //33333333333333333
            var tempM3 = tempM2.Select(x => new { PersonId = x.First().PersonId, Color = x.First().Color, Model = x.First().Model});
            var temp3 = tempM3;

            //44444444444444444
            var tempM4 = tempM3.Join(persons, x => x.PersonId, y => y.PersonId, (x, y) => y.FullName);
            var temp4 = tempM4;

            //5555555555555555555
            var tempM5 = tempM4.Distinct();
            var temp5 = tempM5;


            Console.WriteLine("\n----------- 1. Grouping all cars in anonymous type (color model only)");
            Console.WriteLine(string.Join("\n", temp1));
            Console.WriteLine("\n----------- 2. Select all cars in anonymous type that occur only once (unique cars)");
            Console.WriteLine(string.Join("\n", temp2));
            Console.WriteLine("\n----------- 3. Creating anonymous collection of unique car owners");
            Console.WriteLine(string.Join("\n", temp3));
            Console.WriteLine("\n----------- 4. Joining anonymous owners with persons collection (getting owner's name)");
            Console.WriteLine(string.Join("\n", temp4));
            Console.WriteLine("\n----------- 5. Removing duplicate names");
            Console.WriteLine(string.Join("\n", temp5));

            //Console.WriteLine(string.Join("\n", cars.GroupBy( x => new { x.Color, x.Model } ).
            //                                         Where  ( x => x.Count() == 1 ).
            //                                         Select ( x => new { PersonId = x.First().PersonId } ).
            //                                         Join   ( persons, 
            //                                                  x => x.PersonId, 
            //                                                  y => y.PersonId, 
            //                                                  (x, y) => y.FullName ).
            //                                         Distinct()
            //                                         ) + "\n");
            //Console.WriteLine("____________________________________________");
            //Console.WriteLine(string.Join(", ", Enumerable.Range(10, 41)));

            Console.ReadLine();

        }
    }
}
