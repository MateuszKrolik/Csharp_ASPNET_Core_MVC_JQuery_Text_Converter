FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
# Fix long restore
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
WORKDIR /src
COPY ["TextConverterApp/TextConverterApp.csproj", "TextConverterApp/"]
RUN dotnet restore "TextConverterApp/TextConverterApp.csproj"
COPY . .
WORKDIR "/src/TextConverterApp"
RUN dotnet build "TextConverterApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TextConverterApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TextConverterApp.dll"]
