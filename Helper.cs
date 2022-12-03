using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommissionSystemExercise
{
    public static class Helper
    {
        public static void CountSubordinatesWithoutAnySubordinates(ref List<Participant> participants)
        {
            foreach(Participant participant in participants)
            {
                participant.SubordinatesWithoutAnySubordinates = GetSubordinatesWithoutAnySubordinates(participants, participant.Subordinates);
            }
        }

        private static int GetSubordinatesWithoutAnySubordinates(List<Participant> participants , List<int> subordinates)
        {
            List<Participant> test = participants.FindAll(e => subordinates.Contains(e.Id) && e.Subordinates.Count == 0);
            return test.Count;
        }

        public static void HandleTransfers(List<Transaction> transfers, ref List<Participant> participants) 
        {
            foreach(Transaction transfer in transfers)
            {
                HandleTransfer(transfer, ref participants);
            }
        }

        private static void HandleTransfer(Transaction transfer, ref List<Participant> participants)
        {
            Participant owner = participants.First(e => e.Level == 0);
            Participant transferGuy = participants.First(e => e.Id == transfer.From);
            if (transferGuy.Level == 1 || transferGuy.Level == 0)
            {
                owner.Commission += transfer.Amount;
            }
            else
            {
                double half = Math.Floor(transfer.Amount/2);
                transfer.Amount -= half;
                int currentLevel = 1;
                owner.Commission += half;
                while (currentLevel < transferGuy.Level)
                {
                    Participant nextGuy = participants.First(e => e.Subordinates.Contains(transferGuy.Id)&&e.Level==currentLevel);
                    if(currentLevel == transferGuy.Level - 1)
                    {
                        nextGuy.Commission += Math.Round(transfer.Amount,2);
                        break;
                    }
                    else
                    {
                        half = Math.Floor(transfer.Amount / 2);
                        transfer.Amount -= half;
                        nextGuy.Commission += half;
                    }
                    currentLevel +=1;
                }
            }
        }
    }
}
