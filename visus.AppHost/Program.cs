using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithDataVolume(isReadOnly: false);
var postgresdb = postgres.AddDatabase("postgresdb");

var apiService = builder.AddProject<Projects.visus_ApiService>("apiservice").WithReference(postgresdb)
    .WithExternalHttpEndpoints();

builder.AddProject<visus_MigrationService>("migrations")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddNpmApp("webfrontend", "../visus.Frontend")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithEnvironment("services__api__http__0", apiService.GetEndpoint("http"))
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();