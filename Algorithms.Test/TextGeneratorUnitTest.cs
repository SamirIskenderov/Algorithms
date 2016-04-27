using Algorithms.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace Algorithms.Test
{
    [TestClass]
    public class TextGeneratorUnitTest
    {
        #region getNewWord

        [TestMethod]
        public void GetNewWordFromTextGeneratorMustReturnWordWithoutSpaces()
        {
            string word = Common.TextGenerator.GetNewWord(3, 12);
            Assert.AreEqual(false, word.Contains(Common.TextGenerator.SpaceMark.ToString()));
        }

        [TestMethod]
        public void GetNewWordFromTextGeneratorWithFirstUpperLetterMustReturnItProperly()
        {
            string word = Common.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true);
            Assert.AreEqual(true, Char.IsUpper(word[0]));
        }

        [TestMethod]
        public void GetNewWordFromTextGeneratorWithFirstLowerLetterMustReturnItProperly()
        {
            string word = Common.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: false);
            Assert.AreEqual(true, Char.IsLower(word[0]));
        }

        [TestMethod]
        public void GetNewWordFromTextGeneratorMustBeLongerThatMinimalLength()
        {
            const int minLength = 3;

            string word = Common.TextGenerator.GetNewWord(minLength, 12);
            Assert.AreEqual(true, word.Length >= minLength);
        }

        [TestMethod]
        public void GetNewWordFromTextGeneratorMustBeShorterThatMaximalLength()
        {
            const int maxLength = 10;

            string word = Common.TextGenerator.GetNewWord(3, maxLength);
            Assert.AreEqual(true, word.Length <= maxLength);
        }

        #endregion getNewWord

        #region getWords

        [TestMethod]
        public void GettingNWordsFromTextGeneratorMustGetNWords()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            Assert.AreEqual(true, words.Length == N);
        }

        [TestMethod]
        public void GettingWordsFromTextGeneratorMustEndedWithDot()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            string lastWord = words[words.Length - 1].Trim();

            Assert.AreEqual(true, lastWord[lastWord.Length - 1] == Common.TextGenerator.DotMark);
        }

        [TestMethod]
        public void TextGeneratorsWordsMustHaveSpaceAfterDot()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == Common.TextGenerator.DotMark)
                {
                    if (text[i + 1] != Common.TextGenerator.SpaceMark)
                    {
                        throw new Exception("Test failed");
                    }
                }
            }
        }

        [TestMethod]
        public void TextGeneratorsWordsMustNotHaveSpaceBeforeDot()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == Common.TextGenerator.DotMark)
                {
                    if (text[i - 1] == Common.TextGenerator.SpaceMark)
                    {
                        throw new Exception("Test failed");
                    }
                }
            }
        }

        [TestMethod]
        public void TextGeneratorsWordsMustHaveSpaceAfterExclamation()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == Common.TextGenerator.ExclamationMark)
                {
                    if (text[i + 1] != Common.TextGenerator.SpaceMark)
                    {
                        throw new Exception("Test failed");
                    }
                }
            }
        }

        [TestMethod]
        public void TextGeneratorsWordsMustNotHaveSpaceBeforeExclamation()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == Common.TextGenerator.DotMark)
                {
                    if (text[i - 1] == Common.TextGenerator.ExclamationMark)
                    {
                        throw new Exception("Test failed");
                    }
                }
            }
        }

        [TestMethod]
        public void TextGeneratorsWordsMustHaveSpaceAfterQuestion()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == Common.TextGenerator.QuestionMark)
                {
                    if (text[i + 1] != Common.TextGenerator.SpaceMark)
                    {
                        throw new Exception("Test failed");
                    }
                }
            }
        }

        [TestMethod]
        public void TextGeneratorsWordsMustNotHaveSpaceBeforeQuestion()
        {
            const int N = 60;

            string[] words = Common.TextGenerator.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == Common.TextGenerator.DotMark)
                {
                    if (text[i - 1] == Common.TextGenerator.QuestionMark)
                    {
                        throw new Exception("Test failed");
                    }
                }
            }
        }

        #region is

        [TestMethod]
        public void GettingWordsFromTextGeneratorWithoutDotsMustNotHaveDots()
        {
            const int N = 60;

            TextGenerator gen = Common.TextGenerator.Clone();

            gen.IsUsingDots = false;

            string[] words = gen.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            Assert.AreEqual(false, text.Contains(gen.DotMark.ToString()));
        }

        [TestMethod]
        public void GettingWordsFromTextGeneratorWithoutCommasMustNotHaveCommas()
        {
            const int N = 60;

            TextGenerator gen = Common.TextGenerator.Clone();

            gen.IsUsingCommas = false;

            string[] words = gen.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            Assert.AreEqual(false, text.Contains(gen.CommaMark.ToString()));
        }

        [TestMethod]
        public void GettingWordsFromTextGeneratorWithoutExclamationsMustNotHaveExclamations()
        {
            const int N = 60;

            TextGenerator gen = Common.TextGenerator.Clone();

            gen.IsUsingExclamationMarks = false;

            string[] words = gen.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            Assert.AreEqual(false, text.Contains(gen.ExclamationMark.ToString()));
        }

        [TestMethod]
        public void GettingWordsFromTextGeneratorWithoutQuestionsMustNotHaveQuestions()
        {
            const int N = 60;

            TextGenerator gen = Common.TextGenerator.Clone();

            gen.IsUsingQuestionMarks = false;

            string[] words = gen.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            Assert.AreEqual(false, text.Contains(gen.QuestionMark.ToString()));
        }

        [TestMethod]
        public void GettingWordsFromTextGeneratorWithoutThreeDotsMustNotHaveThreeDots()
        {
            const int N = 60;

            TextGenerator gen = Common.TextGenerator.Clone();

            gen.IsUsingThreeDotsMark = false;

            string[] words = gen.GetWords(N).ToArray();

            StringBuilder sb = new StringBuilder(128);

            foreach (var item in words)
            {
                sb.Append(item);
            }

            string text = sb.ToString();

            Assert.AreEqual(false, text.Contains(gen.ThreeDotsMark.ToString()));
        }

        #endregion is

        #endregion getWords
    }
}