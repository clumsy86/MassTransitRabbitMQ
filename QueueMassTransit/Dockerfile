#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QueueMassTransit/QueueMassTransit.Sender.csproj", "QueueMassTransit/"]
COPY ["SharedMessage/SharedMessage.csproj", "SharedMessage/"]
RUN dotnet restore "QueueMassTransit/QueueMassTransit.Sender.csproj"
COPY . .
WORKDIR "/src/QueueMassTransit"
RUN dotnet build "QueueMassTransit.Sender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueueMassTransit.Sender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueueMassTransit.Sender.dll"]