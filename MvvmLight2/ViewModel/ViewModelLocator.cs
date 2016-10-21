/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MvvmLight2.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmLight2.ServiceData;


namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        #region Field

        private static MainViewModel main;
        private static UniversitiesViewModel universityVM;
        private static FacultiesViewModel facultyVM;
        private static ChairsViewModel chairVM;
        private static SpecialityViewModel specialityVM;
        private static TeachersViewModel teacherVM;
        private static GroupViewModel groupVM;
        private static OrderStandardTimeViewModel orderStandardTimeVM;
        private static CurriculumFromDBViewMode curriculumFromDBVM;
        private static CurriculumFromXmlViewMode curriculumFromXmlVM;
        private static CurriculumFromExlViewMode curriculumFromExlVM;
        private static CurriculumViewMode curriculumVM;
        private static DisciplineChairViewModel disciplineChairVM;
        private static FixedDisciplineViewModel fixedDisciplineVM;
        private static DictAcademicYearViewModel academicYearVM;
        private static DictEducationFormViewModel educationFormVM;
        private static DictPostViewModel dictPostVM;
        private static DictQualificationViewModel dictQualificationVM;
        private static DictUnitViewModel dictUnitVM;
        private static DictTypeEmploymentViewModel dictTypeEmploymentVM;
        private static DictTypeTrainingViewModel dictTypeTrainingVM;
        private static LoadChairViewModel loadChairVM;
        private static LoadTeacherViewModel loadTeacherVM;
        private static ReportChairDisciplinesViewModel reportChairDisciplinesVM;
        private static ReportChairLoadViewModel reportChairLoadVM;
        private static ReportTeacherLoadViewModel reportTeacherLoadVM;
        private static ReportSummaryCurriculumWorkViewModel reportSummaryCurriculumWorkVM;
        private static ReportFixedDisciplinesViewModel reportFixedDisciplinesVM;
        private static ReportChairDisciplinesFullTimeViewModel reportChairDisciplinesFullTimeVM;
        private static ReportChairDisciplinesPartTimeViewModel reportChairDisciplinesPartTimeVM;
        private static ReportChairLoadFullTimeViewModel reportChairLoadFullTimeVM;
        private static ReportChairLoadPartTimeViewModel reportChairLoadPartTimeVM;
        private static ReportGroupViewModel reportGroupVM;

        #endregion Field

        #region Constractor

        public ViewModelLocator()
        {
            CreateMain();
            //CreateUniversityVM();
            CreateFacultyVM();
            CreateChairVM();
            CreateSpecialityVM();
            CreateAcademicYearVM();
            CreateEducationFormVM();
            CreatePostVM();
            CreateQualificationVM();
            CreateUnitVM();
            CreateTypeEmploymentVM();
            CreateTypeTrainingVM();
            CreateTeacherVM();
            CreateGroupVM();
            CreateOrderStandardTime();
            CreateCurriculum();
            CreateCurriculumFromDB();
            CreateCurriculumFromXml();
            CreateCurriculumFromExl();
            CreateDisciplineChair();
            CreateLoadChair();
            CreateLoadTeacherVM();
            CreateReportChairDisciplinesVM();
            CreateReportChairLoadVM();
            CreateReportTeacherLoadVM();
            CreateReportSummaryCurriculumWorkVM();
            CreateReportFixedDisciplinesVM();
            CreateFixedDisciplineVM();
            CreateReportChairDisciplinesFullTimeVM();
            CreateReportChairDisciplinesPartTimeVM();
            CreateReportChairLoadFullTimeVM();
            CreateReportChairLoadPartTimeVM();
            CreateReportGroupVM();
        }

        #endregion Constractor

        #region Static Properteis

        public static MainViewModel MainStatic
        {
            get
            {
                if (main == null)
                    CreateMain();

                return main;

            }
        }

        public static UniversitiesViewModel UniversityVMStatic
        {
            get
            {
                if (universityVM == null)
                    CreateUniversityVM();
                return universityVM;
            }
        }

        public static FacultiesViewModel FacultyVMStatic
        {
            get
            {
                if (facultyVM == null)
                    CreateFacultyVM();
                return facultyVM;

            }
        }

        public static ChairsViewModel ChairVMStatic
        {
            get
            {
                if (chairVM == null)
                    CreateChairVM();
                return chairVM;
            }
        }

        public static SpecialityViewModel SpecialityVMStatic
        {
            get
            {
                if (specialityVM == null)
                    CreateSpecialityVM();
                return specialityVM;
            }
        }

        public static DictAcademicYearViewModel AcademicYearVMStatic
        {
            get
            {
                if (academicYearVM == null)
                    CreateAcademicYearVM();
                return academicYearVM;
            }
        }

        public static DictEducationFormViewModel EducationFormVMStatic
        {
            get
            {
                if (educationFormVM == null)
                    CreateEducationFormVM();
                return educationFormVM;
            }
        }

        public static DictPostViewModel PostVMStatic
        {
            get
            {
                if (dictPostVM == null)
                    CreatePostVM();
                return dictPostVM;
            }
        }

        public static DictQualificationViewModel QualificationVMStatic
        {
            get
            {
                if (dictQualificationVM == null)
                    CreateQualificationVM();
                return dictQualificationVM;
            }
        }

        public static DictUnitViewModel UnitVMStatic
        {
            get
            {
                if (dictUnitVM == null)
                    CreateUnitVM();
                return dictUnitVM;
            }
        }

        public static DictTypeEmploymentViewModel TypeEmploymentVMStatic
        {
            get
            {
                if (dictTypeEmploymentVM == null)
                    CreateTypeEmploymentVM();
                return dictTypeEmploymentVM;
            }
        }

        public static DictTypeTrainingViewModel TypeTrainingVMStatic
        {
            get
            {
                if (dictTypeTrainingVM == null)
                    CreateTypeTrainingVM();
                return dictTypeTrainingVM;
            }
        }

        public static TeachersViewModel TeacherVMStatic
        {
            get
            {
                if (teacherVM == null)
                    CreateTeacherVM();
                return teacherVM;
            }
        }

        public static GroupViewModel GroupVMStatic
        {
            get
            {
                if (groupVM == null)
                    CreateGroupVM();
                return groupVM;
            }
        }

        public static OrderStandardTimeViewModel OrderStandardTimeVMStatic
        {
            get
            {
                if (orderStandardTimeVM == null)
                    CreateOrderStandardTime();

                return orderStandardTimeVM;
            }
        }

        public static CurriculumViewMode CurriculumVMStatic
        {
            get
            {
                if (curriculumVM == null)
                    CreateCurriculum();
                return curriculumVM;
            }
        }

        public static CurriculumFromDBViewMode CurriculumFromDBVMStatic
        {
            get
            {
                if (curriculumFromDBVM == null)
                    CreateCurriculumFromDB();
                return curriculumFromDBVM;
            }
        }

        public static CurriculumFromXmlViewMode CurriculumFromXmlVMStatic
        {
            get
            {
                if (curriculumFromXmlVM == null)
                    CreateCurriculumFromXml();
                return curriculumFromXmlVM;
            }
        }

        public static CurriculumFromExlViewMode CurriculumFromExlVMStatic
        {
            get
            {
                if (curriculumFromExlVM == null)
                    CreateCurriculumFromExl();
                return curriculumFromExlVM;
            }
        }

        public static DisciplineChairViewModel DisciplineChairVMStatic
        {
            get
            {
                if (disciplineChairVM == null)
                    CreateDisciplineChair();

                return disciplineChairVM;
            }
        }

        public static FixedDisciplineViewModel FixedDisciplineVMStatic
        {
            get
            {
                if (fixedDisciplineVM == null)
                    CreateFixedDisciplineVM();
                return fixedDisciplineVM;
            }
        }

        public static LoadChairViewModel LoadChairVMStatic
        {
            get
            {
                if (loadChairVM == null)
                    CreateLoadChair();

                return loadChairVM;
            }
        }

        public static LoadTeacherViewModel LoadTeacherVMStatic
        {
            get
            {
                if (loadTeacherVM == null)
                    CreateLoadTeacherVM();

                return loadTeacherVM;
            }
        }


        public static ReportChairDisciplinesViewModel ReportChairDisciplinesVMStatic
        {
            get
            {
                if (reportChairDisciplinesVM == null)
                    CreateReportChairDisciplinesVM();
                return reportChairDisciplinesVM;
            }
        }

        public static ReportChairDisciplinesFullTimeViewModel ReportChairDisciplinesFullTimeVMStatic
        {
            get
            {
                if (reportChairDisciplinesFullTimeVM == null)
                    CreateReportChairDisciplinesFullTimeVM();
                return reportChairDisciplinesFullTimeVM;
            }
        }

        public static ReportChairDisciplinesPartTimeViewModel ReportChairDisciplinesPartTimeVMStatic
        {
            get
            {
                if (reportChairDisciplinesPartTimeVM == null)
                    CreateReportChairDisciplinesPartTimeVM();
                return reportChairDisciplinesPartTimeVM;
            }
        }




        public static ReportChairLoadViewModel ReportChairLoadVMStatic
        {
            get
            {
                if (reportChairLoadVM == null)
                    CreateReportChairLoadVM();
                return reportChairLoadVM;
            }
        }

        public static ReportChairLoadFullTimeViewModel ReportChairLoadFullTimeVMStatic
        {
            get
            {
                if (reportChairLoadFullTimeVM == null)
                    CreateReportChairLoadFullTimeVM();
                return reportChairLoadFullTimeVM;
            }
        }

        public static ReportChairLoadPartTimeViewModel ReportChairLoadPartTimeVMStatic
        {
            get
            {
                if (reportChairLoadPartTimeVM == null)
                    CreateReportChairLoadPartTimeVM();
                return reportChairLoadPartTimeVM;
            }
        }

        public static ReportTeacherLoadViewModel ReportTeacherLoadVMStatic
        {
            get
            {
                if (reportTeacherLoadVM == null)
                    CreateReportTeacherLoadVM();
                return reportTeacherLoadVM;
            }
        }

        public static ReportSummaryCurriculumWorkViewModel ReportSummaryCurriculumWorkVMStatic
        {
            get
            {
                if (reportSummaryCurriculumWorkVM == null)
                    CreateReportSummaryCurriculumWorkVM();
                return reportSummaryCurriculumWorkVM;
            }
        }

        public static ReportFixedDisciplinesViewModel ReportFixedDisciplinesVMStatic
        {
            get
            {
                if (reportFixedDisciplinesVM == null)
                    CreateReportFixedDisciplinesVM();
                return reportFixedDisciplinesVM;
            }
        }

        public static ReportGroupViewModel ReportGroupVMStatic
        {
            get 
            {
                if (reportGroupVM == null)
                    CreateReportGroupVM();
                return reportGroupVM;
            }
        }

        #endregion Static Properteis

        #region Properties for bindings

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Gets the UniversityVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UniversitiesViewModel UniversityVM
        {
            get
            {
                return UniversityVMStatic;
            }
        }


        /// <summary>
        /// Gets the FacultyVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FacultiesViewModel FacultyVM
        {
            get
            {
                return FacultyVMStatic;
            }
        }

        /// <summary>
        /// Gets the ChairsrVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ChairsViewModel ChairVM
        {
            get
            {
                return ChairVMStatic;
            }
        }

        /// <summary>
        /// Gets the SpecialityVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SpecialityViewModel SpecialityVM
        {
            get
            {
                return SpecialityVMStatic;
            }
        }

        /// <summary>
        /// Gets the AcademicYearVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DictAcademicYearViewModel AcademicYearVM
        {
            get
            {
                return AcademicYearVMStatic;
            }
        }


        /// <summary>
        /// Gets the EducationFormVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DictEducationFormViewModel EducationFormVM
        {
            get
            {
                return EducationFormVMStatic;
            }
        }

        /// <summary>
        /// Gets the PostVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DictPostViewModel PostVM
        {
            get
            {
                return PostVMStatic;
            }
        }

        /// <summary>
        /// Gets the QualificationVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DictQualificationViewModel QualificationVM
        {
            get
            {
                return QualificationVMStatic;
            }
        }

        /// <summary>
        /// Gets the UnitVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DictUnitViewModel UnitVM
        {
            get
            {
                return UnitVMStatic;
            }
        }

        /// <summary>
        /// Gets the TypeEmploymentVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DictTypeEmploymentViewModel TypeEmploymentVM
        {
            get
            {
                return TypeEmploymentVMStatic;
            }
        }

        /// <summary>
        /// Gets the TypeTrainingVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DictTypeTrainingViewModel TypeTrainingVM
        {
            get
            {
                return TypeTrainingVMStatic;
            }
        }

        /// <summary>
        /// Gets the TeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public TeachersViewModel TeacherVM
        {
            get
            {
                return TeacherVMStatic;
            }
        }


        /// <summary>
        /// Gets the GroupVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public GroupViewModel GroupVM
        {
            get
            {
                return GroupVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public OrderStandardTimeViewModel OrderStandardTimeVM
        {
            get
            {
                return OrderStandardTimeVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CurriculumViewMode CurriculumVM
        {
            get
            {
                return CurriculumVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CurriculumFromDBViewMode CurriculumFromDBVM
        {
            get
            {
                return CurriculumFromDBVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CurriculumFromXmlViewMode CurriculumFromXmlVM
        {
            get
            {
                return CurriculumFromXmlVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CurriculumFromExlViewMode CurriculumFromExlVM
        {
            get
            {
                return CurriculumFromExlVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DisciplineChairViewModel DisciplineChairVM
        {
            get
            {
                return DisciplineChairVMStatic;
            }
        }


        /// <summary>
        /// Gets the FixedDisciplineVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FixedDisciplineViewModel FixedDisciplineVM
        {
            get
            {
                return FixedDisciplineVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LoadChairViewModel LoadChairVM
        {
            get
            {
                return LoadChairVMStatic;
            }
        }

        /// <summary>
        /// Gets the LoadTeacherVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public LoadTeacherViewModel LoadTeacherVM
        {
            get
            {
                return LoadTeacherVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportChairDisciplinesVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportChairDisciplinesViewModel ReportChairDisciplinesVM
        {
            get
            {
                return ReportChairDisciplinesVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportChairDisciplinesFullTimeVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportChairDisciplinesFullTimeViewModel ReportChairDisciplinesFullTimeVM
        {
            get
            {
                return ReportChairDisciplinesFullTimeVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportChairDisciplinesPartTimeVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportChairDisciplinesPartTimeViewModel ReportChairDisciplinesPartTimeVM
        {
            get
            {
                return reportChairDisciplinesPartTimeVM;
            }
        }


        /// <summary>
        /// Gets the ReportChairDisciplinesVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportChairLoadViewModel ReportChairLoadVM
        {
            get
            {
                return ReportChairLoadVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportChairLoadFullTimeVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportChairLoadFullTimeViewModel ReportChairLoadFullTimeVM
        {
            get
            {
                return ReportChairLoadFullTimeVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportChairLoadPartTimeVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportChairLoadPartTimeViewModel ReportChairLoadPartTimeVM
        {
            get
            {
                return ReportChairLoadPartTimeVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportChairDisciplinesVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportTeacherLoadViewModel ReportTeacherLoadVM
        {
            get
            {
                return ReportTeacherLoadVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportManpoweCurriculumVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportSummaryCurriculumWorkViewModel ReportSummaryCurriculumWorkVM
        {
            get
            {
                return ReportSummaryCurriculumWorkVMStatic;
            }
        }

        /// <summary>
        /// Gets the ReportManpoweCurriculumVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportFixedDisciplinesViewModel ReportFixedDisciplinesVM
        {
            get
            {
                return ReportFixedDisciplinesVMStatic;
            }
        }

        // <summary>
        /// Gets the ReportGroupVM property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ReportGroupViewModel ReportGroupVM
        {
            get
            {
                return ReportGroupVMStatic;
            }
        }


        #endregion Properties for bindings

        #region Create Properties for bindings

        private static void CreateMain()
        {
            if (main == null)
                main = new MainViewModel();
        }

        private static void CreateUniversityVM()
        {
            if (universityVM == null)
            {
                universityVM = new UniversitiesViewModel(new ServiceUniversity());
            }
        }

        private static void CreateFacultyVM()
        {
            if (facultyVM == null)
            {
                facultyVM = new FacultiesViewModel(new ServiceFaculty());
            }
        }

        private static void CreateChairVM()
        {
            if (chairVM == null)
            {
                chairVM = new ChairsViewModel(new ServiceChair());
            }
        }

        private static void CreateSpecialityVM()
        {
            if (specialityVM == null)
            {
                specialityVM = new SpecialityViewModel(new ServiceSpeciality());
            }
        }

        private static void CreateAcademicYearVM()
        {
            if (academicYearVM == null)
            {
                academicYearVM = new DictAcademicYearViewModel(new ServiceAcademicYear());
            }
        }

        private static void CreateEducationFormVM()
        {
            if (educationFormVM == null)
            {
                educationFormVM = new DictEducationFormViewModel(new ServiceEducationForm());
            }
        }

        private static void CreatePostVM()
        {
            if (dictPostVM == null)
            {
                dictPostVM = new DictPostViewModel(new ServicePost());
            }
        }

        private static void CreateQualificationVM()
        {
            if (dictQualificationVM == null)
            {
                dictQualificationVM = new DictQualificationViewModel(new ServiceQualification());
            }
        }

        private static void CreateUnitVM()
        {
            if (dictUnitVM == null)
            {
                dictUnitVM = new DictUnitViewModel(new ServiceUnit());
            }
        }

        private static void CreateTypeEmploymentVM()
        {
            if (dictTypeEmploymentVM == null)
            {
                dictTypeEmploymentVM = new DictTypeEmploymentViewModel(new ServiceTypeEmployment());
            }
        }

        private static void CreateTypeTrainingVM()
        {
            if (dictTypeTrainingVM == null)
            {
                dictTypeTrainingVM = new DictTypeTrainingViewModel(new ServiceTypeTraining());
            }
        }

        private static void CreateTeacherVM()
        {
            if (teacherVM == null)
            {
                teacherVM = new TeachersViewModel(new ServiceTeaher());
            }
        }

        private static void CreateGroupVM()
        {
            if (groupVM == null)
                groupVM = new GroupViewModel(new ServiceGroup());
        }

        private static void CreateOrderStandardTime()
        {
            if (orderStandardTimeVM == null)
                orderStandardTimeVM = new OrderStandardTimeViewModel(new ServiceOrderStandartTime());
        }

        private static void CreateCurriculum()
        {
            if (curriculumVM == null)
                curriculumVM = new CurriculumViewMode(new ServiceCurriculum());
        }

        private static void CreateCurriculumFromDB()
        {
            if (curriculumFromDBVM == null)
                curriculumFromDBVM = new CurriculumFromDBViewMode(new ServiceCurriculum());
        }

        private static void CreateCurriculumFromXml()
        {
            if (curriculumFromXmlVM == null)
                curriculumFromXmlVM = new CurriculumFromXmlViewMode(new ServiceCurriculum());

        }

        private static void CreateCurriculumFromExl()
        {
            if (curriculumFromExlVM == null)
                curriculumFromExlVM = new CurriculumFromExlViewMode(new ServiceCurriculum());

        }

        private static void CreateDisciplineChair()
        {
            if (disciplineChairVM == null)
                disciplineChairVM = new DisciplineChairViewModel(new ServiceDisciplineChair());
        }

        private static void CreateFixedDisciplineVM()
        {
            if (fixedDisciplineVM == null)
                fixedDisciplineVM = new FixedDisciplineViewModel(new ServiceFixedDiscipline());
        }

        private static void CreateLoadChair()
        {
            if (loadChairVM == null)
                loadChairVM = new LoadChairViewModel(new ServiceLoadChair());
        }

        private static void CreateLoadTeacherVM()
        {
            if (loadTeacherVM == null)
            {
                loadTeacherVM = new LoadTeacherViewModel(new ServiceLoadTeaher());
            }
        }

        private static void CreateReportChairDisciplinesVM()
        {
            if (reportChairDisciplinesVM == null)
                reportChairDisciplinesVM = new ReportChairDisciplinesViewModel();
        }

        private static void CreateReportChairDisciplinesFullTimeVM()
        {
            if (reportChairDisciplinesFullTimeVM == null)
                reportChairDisciplinesFullTimeVM = new ReportChairDisciplinesFullTimeViewModel(new ServiceDisciplineChair());
        }

        private static void CreateReportChairDisciplinesPartTimeVM()
        {
            if (reportChairDisciplinesPartTimeVM == null)
                reportChairDisciplinesPartTimeVM = new ReportChairDisciplinesPartTimeViewModel();
        }


        private static void CreateReportChairLoadVM()
        {
            if (reportChairLoadVM == null)
                reportChairLoadVM = new ReportChairLoadViewModel();
        }

        private static void CreateReportChairLoadFullTimeVM()
        {
            if (reportChairLoadFullTimeVM == null)
                reportChairLoadFullTimeVM = new ReportChairLoadFullTimeViewModel();
        }

        private static void CreateReportChairLoadPartTimeVM()
        {
            if (reportChairLoadPartTimeVM == null)
                reportChairLoadPartTimeVM = new ReportChairLoadPartTimeViewModel(new ServiceLoadChair());
        }

        private static void CreateReportTeacherLoadVM()
        {
            if (reportTeacherLoadVM == null)
                reportTeacherLoadVM = new ReportTeacherLoadViewModel(new ServiceLoadTeaher());
        }

        private static void CreateReportSummaryCurriculumWorkVM()
        {
            if (reportSummaryCurriculumWorkVM == null)
                //reportSummaryCurriculumWorkVM = new ReportSummaryCurriculumWorkViewModel(new ServiceCurriculum());
                reportSummaryCurriculumWorkVM = new ReportSummaryCurriculumWorkViewModel();
        }

        private static void CreateReportFixedDisciplinesVM()
        {
            if (reportFixedDisciplinesVM == null)
                //reportFixedDisciplinesVM = new ReportFixedDisciplinesViewModel(new ServiceCurriculum());
                reportFixedDisciplinesVM = new ReportFixedDisciplinesViewModel();
        }

        private static void CreateReportGroupVM()
        {
            if (reportGroupVM == null)
                reportGroupVM = new ReportGroupViewModel();
        }


        #endregion Create Properties for bindings

        #region Clear property

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMain()
        {
            main.Cleanup();
            main = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearUniversityVM property.
        /// </summary>
        public static void ClearUniversityVM()
        {
            universityVM.Cleanup();
            universityVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearFacultyVM property.
        /// </summary>
        public static void ClearFacultyVM()
        {
            facultyVM.Cleanup();
            facultyVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearChairVM property.
        /// </summary>
        public static void ClearChairVM()
        {
            chairVM.Cleanup();
            chairVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearTeacherVM property.
        /// </summary>
        public static void ClearTeacherVM()
        {
            teacherVM.Cleanup();
            teacherVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearGroupVM property.
        /// </summary>
        public static void ClearGroupVM()
        {
            groupVM.Cleanup();
            groupVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearOrderStandardTimeVM property.
        /// </summary>
        public static void ClearOrderStandardTimeVM()
        {
            orderStandardTimeVM.Cleanup();
            orderStandardTimeVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearCurriculumVM property.
        /// </summary>
        public static void ClearCurriculumVM()
        {
            curriculumVM.Cleanup();
            curriculumVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearCurriculumDBVM property.
        /// </summary>
        public static void ClearCurriculumFromDBVM()
        {
            curriculumFromDBVM.Cleanup();
            curriculumFromDBVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearCurriculumXmlVM property.
        /// </summary>
        public static void ClearCurriculumFromXMLVM()
        {
            curriculumFromXmlVM.Cleanup();
            curriculumFromXmlVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearCurriculumExlVM property.
        /// </summary>
        public static void ClearCurriculumFromEXLVM()
        {
            curriculumFromExlVM.Cleanup();
            curriculumFromExlVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearDisciplineChairVM property
        /// </summary>
        public static void ClearDisciplineChairVM()
        {
            disciplineChairVM.Cleanup();
            disciplineChairVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearDisciplineChairVM property
        /// </summary>
        public static void ClearFixedDisciplineVMVM()
        {
            fixedDisciplineVM.Cleanup();
            fixedDisciplineVM = null;
        }

        

        public static void ClearLoadChair()
        {
            loadChairVM.Cleanup();
            loadChairVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearSetTeachingLoadVM property.
        /// </summary>
        public static void ClearLoadTeacherdVM()
        {
            loadTeacherVM.Cleanup();
            loadTeacherVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearSetTeachingLoadVM property.
        /// </summary>
        public static void ClearLoadSpecialityVM()
        {
            specialityVM.Cleanup();
            specialityVM = null;
        }

        public static void ClearAcademicYearVM()
        {
            academicYearVM.Cleanup();
            academicYearVM = null;
        }


        public static void ClearEducationFormVM()
        {
            educationFormVM.Cleanup();
            educationFormVM = null;
        }

        public static void ClearPostVM()
        {
            dictPostVM.Cleanup();
            dictPostVM = null;
        }

        public static void ClearQualificationVM()
        {
            dictQualificationVM.Cleanup();
            dictQualificationVM = null;
        }

        public static void ClearUnitVM()
        {
            dictUnitVM.Cleanup();
            dictUnitVM = null;
        }

        public static void ClearTypeEmploymentVM()
        {
            dictTypeEmploymentVM.Cleanup();
            dictTypeEmploymentVM = null;
        }

        public static void ClearTypeTrainingVM()
        {
            dictTypeTrainingVM.Cleanup();
            dictTypeTrainingVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearReportChairDisciplinesVM property.
        /// </summary>
        public static void ClearReportChairDisciplinesVM()
        {
            reportChairDisciplinesVM.Cleanup();
            reportChairDisciplinesVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearReportChairDisciplinesFullTimeVM property.
        /// </summary>
        public static void ClearReportChairDisciplinesFullTimeVM()
        {
            reportChairDisciplinesFullTimeVM.Cleanup();
            reportChairDisciplinesFullTimeVM = null;
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearReportChairDisciplinesPartTimeVM property.
        /// </summary>
        public static void ClearReportChairDisciplinesPartTimeVM()
        {
            reportChairDisciplinesPartTimeVM.Cleanup();
            reportChairDisciplinesPartTimeVM = null;
        }

        public static void ClearReportChairLoadVM()
        {
            reportChairLoadVM.Cleanup();
            reportChairLoadVM = null;
        }

        public static void ClearReportChairLoadFullTimeVM()
        {
            reportChairLoadFullTimeVM.Cleanup();
            reportChairLoadFullTimeVM = null;
        }

        public static void ClearReportChairLoadPartTimeVM()
        {
            reportChairLoadPartTimeVM.Cleanup();
            reportChairLoadPartTimeVM = null;
        }


        public static void ClearReportTeacherLoadVM()
        {
            reportTeacherLoadVM.Cleanup();
            reportTeacherLoadVM = null;
        }

        public static void ClearReportSummaryCurriculumWorkVM()
        {
            reportSummaryCurriculumWorkVM.Cleanup();
            reportSummaryCurriculumWorkVM = null;
        }

        public static void ClearReportFixedDisciplinesVM()
        {
            reportFixedDisciplinesVM.Cleanup();
            reportFixedDisciplinesVM = null;
        }

        public static void ClearReportGroupVM()
        {
            reportGroupVM.Cleanup();
            reportGroupVM = null;
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearMain();
        }

        #endregion Clear property
    }
}