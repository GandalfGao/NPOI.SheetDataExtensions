using NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOI.SheetDataExtensionsTests.UnitTests
{
    /// <summary>
    /// xls文件工作表行扩展类测试
    /// </summary>
    [Collection(nameof(TestSheetXlsFixturesCollection))]
    public class RowExtensionXlsTest : RowExtensionTestBase
    {
        public RowExtensionXlsTest(TestSheetReaderXlsFixture testSheetReaderXlsFixture, ITestOutputHelper outputHelper) : base(testSheetReaderXlsFixture, outputHelper)
        { }

        /// <summary>
        /// 测试当行对象没有任何单元格时，IsEmpty方法应返回true
        /// </summary>
        [Fact]
        public override void Test_IsEmpty_WhenRowHasNoCells()
        {
            base.Test_IsEmpty_WhenRowHasNoCells();
        }

        /// <summary>
        /// 测试当行对象为null时，IsEmpty方法应返回true
        /// </summary>
        [Fact]
        public override void Test_IsEmpty_WhenRowIsNull()
        {
            base.Test_IsEmpty_WhenRowIsNull();
        }
    }
}
