FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /Valentin_Core
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /Valentin_Core/out
COPY --from=build-env /Valentin_Core/out .
ENTRYPOINT ["dotnet", "Valentin_Core.dll"]
