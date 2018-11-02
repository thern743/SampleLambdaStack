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
    serverless.cli.consoleLog("Using Options: " + JSON.stringify(serverless.variables.options));
    
    var results = {
        stage: "${opt:stage, self:provider.stage}",
        profile: {
            dev: "default",
            test: "",
            prod: "",
            dummyprod: "",
            localdev: "specify-a-stage"
        },
        accountId: {
            prod: "765473857789",
            default: "960522034815"
        },
        role: {
            name: "SampleServerlessNetCoreLambdaRole",
            path: "/SampleServerlessNetCoreLambdaRole/"
        },
        kms: {
            alias: "SampleServerlessNetCoreKmsKey",
            keyId: "SampleServerlessNetCoreKmsKey"
        }
    };

    serverless.cli.consoleLog("Using custom values: " + JSON.stringify(results));

    return results;
}
