using System;
using System.Collections.Generic;

namespace SimpleDI
{
    /// <summary>
    ///     DI контейнер
    /// </summary>
    public static class DIContainer
    {
        /// <summary>
        ///     Зарегистрированные объекты
        /// </summary>
        private static readonly Dictionary<Type, Type> RegisteredObjects =
            new Dictionary<Type, Type>();

        /// <summary>
        ///     Регистрация объекта в контейнере
        /// </summary>
        /// <typeparam name="TKey">Тип абстракции</typeparam>
        /// <typeparam name="TConcrete">Тип объекта</typeparam>
        public static void Register<TKey, TConcrete>() where TConcrete : TKey
        {
            RegisteredObjects[typeof(TKey)] = typeof(TConcrete);
        }

        /// <summary>
        ///     Получение объекта из контейнера
        /// </summary>
        /// <typeparam name="TKey">Тип абстракции</typeparam>
        /// <returns>Объект</returns>
        public static dynamic Resolve<TKey>()
        {
            return Activator.CreateInstance(RegisteredObjects[typeof(TKey)]);
        }
    }
}