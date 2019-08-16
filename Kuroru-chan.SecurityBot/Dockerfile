# Versions 0.1
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

LABEL MAINTAINER="Samersions"

WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
COPY . /src
WORKDIR /src

RUN dotnet restore Kuroru-chan.SecurityBot.csproj
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
RUN echo "dotnet KuroruChan.SecurityBot.dll --enviroment=${ASPNETCORE_ENVIRONMENT}" > entrypoint.sh
ENTRYPOINT ["/bin/bash", "entrypoint.sh"]