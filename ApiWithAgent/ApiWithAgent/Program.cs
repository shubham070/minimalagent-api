using ApiWithAgent.AI.AIAgents;
using ApiWithAgent.AI.AIWorkflows;
using Microsoft.Agents.AI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

string token = Environment.GetEnvironmentVariable("GH_TOKEN")
	?? throw new InvalidOperationException("GH_TOKEN is not set.");

builder.Configuration["ConnectionStrings:chat"] = $"Endpoint=https://models.github.ai/inference;Key={token}";

builder.AddOpenAIClient("chat")
    .AddChatClient("gpt-4o-mini");

builder.Services.AddSingleton<WorkflowFactory>();
builder.Services.AddSingleton<Agents>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}


app.MapGet("/agent/chat", async (Agents agents, string prompt) =>
{
	AgentResponse response = await agents.RunWorkflowAsync(prompt);
	return Results.Ok(response);
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
