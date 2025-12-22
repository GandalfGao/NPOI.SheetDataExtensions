using NPOI.SS.UserModel;
using System;

namespace NPOI.SheetDataExtensions
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
        public static object GetValue(this ICell? cell, IFormulaEvaluator? formulaEvaluator = null)
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

            var value = cell.GetValue(cellType);
            return value;
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellType">单元格类型</param>
        /// <returns>单元格值</returns>
        private static object GetValue(this ICell cell, CellType cellType)
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
        /// <param name="value">对象值</param>
        /// <exception cref="ArgumentNullException">当单元格为null时抛出</exception>
        public static void SetValue(this ICell cell, object value)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell), "单元格对象不可以为null！");
            }

            if (value == null)
            {
                cell.SetBlank();
            }
            else if (value is bool boolVal)
            {
                cell.SetCellValue(boolVal);
            }
            else if (value.IsNumericType())
            {
                cell.SetCellValue(Convert.ToDouble(value));
            }
            else if (value is DateTime dt)
            {
                cell.SetCellValue(dt);
            }
            else if (value is string str)
            {
                cell.SetValue(str);
            }
            else
            {
                cell.SetValue(value.ToString());
            }
        }

        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">字符串值</param>
        /// <exception cref="ArgumentNullException">当单元格为null时抛出</exception>
        public static void SetValue(this ICell cell, string value)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell), "单元格对象不可以为null！");
            }

            string val = value ?? string.Empty;

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
                cell.SetCellFormula(val[1..]);
            }
            //如果是空字符串时设置为blank
            else if (val == string.Empty)
            {
                cell.SetBlank();
            }
            else
            {
                cell.SetCellValue(val);
            }
        }

        /// <summary>
        /// 判断对象是否为数值类型
        /// </summary>
        /// <param name="obj">对象值</param>
        /// <returns></returns>
        private static bool IsNumericType(this object obj)
        {
            return obj is byte || obj is sbyte ||
                   obj is short || obj is ushort ||
                   obj is int || obj is uint ||
                   obj is long || obj is ulong ||
                   obj is float || obj is double || obj is decimal;
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
