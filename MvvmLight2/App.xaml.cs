using System.Windows;
using ClassLibraryServiceDB.ServiceData;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using MvvmLight2.Helper;
using MvvmLight2.ServiceData;

namespace MvvmLight2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();

            var container = Container.Instance;
            container.RegisterAsSingleton<IWindowService, WindowService>();
            container.RegisterAsSingleton<IServiceUniversity, ServiceUniversity>();
            container.RegisterAsSingleton<IServiceFaculty, ServiceFaculty>();
            container.RegisterAsSingleton<IServiceChair, ServiceChair>();
            container.RegisterAsSingleton<IServiceTeacher, ServiceTeaher>();
            container.RegisterAsSingleton<IServiceGroup, ServiceGroup>();
            container.RegisterAsSingleton<IServiceOrderStandartTime, ServiceOrderStandartTime>();
            container.RegisterAsSingleton<IServiceDisciplineChair, ServiceDisciplineChair>();
            container.RegisterAsSingleton<IServiceCurriculum, ServiceCurriculum>();
            container.RegisterAsSingleton<IServiceCurriculumFromDB, ServiceCurriculumFromDB>();
            container.RegisterAsSingleton<IServiceCurriculumFromXml, ServiceCurriculumFromXml>();
            container.RegisterAsSingleton<IServiceCurriculumFromExl, ServiceCurriculumFromExl>();
            container.RegisterAsSingleton<IServiceLoadChair, ServiceLoadChair>();
            container.RegisterAsSingleton<IServiceLoadTeaher, ServiceLoadTeaher>();
            container.RegisterAsSingleton<IServiceSpeciality, ServiceSpeciality>();
            container.RegisterAsSingleton<IServiceAcademicYear, ServiceAcademicYear>();
            container.RegisterAsSingleton<IServiceEducationForm, ServiceEducationForm>();
            container.RegisterAsSingleton<IServicePost, ServicePost>();
            container.RegisterAsSingleton<IServiceQualification, ServiceQualification>();
            container.RegisterAsSingleton<IServiceUnit, ServiceUnit>();
            container.RegisterAsSingleton<IServiceTypeEmployment, ServiceTypeEmployment>();
            container.RegisterAsSingleton<IServiceTypeTraining, ServiceTypeTraining>();
            container.RegisterAsSingleton<IServiceConnectionStringDB, ServiceConnectionStringDB>();
            container.RegisterAsSingleton<IServiceFixedDiscipline, ServiceFixedDiscipline>();
            
            container.RegisterInstance(typeof(IMessenger), Messenger.Default);
            container.RegisterInstance(typeof(IContainer), container);
        }
    }
}
