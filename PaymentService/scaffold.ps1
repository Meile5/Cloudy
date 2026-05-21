dotnet ef dbcontext scaffold "Server=localhost,1435;Database=payment-db;User Id=sa;Password=SuperSecret7!;TrustServerCertificate=True;" `
  Microsoft.EntityFrameworkCore.SqlServer `
  --output-dir Models `
  --context-dir Database `
  --context PaymentContext `
  --no-onconfiguring `
  --force