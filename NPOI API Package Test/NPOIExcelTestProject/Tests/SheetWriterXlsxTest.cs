using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// 工作表写入器测试类
    /// </summary>
    [Collection(nameof(TestSheetXlsxFixturesCollection))]
    public class SheetWriterXlsxTest : SheetWriterTestBase
    {
        public SheetWriterXlsxTest(TestSheetWriterXlsxFixture testSheetWriterXlsxFixture, ITestOutputHelper outputHelper) :
            base(testSheetWriterXlsxFixture, outputHelper)
        { }

        /// <summary>
        /// 当DataTable是null时, 应抛出ArgumentNullException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenDataTableIsNull()
        {
            base.Test_Write_WhenDataTableIsNull();
        }

        /// <summary>
        /// 当DataTable列为空时, 应抛出ArgumentException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenDataColumnCountIsZero()
        {
            base.Test_Write_WhenDataColumnCountIsZero();
        }

        /// <summary>
        /// 当首行索引值为负数的时候, 应抛出ArgumentException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenFirstRowIndexIsNegative()
        {
            base.Test_Write_WhenFirstRowIndexIsNegative();
        }

        /// <summary>
        /// 当首列索引值为负数的时候, 应抛出ArgumentException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenFirstColIndexIsNegative()
        {
            base.Test_Write_WhenFirstColIndexIsNegative();
        }
    }
}
