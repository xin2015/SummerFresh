
using System;
using System.Collections.Generic;
using SummerFresh.Data.Mapping;
namespace SummerFresh.Business
{
    public interface ISQLSyntaxGenerator
    {
        /// <summary>
        /// 生成新建表的SQL语句
        /// </summary>
        /// <returns></returns>
        string GetCreateTableSQL();

        /// <summary>
        /// 获取删除表的SQL语句
        /// </summary>
        /// <returns></returns>
        string GetDropTableSQL();

        /// <summary>
        /// 生成新增列的SQL语句
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        string GetAddColumnSQL(Column field);

        /// <summary>
        /// 获取删除列的SQL语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        string GetDropColumnSQL(string tableName, string fieldName);



        /// <summary>
        /// 获取默认排序表达式
        /// </summary>
        /// <returns></returns>
        string GetDefaultSortExpression();

        /// <summary>
        /// 获取删除语句
        /// </summary>
        /// <returns></returns>
        string GetDeleteSQL();

        /// <summary>
        /// 获取判断表中某列是否存在的SQL语句
        /// </summary>
        /// <param name="fieldName">列名</param>
        /// <returns></returns>
        string GetIfExistColumnSQL(string fieldName);

        /// <summary>
        /// 获取判断表名是否存在的SQL语句
        /// </summary>
        /// <returns></returns>
        string GetIfExistTableSQL();

        /// <summary>
        /// 获取INSERT语句
        /// </summary>
        /// <returns></returns>
        string GetInsertSQL();

        /// <summary>
        /// 获取INSERT语句
        /// </summary>
        /// <returns></returns>
        string GetSearchCondition();

        /// <summary>
        /// 获取GET ONE语句
        /// </summary>
        /// <returns></returns>
        string GetSelectOneSQL();

        /// <summary>
        /// 获取SELECT语句
        /// </summary>
        /// <returns></returns>
        string GetSelectSQL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        string GetSelectSQL(string searchCondition);

        /// <summary>
        /// 获取UPDATE语句
        /// </summary>
        /// <returns></returns>
        string GetUpdateSQL();

        /// <summary>
        /// 获取判断字段值是否唯一的语句
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        string GetCheckUnionSQL(string fieldName);

        /// <summary>
        /// 获取where条件
        /// </summary>
        /// <returns></returns>
        string GetWhereCondition();

        /// <summary>
        /// 支持的字段类型
        /// </summary>
        string FieldType { get;}

        TableMapping Mapping { get; }
        
    }
}
