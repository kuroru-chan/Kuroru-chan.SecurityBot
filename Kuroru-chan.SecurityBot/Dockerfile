# Versions 0.1
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

LABEL MAINTAINER="Samersions"

WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Production \
BotConfiguration:BotToken "" \
BotConfiguration:Socks5Host "" \
BotConfiguration:Socks5Port 8080

FROM microsoft/dotnet:2.2-sdk AS build
COPY . /src
WORKDIR /src

RUN dotnet restore Kuroru-chan.SecurityBot.csproj \
    && dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "KuroruChan.SecurityBot.dll"]