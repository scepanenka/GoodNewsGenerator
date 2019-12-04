using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GoodNews.ApiServices.PositivityScorer
{
    public class PositivityScorer
    {
        #region Fields
        private static volatile PositivityScorer _instance;
        private static readonly object SyncRoot = new Object();
        private readonly IDictionary<string, int> _words;
        #endregion

        #region Properties
        public IDictionary<string, int> Words { get { return _words; } }
        public int WordsCount { get { return _words.Count; } }
        #endregion

        public static PositivityScorer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new PositivityScorer();
                    }
                }

                return _instance;
            }
        }

        #region ctor
        private PositivityScorer()
        {
            _words = new Dictionary<string, int>();

            using (var file = new StreamReader("AFINN-ru.json"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var bits = line.Split('\t');
                    _words.Add(bits[0], int.Parse(bits[1]));
                }
            }
        }
        #endregion

        /// <summary>
        /// Tokenizes a string. This method first removes non-alpha characters,
        /// removes multiple spaces, and lowercases every word. Then splits the
        /// string into an array of words.
        /// </summary>
        /// <param name="input">String to be tokenized</param>
        /// <returns>Array of words (tokens)</returns>
        private static IEnumerable<string> Tokenize(string input)
        {
            input = Regex.Replace(input, "[^a-zA-Z ]+", "");
            input = Regex.Replace(input, @"\s+", " ");
            input = input.ToLower();
            return input.Split(' ');
        }

        /// <summary>
        /// Calculates sentiment score of a sentence
        /// </summary>
        /// <param name="input">Sentence</param>
        /// <returns>Score object</returns>
        public Score GetScore(string input)
        {
            var score = new Score { Tokens = Tokenize(input) };

            foreach (var token in score.Tokens)
            {
                if (!_words.ContainsKey(token)) continue;

                var item = _words[token];
                score.Words.Add(token);

                if (item > 0) score.Positive.Add(token);
                if (item < 0) score.Negative.Add(token);

                score.Sentiment += item;
            }

            return score;
        }

        /// <summary>
        /// Add extra words for scoring
        /// </summary>
        /// <param name="words">Dictionary of string keys and int values</param>
        public void InjectWords(IDictionary<string, int> words)
        {
            foreach (var word in words.Where(word => !_words.ContainsKey(word.Key)))
            {
                _words.Add(word.Key, word.Value);
            }
        }

        #region Inner Score Class
        public class Score
        {
            /// <summary>
            /// Tokens which were scored
            /// </summary>
            public IEnumerable<string> Tokens { get; set; }
            /// <summary>
            /// Total sentiment score of the tokens
            /// </summary>
            public int Sentiment { get; set; }
            /// <summary>
            /// Average sentiment score Sentiment/Tokens.Count
            /// </summary>
            public double AverageSentimentTokens { get { return (double)Sentiment / Tokens.Count(); } }
            /// <summary>
            /// Average sentiment score Sentiment/Words.Count
            /// </summary>
            public double AverageSentimentWords { get { return (double)Sentiment / Words.Count(); } }
            /// <summary>
            /// Words that were used from AFINN
            /// </summary>
            public IList<string> Words { get; set; }
            /// <summary>
            /// Words that had negative sentiment
            /// </summary>
            public IList<string> Negative { get; set; }
            /// <summary>
            /// Words that had positive sentiment
            /// </summary>
            public IList<string> Positive { get; set; }

            public Score()
            {
                Words = new List<string>();
                Negative = new List<string>();
                Positive = new List<string>();
            }
        }
        #endregion
    }
}
