using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;

namespace aws
{
    public class AwsGatewayLambdaInfrastructureStack : Stack
    {
        internal AwsGatewayLambdaInfrastructureStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var getperson = new Function(this,"getperson", new FunctionProps() {
                    Runtime = Runtime.DOTNET_CORE_2_1,
                    FunctionName = "TestAPI_GetPerson",
                    Timeout = Duration.Minutes(1),
                    MemorySize = 128,
                    Code = Code.FromAsset("../aws-gateway-lambda.lambda/bin/Release/netcoreapp2.1/publish"),
                    Handler = "aws-gateway-lambda.lambda::aws_gateway_lambda.lambda.Function::FunctionHandler"
                });
            
            var addperson = new Function(this,"addperson", new FunctionProps() {
                Runtime = Runtime.DOTNET_CORE_2_1,
                FunctionName = "TestAPI_AddPerson",
                Timeout = Duration.Minutes(1),
                MemorySize = 128,
                Code = Code.FromAsset("../aws-gateway-lambda.lambda/bin/Release/netcoreapp2.1/publish"),
                Handler = "aws-gateway-lambda.lambda::aws_gateway_lambda.lambda.Function::AddPerson"
            });

            var api = new RestApi(this, "api", new RestApiProps()
            {
                RestApiName = "TestApi"
            });
            
            api.Root.AddMethod("ANY");
            var root = api.Root.AddResource("{name}");
            
            var getpersonIntegration = new LambdaIntegration(getperson);
            root.AddMethod("GET", getpersonIntegration);
            
            var addpersonintegration = new LambdaIntegration(addperson);
            root.AddMethod("POST", addpersonintegration);
        }
    }
}
