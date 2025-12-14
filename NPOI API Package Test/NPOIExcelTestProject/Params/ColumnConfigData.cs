using NPOI_API_Package;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NPOIExcelTestProject.Params
{
    /// <summary>
    /// 列配置参数类
    /// </summary>
    public static class ColumnConfigData
    {
        /// <summary>
        /// 列配置为空的参数集合
        /// </summary>
        public static TheoryData<IEnumerable<ColumnConfigAttribute>?> EmptyColumnConfigParams { get; } =
        [
            null,
            []
        ];

        /// <summary>
        /// 列配置非空的参数集合
        /// </summary>
        public static TheoryData<IEnumerable<ColumnConfigAttribute>> ColumnConfigParams { get; } =
        [
            [
                new ColumnConfigAttribute
                { 
                    ColumnIndex = 1,
                    ColumnMapping = "序号",
                },
                new ColumnConfigAttribute
                {
                    ColumnIndex = 2,
                    ColumnMapping = "姓名",
                },
                new ColumnConfigAttribute
                {
                    ColumnIndex = 3,
                    ColumnMapping = "年龄",
                },
            ]
        ];
    }
}
