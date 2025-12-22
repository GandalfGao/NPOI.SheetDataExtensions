using NPOI.SS.UserModel;
using NPOI_API_Package;
using NPOI.SS.Util;
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
        private static readonly Action<ISheet> setSheetStyleFunc;

        static ParamsData()
        {
            setSheetStyleFunc = (sheet) =>
            {
                //设置合并
                sheet.AddMergedRegion(new CellRangeAddress(0, 0, 4, 8));
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 0));
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 1, 1));
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 2, 2));
                sheet.AddMergedRegion(new CellRangeAddress(0, 1, 3, 3));
                sheet.AddMergedRegion(new CellRangeAddress(2, 4, 0, 0));
                sheet.AddMergedRegion(new CellRangeAddress(5, 7, 0, 0));

                var workbook = sheet.Workbook;

                var cellStyle = workbook.CreateCellStyle();
                //设置文字位置居中
                cellStyle.Alignment = HorizontalAlignment.Center;
                cellStyle.VerticalAlignment = VerticalAlignment.Center;
                //设置文字字体
                var font = workbook.CreateFont();
                font.FontName = "华文行楷";
                font.FontHeight = 20 * 20;
                cellStyle.SetFont(font);
                //设置单元格边框
                cellStyle.BorderTop = BorderStyle.Thin;     // 上边框
                cellStyle.BorderRight = BorderStyle.Thin;   // 右边框
                cellStyle.BorderBottom = BorderStyle.Thin;  // 下边框
                cellStyle.BorderLeft = BorderStyle.Thin;    // 左边框

                var headerCellStyle = workbook.CreateCellStyle();
                //设置文字位置居中
                headerCellStyle.Alignment = HorizontalAlignment.Center;
                headerCellStyle.VerticalAlignment = VerticalAlignment.Center;
                //设置文字字体
                var headerFont = workbook.CreateFont();
                headerFont.FontName = "华文行楷";
                headerFont.FontHeight = 20 * 20;
                headerFont.IsBold = true;
                headerCellStyle.SetFont(headerFont);
                //设置单元格边框
                headerCellStyle.BorderTop = BorderStyle.Thin;     // 上边框
                headerCellStyle.BorderRight = BorderStyle.Thin;   // 右边框
                headerCellStyle.BorderBottom = BorderStyle.Thin;  // 下边框
                headerCellStyle.BorderLeft = BorderStyle.Thin;    // 左边框

                var rowEnumerator = sheet.GetEnumerator();
                if (rowEnumerator.MoveNext())
                {
                    var firstRow = (IRow)rowEnumerator.Current;
                    foreach (var cell in firstRow)
                    {
                        cell.CellStyle = headerCellStyle;
                    }
                }

                while (rowEnumerator.MoveNext())
                { 
                    var row = (IRow)rowEnumerator.Current;
                    foreach (var cell in row)
                    {
                        cell.CellStyle = cellStyle;
                    }
                }

                //设置列宽
                int colIndex = 0;
                sheet.SetColumnWidth(colIndex++, 20 * 256);
                sheet.SetColumnWidth(colIndex++, 15 * 256);
                sheet.SetColumnWidth(colIndex++, 15 * 256);
                sheet.SetColumnWidth(colIndex++, 15 * 256);
                sheet.SetColumnWidth(colIndex++, 15 * 256);
                sheet.SetColumnWidth(colIndex++, 15 * 256);
                sheet.SetColumnWidth(colIndex++, 15 * 256);
                sheet.SetColumnWidth(colIndex++, 15 * 256);
                sheet.SetColumnWidth(colIndex++, 25 * 256);
            };
        }

        /// <summary>
        /// 列配置为空的参数集合
        /// </summary>
        public static TheoryData<IEnumerable<ColumnConfig>?> EmptyColumnConfigParams { get; } =
        [
            null,
            []
        ];

        /// <summary>
        /// 是否包含头部信息及列配置参数集合参数
        /// </summary>
        public static TheoryData<bool, IEnumerable<ColumnConfig>> HasHeaderAndColumnConfigsParam { get; } = new()
        {
            { 
                false, 
                [
                    new ColumnConfig
                    {
                        ColumnIndex = 1,
                        ColumnMapping = "序号",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 2,
                        ColumnMapping = "姓名",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 3,
                        ColumnMapping = "年龄",
                    },
                ]
            },
            { 
                true,
                [
                    new ColumnConfig
                    {
                        ColumnIndex = 1,
                        ColumnMapping = "序号",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 2,
                        ColumnMapping = "姓名",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 3,
                        ColumnMapping = "年龄",
                    },
                ]
            }
        };

        /// <summary>
        /// 是否包含头部信息及列配置参数集合参数(包含空列和空行)
        /// </summary>
        public static TheoryData<bool, IEnumerable<ColumnConfig>> HasHeaderAndColumnConfigsParam_WithHasBlankRowsAndCols { get; } = new()
        {
            {
                false,
                [
                    new ColumnConfig
                    {
                        ColumnIndex = 1,
                        ColumnMapping = "序号",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 3,
                        ColumnMapping = "姓名",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 5,
                        ColumnMapping = "年龄",
                    },
                ]
            },
            {
                true,
                [
                    new ColumnConfig
                    {
                        ColumnIndex = 1,
                        ColumnMapping = "序号",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 3,
                        ColumnMapping = "姓名",
                    },
                    new ColumnConfig
                    {
                        ColumnIndex = 5,
                        ColumnMapping = "年龄",
                    },
                ]
            }
        };

        /// <summary>
        /// 写入工作表的相关参数(sheet索引值, 首行索引, 首列索引, 是否包含头部, 工作表风格时间提供器)
        /// </summary>
        public static TheoryData<int, int, int, bool, Action<ISheet>?> WriteToSheetParam => new()
        {
            { 2, 0, 0, true, null },
            { 3, 2, 3, true, null },
            { 4, 0, 0, false, null },
            { 5, 0, 0, true, setSheetStyleFunc }
        };
    }
}
