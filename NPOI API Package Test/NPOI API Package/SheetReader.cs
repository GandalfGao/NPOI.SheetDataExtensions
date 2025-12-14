using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NPOI_API_Package
{
    /// <summary>
    /// 工作表读取器
    /// </summary>
    public class SheetReader
    {
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sheet">工作表对象</param>
        public SheetReader(ISheet sheet)
        {
            this.sheet = sheet;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="rowsCount">行数</param>
        /// <param name="firstRowIndex">首行索引(从0开始)</param>
        /// <param name="hasHeader">是否包含头部信息(true: 读取数据时会以firstRowIndex基础上加1访问, false: 读取数据时直接以firstRowIndex基础上访问)</param>
        /// <param name="columnConfigs">列信息配置集合</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">当firstRowIndex小于0时抛出</exception>
        /// <exception cref="ArgumentNullException">当hasHeader为false且columnConfigs为null或空时抛出</exception>
        public DataTable Read(int rowsCount, int firstRowIndex = 0, bool hasHeader = true, IEnumerable<ColumnConfigAttribute>? columnConfigs = null)
        {
            //校验首行索引值
            if (firstRowIndex < 0)
            {
                throw new ArgumentException($"无效的首行索引值, firstRowIndex: {firstRowIndex}", nameof(firstRowIndex));
            }

            //如果工作表包含头部, 且列配置集合为空, 则自动创建列集合
            if (hasHeader && (columnConfigs == null || !columnConfigs.Any()))
            {
                var tempColumnConfigs = new List<ColumnConfigAttribute>();

                //访问工作表头部信息
                var headerRow = sheet.GetRow(firstRowIndex);

                //遍历单元格
                foreach (ICell cell in headerRow.Cells)
                {
                    var colConfig = new ColumnConfigAttribute
                    { 
                        ColumnMapping = cell.ToString(),
                        ColumnIndex = cell.ColumnIndex,
                    };
                    tempColumnConfigs.Add(colConfig);
                }

                columnConfigs = tempColumnConfigs;
            }
            else
            {
                //如果工作表不包含头部, 且列配置集合依旧为空, 则抛出异常
                if (!hasHeader && (columnConfigs == null || !columnConfigs.Any()))
                {
                    throw new ArgumentNullException(nameof(columnConfigs), "当工作表不包含头部信息时, 列配置集合不可以为空！");
                }
            }

            var table = new DataTable();

            //对table添加列
            foreach (var columnConfig in columnConfigs!)
            {
                var column = new DataColumn(columnConfig.ColumnMapping);
                table.Columns.Add(column);
            }

            //对table添加数据
            int startRowIndex = firstRowIndex + (hasHeader ? 1 : 0);
            for (int i = startRowIndex; i < rowsCount + startRowIndex; i++)
            {
                var row = sheet.GetRow(i);
                if (row != null)
                {
                    DataRow dataRow = table.NewRow();
                    foreach (var columnConfig in columnConfigs)
                    {
                        dataRow[columnConfig.ColumnMapping] = row.GetCell(columnConfig.ColumnIndex).GetCellValue();
                    }
                    table.Rows.Add(dataRow);
                }
            }

            return table;
        }
    }
}
