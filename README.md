# Stock Exchange

## Instructions to Run Application
### 1. Restore All Dependencies 
`dotnet restore StockExchange.App.sln`

### 2. Build the Solution
`dotnet build -o geektrust`

### 3. Test the Application
`dotnet test StockExchange.Test\StockExchange.Test.csproj`
> Note: For Mac/Linux System please use / slash. `dotnet test StockExchange.Test/StockExchange.Test.csproj`

### 4. Run Application
`dotnet StockExchange.App\geektrust\geektrust.dll <absolute_path_to_input_file>` <br><br>
For example - The input file is provide in the solution. `StockExchange.App\Input\input.txt`.<br>
To Run the application using default input file, use the command - `dotnet StockExchange.App\geektrust\geektrust.dll StockExchange.App\Input\input.txt` <br>
> Note: For Mac/Linux System please use / slash. `dotnet StockExchange.App/geektrust/geektrust.dll StockExchange.App/Input/input.txt`

### 5. User Input
User input file stored in `StockExchange.App\Input` folder.

