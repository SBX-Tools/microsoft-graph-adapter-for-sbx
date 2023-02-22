FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MicrosoftGraphAdapterForSbx/MicrosoftGraphAdapterForSbx.csproj", "MicrosoftGraphAdapterForSbx/"]
RUN dotnet restore "MicrosoftGraphAdapterForSbx/MicrosoftGraphAdapterForSbx.csproj"
COPY . .
WORKDIR "/src/MicrosoftGraphAdapterForSbx"
RUN dotnet build "MicrosoftGraphAdapterForSbx.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicrosoftGraphAdapterForSbx.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicrosoftGraphAdapterForSbx.dll"]
