using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPOI_API_Package
{
    /// <summary>
    /// workbook对象创建器
    /// </summary>
    public static class WorkbookCreater
    {
        /// <summary>
        /// 创建Excel对象(应用于创建文件)
        /// </summary>
        /// <param name="excelType">Excel文件类型枚举</param>
        /// <returns>Excel对象</returns>
        public static IWorkbook Create(ExcelType excelType)
        {
            IWorkbook workbook;

            switch (excelType)
            {
                case ExcelType.Xls:
                    workbook = new HSSFWorkbook();
                    break;
                case ExcelType.Xlsx:
                default:
                    workbook = new XSSFWorkbook();
                    break;
            }

            return workbook;
        }

        /// <summary>
        /// 创建Excel对象(应用于加载文件)
        /// </summary>
        /// <param name="file">文件路径参数</param>
        /// <returns>Excel对象</returns>
        /// <exception cref="ArgumentNullException">当文件路径参数为空或NULL时抛出ArgumentNullException异常</exception>
        /// <exception cref="FileNotFoundException">当指定的文件不存在不存在时抛出FileNotFoundException异常</exception>
        public static IWorkbook Create(string file)
        {
            //校验file参数
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file), "文件参数不可以为空！");
            }
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("指定的文件不存在！", file);
            }

            //创建workbook对象
            var workbook = WorkbookFactory.Create(file);
            return workbook;
        }
    }

    /// <summary>
    /// Excel文件类型枚举
    /// </summary>
    public enum ExcelType
    {
        Xls,
        Xlsx
    }
}
