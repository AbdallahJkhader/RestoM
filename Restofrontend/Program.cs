var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Serve static files (HTML, CSS, JS)
app.UseDefaultFiles();  // Serves index.html by default
app.UseStaticFiles();   // Serves files from wwwroot

app.Run();
