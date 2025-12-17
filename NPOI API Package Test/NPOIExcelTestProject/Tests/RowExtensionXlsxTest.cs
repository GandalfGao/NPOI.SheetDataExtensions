using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// xlsx文件工作表行扩展类测试
    /// </summary>
    [Collection(nameof(TestSheetXlsxFixturesCollection))]
    public class RowExtensionXlsxTest : RowExtensionTestBase
    {
        public RowExtensionXlsxTest(TestSheetReaderXlsxFixture testSheetReaderXlsxFixture, ITestOutputHelper outputHelper) : base(testSheetReaderXlsxFixture, outputHelper)
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
