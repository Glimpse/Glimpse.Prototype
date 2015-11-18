@echo off

del /Q /S dist
del /Q /S src\Glimpse.Common\bin\Release
del /Q /S src\Glimpse.Server\bin\Release
del /Q /S src\Glimpse.Agent.AspNet\bin\Release
del /Q /S src\Glimpse.Agent.AspNet.Mvc\bin\Release

md dist

call dnu restore .\src\Glimpse.Common\project.json .\src\Glimpse.Server\project.json .\src\Glimpse.Agent.AspNet\project.json .\src\Glimpse.Agent.AspNet.Mvc\project.json

REM get time
For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c%%a%%b)
For /f "tokens=1-2 delims=/:" %%a in ("%TIME%") do (set mytime=%%a%%b)

set DNX_BUILD_VERSION=beta1

call dnu pack .\src\Glimpse.Common\project.json .\src\Glimpse.Server\project.json .\src\Glimpse.Agent.AspNet\project.json .\src\Glimpse.Agent.AspNet.Mvc\project.json --configuration Release

call nuget pack src\Glimpse\Glimpse.nuspec -OutputDirectory dist -version 2.0.0-%DNX_BUILD_VERSION%

copy /Y src\Glimpse.Common\bin\Release\*.nupkg dist
copy /Y src\Glimpse.Server\bin\Release\*.nupkg dist
copy /Y src\Glimpse.Agent.AspNet\bin\Release\*.nupkg dist
copy /Y src\Glimpse.Agent.AspNet.Mvc\bin\Release\*.nupkg dist