using NPOI.SS.UserModel;
using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// 工作表行扩展类测试基类
    /// </summary>
    public abstract class RowExtensionTestBase
    {
        private readonly TestSheetReaderFixtureBase testSheetReaderFixtureBase;
        private readonly ITestOutputHelper outputHelper;

        public RowExtensionTestBase(TestSheetReaderFixtureBase testSheetReaderFixtureBase, ITestOutputHelper outputHelper)
        {
            this.testSheetReaderFixtureBase = testSheetReaderFixtureBase;
            this.outputHelper = outputHelper;
        }

        /// <summary>
        /// 测试当行对象为null时，IsEmpty方法应返回true
        /// </summary>
        public virtual void Test_IsEmpty_WhenRowIsNull()
        {
            IRow? row = null;
            Assert.True(row.IsEmpty());
        }

        /// <summary>
        /// 测试当行对象没有任何单元格时，IsEmpty方法应返回true
        /// </summary>
        public virtual void Test_IsEmpty_WhenRowHasNoCells()
        {
            IRow row = testSheetReaderFixtureBase.EmptyRow;
            Assert.NotNull(row);
            Assert.True(row.IsEmpty());
        }
    }
}
