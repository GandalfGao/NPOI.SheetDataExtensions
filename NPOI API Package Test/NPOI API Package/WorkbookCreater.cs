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
    public class WorkbookCreater
    {
        /// <summary>
        /// 创建对象(应用于创建文件)
        /// </summary>
        /// <param name="excelType">Excel文件类型枚举</param>
        /// <returns></returns>
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
        /// 创建对象(应用于读取文件)
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns></returns>
        public static IWorkbook Create(string file)
        {
            //获取扩展名称
            string extName = Path.GetExtension(file);

            //校验扩展名称
            if (string.IsNullOrEmpty(extName))
            {
                throw new ArgumentNullException(nameof(extName), "文件扩展名称不可以为空！");
            }

            //将扩展名称转换为枚举
            ExcelType excelType = Enum.Parse<ExcelType>(extName);
            //创建对象
            IWorkbook workbook = Create(file, excelType);
            return workbook;
        }

        /// <summary>
        /// 创建对象(应用于读取文件)
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="excelType">Excel文件类型枚举</param>
        /// <returns></returns>
        public static IWorkbook Create(string file, ExcelType excelType)
        {
            //校验file变量
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file), "file变量不可以为空！");
            }
            if (!File.Exists(file))
            {
                throw new ArgumentException($"当前文件不存在！file: {file}", nameof(file));
            }

            using var fs = new FileStream(file, FileMode.Open, FileAccess.Read);

            IWorkbook workbook;
            switch (excelType)
            {
                case ExcelType.Xls:
                    workbook = new HSSFWorkbook(fs);
                    break;
                case ExcelType.Xlsx:
                default:
                    workbook = new XSSFWorkbook(fs);
                    break;
            }

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
