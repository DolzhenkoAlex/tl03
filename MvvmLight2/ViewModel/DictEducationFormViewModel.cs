using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;

namespace MvvmLight2.ViewModel
{
    public class DictEducationFormViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedEducationForm

        /// <summary>
        /// Поле - выделенная в списке форма обучения
        /// </summary>
        private DictEducationForm selectedEducationForm;

        /// <summary>
        /// Свойство - выделенная в списке  форма обучения
        /// </summary>
        public DictEducationForm SelectedEducationForm
        {
            get { return selectedEducationForm; }
            set
            {
                if (value == selectedEducationForm)
                    return;
                else
                {
                    selectedEducationForm = value;
                    RaisePropertyChanged("SelectedEducationForm");
                }
            }
        }

        #endregion SelectedEducationForm

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по формам обучения
        /// </summary>
        public ObservableCollection<DictEducationForm> EducationForms { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SpecialityViewModel class.
        /// </summary>
        public DictEducationFormViewModel(IServiceEducationForm service)
        {
            service.GetDataEducationForm(
               (educationform, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   EducationForms = educationform;
               });

            base.DisplayName = "Форма обучения";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по формам обучения - поле
        /// </summary>
        private ICommand getEducationForm;

        /// <summary>
        /// Команда - Загрузка данных по формам обучения - Свойство
        /// </summary>
        public ICommand GetEducationForm
        {
            get
            {
                return getEducationForm ??
                    (getEducationForm = new RelayCommand(GetEducationFormExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по формам обучения - метод
        /// </summary>
        private void GetEducationFormExecute()
        {
            var service = container.Resolve<IServiceEducationForm>();
            EducationForms.Clear();
            EducationForms = service.GetEducationForm(EducationForms);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по формам обучения - поле
        /// </summary>
        private ICommand editEducationForm;

        /// <summary>
        /// Редактирование данных по формам обучения - Свойство
        /// </summary>
        public ICommand EditEducationForm
        {
            get
            {
                return editEducationForm ??
                    (editEducationForm = new RelayCommand(EditEducationFormExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по формам обучения - метод 
        /// </summary>
        private void EditEducationFormExecute()
        {
            var viewModel = container.Resolve<EditEducationFormViewModel>();

            viewModel.DictEducationForm = SelectedEducationForm;

            var viewEdit = container.Resolve<EditEducationFormView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var formService = container.Resolve<IServiceEducationForm>();
                formService.EditItemEducationForm(SelectedEducationForm);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveEducationFormCommand -  Редактирование / Удаление данных по формам обучения
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedEducationForm != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по формам обучения - поле
        /// </summary>
        private ICommand addEducationForm;

        /// <summary>
        /// Добавление данных по формам обучения - Свойство
        /// </summary>
        public ICommand AddEducationForm
        {
            get
            {
                return addEducationForm ??
                    (addEducationForm = new RelayCommand(AddEducationFormExecute));
            }
        }

        /// <summary>
        /// Добавление данных по формам обучения - метод
        /// </summary>
        private void AddEducationFormExecute()
        {
            var viewModel = container.Resolve<EditEducationFormViewModel>();

            DictEducationForm newEducationForm = new DictEducationForm();
            newEducationForm.StatusDel = true;
            viewModel.DictEducationForm = newEducationForm;

            var viewEdit = container.Resolve<EditEducationFormView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceEducationForm>();
                service.AddItemDataEducationForm(newEducationForm);

                EducationForms.Clear();
                EducationForms = service.GetEducationForm(EducationForms);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по формам обучения - поле
        /// </summary>
        private ICommand removeEducationForm;

        /// <summary>
        /// Удаление данных по формам обучения - Свойство
        /// </summary>
        public ICommand RemoveEducationForm
        {
            get
            {
                return removeEducationForm ??
                    (removeEducationForm = new RelayCommand(RemoveEducationFormExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по формам обучения - метод
        /// </summary>
        private void RemoveEducationFormExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по форме обучения: \n" + SelectedEducationForm.FormEducation,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceEducationForm>();
                service.RemoveItemEducationForm(selectedEducationForm);

                EducationForms.Remove(selectedEducationForm);
            }
        }


        #endregion CommandEdit

    }
}