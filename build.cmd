@echo off
cls

ECHO.
ECHO Building xCache.Redis
ECHO =======================================

nuget pack src\xCache.Redis\xCache.Redis.csproj -build -Prop Configuration=Release -IncludeReferencedProjects -OutputDirectory artifacts

ECHO.
ECHO All done
