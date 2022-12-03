using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CommissionSystemExercise
{
    public static class TransfersReader
    {
        public static List<Transaction> ReadTransactions(string path)
        {
            List<Transaction> transactions = new();
            const string fileContent = "przelew";
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            var s = string.Empty;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    s = sr.ReadToEnd();
                }
            }

            var xDocument = XDocument.Parse(s);
            var xNodes = xDocument.DescendantNodes().ToList();
            foreach (var node in xNodes)
            {
                var element = node as XElement;
                if (element.Name != fileContent) continue;
                int from = Int32.Parse(element.Attribute("od").Value);
                double amount = double.Parse(element.Attribute("kwota").Value, CultureInfo.InvariantCulture);
                transactions.Add(new Transaction(from, Math.Round(amount,2)));
            }

            return transactions;
        }
    }
}
