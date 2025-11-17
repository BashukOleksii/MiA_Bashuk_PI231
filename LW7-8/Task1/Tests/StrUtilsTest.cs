using System.Collections;
using Task1.Services;

namespace Tests
{
    public class StrUtilsTest
    {
     
        public class ReverseTestData: IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "Привіт", "тівирП"};
                yield return new object[] { "a", "a" };
                yield return new object[] { "12345", "54321" };
                yield return new object[] { "Hi", "iH" };
                yield return new object[] { "AbC", "CbA" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public static IEnumerable<object[]> TestContainsMethod() 
        {
            yield return new object[] { "Кит", 'т' };
            yield return new object[] { "Два слова", ' ' };
            yield return new object[] { "_", '_' };
            yield return new object[] { "Велика буква", 'В' };
            yield return new object[] { "Word", 'd' };
        }

        [Fact]
        public void ArgumentNullExceptionTest()
        {
            StringUtils stringUtils = new StringUtils();

            Assert.Throws<ArgumentNullException>(() => stringUtils.AddToEnd(""));
        }

        [Fact]
        public void TypeTest()
        {
            StringUtils stringUtils = new StringUtils("Слово");

            var type = stringUtils.CountWord();

            Assert.IsType<Int32>(type);
        }

        [Fact]
        public void RangeTest()
        {
            int maxLen = StringUtils.MaxLen("123", "1234", "12", "12345", "1");

            Assert.InRange(maxLen, 1, 10);
        }

        [Fact]
        public void SameTest()
        {
            StringUtils s0 = new StringUtils("Слово");
            StringUtils s1 = new StringUtils("Слово");

            Assert.NotSame(s0, s1);
        }

        [Theory]
        [InlineData("око")]
        [InlineData("ОкО")]
        [InlineData("Око")]
        [InlineData("О ко")]
        [InlineData("Я несу гусеня")]
        [InlineData("кит на морі романтик")]
        [InlineData("Кит на морі романтик")]
        public void PalindromTest(string palindrom)
        {
            StringUtils stringUtils = new StringUtils(palindrom);

            bool isPalindrom = stringUtils.IsPalindrom();

            Assert.True(isPalindrom);
        }

        [Theory]
        [ClassData(typeof(ReverseTestData))]
        public void ReverseTest(string word, string expectedReverse)
        {
            StringUtils stringUtilities = new StringUtils(word);

            string reverse = stringUtilities.Reverse();

            Assert.Equal(reverse, expectedReverse);
        }

        [Theory]
        [MemberData(nameof(TestContainsMethod))]
        public void CollectionTest(string word, char symbol)
        {
            StringUtils stringUtils = new StringUtils(word);

            char[] chars = stringUtils.ToCharArray();

            Assert.Contains(symbol,chars);
        }


    }
}