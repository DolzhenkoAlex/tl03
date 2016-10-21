using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по должностям
    /// </summary>
    public interface IServicePost
    {
        /// <summary>
        /// Интерфейс доступа к данным по  должностям 
        /// </summary>
        void GetDataPost(Action<ObservableCollection<DictPost>, Exception> callback);

        /// <summary>
        /// Загрузка данных по должностям
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictPost> GetPost(ObservableCollection<DictPost> posts);

        /// <summary>
        /// Добавление данных по новому должности
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataPost(DictPost newPost);

        /// <summary>
        /// Редактирование данных по должности
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemPost(DictPost editPost);

        /// <summary>
        /// Удаление данных по должности
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemPost(DictPost delPost);

        /// <summary>
        /// Интерфейс нахождения должности по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictPost GetItemPost(string pPost);
    }
}
