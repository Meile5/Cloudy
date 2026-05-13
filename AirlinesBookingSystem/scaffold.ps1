dotnet ef dbcontext scaffold "Server=localhost,1433;Database=booking-db;User Id=sa;Password=SuperSecret7!;TrustServerCertificate=True;" `
  Microsoft.EntityFrameworkCore.SqlServer `
  --output-dir Models `
  --context-dir Database `
  --context BookingContext `
  --no-onconfiguring `
  --force