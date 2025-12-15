using NPOI_API_Package;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject
{
    /// <summary>
    /// 参数类
    /// </summary>
    public static class ParamsData
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
        /// 是否包含头部信息及列配置参数集合参数
        /// </summary>
        public static TheoryData<bool, IEnumerable<ColumnConfigAttribute>> HasHeaderAndColumnConfigsParam { get; } = new()
        {
            { 
                false, 
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
            },
            { 
                true,
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
            }
        };
    }
}
