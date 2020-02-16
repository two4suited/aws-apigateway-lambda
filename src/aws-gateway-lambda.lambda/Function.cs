using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace aws_gateway_lambda.lambda
{
    public class Function
    {
        
        
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (request == null ) return new APIGatewayProxyResponse(){ StatusCode = (int)HttpStatusCode.InternalServerError};
            if(request.PathParameters == null) return new APIGatewayProxyResponse(){ StatusCode = (int)HttpStatusCode.BadRequest};
            if(!request.PathParameters.ContainsKey("name")) return new APIGatewayProxyResponse(){ StatusCode = (int)HttpStatusCode.NotFound};
            
            var name =request.PathParameters["name"];
            
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = name?.ToUpper(),
                Headers = new Dictionary<string, string>
                { 
                    { "Content-Type", "application/json" }, 
                    { "Access-Control-Allow-Origin", "*" } 
                }
            };
        }

        public APIGatewayProxyResponse AddPerson(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (request == null ) return new APIGatewayProxyResponse(){ StatusCode = (int)HttpStatusCode.BadRequest};

            var person = JsonConvert.DeserializeObject<Person>(request.Body);
            
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = person.ToString(),
                Headers = new Dictionary<string, string>
                { 
                    { "Content-Type", "application/json" }, 
                    { "Access-Control-Allow-Origin", "*" } 
                }
            };
        }
    }
}
