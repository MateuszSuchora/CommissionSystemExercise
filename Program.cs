using System;
using System.Collections.Generic;
using System.IO;

namespace CommissionSystemExercise
{
    class Program
    {
         static void Main(string[] args)
        {
            string structurePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net5.0", ""), "struktura.xml");
            string transactionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net5.0", ""), "przelewy.xml");
            List<Participant> participants = StructureReader.ReadStructure(structurePath);
            List<Transaction> transactions = TransfersReader.ReadTransactions(transactionsPath);
            participants.Sort((p, q) => p.Id.CompareTo(q.Id));
            Helper.CountSubordinatesWithoutAnySubordinates(ref participants);
            Helper.HandleTransfers(transactions, ref participants);
            foreach (var e in participants)
            {
                Console.WriteLine($"{e.Id} {e.Level} {e.SubordinatesWithoutAnySubordinates} {e.Commission}");
            }
        }
    }
}
