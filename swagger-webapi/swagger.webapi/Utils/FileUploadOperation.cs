using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace swagger.webapi.Utils
{
    public class FileUploadOperation : IOperationFilter
    {
        //old swagger verison
        //public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        //{
        //    if (operation.OperationId.ToLower().Contains("fileupload")) //will add the File upload button in UI for every operation that contains the name "fileupload"
        //    {
        //        operation.Consumes.Add("multipart/form-data");
        //        operation.Parameters.Add(
        //            new BodyParameter
        //            {
        //                name = "file",
        //                @in = "formData",
        //                required = true,
        //                type = "file"
        //            });
        //    }
        //}

        private const string FormDataMimeType = "multipart/form-data";
        private static readonly string[] FormFilePropertyNames =
            typeof(IFormFile).GetTypeInfo().DeclaredProperties.Select(x => x.Name).ToArray();

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
    }
}
