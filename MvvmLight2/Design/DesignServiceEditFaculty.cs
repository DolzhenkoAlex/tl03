using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditFaculty
    {
        public Faculty Faculty { get; private set; }

        public DesignServiceEditFaculty()
        {
            Faculty = new Faculty()
            {
                Code = 3,
                Name = "Компьютерных технологий и информационной безопасности",
                ShortName = "КТИБ",
                //Chairs = new ObservableCollection<Chair>()
            };
        }
    }
}
