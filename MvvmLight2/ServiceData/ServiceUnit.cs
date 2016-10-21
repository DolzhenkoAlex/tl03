      using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных по единицам измерения
    /// </summary>
    public class ServiceUnit : IServiceUnit
    {
        /// <summary>
        /// Загрузка данных по единицам измерения из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataUnit(Action<ObservableCollection<DictUnit>, Exception> callback)
        {
            ObservableCollection<DictUnit> units = new ObservableCollection<DictUnit>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryUnit = from c in context.DictUnits
                                    where c.StatusDel == true
                                    orderby c.Unit
                                    select c;
                    foreach (DictUnit c in QueryUnit)
                        units.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(units, null);
        }

        /// <summary>
        /// Получение данных по единицам измерения из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictUnit> GetUnit(ObservableCollection<DictUnit> units)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryUnit = from c in context.DictUnits
                                where c.StatusDel == true
                                orderby c.Unit
                                select c;

                foreach (DictUnit c in QueryUnit)
                    units.Add(c);
            }

            return units;
        }

        /// <summary>
        /// Добавление данных по новой единице измерения
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataUnit(DictUnit newUnit)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictUnits.AddObject(newUnit);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по единицам измерения
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemUnit(DictUnit editUnit)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var UnitEdit = (from unit in context.DictUnits
                                    where unit.Id == editUnit.Id
                                    select unit).FirstOrDefault();
                    if (UnitEdit != null)
                    {
                        context.DictUnits.ApplyCurrentValues(editUnit);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Единица измерения с Id '{0}' не может быть отредактирована.\n", editUnit.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по единицам измерения
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemUnit(DictUnit delUnit)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteUnit = (from unit in context.DictUnits
                                      where unit.Id == delUnit.Id
                                      select unit).FirstOrDefault();

                    if (deleteUnit != null)
                    {

                        deleteUnit.StatusDel = false;
                        context.DictUnits.ApplyCurrentValues(deleteUnit);
                        //context.DictUnits.DeleteObject(deleteUnit);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Единица измерения с Id '{0}' не может быть удалена.\n", delUnit.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождения единицы измерения по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictUnit GetItemUnit(string uUnit)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var unit = (from ch in context.DictUnits
                            where (ch.Unit == uUnit)
                            select ch).FirstOrDefault();
                return unit;
            }
        }
    }
}