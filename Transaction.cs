
namespace CommissionSystemExercise
{
    public class Transaction
    {
        public int From { get; set; }
        public double Amount { get; set; }

        public Transaction(int from, double amount)
        {
            From = from;
            Amount = amount;
        }
    }
}
