FROM mcr.microsoft.com/dotnet/core/runtime:3.1
COPY Valentin_Core/bin/Release/netcoreapp3.0/publish Valentin_Core/
ENTRYPOINT ["dotnet", "Valentin_Core/Valentin_Core.dll"]