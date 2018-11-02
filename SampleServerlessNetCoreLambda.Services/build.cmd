dotnet restore ..\SampleServerlessNetCoreLambda.sln
dotnet lambda package --configuration release --framework netcoreapp2.1 --output-package bin/deploy/deploy-package.zip
