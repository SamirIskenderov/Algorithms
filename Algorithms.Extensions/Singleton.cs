namespace Algorithms.Extensions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Thread-safe generic singleton
    /// </summary>
    /// <typeparam name="T">Singleton class</typeparam>
    public class Singleton<T>
        where T : class
    {
        protected Singleton()
        {
        }

        public static T Instance => SingletonCreator<T>.CreatorInstance;

        private sealed class SingletonCreator<S>
            where S : class
        {
            public static S CreatorInstance { get; } = (S)typeof(S).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                                    null,
                                                                                    new Type[0],
                                                                                    new ParameterModifier[0]).Invoke(null);
        }
    }
}