using System;
using System.Collections.Generic;
using System.Linq;
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
            request.Body = "hello world";
            
            var upperCase = function.FunctionHandler(request, context);
            
            Assert.Equal(JsonConvert.SerializeObject("HELLO WORLD"), upperCase.Body);
        }
    }
}
