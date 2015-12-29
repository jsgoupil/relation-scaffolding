tools\nuget\nuget.exe restore RelationScaffolding.sln

set MSBUILD="C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"

rd /S /Q .\src\RelationScaffolding\bin\Debug
rd /S /Q .\src\RelationScaffolding\bin\Release

%MSBUILD% RelationScaffolding.sln /p:Configuration="Debug"
if %errorlevel% neq 0 exit /b %errorlevel%

%MSBUILD% RelationScaffolding.sln /p:Configuration="Release"
if %errorlevel% neq 0 exit /b %errorlevel%