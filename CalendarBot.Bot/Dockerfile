#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CalendarBot.Bot/CalendarBot.Bot.csproj", "CalendarBot.Bot/"]
COPY ["CalendarBot.Data/CalendarBot.Data.csproj", "CalendarBot.Data/"]
RUN dotnet restore "CalendarBot.Bot/CalendarBot.Bot.csproj"
COPY . .
WORKDIR "/src/CalendarBot.Bot"
RUN dotnet build "CalendarBot.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalendarBot.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalendarBot.Bot.dll"]