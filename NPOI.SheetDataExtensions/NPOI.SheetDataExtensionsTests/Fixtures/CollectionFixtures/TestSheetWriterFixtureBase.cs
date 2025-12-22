using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures
{
    /// <summary>
    /// 测试工作表写入器基类
    /// </summary>
    public abstract class TestSheetWriterFixtureBase : IDisposable
    {
        /// <summary>
        /// Excel文件对象
        /// </summary>
        protected readonly IWorkbook workbook;
        /// <summary>
        /// 公式评估器对象
        /// </summary>
        private readonly IFormulaEvaluator formulaEvaluator;
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet1;
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet2;
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet3;
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet4;
        /// <summary>
        /// 工作表对象
        /// </summary>
        private readonly ISheet sheet5;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workbook"></param>
        public TestSheetWriterFixtureBase(IWorkbook workbook)
        {
            this.workbook = workbook;
            this.formulaEvaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);

            this.sheet1 = workbook.CreateSheet("测试设置单元格扩展方法");
            this.sheet2 = workbook.CreateSheet("测试写入可选参数为默认值时导出");
            this.sheet3 = workbook.CreateSheet("测试写入首行索引与首列索引大于0");
            this.sheet4 = workbook.CreateSheet("测试写入不包含头部");
            this.sheet5 = workbook.CreateSheet("测试写入设置样式");
        }

        /// <summary>
        /// Excel文件对象属性
        /// </summary>
        public IWorkbook Workbook => workbook;

        /// <summary>
        /// 公式评估器对象属性
        /// </summary>
        public IFormulaEvaluator FormulaEvaluator => formulaEvaluator;

        /// <summary>
        /// 工作表对象属性
        /// </summary>
        public ISheet Sheet1 => sheet1;

        /// <summary>
        /// 工作表对象属性
        /// </summary>
        public ISheet Sheet2 => sheet2;

        /// <summary>
        /// 工作表对象属性
        /// </summary>
        public ISheet Sheet3 => sheet3;

        /// <summary>
        /// 工作表对象属性
        /// </summary>
        public ISheet Sheet4 => sheet4;

        /// <summary>
        /// 工作表对象属性
        /// </summary>
        public ISheet Sheet5 => sheet5;

        /// <summary>
        /// 存储文件
        /// </summary>
        protected abstract void Save();

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            /**
             * Save函数在此被调用不算是很合适的,
             * 但是在xUnit中, 构造函数代表执行开始, Dispose代表执行结束,
             * 因此放置于此, 在所有的单元测试完成之后调用Dispose函数时触发保存, 从而避免在每个单元测试中重复保存文件
             */
            Save();

            //释放资源
            workbook.Dispose();
        }
    }
}
