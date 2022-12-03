using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CommissionSystemExercise
{
    static class StructureReader
    {
        public static List<Participant> ReadStructure(string path)
        {
            List<Participant> participants = new();
            const string fileContent = "uczestnik";
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
                List<int> subordinates = new();
                var element = node as XElement;
                if (element.Name != fileContent) continue;
                int id = element.FirstAttribute.Name == "id" ? Int32.Parse(element.FirstAttribute.Value) : 0;
                int level = -1;
                CheckLevel(element, ref level);
                StructureReader.CheckNodeSubordinates(element, ref subordinates);
                participants.Add(new Participant(id, level, subordinates));
            }

            return participants;
        }
        private static void CheckNodeSubordinates(XElement node, ref List<int> subordinates)
        {
            foreach (XElement childNode in node.Nodes())
            {
                int id = childNode.FirstAttribute.Name == "id" ? Int32.Parse(childNode.FirstAttribute.Value) : 0;
                subordinates.Add(id);
                CheckNodeSubordinates(childNode, ref subordinates);
            } 
        }

        private static void CheckLevel(XElement node, ref int level)
        {
            if (node.Parent == null)
            {
                return;
            }
            else
            {
                level++;
                CheckLevel(node.Parent, ref level);
            }
        }
    }
}
