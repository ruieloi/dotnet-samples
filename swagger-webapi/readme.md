# Swagger-WebAPI

Example of Swagger usage in Web API Core 2.0




## FileUpload via Swagger

https://www.janaks.com.np/upload-file-swagger-ui-asp-net-core-web-api/

FileUploadController.

```c#
[HttpPost]
public IActionResult FileUpload2(IFormFile file)
{
    //Do stuff

    return Ok(file.FileName);
}
```

startup.cs
```c#
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
    c.OperationFilter<FileUploadOperation>();
});

...

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
```

FileUploadOperation.cs
```c#
public void Apply(Operation operation, OperationFilterContext context)
{
    if (context.ApiDescription.ParameterDescriptions.Any(x => x.ModelMetadata.ModelType == typeof(IFormFile)))
    {
        operation.Parameters.Clear();//Clearing parameters
        operation.Parameters.Add(new NonBodyParameter
        {
            Name = "File",
            In = "formData",
            Description = "The file to upload.",
            Required = true,
            Type = "file"
        });
        if (!operation.Consumes.Contains(FormDataMimeType))
        {
            operation.Consumes.Add(FormDataMimeType);
        }
    }
}

```


## Links
https://swagger.io/
https://github.com/domaindrivendev/Swashbuckle.AspNetCore
https://www.janaks.com.np/upload-file-swagger-ui-asp-net-core-web-api/
