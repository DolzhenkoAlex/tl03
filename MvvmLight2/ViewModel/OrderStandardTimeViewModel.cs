using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Приказы по нормам времени
    /// </summary>
    public class OrderStandardTimeViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedOrderStandardTime

        /// <summary>
        /// Выделенный приказ по нормам времени в списке приказов
        /// </summary>
        private OrderStandardTime selectedOrderStandardTime;

        /// <summary>
        /// Выделенный приказ по нормам времени в списке приказов
        /// </summary>
        public OrderStandardTime SelectedOrderStandardTime
        {
            get { return selectedOrderStandardTime; }
            set
            {

                if (value == selectedOrderStandardTime)
                    return;

                selectedOrderStandardTime = value;
                RaisePropertyChanged("SelectedOrderStandardTime");
            }
        }

        #endregion SelectedOrderStandardTime

        #region SelectedStandardTime

        /// <summary>
        /// Выделенная в списке норма времени
        /// </summary>
        private StandartTime selectedStandardTime;

        /// <summary>
        /// Выделенная в списке норма времени
        /// </summary>
        public StandartTime SelectedStandardTime
        {
            get { return selectedStandardTime; }
            set
            {
                if (value == selectedStandardTime)
                    return;
                else
                {
                    selectedStandardTime = value;
                    RaisePropertyChanged("SelectedStandardTime");
                }
            }
        }

        #endregion SelectedStandardTime

        #region Индикатор выполнения длительной операции

        bool isBusyStandartTime = false;

        public bool IsBusyStandartTime
        {
            get { return isBusyStandartTime; }
            set
            {
                if (value == IsBusyStandartTime)
                    return;

                isBusyStandartTime = value;
                RaisePropertyChanged("IsBusyStandartTime");
            }
        }

        #endregion Индикатор выполнения длительной операции

        #region Сообщение при выполнении длительной операции

        string busyMessage;

        public string BusyMessage
        {
            get { return busyMessage; }
            set
            {
                if (value == busyMessage)
                    return;

                busyMessage = value;
                RaisePropertyChanged("BusyMessage");
            }
        }

        #endregion Сообщение при выполнении длительной операции

        #region Properties for binding

        /// <summary>
        /// Коллекция приказов по нормам времени в списке приказов
        /// </summary>
        public ObservableCollection<OrderStandardTime> Orders { get; private set; }

        /// <summary>
        /// Коллекция норм времени
        /// </summary>
        public ObservableCollection<StandartTime> StandartsTime { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ChairsViewModel class.
        /// </summary>
        public OrderStandardTimeViewModel(IServiceOrderStandartTime service)
        {
            AppSettingsReader ar = new AppSettingsReader();
            int idUniver = (int)ar.GetValue("IdUniversity", typeof(int));

            service.GetDataOrders(
               (orders, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Orders = orders;
               }, idUniver);

            StandartsTime = new ObservableCollection<StandartTime>();
            base.DisplayName = "Нормы времени";
        }

        #endregion Constructor

        #region CommandGetStandartTime

        /// <summary>
        /// Команда - Загрузка данных по нормам времени - поле
        /// </summary>
        private ICommand getStandartTime;

        /// <summary>
        /// Команда - Загрузка данных по нормам времени - Свойство
        /// </summary>
        public ICommand GetStandartTime
        {
            get
            {
                return getStandartTime ??
                    (getStandartTime = new RelayCommand(GetStandartTimeExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по нормам времени- метод
        /// </summary>
        private void GetStandartTimeExecute()
        {
            var service = container.Resolve<IServiceOrderStandartTime>();

            StandartsTime.Clear();
            if (SelectedOrderStandardTime != null)
                StandartsTime = service.GetStandartTime(StandartsTime, SelectedOrderStandardTime.Id);
        }

        #endregion CommandGetStandartTime

        #region CommandEditOrder

        /// <summary>
        /// Редактирование данных по приказу о нормах времени - поле
        /// </summary>
        private ICommand editOrder;

        /// <summary>
        /// Редактирование данных по приказу о нормах времени - Свойство
        /// </summary>
        public ICommand EditOrder
        {
            get
            {
                return editOrder ??
                    (editOrder = new RelayCommand(EditOrderExecute, CanOrderExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по приказу о нормах времени - метод 
        /// </summary>
        private void EditOrderExecute()
        {
            AppSettingsReader ar = new AppSettingsReader();
            int idUniver = (int)ar.GetValue("IdUniversity", typeof(int));
            
            var viewModel = container.Resolve<EditOrderStandardTimeViewModel>();

            viewModel.Order = SelectedOrderStandardTime;

            var viewEdit = container.Resolve<EditOrderStandartTimeView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var orderService = container.Resolve<IServiceOrderStandartTime>();
                orderService.EditItemOrder(SelectedOrderStandardTime);

                Orders.Clear();
                Orders = orderService.GetOrder(Orders, idUniver);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по нормам нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanOrderExecute()
        {
            return selectedOrderStandardTime != null;
        }

        #endregion CommandEditOrder

        #region CommandAddOrder

        /// <summary>
        /// Добавление данных по приказу о нормах времени - поле
        /// </summary>
        private ICommand addOrderCommand;

        /// <summary>
        /// Добавление данных по приказу о нормах времени - Свойство
        /// </summary>
        public ICommand AddOrderCommand
        {
            get
            {
                return addOrderCommand ??
                    (addOrderCommand = new RelayCommand(AddOrderExecute));
            }
        }

        /// <summary>
        /// Добавление данных по приказу о нормах времени - метод
        /// </summary>
        private void AddOrderExecute()
        {
            AppSettingsReader ar = new AppSettingsReader();
            int idUniver = (int)ar.GetValue("IdUniversity", typeof(int));

            var viewModel = container.Resolve<EditOrderStandardTimeViewModel>();

            OrderStandardTime newOrder = new OrderStandardTime();
            newOrder.IdUniversity = idUniver;
            newOrder.DataOrder = DateTime.Now;
            newOrder.IdAcademicYear = 1;
            newOrder.StatusDel = true;

            viewModel.Order = newOrder;
            

            var viewEdit = container.Resolve<EditOrderStandartTimeView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceOrderStandartTime>();
                service.AddItemDataOrder(newOrder);

                Orders.Clear();
                Orders = service.GetOrder(Orders, idUniver);
            }
        }

        #endregion CommandAddOrder

        #region CommandRemoveOrder

        /// <summary>
        /// Удаление данных по приказу о нормах времени - поле
        /// </summary>
        private ICommand removeOrder;

        /// <summary>
        /// Удаление данных по приказу о нормах времени - Свойство
        /// </summary>
        public ICommand RemoveOrder
        {
            get
            {
                return removeOrder ??
                    (removeOrder = new RelayCommand(RemoveOrderExecute, CanOrderExecute));
            }
        }

        /// <summary>
        /// Удаление данных по приказу о нормах времени - метод
        /// </summary>
        private void RemoveOrderExecute()
        {
 
            MessageBoxResult result = MessageBox.Show("Удалить данные\n по приказу № " + SelectedOrderStandardTime.NumberOrder,
                    "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceOrderStandartTime>();

                if (StandartsTime.Count > 0)
                {
                    foreach (StandartTime st in StandartsTime)
                        service.RemoveItemStandartTime(st);
                }

                service.RemoveItemOrder(selectedOrderStandardTime);

                Orders.Remove(SelectedOrderStandardTime);
            }
        }

        #endregion CommandRemoveOrder

        #region CommandEditStandartTime

        /// <summary>
        /// Редактирование данных по нормам времени - поле
        /// </summary>
        private ICommand editStandartTime;

        /// <summary>
        /// Редактирование данных по нормам времени - Свойство
        /// </summary>
        public ICommand EditStandartTime
        {
            get
            {
                return editStandartTime ??
                    (editStandartTime = new RelayCommand(EditStandartTimeExecute, CanStandartTimeExecute));
            }
        }

        /// <summary>
        /// Редактирование данных по нормам времени - метод 
        /// </summary>
        private void EditStandartTimeExecute()
        {
            var viewModel = container.Resolve<EditStandartTimeViewModel>();

            viewModel.StandartTime = SelectedStandardTime;
            var viewEdit = container.Resolve<EditStandartTimeView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceOrderStandartTime>();
                service.EditItemStandartTime(SelectedStandardTime);

                StandartsTime.Clear();
                StandartsTime = service.GetStandartTime(StandartsTime, SelectedOrderStandardTime.Id);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по нормам нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanStandartTimeExecute()
        {
            return selectedStandardTime != null;
        }

        #endregion CommandEditStandartTime

        #region CommandAddStandartTime

        /// <summary>
        /// Добавление данных по нормам времени - поле
        /// </summary>
        private ICommand addStandartTime;

        /// <summary>
        /// Добавление данных по нормам времени - Свойство
        /// </summary>
        public ICommand AddStandartTime
        {
            get
            {
                return addStandartTime ??
                    (addStandartTime = new RelayCommand(AddStandartTimeExecute, CanOrderExecute));
            }
        }

        /// <summary>
        /// Добавление данных по нормам времени - метод
        /// </summary>
        private void AddStandartTimeExecute()
        {
            var viewModel = container.Resolve<EditStandartTimeViewModel>();

            StandartTime newStandartTime = new StandartTime();
            newStandartTime.IdOrderStandartTime = SelectedOrderStandardTime.Id;

            viewModel.StandartTime = newStandartTime;

            var viewEdit = container.Resolve<EditStandartTimeView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceOrderStandartTime>();
                service.AddItemStandartTime(newStandartTime);

                StandartsTime.Clear();
                StandartsTime = service.GetStandartTime(StandartsTime, SelectedOrderStandardTime.Id);
            }
        }

        #endregion CommandAddStandartTime

        #region CommandRemoveStandartTime

        /// <summary>
        /// Удаление данных по нормам времени - поле
        /// </summary>
        private ICommand removeStandartTime;

        /// <summary>
        /// Удаление данных по нормам времени - Свойство
        /// </summary>
        public ICommand RemoveStandartTime
        {
            get
            {
                return removeStandartTime ??
                    (removeStandartTime = new RelayCommand(RemoveStandartTimeExecute, CanStandartTimeExecute));
            }
        }

        /// <summary>
        /// Удаление данных по нормам времени - метод
        /// </summary>
        private void RemoveStandartTimeExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по норме времени: \n" + SelectedStandardTime.DictTypeTraining.TypeWork,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceOrderStandartTime>();
                service.RemoveItemStandartTime(SelectedStandardTime);

                StandartsTime.Remove(SelectedStandardTime);
            }
        }

        #endregion CommandRemoveStandartTime

        #region CommandTransferOrder

        /// <summary>
        /// Формирование приказа на следующий учебный год - Поле
        /// </summary>
        private ICommand transferOrder;

        /// <summary>
        /// Формирование приказа на следующий учебный год - Свойство
        /// </summary>
        public ICommand TransferOrder
        {
            get
            {
                return transferOrder ??
                    (transferOrder = new RelayCommand(TransferOrderExecute, CanOrderExecute));
            }
        }

        /// <summary>
        /// Формирование приказа на следующий учебный год - Метод
        /// </summary>
        private void TransferOrderExecute()
        {
            MessageBoxResult result = MessageBox.Show("Формировать приказ на следующий учебный год\nна основе приказа №" + SelectedOrderStandardTime.NumberOrder + "?",
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceOrderStandartTime>();
                AppSettingsReader ar = new AppSettingsReader();
                int idUniver = (int)ar.GetValue("IdUniversity", typeof(int));
                OrderStandardTime newOrder = new OrderStandardTime();

                System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
                // Запуск длительной операции создания приказа и копирования норм времени для приказа
                worker.DoWork += (o, ea) =>
                {
                    newOrder.NumberOrder = selectedOrderStandardTime.NumberOrder;
                    newOrder.IdUniversity = selectedOrderStandardTime.IdUniversity;
                    newOrder.IdAcademicYear = selectedOrderStandardTime.IdAcademicYear + 1;
                    newOrder.StatusDel = true;

                    service.AddItemDataOrder(newOrder);

                    foreach (var standartTime in StandartsTime)
                    {
                        StandartTime newStandartTime = new StandartTime();
                        newStandartTime.IdOrderStandartTime = newOrder.Id;
                        newStandartTime.IdUnit = standartTime.IdUnit;
                        newStandartTime.IdTypeTraining = standartTime.IdTypeTraining;
                        newStandartTime.NormTime = standartTime.NormTime;
                        newStandartTime.TypeOfWork = standartTime.TypeOfWork;
                        newStandartTime.UnitMesurement = standartTime.UnitMesurement;
                        newStandartTime.Notes = standartTime.Notes;

                        service.AddItemStandartTime(newStandartTime);
                    }

                    IsBusyStandartTime = false;
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    Orders.Clear();
                    Orders = service.GetOrder(Orders, idUniver);

                    StandartsTime.Clear();
                    StandartsTime = service.GetStandartTime(StandartsTime, newOrder.Id);
                };
                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusyStandartTime = true;
                BusyMessage = "Формирование норм времени . . .";
                worker.RunWorkerAsync();
            }

        }

        #endregion CommandTransferOrder

    }
}
