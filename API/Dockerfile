FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR API

COPY CityApp-ANET/*.csproj ./
RUN dotnet restore

# Copy everything
COPY . .
# Build and publish
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /API
COPY --from=build-env /API/CityApp-ANET/Static ./Static
COPY --from=build-env /API/out .

ENTRYPOINT ["dotnet", "CityApp-ANET.dll", "--urls", "http://0.0.0.0:5083"]