using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordLadder.Test
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        [DataRow("hit", "cog", new string[]
        {
            "hot","dot","dog","lot","log","cog"
        }, 2, 5)]
        [DataRow("hit", "cog", new string[]
        {
            "hot","dot","dog","lot","log"
        }, 0, 0)]
        public void TestCountAndSize(string beginWord, string endWord, IList<string> wordList, int resultCount, int resultSize)
        {
            var result = new Problem(beginWord, endWord, wordList).Solve();

            Assert.IsTrue(result.Count == resultCount && (result.Any()? result.First().Count : 0) == resultSize);
        }

    }
}