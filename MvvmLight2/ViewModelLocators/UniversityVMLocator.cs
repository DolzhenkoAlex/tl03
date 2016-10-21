using MvvmLight2.ServiceData;
using MvvmLight2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.ViewModelLocators
{
    public class UniversityVMLocator
    {
        private static UniversitiesViewModel universityVM;

        public UniversityVMLocator()
        {
            CreateUniversityVM();
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

        private static void CreateUniversityVM()
        {
            if (universityVM == null)
            {
                universityVM = new UniversitiesViewModel(new ServiceUniversity());
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ClearUniversityVM property.
        /// </summary>
        public static void ClearUniversityVM()
        {
            universityVM.Cleanup();
            universityVM = null;
        }

    }
}
