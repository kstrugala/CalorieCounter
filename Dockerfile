FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY . ./
WORKDIR /app/CalorieCounter.Api
RUN dotnet restore

RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS final
WORKDIR /app/CalorieCounter.Api
COPY --from=build /app/CalorieCounter.Api/out .
ENTRYPOINT ["dotnet", "CalorieCounter.Api.dll"]