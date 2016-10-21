using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по учебным годам
    /// </summary>
    public interface IServiceAcademicYear
    {
        /// <summary>
        /// Интерфейс доступа к данным по учебным годам 
        /// </summary>
        void GetDataAcademicYear(Action<ObservableCollection<DictAcademicYear>, Exception> callback);

        /// <summary>
        /// Загрузка данных по учебным годам
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictAcademicYear> GetAcademicYear(ObservableCollection<DictAcademicYear> years);

        /// <summary>
        /// Добавление данных по новому учебному году
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataAcademicYear(DictAcademicYear newAcademicYear);

        /// <summary>
        /// Редактирование данных по учебному году
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemAcademicYear(DictAcademicYear editAcademicYear);

        /// <summary>
        /// Удаление данных по учебному году
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemAcademicYear(DictAcademicYear delAcademicYear);

        /// <summary>
        /// Интерфейс нахождения учебного года по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictAcademicYear GetItemAcademicYear(string acYear);
    }
}
