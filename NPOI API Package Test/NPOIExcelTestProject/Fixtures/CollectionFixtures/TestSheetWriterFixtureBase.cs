using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures.CollectionFixtures
{
    /// <summary>
    /// 测试工作表写入器基类
    /// </summary>
    public abstract class TestSheetWriterFixtureBase : IDisposable
    {
        /// <summary>
        /// Excel文件对象
        /// </summary>
        private readonly IWorkbook workbook;
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workbook"></param>
        public TestSheetWriterFixtureBase(IWorkbook workbook)
        {
            this.workbook = workbook;
            this.sheet1 = workbook.CreateSheet("测试工作表1");
        }

        /// <summary>
        /// 工作表对象属性
        /// </summary>
        public ISheet Sheet1 => sheet1;

        /// <summary>
        /// 销毁资源
        /// </summary>
        public void Dispose()
        {
            workbook.Dispose();
        }
    }
}
