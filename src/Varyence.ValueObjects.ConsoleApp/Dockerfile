ARG DotnetVersion=3.1
FROM mcr.microsoft.com/dotnet/core/sdk:$DotnetVersion-alpine AS build
WORKDIR /app

# Copy csproj files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do \
        echo $file \
        && mkdir -p src/${file%.*}/ \
        && mv $file src/${file%.*}/; \
    done

# Restore api csproj
RUN dotnet restore ./src/Varyence.ValueObjects.ConsoleApp/Varyence.ValueObjects.ConsoleApp.csproj

# Copy everything else and build
COPY . ./

# Publish to out folder
RUN dotnet publish ./src/Varyence.ValueObjects.ConsoleApp/Varyence.ValueObjects.ConsoleApp.csproj -c Release --no-restore -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:$DotnetVersion-alpine AS base

RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false

WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Varyence.ValueObjects.ConsoleApp.dll"]