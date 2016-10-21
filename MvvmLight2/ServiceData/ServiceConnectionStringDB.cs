using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.ServiceData
{
    public class ServiceConnectionStringDB:IServiceConnectionStringDB
    {
        public void SetConnection(string con)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            // Get the application configuration file.
            System.Configuration.Configuration config = 
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Get the conectionStrings section.
            ConnectionStringsSection csSection = config.ConnectionStrings;

            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                ConnectionStringSettings cs = csSection.ConnectionStrings[i];

                if (cs.Name == "DBTeachingLoadEntities")
                {
                    int indexSection = i;                // индекс строки соединения
                    string csName = cs.Name;             // имя строки соединения
                    string conSql = cs.ConnectionString; // строка соединения

                    int indexProvider = conSql.IndexOf("data source");
                    int len = conSql.Length;
                    // Выделение метаданных из строки соединения
                    string metaDate = conSql.Substring(0, indexProvider - 1); 
                    // Формирование строки соединения
                    string conBuilder = conSql.Substring(indexProvider, len - indexProvider - 1);

                    builder = new SqlConnectionStringBuilder(conBuilder);
                    builder.DataSource = con;
                    Char c = '"';

                    // строка соединения с обновленным сервером
                    string newcs = String.Format("{0}{1}{2}", metaDate, builder.ToString(), c);
                    csSection.ConnectionStrings[indexSection].ConnectionString = newcs;

                    // Save the configuration file.
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
        }
    }
}
