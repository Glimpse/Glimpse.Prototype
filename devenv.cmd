@setlocal
@echo off

set _UNSTABLE_FEED=https://www.myget.org/F/aspnetcidev/api/v2

if  NOT "%DNX_UNSTABLE_FEED%" == "%_UNSTABLE_FEED%"  (
 echo "ERROR:  DNX_UNSTABLE_FEED is not set to the recommended value %_UNSTABLE_FEED%.  You may need to update this in system env settings."
 goto END
)


call dnvm update-self
call dnvm upgrade -u
call dnu restore

:END
goto :EOF
