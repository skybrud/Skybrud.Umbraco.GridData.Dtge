@echo off
dotnet build src/Skybrud.Umbraco.GridData.Dtge --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget