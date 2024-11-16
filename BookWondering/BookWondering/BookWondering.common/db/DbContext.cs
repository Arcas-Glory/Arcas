using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbType = SqlSugar.DbType;

namespace BookWondering.Common.db
{
    public class DbContext
    {
        public static SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = "Data Source=LAPTOP-JV6R79J7;Initial Catalog=BookWonderingDB;Persist Security Info=True;User ID=sa;Password=123456",//连接符字串
            DbType = DbType.SqlServer,//数据库类型
            IsAutoCloseConnection = true //不设成true要手动close
        });
    }
}
