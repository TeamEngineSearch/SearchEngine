using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using TikaOnDotNet.TextExtraction;
using System.Text.RegularExpressions;

namespace DocApi.Utils
{
    public class GlobalUtils
    {
       public static string[] stopWords = {"to", "from", "in", "on", "with", "without", "within",
                                           "which", "a", "the", "an", "and", "upon", "by", "about", "for",
                                           "after", "but", "above", "over", "at", "into", "until", "it" };

        private static HashSet<FileInfo> listofDocuments = new HashSet<FileInfo> { };

        private static Dictionary<FileInfo, List<string>> _Index = new Dictionary<FileInfo, List<string>> { };


        public static IWebHostEnvironment _webHostEnvironment;

        public GlobalUtils(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<string> QueryParser(string query)
        {
            string[] splittedQueries = query.Split(" ");

            var filteredQueries = splittedQueries.Where(query => !stopWords.Contains(query));


            List<string> keywords = new List<string>();

            foreach(string fQuery in filteredQueries)
            {
                keywords.Add(fQuery);
            }

            return keywords;
        }


        public static Dictionary<string, List<string>> DocumentParser(string path)
        {
            Dictionary<string, List<string>> documentTexts = new Dictionary<string, List<string>>();

            string[] documents = Directory.GetFiles(path);

            foreach (string doc in documents)
            {
                documentTexts.Add(doc, GetDocumentText(doc));
            }

            return documentTexts;
        }


        public static List<string> GetDocumentText(string document)
        {
            List<string> documentTexts = new List<string> { };
            TextExtractor textextractor = new TextExtractor();

            var result = textextractor.Extract(document);

            MatchCollection matches = Regex.Matches(result.Text, "[a-z]([:']?[a-z])*", RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                documentTexts.Add(match.Value);
            }

            return documentTexts;
        }


        //public void Indexer

    }
}
