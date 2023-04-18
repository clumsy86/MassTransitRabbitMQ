#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QueueMassTransit.Consumer/QueueMassTransit.Consumer.csproj", "QueueMassTransit.Consumer/"]
COPY ["SharedMessage/SharedMessage.csproj", "SharedMessage/"]
RUN dotnet restore "QueueMassTransit.Consumer/QueueMassTransit.Consumer.csproj"
COPY . .
WORKDIR "/src/QueueMassTransit.Consumer"
RUN dotnet build "QueueMassTransit.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueueMassTransit.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueueMassTransit.Consumer.dll"]