using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс формирования строки подключения к базе  данных
    /// </summary>
    interface IServiceConnectionStringDB
    {
        /// <summary>
        /// Интерфейс формирования подключения к серверу
        /// </summary>
        /// <param name="con"></param>
        void SetConnection(string con);
    }
}
