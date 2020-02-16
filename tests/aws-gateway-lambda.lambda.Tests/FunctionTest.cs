using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using aws_gateway_lambda.lambda;
using Newtonsoft.Json;

namespace aws_gateway_lambda.lambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest();
            request.PathParameters = new Dictionary<string, string> { { "name", "hello world" }};
            
            var upperCase = function.FunctionHandler(request, context);
            
            Assert.Equal("HELLO WORLD", upperCase.Body);
        }

        [Fact]
        public void NullRequestReturnsInternalServerError()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var response = function.FunctionHandler(null, context);
            
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);
        }
        
        [Fact]
        public void NoPathParameterReturnsBadRequest()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest();
            request.PathParameters = null;
            var response = function.FunctionHandler(request, context);
            
            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public void NoPathParameterMatchingNameReturnsNotFound()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest();
            request.PathParameters = new Dictionary<string, string> { { "test", "hello world" }};;
            var response = function.FunctionHandler(request, context);
            
            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }
        
    }
}
