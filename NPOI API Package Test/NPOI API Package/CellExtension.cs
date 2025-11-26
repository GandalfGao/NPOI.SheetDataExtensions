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
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="formulaEvaluator">公式评估器对象</param>
        /// <returns>单元格值</returns>
        public static object GetCellValue(this ICell cell, IFormulaEvaluator? formulaEvaluator = null)
        {
            //校验cell对象是否为空
            if (cell == null)
            {
                return string.Empty;
            }

            var cellType = cell.CellType;

            if (cellType == CellType.Formula)
            {
                if (formulaEvaluator != null)
                {
                    cellType = formulaEvaluator.EvaluateFormulaCell(cell);
                }
            }

            var value = cell.GetCellValue(cellType);
            return value;
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellType">单元格类型</param>
        /// <returns>单元格值</returns>
        private static object GetCellValue(this ICell cell, CellType cellType)
        {
            object value;

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
