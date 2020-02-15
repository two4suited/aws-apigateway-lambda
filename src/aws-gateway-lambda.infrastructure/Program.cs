﻿using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwsGatewayLambdaInfrastructure
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new AwsGatewayLambdaInfrastructureStack(app, "AwsGatewayLambdaInfrastructureStack");
            app.Synth();
        }
    }
}
