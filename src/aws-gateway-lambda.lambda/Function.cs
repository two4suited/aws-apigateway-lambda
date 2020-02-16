using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            
            int statusCode = (request != null) ? 
                (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.InternalServerError;

            string body = (request != null) ? 
                JsonConvert.SerializeObject(request.Body) : string.Empty;
         
            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = body.ToUpper(),
                Headers = new Dictionary<string, string>
                { 
                    { "Content-Type", "application/json" }, 
                    { "Access-Control-Allow-Origin", "*" } 
                }
            };
    
            return response;
        }
    }
}
