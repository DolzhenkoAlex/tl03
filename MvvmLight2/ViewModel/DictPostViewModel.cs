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
    public class DictPostViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedPost

        /// <summary>
        /// Поле - выделенная в списке должность
        /// </summary>
        private DictPost selectedPost;

        /// <summary>
        /// Свойство - выделенная в списке  должность
        /// </summary>
        public DictPost SelectedPost
        {
            get { return selectedPost; }
            set
            {
                if (value == selectedPost)
                    return;
                else
                {
                    selectedPost = value;
                    RaisePropertyChanged("SelectedPost");
                }
            }
        }

        #endregion SelectedPost

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по должностям
        /// </summary>
        public ObservableCollection<DictPost> Posts { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DictPostViewModel class.
        /// </summary>
        public DictPostViewModel(IServicePost service)
        {
            service.GetDataPost(
               (posts, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Posts = posts;
               });

            base.DisplayName = "Должность преподавателя";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по должности - поле
        /// </summary>
        private ICommand getPost;

        /// <summary>
        /// Команда - Загрузка данных по должности - Свойство
        /// </summary>
        public ICommand GetPost
        {
            get
            {
                return getPost ??
                    (getPost = new RelayCommand(GetPostExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по должности - метод
        /// </summary>
        private void GetPostExecute()
        {
            var service = container.Resolve<IServicePost>();
            Posts.Clear();
            Posts = service.GetPost(Posts);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по должности - поле
        /// </summary>
        private ICommand editPost;

        /// <summary>
        /// Редактирование данных по должности - Свойство
        /// </summary>
        public ICommand EditPost
        {
            get
            {
                return editPost ??
                    (editPost = new RelayCommand(EditPostExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по должности - метод 
        /// </summary>
        private void EditPostExecute()
        {
            var viewModel = container.Resolve<EditPostViewModel>();

            viewModel.DictPost = SelectedPost;

            var viewEdit = container.Resolve<EditPostView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var postService = container.Resolve<IServicePost>();
                postService.EditItemPost(SelectedPost);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemovePostCommand -  Редактирование / Удаление данных по должности
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedPost != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по должности - поле
        /// </summary>
        private ICommand addPost;

        /// <summary>
        /// Добавление данных по должности - Свойство
        /// </summary>
        public ICommand AddPost
        {
            get
            {
                return addPost ??
                    (addPost = new RelayCommand(AddPostExecute));
            }
        }

        /// <summary>
        /// Добавление данных по должности - метод
        /// </summary>
        private void AddPostExecute()
        {
            var viewModel = container.Resolve<EditPostViewModel>();

            DictPost newPost = new DictPost();
            newPost.StatusDel = true;
            viewModel.DictPost = newPost;

            var viewEdit = container.Resolve<EditPostView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServicePost>();
                service.AddItemDataPost(newPost);

                Posts.Clear();
                Posts = service.GetPost(Posts);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по должности - поле
        /// </summary>
        private ICommand removePost;

        /// <summary>
        /// Удаление данных по должности - Свойство
        /// </summary>
        public ICommand RemovePost
        {
            get
            {
                return removePost ??
                    (removePost = new RelayCommand(RemovePostExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по должности - метод
        /// </summary>
        private void RemovePostExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по должности преподавателя: \n" + SelectedPost.Post,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServicePost>();
                service.RemoveItemPost(selectedPost);

                Posts.Remove(selectedPost);
            }
        }


        #endregion CommandEdit

    }
}

           