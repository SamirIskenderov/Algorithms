using System;

namespace Algorithms.Library
{
    public class Divider
    {
        public bool IsDivBy3(long number)
        {
            long buf = 0;

            while (number > 0)
            {
                buf += number % 10;
                number = number / 10;
            }

            return buf % 3 == 0;
        }

        public bool IsDivBy6(long number)
            => number % 2 == 0 && this.IsDivBy3(number);

        public bool IsDivBy7(long number)
        {
            while (true)
            {
                if (number < 1000)
                {
                    return number % 7 == 0;
                }

                long head = (number / 1000);
                long tail = (number % 1000);

                long result = Math.Abs(head - tail);

                number = result;
            }
        }

        public bool IsDivBy8(long number)
        {
            if (number < 1000)
            {
                return number % 8 == 0;
            }

            long tail = number % 1000;

            long a = (tail / 100) * 4; // TODO to check speed
            long b = ((tail / 10) % 10) * 2;
            long c = tail % 10;

            long result = a + b + c;

            return result % 8 == 0;
        }

        public bool IsDivBy9(long number)
        {
            long buf = 0;

            if (number < 100)
            {
                return number % 9 == 0;
            }

            while (number > 0)
            {
                buf += number % 10;
                number = number / 10;
            }

            return this.IsDivBy9(buf);
        }

        public bool IsDivBy11(long number)
        {
            long buf = 0;
            long dec = 1;
            bool flag = true;

            if (buf < 122)
            {
                return number % 11 == 0;
            }

            while (number > 0)
            {
                dec = dec * 10;

                if (flag)
                {
                    buf += number % dec;
                }
                else
                {
                    buf -= number % dec;
                }

                flag = !flag;
                number = number / 10;
            }

            return this.IsDivBy11(buf);
        }

        public bool IsDivBy13(long number)
        {
            if (number < 1000)
            {
                return number % 13 == 0;
            }

            long head = (number / 1000);
            long tail = (number % 1000);

            long result = Math.Abs(head - tail);

            return this.IsDivBy13(result);
        }

        public bool IsDivBy17(long number)
        {
            if (number < 1000)
            {
                return number % 17 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = Math.Abs(head - tail * 5);

            return this.IsDivBy17(result);
        }

        public bool IsDivBy19(long number)
        {
            if (number < 1000)
            {
                return number % 19 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = head + tail * 2;

            return this.IsDivBy19(result);
        }

        public bool IsDivBy20(long number)
        {
            long tail = number % 100;

            if ((tail == 0) ||
                (tail == 20) ||
                (tail == 40) ||
                (tail == 60) ||
                (tail == 80))
            {
                return true;
            }

            return false;
        }

        public bool IsDivBy23(long number)
        {
            if (number < 1000)
            {
                return number % 23 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = head + tail * 7;

            return this.IsDivBy23(result);
        }

        public bool IsDivBy25(long number)
        {
            long tail = number % 100;

            if ((tail == 0) ||
                (tail == 25) ||
                (tail == 50) ||
                (tail == 75))
            {
                return true;
            }

            return false;
        }

        public bool IsDivBy29(long number)
        {
            if (number < 1000)
            {
                return number % 29 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = head + tail * 3;

            return this.IsDivBy29(result);
        }

        public bool IsDivBy10(long number)
            => number % 10 == 0;

        public bool IsDivBy30(long number)
            => this.IsDivBy10(number) && this.IsDivBy3(number);

        public bool IsDivBy31(long number)
        {
            if (number < 1000)
            {
                return number % 31 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = Math.Abs(head - tail * 3);

            return this.IsDivBy31(result);
        }

        public bool IsDivBy37(long number)
        {
            if (number < 1000)
            {
                return number % 37 == 0;
            }

            long head = number / 100;
            long body = (number % 100) / 10;
            long tail = number % 10;

            long result = Math.Abs(head + tail * 10 - body * 11);

            return this.IsDivBy37(result);
        }

        public bool IsDivBy41(long number)
        {
            if (number < 1000)
            {
                return number % 41 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = Math.Abs(head - tail * 4);

            return this.IsDivBy41(result);
        }

        public bool IsDivBy50(long number)
        {
            long tail = number % 100;

            if ((tail == 0) ||
                (tail == 50))
            {
                return true;
            }

            return false;
        }

        public bool IsDivBy59(long number)
        {
            if (number < 1000)
            {
                return number % 59 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = head + tail * 6;

            return this.IsDivBy59(result);
        }

        public bool IsDivBy79(long number)
        {
            if (number < 1000)
            {
                return number % 79 == 0;
            }

            long head = number / 10;
            long tail = number % 10;

            long result = head + tail * 8;

            return this.IsDivBy79(result);
        }

        public bool IsDivBy101(long number)
        {
            long buf = 0;
            long dec = 1;
            bool flag = true;

            if (buf < 1000)
            {
                return number % 101 == 0;
            }

            while (number > 0)
            {
                dec = dec * 100;

                if (flag)
                {
                    buf += number % dec;
                }
                else
                {
                    buf -= number % dec;
                }

                flag = !flag;
                number = number / 100;
            }

            return this.IsDivBy101(buf);
        }
    }
}