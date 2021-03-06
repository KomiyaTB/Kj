﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;//MSのSQLサーバーへのアクセス

namespace DbAccessConsoleApp
{
    public class Database
    {
        /// <summary>
        /// 接続文字列の値を保存するプロパティ
        /// </summary>
        public String ConnectionString { get; set; }

        public void ExecuteNonQuery(String query)
        {
            using (var cn = new SqlConnection(this.ConnectionString))
            {
                //SQLを設定するSqlCommandクラスを作成
                var cm = new SqlCommand(query);
                //SQLを実行する先のDBを設定する
                cm.Connection = cn;
                //接続を開く
                cn.Open();
                //実際にSQLを実行する
                var insertCount = cm.ExecuteNonQuery();
                //接続を閉じる
                cn.Close();
            }
        }
    }
}
