/* Available properties of 'serverless' object:
 *      "providers",
 *      "version",
 *      "yamlParser",
 *      "utils",
 *      "service",
 *      "variables",
 *      "pluginManager",
 *      "config",
 *      "classes",
 *      "serverlessDirPath",
 *      "invocationId",
 *      "cli",
 *      "processedInput"
 */
module.exports = (serverless) => {
    serverless.cli.consoleLog("\r\nUsing Options: " + JSON.stringify(serverless.variables.options));

    var results = {
        stage: "${opt:stage, self:provider.stage}",
        profile: {
            dev: "",
            test: "",
            prod: "",
            dummyprod: "",
            localdev: "specify-a-stage"
        },
        accountId: {
            // Change these to the correct account #s
            prod: "123456789",
            default: "987654321"
        },
        securityGroupName: "SampleSecurityGroup-${opt:stage, self:provider.stage}",
        vpcIds: {
            // Change these to the correct VPCs (if applicapable)
            prod: "vpc-123456789",
            default: "vpc-987654321"
        },
        subnetIds: {
            // Change these to the correct Subnets (if applicapable)
            prod: ["subnet-123456789", "subnet-123456789"],
            default: ["subnet-987654321", "subnet-987654321"]
        },
        kms: {
            // This comes from the SampleServerlessNetCoreLambdaRole project
            arn: "${cf:SampleServerlessNetCoreLambdaRole-${self:custom.kms.stage.${self:custom.stage}, self:custom.kms.stage.default}.SampleServerlessNetCoreKmsKeyArn}",
            stage: {
                prod: "prod",
                default: "dev"
            }
        }
    };

    serverless.cli.consoleLog("Using custom values: " + JSON.stringify(results));
    
    return results;
}