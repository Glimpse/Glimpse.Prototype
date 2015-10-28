@echo off

del /Q /S dist
del /Q /S src\Glimpse.Common\bin\Release
del /Q /S src\Glimpse.Server\bin\Release
del /Q /S src\Glimpse.Agent.AspNet\bin\Release
del /Q /S src\Glimpse.Agent.AspNet.Mvc\bin\Release

md dist

call dnu restore .\src\Glimpse.Common\project.json .\src\Glimpse.Server\project.json .\src\Glimpse.Agent.AspNet\project.json .\src\Glimpse.Agent.AspNet.Mvc\project.json

set DNX_BUILD_VERSION=beta1

call dnu pack .\src\Glimpse.Common\project.json .\src\Glimpse.Server\project.json .\src\Glimpse.Agent.AspNet\project.json .\src\Glimpse.Agent.AspNet.Mvc\project.json --configuration Release

call nuget pack src\Glimpse\Glimpse.nuspec -OutputDirectory dist

copy /Y src\Glimpse.Common\bin\Release\*.nupkg dist
copy /Y src\Glimpse.Server\bin\Release\*.nupkg dist
copy /Y src\Glimpse.Agent.AspNet\bin\Release\*.nupkg dist
copy /Y src\Glimpse.Agent.AspNet.Mvc\bin\Release\*.nupkg dist