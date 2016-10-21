using GalaSoft.MvvmLight.Command;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Класс FixedDisciplineViewModel содержит свойства для связывания  
    /// свойств с представлением FixedDisciplineView
    /// </summary>
    public class FixedDisciplineViewModel : WorkspaceViewModel
    {
        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        ObservableCollection<tlsp_getFixedDisciplines_Result> fixedDisciplines;

        #region  SelectedAcademicYear

        DictAcademicYear selectedAcademicYear;

        public DictAcademicYear SelectedAcademicYear
        {
            get { return selectedAcademicYear; }
            set
            {
                if (value == selectedAcademicYear)
                    return;

                selectedAcademicYear = value;
                RaisePropertyChanged("SelectedAcademicYear");
            }
        }

        #endregion  SelectedAcademicYear

        #region Индикатор выполнения длительной операции

        bool isBusyCurriculum = false;


        public bool IsBusyCurriculum
        {
            get { return isBusyCurriculum; }
            set
            {
                if (value == IsBusyCurriculum)
                    return;

                isBusyCurriculum = value;
                RaisePropertyChanged("IsBusyCurriculum");
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

        #region PlanCurriculum

        /// <summary>
        /// Учебный план направления подготовки - поле
        /// </summary>
        string planCurriculum = string.Empty;

        /// <summary>
        /// Учебный план направления подготовки - свойство
        /// </summary>
        public string PlanCurriculum
        {
            get { return planCurriculum; }
            set
            {
                if (value == planCurriculum)
                    return;
                else
                {
                    planCurriculum = value;
                    RaisePropertyChanged("PlanCurriculum");
                }
            }
        }

        #endregion PlanCurriculum

        #region CodeDiscipline

        /// <summary>
        /// Код дисциплины - поле
        /// </summary>
        string codeDiscipline = string.Empty;

        /// <summary>
        /// Код дисциплины - свойство
        /// </summary>
        public string CodeDiscipline
        {
            get { return codeDiscipline; }
            set
            {
                if (value == codeDiscipline)
                    return;
                else
                {
                    codeDiscipline = value;
                    RaisePropertyChanged("CodeDiscipline");
                }
            }
        }

        #endregion CodeDiscipline

        #region NameDiscipline

        /// <summary>
        /// Наименование дисциплины - поле
        /// </summary>
        string nameDiscipline = string.Empty;

        /// <summary>
        /// Наименование дисциплины - свойство
        /// </summary>
        public string NameDiscipline
        {
            get { return nameDiscipline; }
            set
            {
                if (value == nameDiscipline)
                    return;
                else
                {
                    nameDiscipline = value;
                    RaisePropertyChanged("NameDiscipline");
                }
            }
        }

        #endregion NameDiscipline

        #region Property for binding
        /// <summary>
        /// Коллекция закрепленных дисциплин
        /// </summary>
        public ObservableCollection<tlsp_getFixedDisciplines_Result> FixedDisciplines { get; set; }

        #endregion Property for binding

        #region Constractor
        public FixedDisciplineViewModel(IServiceFixedDiscipline service)
        {
            FixedDisciplines = new ObservableCollection<tlsp_getFixedDisciplines_Result>();
            fixedDisciplines = new ObservableCollection<tlsp_getFixedDisciplines_Result>();
            base.DisplayName = "Закреплнение дисциплин";
        }

        #endregion Constractor

        #region CommandGetFixedDiscipline

        /// <summary>
        /// Команда - Загрузка данных по закрепленным дисциплинам  - поле
        /// </summary>
        private ICommand getFixedDiscipline;

        /// <summary>
        /// Команда - Загрузка данных по закрепленным дисциплинам - Свойство
        /// </summary>
        public ICommand GetFixedDiscipline
        {
            get
            {
                return getFixedDiscipline ??
                    (getFixedDiscipline = new RelayCommand(GetFixedDisciplineExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по закрепленным дисциплинам- метод
        /// </summary>
        private void GetFixedDisciplineExecute()
        {
            var service = container.Resolve<IServiceFixedDiscipline>();

            FixedDisciplines.Clear();

            BackgroundWorker worker = new BackgroundWorker();
            // Запуск длительной операции добавления данных о новом учебном плане в базу данных
            worker.DoWork += (o, ea) =>
            {
                fixedDisciplines = service.GetAllFixedDisciplines(selectedAcademicYear.Id);
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                foreach (var disc in fixedDisciplines)
                FixedDisciplines.Add(disc);

                ClearFindParametr();

                IsBusyCurriculum = false;
            };

            // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
            IsBusyCurriculum = true;
            BusyMessage = "Формирование данных . . .";
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Очистка параметров поиска
        /// </summary>
        private void ClearFindParametr()
        {
            // Очистка параметров поиска/фильтрации
            PlanCurriculum = string.Empty;
            CodeDiscipline = string.Empty;
            NameDiscipline = string.Empty;
        }

        #endregion CommandGetFixedDiscipline

        #region CommandFindDiscipline

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, коду и наименованию дисциплины   - поле
        /// </summary>
        private ICommand findDiscipline;

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, коду и наименованию дисциплины - Свойство
        /// </summary>
        public ICommand FindDiscipline
        {
            get
            {
                return findDiscipline ??
                    (findDiscipline = new RelayCommand(FindfindDisciplineExecute, CanExecuteFind));
            }
        }

        /// <summary>
        /// Признак доступности команды FindDiscipline
        /// </summary>
        /// <returns></returns>

        private bool CanExecuteFind()
        {
            return (codeDiscipline != "") || (planCurriculum != "") || (nameDiscipline != "");
        }

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, коду и наименованию дисциплины - метод
        /// </summary>
        private void FindfindDisciplineExecute()
        {
            // Временная коллекция для поиска/фильтрации записей по дисциплинам кафедры
            List<tlsp_getFixedDisciplines_Result> disciplineListFind = FixedDisciplines.ToList<tlsp_getFixedDisciplines_Result>();

            // Выходная последовательность - результат Linq-запроса
            IEnumerable<tlsp_getFixedDisciplines_Result> query;

            // Запросы для фильтрации данных
            if (planCurriculum != "" && codeDiscipline != "" && nameDiscipline != "")
            {
                // по коду учебного плана, коду и наименованию дисциплины
                query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                      n.CodePlan != null &&  
                                                      n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()) &&
                                                      n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
            }
            else
                if (planCurriculum != "" && codeDiscipline != "" && nameDiscipline == "")
                {
                    // по коду учебного плана, коду дисциплины
                    query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                          n.CodePlan != null &&   
                                                          n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()));
                }
                else
                    if (planCurriculum != "" && codeDiscipline == "" && nameDiscipline != "")
                    {
                        // по коду учебного плана и наименованию дисциплины
                        query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                              n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
                    }
                    else
                        if (planCurriculum == "" && codeDiscipline != "" && nameDiscipline != "")
                        {
                            // по  коду и наименованию дисциплины
                            query = disciplineListFind.Where(n => n.CodePlan != null &&  
                                                                  n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()) &&
                                                                  n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
                        }
                        else
                            if (planCurriculum != "" && codeDiscipline == "" && nameDiscipline == "")
                            {
                                // по коду учебного плана
                                query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()));
                            }
                            else
                                if (planCurriculum == "" && codeDiscipline != "" && nameDiscipline == "")
                                {
                                    // по коду  дисциплины
                                    //query = disciplineListFind.Where(n => n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()));
                                    query = disciplineListFind.Where(n => n.CodePlan != null && 
                                                                          n.CodePlan != null && n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()));
                                }
                                else
                                    // по наименованию дисциплины
                                    query = disciplineListFind.Where(n => n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));


            /// Анализ результатов поиска/фильтрации
            if (query.Count<tlsp_getFixedDisciplines_Result>() > 0)
            {
                // Обновление коллекции дисциплин кафедры
                FixedDisciplines.Clear();
                foreach (var sp in query)
                    FixedDisciplines.Add(sp);
            }
            else
            {
                MessageBox.Show("Дисциплины кафедры\nУчебный план: " + planCurriculum +
                    "\nКод дисциплины:    " + codeDiscipline + "\nДисциплина:    " + nameDiscipline + "\n НЕ НАЙДЕНЫ!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion CommandFindDiscipline

        #region RefreshDiscipline

        /// <summary>
        /// Команда - Обновление данных по дисциплинам кафедры - поле
        /// </summary>
        private ICommand refreshDiscipline;

        /// <summary>
        /// Команда - Обновление данных по дисциплинам кафедры - Свойство
        /// </summary>
        public ICommand RefreshDiscipline
        {
            get
            {
                return refreshDiscipline ??
                    (refreshDiscipline = new RelayCommand(RefreshDisciplineExecute));
            }

        }

        private void RefreshDisciplineExecute()
        {
            FixedDisciplines.Clear();

            foreach (var disc in fixedDisciplines)
                FixedDisciplines.Add(disc);

            ClearFindParametr();
        }

        #endregion RefreshDiscipline
    }
}
