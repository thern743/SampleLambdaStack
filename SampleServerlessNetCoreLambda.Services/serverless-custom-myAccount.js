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
        resourceFolder: {
            prod: "Prod",
            default: "Default"
        },
        accountId: {
            // Change these to the correct account #s
            prod: "765473857789",
            default: "960522034815"
        },
        securityGroupName: "SampleSecurityGroup-${opt:stage, self:provider.stage}",
        vpcIds: {
            // Change these to the correct VPCs (if applicapable)
            prod: "vpc-36873150",
            default: "vpc-f29e2894"
        },
        subnetIds: {
            // Change these to the correct Subnets (if applicapable)
            prod: ["subnet-137f655a", "subnet-ba8c4cdc"],
            default: ["subnet-6b83430d", "subnet-20475d69"]
        },
        myEnvVariable: {
            prod: "ProdValueHere",
            default: "NonProdValueHere"
        },
        kms: {
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