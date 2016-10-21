using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по кафедрам
    /// </summary>
    public interface IServiceChair
    {
        /// <summary>
        /// Интерфейс доступа к данным по  кафедрам 
        /// </summary>
        void GetDataChair(Action<ObservableCollection<Chair>, Exception> callback, int idFaculty);

        /// <summary>
        /// Загрузка данных по кафедрам определенного факультетам
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<Chair> GetChair(ObservableCollection<Chair> chairs, int idFaculty);

        /// <summary>
        /// Добавление данных по новой кафедре
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataChair(Chair newChair);

        /// <summary>
        /// Редактирование данных по кафедре
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemChiar(Chair editChair);

        /// <summary>
        /// Удаление данных по кафедре
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemChair(Chair delChair);

        /// <summary>
        /// Интерфейс нахождения кафдры по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        Chair GetItemChair(int codeChair);
    }
}
