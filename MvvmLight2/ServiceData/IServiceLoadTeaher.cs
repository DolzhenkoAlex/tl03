using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    public interface IServiceLoadTeaher
    {
        /// <summary>
        /// Интерфейс загрузки обобщенных данных по нагрузке кафедры из базы данных для заданного учебного года
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        void GetDataAllLoadChair(Action<ObservableCollection<TeachingLoadChair>, Exception> callback, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс получения обобщенных данных по нагрузке кафедры из базы данных для заданного учебного года
        /// </summary>
        /// <param name="allLoadChair"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoadChair> GetAllLoadChair(ObservableCollection<TeachingLoadChair> allLoadChair, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс получения обобщенных данных о нагрузке преподавателя
        /// </summary>
        /// <param name="IdTeacher"></param>
        /// <param name="IdAcademicYear"></param>
        /// <returns></returns>
        TeahingLoadTeacher GetTeahingLoadTeacher(int idTeacher, int idAcademicYear, string nameLoadChair);

        /// <summary>
        /// Интерфейс получения данных о нагрузке преподавателя по дисциплинам 
        /// </summary>
        /// <param name="IdTeachingLoadTeacher"></param>
        /// <returns></returns>
        ObservableCollection<LoadTeacher> GetLoadTeacher(int IdTeachingLoadTeacher, ObservableCollection<LoadTeacher> teacherLoads);

        /// <summary>
        /// Интерфейс получения данных о дисциплине, распределенной преподавателю
        /// </summary>
        /// <param name="IdLoadTeacher"></param>
        /// <returns></returns>
        LoadTeacher GetDisciplineTeacher(int IdLoadTeacher);

        /// <summary>
        /// Интерфейс получения данных о соответствии дисциплин нагрузки преподавателя и дисциплин кафедры
        /// для преподавателей
        /// </summary>
        /// <param name="idTeacher"></param>
        /// <returns></returns>
        ObservableCollection<ChairTeacherLoad> GetChairTeacherLoad(int idTeacher, int status, string nameLoadChair);

        /// <summary>
        /// Интерфейс получения данных о соответствии дисциплин нагрузки преподавателя и дисциплин кафедры
        /// для дисциплины
        /// </summary>
        /// <param name="idTeachingLoad"></param>
        /// <param name="nameLoadChair"></param>
        /// <returns></returns>
        ObservableCollection<ChairTeacherLoad> GetChairTeacherLoadDiscipline(int idTeachingLoad, string nameLoadChair);

        /// <summary>
        /// Инферфейс определения существования дисциплины учебной нагрузки
        /// </summary>
        /// <param name="code"></param>
        /// <param name="discipline"></param>
        /// <param name="speciality"></param>
        /// <param name="nameGroup"></param>
        /// <param name="formEducation"></param>
        /// <param name="course"></param>
        /// <param name="semestr"></param>
        /// <returns></returns>
        bool IsLoad(int idTeachingLoadTeacher, string code, string discipline, string speciality, string nameGroup, string formEducation, int? course, int? semestr);

        /// <summary>
        /// Интерфейс добавление дисциплины в нагрузку преподавателя
        /// </summary>
        /// <param name="loadTeacher"></param>
        LoadTeacher AddItemLoadTeacher(LoadTeacher newLoadTeacher);

        
        /// <summary>
        /// Интерфейс добавления данных о соответствии дисциплин нагрузки преподавателя и дисциплин кафедры
        /// </summary>
        /// <param name="teach"></param>
        void AddItemChairTeacherLoad(ChairTeacherLoad newChairTeacherLoad, string nameLoadChair);

        /// <summary>
        /// Интерфейс поиска данных соответствия дисциплин нагрузки преподавателя по дисциплине кафедры
        /// </summary>
        /// <param name="teach"></param>
        /// <returns></returns>
        ChairTeacherLoad FindChairTeacherLoad(int idTeachingLoad, int status);

        /// <summary>
        /// Интерфейс получения данных соответствия дисциплин нагрузки преподавателей по дисциплине кафедры
        /// </summary>
        /// <param name="idTeachingLoad"></param>
        /// <returns></returns>
        ObservableCollection<ChairTeacherLoad> GetChairTeacherLoad(int idTeachingLoad);

        /// <summary>
        /// Интерфейс получения данных соответствия дисциплин нагрузки ассистетна по дисциплине кафедры
        /// </summary>
        /// <param name="idTeachingLoad"></param>
        /// <returns></returns>
        ObservableCollection<ChairTeacherLoad> GetChairAssistentLoad(int idTeachingLoad, int status);

        /// <summary>
        /// Интерфейс удаления данных соответствия дисциплин нагрузки преподавателя по дисциплине кафедры
        /// </summary>
        /// <param name="DelChairTeacherLoad"></param>
        void RemoveItemChairTeacherLoad(ChairTeacherLoad delChairTeacherLoad);

        /// <summary>
        /// Интерфейс удаления дисциплины нагрузки преподавателя 
        /// </summary>
        /// <param name="delLoadTeacher"></param>
        void RemoveItemLoadTeacher(LoadTeacher delDiscipline);

        /// <summary>
        /// Интерфейс редактирования дисциплины нагрузки преподавателя 
        /// </summary>
        /// <param name="editLoadTeacher"></param>
        void EditItemLoadTeacher(LoadTeacher editLoadTeacher);

        /// <summary>
        /// Интерфейс редактирования общих данных нагрузки преподавателя 
        /// </summary>
        /// <param name="editTeahingLoadTeacher"></param>
        void EditItemTeahingLoadTeacher(TeahingLoadTeacher editTeahingLoadTeacher);

        /// <summary>
        /// Интерфейс редактирования дисциплины нагрузки кафедры
        /// </summary>
        /// <param name="editTeachingLoad"></param>
        void EditTeachingLoad(TeachingLoad editTeachingLoad);

        /// <summary>
        /// Интерфейс редактирования общих данных нагрузки кафедры 
        /// </summary>
        /// <param name="editTeachingLoadChair"></param>
        void EditTeachingLoadChair(TeachingLoadChair editTeachingLoadChair);

        /// <summary>
        /// Интерфейс получения обобщенных данных о нагрузке преподавателей кафедры
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getAllLoadTeacher_Result> GetAllLoadTeacher(int idChair, int? idAcademicYear);
    }
}
