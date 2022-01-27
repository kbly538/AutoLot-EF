using ExportDataToOfficeApp;
using Excel = Microsoft.Office.Interop.Excel;

List<Car> carsInStock = new List<Car>
{
	new Car { Color="Green", Make="VW", PetName="Mary"},
	new Car { Color="Red", Make="Saab", PetName="Mel"},
	new Car { Color="Black", Make="Ford", PetName="Hank"},
	new Car { Color="yellow", Make="BMW", PetName="Davie"}
};

void ExportToExcel(List<Car> stocks)
{
	Excel.Application application = new Excel.Application();

	application.Visible = true;

	application.Workbooks.Add();

	Excel._Worksheet workSheet = (Excel._Worksheet)application.ActiveSheet;

	workSheet.Cells[1, "A"] = "Make";
	workSheet.Cells[1, "B"] = "Color";
	workSheet.Cells[1, "C"] = "Pet Name";

	int row = 1;
	foreach (Car car in stocks)
	{
		row++;
		workSheet.Cells[row, "A"] = car.Make;
		workSheet.Cells[row, "B"] = car.Color;
		workSheet.Cells[row, "C"] = car.PetName;
	}

	workSheet.Range["A1"].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);


	workSheet.SaveAs($@"{Environment.CurrentDirectory}\Inventory.xlsx");

	application.Quit();
	Console.WriteLine("File saved.");
}

ExportToExcel(carsInStock);