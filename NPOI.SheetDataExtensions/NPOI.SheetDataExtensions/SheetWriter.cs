using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace NPOI.SheetDataExtensions
{
    /// <summary>
    /// 工作表写入器
    /// </summary>
    public class SheetWriter
    {
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sheet">工作表对象</param>
        public SheetWriter(ISheet sheet)
        {
            this.sheet = sheet;
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="firstRowIndex">写入的首行索引</param>
        /// <param name="firstColIndex">写入的首列索引</param>
        /// <param name="hasHader">是否包含头部</param>
        /// <param name="setSheetStyleFunc">设置工作表样式委托</param>
        /// <exception cref="ArgumentException">当首行索引值或首列索引值小于0时抛出</exception>
        public void Write(DataTable dataTable, int firstRowIndex = 0, int firstColIndex = 0, bool hasHader = true, Action<ISheet>? setSheetStyleFunc = null)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable), "DataTable参数不可以为null！");
            }
            if (dataTable.Columns.Count == 0)
            {
                throw new ArgumentException($"DataTable参数列不可以为空！", nameof(dataTable));
            }

            //校验首行索引值
            if (firstRowIndex < 0)
            {
                throw new ArgumentException($"无效的首行索引值, firstRowIndex: {firstRowIndex}", nameof(firstRowIndex));
            }
            //校验首列索引值
            if (firstColIndex < 0)
            {
                throw new ArgumentException($"无效的首列索引值, firstColIndex: {firstColIndex}", nameof(firstColIndex));
            }

            int rowIndex = firstRowIndex;

            //如果需要列标题则将其写入到文件中
            if (hasHader)
            {
                //创建行
                var row = sheet.CreateRow(rowIndex++);
                //设置列索引
                int colIndex = firstColIndex;
                //遍历读取dataTable的列信息
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    //创建单元格
                    var cell = row.CreateCell(colIndex++);
                    //设置单元格值
                    cell.SetCellValue(dataColumn.ColumnName);
                }
            }

            var contentRows = new List<IRow>();
            //遍历dataTable行
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //创建行
                var row = sheet.CreateRow(rowIndex++);
                //设置列索引
                int colIndex = firstColIndex;
                //遍历dataTable列
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    //创建单元格
                    var cell = row.CreateCell(colIndex++);
                    //获取dataTable单个值
                    var val = dataRow[dataColumn];
                    //设置单元格值
                    cell.SetValue(val);
                }
                //添加内容行
                contentRows.Add(row);
            }

            //设置样式
            setSheetStyleFunc?.Invoke(sheet);
        }
    }
}
