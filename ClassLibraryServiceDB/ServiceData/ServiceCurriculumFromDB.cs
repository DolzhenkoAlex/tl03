using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryServiceDB.Model;

namespace ClassLibraryServiceDB.ServiceData
{
    public class ServiceCurriculumFromDB : IServiceCurriculumFromDB
    {
        /// <summary>
        /// Загрузка титулов учебных планов университета
        /// </summary>
        /// <param name="callback"></param>
        public void GetDataCurriculum(Action<ObservableCollection<Планы>, Exception> callback)
        {
            ObservableCollection<Планы> curriculums = new ObservableCollection<Планы>();
            using (var context = new DekanusKontorEntities())
            {
                var QueryCurriculum = from curr in context.Планы
                                        .Include("ПланыКвалификации")
                                        .Include("ПланыПрактики")
                                        .Include("ПланыСпецРаботы")
                                        .Include("ПланыСтроки")
                                      orderby curr.ИмяФайла
                                      select curr;
                foreach (Планы c in QueryCurriculum)
                    curriculums.Add(c);

            }
            callback(curriculums, null);
        }

        /// <summary>
        /// Получение данных по титулам учебных планов университета
        /// </summary>
        /// <param name="curriculums"></param>
        /// <param name="idUniversity"></param>
        /// <returns></returns>
        public ObservableCollection<Планы> GetCurriculum(ObservableCollection<Планы> curriculums, string academicYear)
        {
            using (var context = new DekanusKontorEntities())
            {
                var QueryCurriculum = from curr in context.Планы
                                        .Include("ПланыКвалификации")
                                        .Include("ПланыПрактики")
                                      where curr.УчебныйГод == academicYear
                                      orderby curr.ИмяФайла
                                      select curr;
                foreach (Планы c in QueryCurriculum)
                { 
                    curriculums.Add(c);
                }

                return curriculums;
            }
        }

        /// <summary>
        /// Получение данных по дисциплинам учебного плана университета
        /// </summary>
        /// <param name="idCurriculum"></param>
        /// <returns></returns>
        public ObservableCollection<ПланыСтроки> GetListDiscipline(int idCurriculum)
        {
            ObservableCollection<ПланыСтроки> listDiscipline = new ObservableCollection<ПланыСтроки>();

            using (var context = new DekanusKontorEntities())
            {
                var QueryDiscipline = from disc in context.ПланыСтроки
                                        //.Include("ПланыЧасы")
                                      where disc.КодПлана == idCurriculum 
                                      orderby disc.Дисциплина
                                      select disc;
                
                foreach (ПланыСтроки disc in QueryDiscipline)
                    listDiscipline.Add(disc);
                return listDiscipline;
            }
        }

        /// <summary>
        /// Формирование часовой нагрузки
        /// </summary>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        public List<ПланыЧасы> GetListWatch(int idPlan)
        {
            List<ПланыЧасы> listWatch = new List<ПланыЧасы>();

            using (var context = new DekanusKontorEntities())
            {
                var QueryWatch = from watch in context.ПланыЧасы
                                 where watch.КодСтроки == idPlan
                                      select watch;

                foreach (ПланыЧасы w in QueryWatch)
                    listWatch.Add(w);
                return listWatch;
            }
        }

        /// <summary>
        /// Формирование данных по учебным практикам
        /// </summary>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        public List<ПланыПрактики> GetListPractic(int idPlan)
        {
            List<ПланыПрактики> listPractic = new List<ПланыПрактики>();

            using (var context = new DekanusKontorEntities())
            {
                var QueryPractic = from watch in context.ПланыПрактики
                                 where watch.КодПлана == idPlan
                                 select watch;

                foreach (ПланыПрактики pract in QueryPractic)
                    listPractic.Add(pract);
                return listPractic;
            }
        }


        /// <summary>
        /// Формирование данных по титулу учебного плана
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public List<ПланыТитулы> GetListTitle(int idPlan)
        {
            List<ПланыТитулы> listTitle = new List<ПланыТитулы>();

            using (var context = new DekanusKontorEntities())
            {
                var QueryTitle = from title in context.ПланыТитулы
                                 where title.КодПлана == idPlan
                                 select title;

                foreach (ПланыТитулы t in QueryTitle)
                    listTitle.Add(t);
                return listTitle;
            }
        }

        /// <summary>
        /// Получение данных о квалификации
        /// </summary>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        public ПланыКвалификации GetQualification(int idPlan)
        {
            using (var context = new DekanusKontorEntities())
            {
                var queryQual = (from qual in context.ПланыКвалификации
                                 where qual.КодПлана == idPlan
                                 select qual).FirstOrDefault();

                return queryQual;
            }
        }
    }
}
