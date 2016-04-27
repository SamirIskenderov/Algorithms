using System.Text;

namespace Algorithms.Extensions
{
    public static class StringBuilderExtension
    {
        public static void Trim(this StringBuilder sb, bool saveFirst, bool saveLast)
        {
            for (int i = 0; i < sb.Length - 1; i++)
            {
                if (saveFirst &&
                    (sb[i] == ' ') &&
                    (sb[i + 1] != ' '))
                {
                    break;
                }

                if (sb[i] == ' ')
                {
                    sb.Remove(i, 1);
                }
            }

            for (int i = sb.Length - 1; i > 1; i--)
            {
                if (saveLast &&
                    (sb[i] == ' ') &&
                    (sb[i - 1] != ' '))
                {
                    break;
                }

                if (sb[i] == ' ')
                {
                    sb.Remove(i, 1);
                }
            }
        }
    }
}