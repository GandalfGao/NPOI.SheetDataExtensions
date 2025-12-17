using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// 工作表写入测试基类
    /// </summary>
    public abstract class SheetWriterTestBase
    {
        private readonly TestSheetWriterFixtureBase testSheetWriterFixtureBase;
        private readonly ITestOutputHelper outputHelper;

        public SheetWriterTestBase(TestSheetWriterFixtureBase testSheetWriterFixtureBase, ITestOutputHelper outputHelper)
        {
            this.testSheetWriterFixtureBase = testSheetWriterFixtureBase;
            this.outputHelper = outputHelper;
        }
    }
}
