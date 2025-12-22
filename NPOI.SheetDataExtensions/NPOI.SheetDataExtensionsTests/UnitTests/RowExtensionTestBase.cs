using NPOI.SheetDataExtensions;
using NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures;
using NPOI.SS.UserModel;
using Xunit.Abstractions;

namespace NPOI.SheetDataExtensionsTests.UnitTests
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
