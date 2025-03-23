var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.visus_ApiService>("apiservice")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("webfrontend", "../visus.Frontend")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithEnvironment("services__api__http__0", apiService.GetEndpoint("http"))
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();