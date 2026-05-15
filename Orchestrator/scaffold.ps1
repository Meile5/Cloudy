dotnet ef dbcontext scaffold "Server=localhost,1434;Database=saga-db;User Id=sa;Password=SuperSecret7!;TrustServerCertificate=True;" `
  Microsoft.EntityFrameworkCore.SqlServer `
  --output-dir Models `
  --context-dir Database `
  --context SagaContext `
  --no-onconfiguring `
  --force