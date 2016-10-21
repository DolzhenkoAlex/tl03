using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по направлениям
    /// </summary>
    public interface IServiceSpeciality
    {
        /// <summary>
        /// Интерфейс доступа к данным по  направлениям 
        /// </summary>
        void GetDataSpeciality(Action<ObservableCollection<DictSpeciality>, Exception> callback);

        /// <summary>
        /// Загрузка данных по направлениям
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictSpeciality> GetSpeciality(ObservableCollection<DictSpeciality> specialitys);

        /// <summary>
        /// Добавление данных по новому направлению
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataSpeciality(DictSpeciality newSpeciality);

        /// <summary>
        /// Редактирование данных по направлению
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemSpeciality(DictSpeciality editSpeciality);

        /// <summary>
        /// Удаление данных по направлению
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemSpeciality(DictSpeciality delSpeciality);

        /// <summary>
        /// Интерфейс нахождения направления по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictSpeciality GetItemSpeciality(string codeSpeciality);
    }
}
