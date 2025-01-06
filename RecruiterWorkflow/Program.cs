using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenAI.Chat;
using RecruiterWorkflow.Data;
using RecruiterWorkflow.Services;
using static System.Environment;

var builder = WebApplication.CreateBuilder(args);

var azureAIUrl = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? "Not Found";
var deploymentName = GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME") ?? "Not Found";
var credential = GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("API key is required.");

var azureClient = new AzureOpenAIClient(new Uri(azureAIUrl), new AzureKeyCredential(credential));
var chatClient = azureClient.GetChatClient(deploymentName);

builder.Services.AddSingleton(chatClient);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RecruiterWorkflowContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
);

builder.Services.AddScoped<AIService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
