using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Newtonsoft.Json;
using Xunit;

namespace aws_gateway_lambda.lambda.Tests
{
    public class AddPersonTest
    {
        [Fact]
        public void ValidRequestReturnFullName()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var person = new Person(){ FirstName = "Bob",LastName = "Joe"};
            var request = new APIGatewayProxyRequest {Body = JsonConvert.SerializeObject(person)};
            var response = function.AddPerson(request, context);
            
            
            Assert.Equal(person.ToString(), response.Body);
        }
        
        [Fact]
        public void NullPersonReturnBadRequest()
        {
            var function = new Function();
            var context = new TestLambdaContext();
          
            var response = function.AddPerson(null, context);
            
            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}