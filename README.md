根据项目代码结构和组件信息，以下是适用于该项目的 README.md 内容：

---

# NPOI.SheetDataExtensions

`NPOI.SheetDataExtensions` 是一个基于 [NPOI](https://github.com/npoi/NPOI) 的扩展库，用于简化 Excel 文件中工作表数据的读写操作。该项目提供了一系列扩展方法，使开发者能够更方便地处理 Excel 表格数据，包括单元格读写、行读取、工作表写入、创建工作簿等操作。

## 特性

- **单元格操作**：扩展 `ICell` 类型，支持从单元格中读取值以及向单元格写入值。
- **行操作**：判断某一行是否为空。
- **工作表读取**：将 Excel 工作表读取为 `DataTable`，支持指定列配置。
- **工作表写入**：将 `DataTable` 数据写入 Excel 工作表，支持自定义样式。
- **创建工作簿**：支持创建 `.xls` 和 `.xlsx` 格式的工作簿。

## 项目结构

- **CellExtension.cs**：提供 `ICell` 的扩展方法，用于读写单元格数据。
- **RowExtension.cs**：提供 `IRow` 的扩展方法，用于判断行是否为空。
- **SheetReader.cs**：将 Excel 工作表读取为 `DataTable`。
- **SheetWriter.cs**：将 `DataTable` 写入 Excel 工作表。
- **WorkbookCreater.cs**：创建工作簿（支持 `.xls` 和 `.xlsx` 格式）。
- **测试文件**：涵盖所有核心功能的单元测试，支持 `.xls` 和 `.xlsx` 格式。

## 使用示例

### 创建工作簿

```csharp
IWorkbook workbook = WorkbookCreater.Create(ExcelType.HSSF); // 创建 .xls 文件
IWorkbook workbookXlsx = WorkbookCreater.Create(ExcelType.XSSF); // 创建 .xlsx 文件
```

### 读取工作表数据

```csharp
ISheet sheet = workbook.GetSheetAt(0);
SheetReader reader = new SheetReader(sheet);
DataTable dataTable = reader.Read(rowsCount: 10, firstRowIndex: 0, hasHeader: true);
```

### 写入工作表数据

```csharp
ISheet sheet = workbook.CreateSheet("Sheet1");
SheetWriter writer = new SheetWriter(sheet);
writer.Write(dataTable, firstRowIndex: 0, firstColIndex: 0, hasHeader: true);
```

### 单元格操作

```csharp
ICell cell = row.CreateCell(0);
cell.SetValue("Hello, World!");

object value = cell.GetValue();
```

### 行操作

```csharp
IRow row = sheet.GetRow(0);
bool isEmpty = row.IsEmpty(); // 判断行是否为空
```

## 单元测试

项目包含完整的单元测试，覆盖 `.xls` 和 `.xlsx` 格式的读写操作，确保功能的稳定性和可靠性。

- 使用 `xUnit` 测试框架。
- 提供测试夹具用于 `.xls` 和 `.xlsx` 文件的测试。
- 支持参数化测试，验证多种输入场景。

## 依赖项

- [NPOI](https://github.com/npoi/NPOI)：用于操作 Excel 文件的核心库。

## 许可证

本项目采用 MIT 许可证。详情请参阅 [LICENSE](LICENSE) 文件。

## 贡献

欢迎贡献代码和提交 Issue。请确保提交的代码通过单元测试，并遵循项目编码规范。

---

该 README 提供了项目的基本介绍、功能特性、使用示例及测试信息，适合开发者快速了解和使用该库。