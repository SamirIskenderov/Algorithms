using Algorithms.Library;
using System;
using System.Linq;
using System.Text;
using bigint = Algorithms.BigRat.BigInt;

namespace Algorithms.BigRat
{
    public class BigInt : IComparable
    {
        public bigint previousBlock;
        internal uint value;
        private static readonly BigIntMath math = new BigIntMath();
        private static readonly BigIntMathHelper mathHelper = new BigIntMathHelper();

        internal static bigint One { get; } = new bigint(1);
        internal static bigint Zero { get; } = new bigint(0);

        #region ctor

        public BigInt(bigint b)
        {
            this.value = b.value;
            this.previousBlock = b.previousBlock;
        }

        public BigInt(uint v)
        {
            this.value = v;
        }

        public BigInt()
        {
        }

        #endregion ctor

        #region operators

        #region unary

        public static bigint operator --(bigint lhs)
            => math.Subtract(lhs, bigint.One);

        public static bigint operator ++(bigint lhs)
            => math.Add(lhs, One);

        #endregion unary

        #region binary

        public static bigint operator -(bigint lhs, bigint rhs)
            => math.Subtract(lhs, rhs);

        public static bigint operator -(uint lhs, bigint rhs)
            => math.Subtract(new bigint(lhs), rhs);

        public static bigint operator -(bigint lhs, uint rhs)
            => math.Subtract(lhs, new bigint(rhs));

        public static bigint operator %(bigint lhs, bigint rhs)
            => math.Reminder(lhs, rhs);

        public static bigint operator %(uint lhs, bigint rhs)
            => math.Reminder(new bigint(lhs), rhs);

        public static bigint operator %(bigint lhs, uint rhs)
            => math.Reminder(lhs, new bigint(rhs));

        public static bigint operator *(bigint lhs, bigint rhs)
            => math.Multiple(lhs, rhs);

        public static bigint operator *(uint lhs, bigint rhs)
            => math.Multiple(new bigint(lhs), rhs);

        public static bigint operator *(bigint lhs, uint rhs)
            => math.Multiple(lhs, new bigint(rhs));

        public static bigint operator /(bigint lhs, bigint rhs)
            => math.Divide(lhs, rhs);

        public static bigint operator /(uint lhs, bigint rhs)
            => math.Divide(new bigint(lhs), rhs);

        public static bigint operator /(bigint lhs, uint rhs)
            => math.Divide(lhs, new bigint(rhs));

        public static bigint operator +(bigint lhs, bigint rhs)
                                                                                                            => math.Add(lhs, rhs);

        public static bigint operator +(uint lhs, bigint rhs)
            => math.Add(new bigint(lhs), rhs);

        public static bigint operator +(bigint lhs, uint rhs)
            => math.Add(lhs, new bigint(rhs));

        #endregion binary

        #region eq

        public static bool operator !=(bigint lhs, bigint rhs)
            => !(lhs == rhs);

        public static bool operator !=(uint lhs, bigint rhs)
            => !(lhs == rhs);

        public static bool operator !=(bigint lhs, uint rhs)
            => !(lhs == rhs);

        public static bool operator <(bigint lhs, bigint rhs)
        {
            if ((object)lhs == null)
            {
                return false;
            }

            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator <(uint lhs, bigint rhs)
            => new bigint(lhs) < rhs;

        public static bool operator <(bigint lhs, uint rhs)
            => lhs < new bigint(rhs);

        public static bool operator <=(bigint lhs, bigint rhs)
            => lhs == rhs || lhs < rhs;

        public static bool operator <=(uint lhs, bigint rhs)
            => lhs == rhs || lhs < rhs;

        public static bool operator <=(bigint lhs, uint rhs)
            => lhs == rhs || lhs < rhs;

        public static bool operator ==(bigint lhs, bigint rhs)
        {
            object olhs = (object)lhs;
            object orhs = (object)rhs;

            if ((olhs == null) && (orhs == null))
            {
                return true;
            }

            if ((olhs == null) ^ (olhs == null))
            {
                return false;
            }

            return lhs.CompareTo(rhs) == 0;
        }

        public static bool operator ==(uint lhs, bigint rhs)
            => new bigint(lhs) == rhs;

        public static bool operator ==(bigint lhs, uint rhs)
            => lhs == new bigint(rhs);

        public static bool operator >(bigint lhs, bigint rhs)
        {
            if ((object)lhs == null)
            {
                return false;
            }

            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator >(uint lhs, bigint rhs)
            => new bigint(lhs) > rhs;

        public static bool operator >(bigint lhs, uint rhs)
            => lhs > new bigint(rhs);

        public static bool operator >=(bigint lhs, bigint rhs)
            => lhs == rhs || lhs > rhs;

        public static bool operator >=(uint lhs, bigint rhs)
            => lhs == rhs || lhs > rhs;

        public static bool operator >=(bigint lhs, uint rhs)
            => lhs == rhs || lhs > rhs;

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            bigint p = obj as bigint;
            if (p == null)
            {
                return 1;
            }

            return this.CompareTo(p);
        }

        public int CompareTo(bigint input)
        {
            if ((object)input == null)
            {
                return 1;
            }

            bigint thiscopy = this;

            //mathHelper.TrimStructure(ref thiscopy);
            //mathHelper.TrimStructure(ref input); TO REALIZE

            int lhsBlockCount = mathHelper.GetBlocksCount(this);
            int rhsBlockCount = mathHelper.GetBlocksCount(input);

            if (lhsBlockCount < rhsBlockCount)
            {
                return -1;
            }
            else if (lhsBlockCount > rhsBlockCount)
            {
                return 1;
            }

            bigint lhscopy = this/*.DeepClone()*/;
            bigint rhscopy = input/*.DeepClone()*/; /*TO REALIZE*/

            bigint lhscopyParent = lhscopy;
            bigint rhscopyParent = rhscopy;

            bigint tmp = new bigint();

            if (lhsBlockCount != 1)
            {
                while (lhscopy.previousBlock.previousBlock != null)
                {
                    lhscopy = lhscopy.previousBlock;
                    rhscopy = rhscopy.previousBlock;
                }

                if (lhscopy.previousBlock.value > rhscopy.previousBlock.value)
                {
                    return 1;
                }
                else if (lhscopy.previousBlock.value < rhscopy.previousBlock.value)
                {
                    return -1;
                }

                lhscopy.previousBlock = null;
                rhscopy.previousBlock = null;

                return lhscopyParent.CompareTo(rhscopyParent);
            }
            else
            {
                if (lhscopy.value > rhscopy.value)
                {
                    return 1;
                }
                else if (lhscopy.value < rhscopy.value)
                {
                    return -1;
                }
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            bigint p = obj as bigint;
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this == p;
        }

        public bool Equals(bigint obj)
        {
            if ((object)obj == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this == obj;
        }

        public override int GetHashCode()
        {
            bigint tmp = this;

            int result = 0;
            while (tmp != null)
            {
                result += (int)tmp.value;

                tmp = tmp.previousBlock;
            }

            return result;
        }

        #endregion eq

        #endregion operators

        public bigint DeepClone()
        {
            bigint tmp = this;
            bigint result = new bigint();
            bigint current = result;

            while (tmp != null)
            {
                current.value = tmp.value;

                tmp = tmp.previousBlock;
                if (tmp != null)
                {
                    current.previousBlock = new bigint();
                    current = current.previousBlock;
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(32);

            bigint current = this;

            while (current != null)
            {
                int count = 0;
                byte a = 0;

                sb.Append("[ ");

                foreach (var item in Bitwise.GetNextBit(current.value).Reverse())
                {
                    sb.Append(item ? '1' : '0');

                    if (a == 3)
                    {
                        sb.Append(' ');
                        a = 0;
                    }
                    else
                    {
                        a++;
                    }
                    count++;
                }

                while (count < 32)
                {
                    sb.Append('0');

                    if (a == 3)
                    {
                        sb.Append(' ');
                        a = 0;
                    }
                    else
                    {
                        a++;
                    }

                    count++;
                }

                sb.Append("] ");
                current = current.previousBlock;
            }

            return sb.ToString();
        }
    }
}