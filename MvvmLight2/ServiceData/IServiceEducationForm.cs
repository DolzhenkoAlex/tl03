using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по формам обучения
    /// </summary>
    public interface IServiceEducationForm
    {
        /// <summary>
        /// Интерфейс доступа к данным по формам обучения
        /// </summary>
        void GetDataEducationForm(Action<ObservableCollection<DictEducationForm>, Exception> callback);

        /// <summary>
        /// Загрузка данных по формам обучения
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictEducationForm> GetEducationForm(ObservableCollection<DictEducationForm> educationform);

        /// <summary>
        /// Добавление данных по новой форме обучения
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataEducationForm(DictEducationForm newEducationForm);

        /// <summary>
        /// Редактирование данных по форме обучения
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemEducationForm(DictEducationForm editEducationForm);

        /// <summary>
        /// Удаление данных по форме обучения
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemEducationForm(DictEducationForm delEducationForm);

        /// <summary>
        /// Интерфейс нахождения формы обучения по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictEducationForm GetItemEducationForm(string edForm);
    }
}

