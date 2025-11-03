using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NPOI_API_Package
{
    /// <summary>
    /// Excel读取器
    /// </summary>
    public class ExcelReader
    {
        /// <summary>
        /// Excel文件对象
        /// </summary>
        private readonly IWorkbook workbook;
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workbook">Excel文件对象</param>
        /// <param name="sheet">工作表对象</param>
        public ExcelReader(IWorkbook workbook, ISheet sheet)
        {
            this.workbook = workbook;
            this.sheet = sheet;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workbook">Excel文件对象</param>
        /// <param name="sheetIndex">工作表索引值(从0开始)</param>
        public ExcelReader(IWorkbook workbook, int sheetIndex) : this(workbook, workbook.GetSheetAt(sheetIndex))
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workbook">Excel文件对象</param>
        /// <param name="sheetName">工作表名称</param>
        public ExcelReader(IWorkbook workbook, string sheetName) : this(workbook, workbook.GetSheet(sheetName))
        { }
    }
}
