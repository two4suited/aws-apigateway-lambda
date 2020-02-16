using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
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
            var request = new Person(){ FirstName = "Bob",LastName = "Joe"};
            var response = function.AddPerson(request, context);
            
            Assert.Equal(request.ToString(), response.Body);
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