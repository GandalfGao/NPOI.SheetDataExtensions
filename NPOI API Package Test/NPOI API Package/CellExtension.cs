using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace NPOI_API_Package
{
    /// <summary>
    /// 单元格扩展类
    /// </summary>
    public static class CellExtension
    {
        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns></returns>
        public static object GetCellValue(this ICell cell)
        {
            //校验cell对象是否为空
            if (cell == null)
            {
                return string.Empty;
            }

            object value;

            CellType cellType = cell.CellType;
            switch (cellType)
            {
                case CellType.Boolean:
                    value = cell.BooleanCellValue;
                    break;
                case CellType.Numeric:
                    //判断是否是日期类型
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        value = cell.DateCellValue ?? new DateTime();
                    }
                    else
                    {
                        value = cell.NumericCellValue;
                    }
                    break;
                default:
                    value = cell.ToString();
                    break;
            }

            return value;
        }
    }
}
