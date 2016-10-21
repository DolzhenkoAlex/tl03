using System;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Интефейс контейнера внедрения зависимотсей
    /// </summary>
    interface IContainer
    {
        /// <summary>
        /// Разрешение доступа к типу Т
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Регистрация внедряемого типа как "Одиночки"
        /// </summary>
        /// <typeparam name="TInterface"></typeparam> - интерфейс типа
        /// <typeparam name="TImplementor"></typeparam> - целевой объект
        void RegisterAsSingleton<TInterface, TImplementor>()
            where TImplementor : TInterface;


        /// <summary>
        /// Регистрация существующего типа
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        void RegisterInstance(Type type, object instance);
    }
}
