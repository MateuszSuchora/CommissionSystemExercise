using System;
using System.Collections.Generic;

namespace CommissionSystemExercise
{
    public class Participant
    {
        public Participant(int id, int level, List<int> subordinates)
        {
            Id = id;
            Level = level;
            Subordinates = subordinates;
        }

        public Participant(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int Level { get; set; }
        public List<int> Subordinates { get; set; }
        public int SubordinatesWithoutAnySubordinates { get; set; }
        public double Commission { get; set; }

        public void Print()//used for tests
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"SubordinatesWithoutAnySubordinates: {SubordinatesWithoutAnySubordinates}");
            if (Subordinates.Count > 0) { 
                Console.WriteLine("Subordinates: {0}", string.Join(',', Subordinates));
            }
            else
            {
                Console.WriteLine("No subordinates");
            }
            Console.WriteLine($"Commmision: {Commission}") ;
            Console.WriteLine("-----------------------------------");
        }
    }
}
