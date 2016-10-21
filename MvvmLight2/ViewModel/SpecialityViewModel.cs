using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;
using System.Collections.Generic;

namespace MvvmLight2.ViewModel
{
   /// <summary>
   /// Справочник направлений подготовки университета
   /// </summary>
    public class SpecialityViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedSpeciality 

        /// <summary>
        /// Поле - выделенная в списке специальность
        /// </summary>
        private DictSpeciality selectedSpeciality;

        /// <summary>
        /// Свойство - выделенная в списке  специальность
        /// </summary>
        public DictSpeciality SelectedSpeciality
        {
            get { return selectedSpeciality; }
            set
            {
                if (value == selectedSpeciality)
                    return;
                else
                {
                    selectedSpeciality = value;
                    RaisePropertyChanged("SelectedSpeciality");
                }
            }
        }

        #endregion SelectedSpeciality

        #region SelectedQualification

       /// <summary>
       /// Выделенная в списке квалификация - поле
       /// </summary>
       DictQualification selectedQualification;
       /// <summary>
       /// Выделенная в списке квалификация - свойство
       /// </summary>
        public DictQualification SelectedQualification
        {
            get { return selectedQualification; }
            set 
            {
                if (value == selectedQualification)
                    return;
                else
                {
                    selectedQualification = value;
                    RaisePropertyChanged("SelectedQualification");
                }
            }
        }

        #endregion SelectedQualification

        #region SelectedIndexQualification

        /// <summary>
        /// Выделенный индекс в списке квалификация - поле
        /// </summary>
        int selectedIndexQualification = 0;
        /// <summary>
        /// Выделенный индекс в списке квалификация - свойство
        /// </summary>
        public int SelectedIndexQualification
        {
            get { return selectedIndexQualification; }
            set
            {
                if (value == selectedIndexQualification)
                    return;
                else
                {
                    selectedIndexQualification = value;
                    RaisePropertyChanged("SelectedIndexQualification");
                }
            }
        }

        #endregion SelectedIndexQualification

        #region CodeSpeciality

        /// <summary>
       /// Код направления подготовки - поле
       /// </summary>
       string codeSpeciality = string.Empty;

       /// <summary>
       /// Код направления подготовки - свойство
       /// </summary>
       public string CodeSpeciality
       {
           get { return codeSpeciality; }
           set
           {
               if (value == codeSpeciality)
                   return;
               else
               {
                   codeSpeciality = value;
                   RaisePropertyChanged("CodeSpeciality");
               }
           }
       }

        #endregion CodeSpeciality

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по специальностям
        /// </summary>
        public ObservableCollection<DictSpeciality> SpecialityList { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SpecialityViewModel class.
        /// </summary>
        public SpecialityViewModel(IServiceSpeciality service)
        {
            service.GetDataSpeciality(
               (specialitys, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   SpecialityList = specialitys;
               });

            base.DisplayName = "Направления подготовки";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по направлению - поле
        /// </summary>
        private ICommand getSpeciality;

        /// <summary>
        /// Команда - Загрузка данных по направлению - Свойство
        /// </summary>
        public ICommand GetSpeciality
        {
            get
            {
                return getSpeciality ??
                    (getSpeciality = new RelayCommand(GetSpecialityExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по направлению - метод
        /// </summary>
        private void GetSpecialityExecute()
        {
            var service = container.Resolve<IServiceSpeciality>();
            SpecialityList.Clear();
            SpecialityList = service.GetSpeciality(SpecialityList);

            CodeSpeciality = string.Empty;
            SelectedIndexQualification = 0;
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по направлению - поле
        /// </summary>
        private ICommand editSpeciality;

        /// <summary>
        /// Редактирование данных по направлению - Свойство
        /// </summary>
        public ICommand EditSpeciality
        {
            get
            {
                return editSpeciality ??
                    (editSpeciality = new RelayCommand(EditSpecialityExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по направлению - метод 
        /// </summary>
        private void EditSpecialityExecute()
        {
            var viewModel = container.Resolve<EditSpecialityViewModel>();

            viewModel.DictSpeciality = SelectedSpeciality;

            var viewEdit = container.Resolve<EditSpecialityView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceSpeciality>();
                service.EditItemSpeciality(SelectedSpeciality);

                SpecialityList.Clear();
                SpecialityList = service.GetSpeciality(SpecialityList);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по направлению
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return SelectedSpeciality != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по направлению - поле
        /// </summary>
        private ICommand addSpeciality;

        /// <summary>
        /// Добавление данных по направлению - Свойство
        /// </summary>
        public ICommand AddSpeciality
        {
            get
            {
                return addSpeciality ??
                    (addSpeciality = new RelayCommand(AddSpecialityExecute));
            }
        }

        /// <summary>
        /// Добавление данных по направлению - метод
        /// </summary>
        private void AddSpecialityExecute()
        {
            var viewModel = container.Resolve<EditSpecialityViewModel>();

            DictSpeciality newSpeciality = new DictSpeciality();
            newSpeciality.StatusDel = true;
            viewModel.DictSpeciality = newSpeciality;

            var viewEdit = container.Resolve<EditSpecialityView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceSpeciality>();
                service.AddItemDataSpeciality(newSpeciality);

                SpecialityList.Clear();
                SpecialityList = service.GetSpeciality(SpecialityList);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по направлению - поле
        /// </summary>
        private ICommand removeSpeciality;

        /// <summary>
        /// Удаление данных по направлению - Свойство
        /// </summary>
        public ICommand RemoveSpeciality
        {
            get
            {
                return removeSpeciality ??
                    (removeSpeciality = new RelayCommand(RemoveSpecialityExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по направлению - метод
        /// </summary>
        private void RemoveSpecialityExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по направлению: \n" + SelectedSpeciality.CodeSpeciality+"  "+ SelectedSpeciality.NameSpeciality + 
                "\n Квалификация: " + SelectedSpeciality.DictQualification.NameQualification,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceSpeciality>();
                service.RemoveItemSpeciality(selectedSpeciality);

                SpecialityList.Remove(selectedSpeciality);
            }
        }


        #endregion CommandRemove

        #region CommandDelete

        private ICommand deleteSpeciality;

        public ICommand DeleteSpeciality
        {
            get 
            {
                return deleteSpeciality ??
                    (deleteSpeciality = new RelayCommand(RemoveSpecialityExecute, CanExecute));
            }
        }

        #endregion CommandDelete

        #region RefreshSpeciality

        /// <summary>
        /// Обновление данных по направлениям подготовки - поле
       /// </summary>
       private ICommand refreshSpeciality;

       /// <summary>
       /// Обновление данных по направлениям подготовки - Свойство
       /// </summary>
 
       public ICommand RefreshSpeciality
        {
            get 
            { 
                return refreshSpeciality ??
                    (refreshSpeciality = new RelayCommand(GetSpecialityExecute)); 
            }
        }

        #endregion RefreshSpeciality

        #region CommandFind

        /// <summary>
        /// Поиск направления подготовки по коду и квалификации  - поле
        /// </summary>
        private ICommand findSpeciality;

        /// <summary>
        /// Поиск направления подготовки по коду и квалификации - Свойство
        /// </summary>
        public ICommand FindSpeciality
        {
            get
            {
                return findSpeciality ??
                    (findSpeciality = new RelayCommand(FindSpecialityExecute, CanExecuteFind));
            }
        }

       /// <summary>
        /// Признак доступности команды FindSpeciality
       /// </summary>
       /// <returns></returns>
 
       private bool CanExecuteFind()
        {
            return (codeSpeciality != "") || (selectedQualification.NameQualification != "не задано");
        }

        /// <summary>
        /// Поиск направлений подготовки/фильтрация по коду и квалификации - метод
        /// </summary>
        private void FindSpecialityExecute()
        {

            // Временная коллекция для поиска/фильтрации записей по направлениям подготовки
            List<DictSpeciality> specialityListFind = SpecialityList.ToList<DictSpeciality>();

            // Выходная последовательность - результат Linq-запроса
            IEnumerable<DictSpeciality> query;

            // Запросы для фильтрации данных
            if (codeSpeciality != ""  && selectedQualification.NameQualification != "не задано")
            {
                // по коду направления и квалификации
                query = specialityListFind.Where(n => n.CodeSpeciality.Contains(codeSpeciality) && n.IdQualification == selectedQualification.Id);
            }
            else
                if (codeSpeciality != ""   && selectedQualification.NameQualification == "не задано")
                {
                    /// по коду направления 
                    query = specialityListFind.Where(n => n.CodeSpeciality.Contains(codeSpeciality));
                }
                else
                {
                    // по квалификации
                    query = specialityListFind.Where(n => n.IdQualification == selectedQualification.Id);
                }
            
            /// Анализ результатов фильтрации
            if (query.Count<DictSpeciality>() > 0)
            {
                // Обновление коллекции направлений подготовки
                SpecialityList.Clear();
                foreach (var sp in query)
                    SpecialityList.Add(sp);
            }
            else
            {
                MessageBox.Show("Направление подготовки\nКод направления: " + codeSpeciality +
                    "\nКвалификация:    " + selectedQualification.NameQualification + "\n НЕ НАЙДЕНО!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion CommandFind

    }
}
