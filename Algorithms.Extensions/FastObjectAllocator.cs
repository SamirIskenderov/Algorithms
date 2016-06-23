using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Algorithms.Extensions
{
    public class FastObjectAllocator<T>
        where T : class, new()
    {
        private static readonly object LockObj = new object();

        private static Func<T> objCreator;

        public static T New()
        {
            if (objCreator != null)
            {
                return objCreator.Invoke();
            }

            lock (LockObj)
            {
                if (objCreator != null)
                {
                    return objCreator.Invoke();
                }

                Type objType = typeof(T);

                DynamicMethod meth = new DynamicMethod(name: Guid.NewGuid().ToString(),
                                                        returnType: objType,
                                                        parameterTypes: null);

                ConstructorInfo defaultCtor = objType.GetConstructor(new Type[] { });

                ILGenerator ilGen = meth.GetILGenerator();

                ilGen.Emit(OpCodes.Newobj, defaultCtor);
                ilGen.Emit(OpCodes.Ret);

                objCreator = meth.CreateDelegate(typeof(Func<T>)) as Func<T>;
            }

            if (objCreator != null)
            {
                return objCreator.Invoke();
            }

            throw new InvalidOperationException("Can't allocate object");
        }
    }
}