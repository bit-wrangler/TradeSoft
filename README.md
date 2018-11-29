Recommended to get the latest .NET Core SDK. (2.1.5 Used)

Structure:

src/Core/Domain/Entities - Domain entities: Transaction, Fund, Investor, SalesPerson

src/Core/Domain/Repositories - Repository interfaces: ITransactionRepository

src/Core/Application - Business app logic: TransactionReporter

src/Core/Application/Models - Business app logic models: SalesPersonSalesSummary, SalesPersonAUMSummary, InvestorShareImbalance, InvestorProfit, FIFOProfitCalculator

src/Core/Application/Utilities - Business app logic utilities / static tools: ToDatePeriodUtility

src/Infrastructure/Repositories/CSVTransactionRepository - CSV based implementation of the repository/data source for the app

src/Presentation/TradeSoftConsole - basic UI app to demonstrate the Application logic

test/manual - sandbox area to mess around in, not considered as part of the solution

test/auto/Core/Application.Test - some basic unit tests to check the app logic


The assumption is that this app would expand to some database driven solution. So the Fund, Investor, and SalesPerson domain entities were created instead of just having plain string properties in the Transaction entity.

Usage:
TradeSoftConsole.exe PATH_TO_TRANSACTIONS_CSV

It is assumed that the commands below are executed in the root folder of the repository (same level as the .sln file)

To test:
dotnet test

To build:
dotnet build

To publish an executable:
dotnet publish -c Release -r win-x64 src\Presentation\TradeSoftConsole\TradeSoftConsole.csproj

To run the built binary on the test CSV file:
bin\TradeSoftConsole.exe assets\Data.csv