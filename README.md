## DotNet Enventory Application


LocalDB connection string: "SqlConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Inventory;Integrated Security=SSPI"


## Migration

```c#
dotnet tool install --global dotnet-ef

dotnet ef migrations add categories

dotnet ef database update


```