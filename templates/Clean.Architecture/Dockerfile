FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source

# Only copy solution and project file(s) for restore
COPY *.sln .
COPY src/Api/Clean.Architecture.Api.WebApi/*.csproj ./src/Api/Clean.Architecture.Api.WebApi/
COPY src/Core/Clean.Architecture.Core.Application/*.csproj ./src/Core/Clean.Architecture.Core.Application/
COPY src/Core/Clean.Architecture.Core.Domain/*.csproj ./src/Core/Clean.Architecture.Core.Domain/
COPY src/Infrastructure/Clean.Architecture.Infrastructure.Persistence/*.csproj ./src/Infrastructure/Clean.Architecture.Infrastructure.Persistence/
COPY src/Shared/Clean.Architecture.Shared.Kernel/*.csproj ./src/Shared/Clean.Architecture.Shared.Kernel/

# Restore for the LINUX platform
RUN dotnet restore -r linux-musl-x64

# Copy all other files
COPY src/Api/Clean.Architecture.Api.WebApi/. ./src/Api/Clean.Architecture.Api.WebApi/
COPY src/Core/Clean.Architecture.Core.Application/. ./src/Core/Clean.Architecture.Core.Application/
COPY src/Core/Clean.Architecture.Core.Domain/. ./src/Core/Clean.Architecture.Core.Domain/
COPY src/Infrastructure/Clean.Architecture.Infrastructure.Persistence/. ./src/Infrastructure/Clean.Architecture.Infrastructure.Persistence/
COPY src/Shared/Clean.Architecture.Shared.Kernel/. ./src/Shared/Clean.Architecture.Shared.Kernel/

# Set working directory to API project
WORKDIR /source/src/Api/Clean.Architecture.Api.WebApi

# Set build env
ARG BUILD_CONFIG=Debug
ENV BUILD_CONFIG=${BUILD_CONFIG}

# Publish application
RUN dotnet publish \
    -c $BUILD_CONFIG \
    -o /app \
    -r linux-musl-x64 \
    --self-contained true \
    /p:PublishTrimmed=true \
    /p:PublishReadyToRun=true \
    /p:PublishSingleFile=true

# Construction of final image
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
EXPOSE 443
CMD ["./Clean.Architecture.Api.WebApi"]