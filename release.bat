@echo off
"src/build/Skybrud.Build.exe"
cd src/Skybrud.Umbraco.GridData.Dtge
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe" /t:pack /p:Configuration=Release /p:PackageOutputPath=../../releases/nuget
cd ../../