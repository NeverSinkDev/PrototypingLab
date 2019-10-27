using System.Linq;

namespace FuncCollection.Mathem.MathOperations
{
    public static class StructureUtility
    {
        public static bool IsPalindrome(this int i)
        {
            var s = i.ToString();
            return s.IsPalindrome();
        }

        public static bool IsPalindrome(this string s)
        {
            bool evenLength = s.Length % 2 == 0;

            string split1 = evenLength ? s.Substring(0, s.Length / 2) : s.Substring(0, (s.Length - 1) / 2);
            string split2 = evenLength ? s.Substring(s.Length / 2) : s.Substring((s.Length + 1) / 2);

            return split1 == string.Concat(split2.Reverse()) ? true : false;
        }
    }
}
