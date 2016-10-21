using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по типам обучения
    /// </summary>
    public interface IServiceTypeTraining
    {
        /// <summary>
        /// Интерфейс доступа к данным по типам обучения
        /// </summary>
        void GetDataTypeTraining(Action<ObservableCollection<DictTypeTraining>, Exception> callback);

        /// <summary>
        /// Загрузка данных по типам обучения
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictTypeTraining> GetTypeTraining(ObservableCollection<DictTypeTraining> typetraining);

        /// <summary>
        /// Добавление данных по новому типу обучения
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataTypeTraining(DictTypeTraining newTypeTraining);

        /// <summary>
        /// Редактирование данных по типам обучения
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemTypeTraining(DictTypeTraining editTypeTraining);

        /// <summary>
        /// Удаление данных по типам обучения
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemTypeTraining(DictTypeTraining delTypeTraining);

        /// <summary>
        /// Интерфейс нахождения типа обучения по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictTypeTraining GetItemTypeTraining(string tTrain);
    }
}


