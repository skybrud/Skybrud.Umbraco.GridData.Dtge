@echo off
dotnet build src/Skybrud.Umbraco.GridData.Dtge --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:\nuget\Umbraco10