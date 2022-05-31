#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *csproj .
RUN dotnet restore 


COPY . .
RUN dotnet publish  -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build /app/ .
ENTRYPOINT ["dotnet", "Backendv2.dll"]


