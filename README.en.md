# NPOI.SheetDataExtensions

`NPOI.SheetDataExtensions` is an extension library built on top of [NPOI](https://github.com/npoi/NPOI) to simplify reading and writing worksheet data in Excel files. This project provides a set of extension methods that enable developers to more easily handle Excel table data, including cell read/write operations, row reading, worksheet writing, and workbook creation.

## Features

- **Cell Operations**: Extends the `ICell` type to support reading values from and writing values to cells.
- **Row Operations**: Determines whether a row is empty.
- **Worksheet Reading**: Reads an Excel worksheet into a `DataTable`, with support for column configuration.
- **Worksheet Writing**: Writes `DataTable` data to an Excel worksheet, with support for custom styling.
- **Workbook Creation**: Supports creating workbooks in both `.xls` and `.xlsx` formats.

## Project Structure

- **CellExtension.cs**: Provides extension methods for `ICell` to read and write cell data.
- **RowExtension.cs**: Provides extension methods for `IRow` to determine if a row is empty.
- **SheetReader.cs**: Reads an Excel worksheet into a `DataTable`.
- **SheetWriter.cs**: Writes `DataTable` data to an Excel worksheet.
- **WorkbookCreater.cs**: Creates workbooks (supports `.xls` and `.xlsx` formats).
- **Test Files**: Comprehensive unit tests covering all core functionalities, supporting both `.xls` and `.xlsx` formats.

## Usage Examples

### Create a Workbook

```csharp
IWorkbook workbook = WorkbookCreater.Create(ExcelType.HSSF); // Create .xls file
IWorkbook workbookXlsx = WorkbookCreater.Create(ExcelType.XSSF); // Create .xlsx file
```

### Read Worksheet Data

```csharp
ISheet sheet = workbook.GetSheetAt(0);
SheetReader reader = new SheetReader(sheet);
DataTable dataTable = reader.Read(rowsCount: 10, firstRowIndex: 0, hasHeader: true);
```

### Write Worksheet Data

```csharp
ISheet sheet = workbook.CreateSheet("Sheet1");
SheetWriter writer = new SheetWriter(sheet);
writer.Write(dataTable, firstRowIndex: 0, firstColIndex: 0, hasHeader: true);
```

### Cell Operations

```csharp
ICell cell = row.CreateCell(0);
cell.SetValue("Hello, World!");

object value = cell.GetValue();
```

### Row Operations

```csharp
IRow row = sheet.GetRow(0);
bool isEmpty = row.IsEmpty(); // Check if row is empty
```

## Unit Tests

The project includes comprehensive unit tests covering read/write operations for both `.xls` and `.xlsx` formats, ensuring stability and reliability.

- Uses the `xUnit` testing framework.
- Provides test fixtures for `.xls` and `.xlsx` file testing.
- Supports parameterized tests to validate multiple input scenarios.

## Dependencies

- [NPOI](https://github.com/npoi/NPOI): Core library for Excel file manipulation.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contributing

Contributions and issue submissions are welcome. Please ensure your code passes all unit tests and follows the project's coding standards.