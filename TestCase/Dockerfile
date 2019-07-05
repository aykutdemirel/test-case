#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-nanoserver-1803 AS base
#WORKDIR /app
#EXPOSE 80

#FROM mcr.microsoft.com/dotnet/core/sdk:2.1-nanoserver-1803 AS build
#WORKDIR /src
#COPY *.csproj ./
#RUN dotnet restore "TestCase/TestCase.csproj"
#COPY . ./
#WORKDIR "/src/TestCase"
#RUN dotnet build "TestCase.csproj" -c Release -o /app

#FROM build AS publish
#RUN dotnet publish "TestCase.csproj" -c Release -o /app

#FROM base AS final
#WORKDIR /app
#COPY --from=build /app/out .
#ENTRYPOINT ["dotnet", "TestCase.dll"]

#FROM mcr.microsoft.com/dotnet/core/sdk:2.1-nanoserver-1803 AS build
#WORKDIR /app
#EXPOSE 80
#COPY *.csproj ./
#RUN dotnet restore TestCase.csproj
#COPY . ./
#RUN dotnet publish TestCase.csproj -c Release -o out
#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
#WORKDIR /app
#COPY — from=publish /app .
#ENTRYPOINT ["dotnet", "TestCase.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "TestCase.dll"]