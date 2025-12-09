using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures.ClassFixtures
{
    /// <summary>
    /// 测试Xls文件读取器夹具
    /// </summary>
    public class TestXlsFileReaderFixture : IDisposable
    {
        /// <summary>
        /// Excel文件对象
        /// </summary>
        private readonly IWorkbook workbook;
        /// <summary>
        /// 公式评估器对象
        /// </summary>
        private readonly IFormulaEvaluator formulaEvaluator;
        /// <summary>
        /// 包含布尔值数据的行对象
        /// </summary>
        private readonly IRow boolRow;
        /// <summary>
        /// 包含数字值数据的行对象
        /// </summary>
        private readonly IRow numRow;
        /// <summary>
        /// 包含日期值数据的行对象
        /// </summary>
        private readonly IRow dateRow;
        /// <summary>
        /// 包含时间值数据的行对象
        /// </summary>
        private readonly IRow timeRow;
        /// <summary>
        /// 包含文本值数据的行对象
        /// </summary>
        private readonly IRow textRow;
        /// <summary>
        /// 包含空数据的行对象
        /// </summary>
        private readonly IRow emptyRow;

        /// <summary>
        /// 通过公式计算得到布尔值的行对象
        /// </summary>
        private readonly IRow fBoolRow;
        /// <summary>
        /// 通过公式计算得到数字值的行对象
        /// </summary>
        private readonly IRow fNumRow;
        /// <summary>
        /// 通过公式计算得到日期值的行对象
        /// </summary>
        private readonly IRow fDateRow;
        /// <summary>
        /// 通过公式计算得到错误值的行对象
        /// </summary>
        private readonly IRow fErrorRow;
        /// <summary>
        /// 通过公式计算得到字符串值的行对象
        /// </summary>
        private readonly IRow fStringRow;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TestXlsFileReaderFixture()
        {
            workbook = WorkbookFactory.Create("TestExcelFiles\\TestXlsFile.xls");
            formulaEvaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);

            int i = 0;
            var sheet = workbook.GetSheetAt(0);
            boolRow = sheet.GetRow(i++);
            numRow = sheet.GetRow(i++);
            dateRow = sheet.GetRow(i++);
            timeRow = sheet.GetRow(i++);
            textRow = sheet.GetRow(i++);
            emptyRow = sheet.GetRow(i++);

            int j = 0;
            var sheet2 = workbook.GetSheetAt(1);
            fBoolRow = sheet2.GetRow(j++);
            fNumRow = sheet2.GetRow(j++);
            fDateRow = sheet2.GetRow(j++);
            fErrorRow = sheet2.GetRow(j++);
            fStringRow = sheet2.GetRow(j++);
        }

        /// <summary>
        /// 公式评估器属性
        /// </summary>
        public IFormulaEvaluator FormulaEvaluator => formulaEvaluator;

        /// <summary>
        /// 包含布尔值数据的行属性
        /// </summary>
        public IRow BoolRow => boolRow;

        /// <summary>
        /// 包含数字值数据的行属性
        /// </summary>
        public IRow NumRow => numRow;

        /// <summary>
        /// 包含日期值数据的行属性
        /// </summary>
        public IRow DateRow => dateRow;

        /// <summary>
        /// 包含时间值数据的行属性
        /// </summary>
        public IRow TimeRow => timeRow;

        /// <summary>
        /// 包含文本值数据的行属性
        /// </summary>
        public IRow TextRow => textRow;

        /// <summary>
        /// 包含空数据的行属性
        /// </summary>
        public IRow EmptyRow => emptyRow;

        /// <summary>
        /// 通过公式计算得到布尔值的行属性
        /// </summary>
        public IRow FBoolRow => fBoolRow;

        /// <summary>
        /// 通过公式计算得到数字值的行属性
        /// </summary>
        public IRow FNumRow => fNumRow;

        /// <summary>
        /// 通过公式计算得到日期值的行属性
        /// </summary>
        public IRow FDateRow => fDateRow;

        /// <summary>
        /// 通过公式计算得到错误值的行对象
        /// </summary>
        public IRow FErrorRow => fErrorRow;

        /// <summary>
        /// 通过公式计算得到字符串值的行对象
        /// </summary>
        public IRow FStringRow => fStringRow;

        /// <summary>
        /// 销毁资源
        /// </summary>
        public void Dispose()
        {
            workbook.Dispose();
        }
    }
}
