# This stage is used for VS debugging on Docker
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt install -y nodejs
WORKDIR /src
COPY ["src/WebAPI.Domain.Core/WebAPI.Domain.Core.csproj","src/WebAPI.Domain.Core/"]
COPY ["src/WebAPI.Infrastructure.Data/WebAPI.Infrastructure.Data.csproj", "src/WebAPI.Infrastructure.Data/"]
COPY ["src/WebAPI.Presentation/WebAPI.Presentation.csproj", "src/WebAPI.Presentation/"]

RUN dotnet restore "src/WebAPI.Presentation/WebAPI.Presentation.csproj"
COPY . .
WORKDIR "/src/src/WebAPI.Presentation"
RUN dotnet build "WebAPI.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.Presentation.dll"]