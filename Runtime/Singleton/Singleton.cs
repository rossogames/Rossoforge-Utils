using System;

namespace Rossoforge.Utils.Singleton
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> instance = new(() => new T());

        public static T Instance => instance.Value;

        protected Singleton()
        {
        }
    }
}