using NPOI.OpenXmlFormats.Dml;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Net;
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
        public static object GetCellValue(this ICell? cell, IFormulaEvaluator? formulaEvaluator = null)
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
                case CellType.Error:
                    var errVal = cell.ErrorCellValue;
                    var fErrVal = FormulaError.ForInt(errVal);
                    value = fErrVal.String;
                    break;
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
                case CellType.Blank:
                case CellType.Formula:
                default:
                    value = cell.ToString();
                    break;
            }

            return value;
        }

        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">值</param>
        /// <exception cref="ArgumentNullException">当单元格为null时抛出</exception>
        public static void SetCellValue(this ICell cell, object value)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell), "单元格对象不可以为null！");
            }

            string val = value?.ToString() ?? string.Empty;

            if (bool.TryParse(val, out bool boolVal))
            {
                cell.SetCellValue(boolVal);
            }
            else if (double.TryParse(val, out double numVal))
            {
                cell.SetCellValue(numVal);
            }
            else if (DateTime.TryParse(val, out DateTime dtVal))
            {
                cell.SetCellValue(dtVal);
            }
            //字符串开头为等号表示公式
            else if (val.StartsWith("="))
            { 
                cell.SetCellFormula(val.Substring(1));
            }
            else
            {
                cell.SetCellValue(val);
            }
        }

        /// <summary>
        /// 判断单元格是否为空
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static bool IsEmpty(this ICell? cell)
        {
            if (cell == null)
            {
                return true;
            }

            var val = cell.ToString();
            return string.IsNullOrEmpty(val);
        }
    }
}
