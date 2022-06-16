#Passo 1
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app

#Passo 2
RUN mkdir -pv /var/lib/docker/tmp/
RUN chmod 777 -R  /var/lib/docker/tmp/
EXPOSE 9898
EXPOSE 27017
EXPOSE 1433
RUN apt-get update
RUN apt-get install -y --no-install-recommends libgdiplus libc6-dev
RUN apt-get clean
RUN rm -rf /var/lib/apt/lists/*
#End

#Passo 3
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

# Start install Node.Js
RUN apt-get update && \ 
apt-get install -y wget && \ 
apt-get install -y gnupg2 && \ 
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \ 
apt-get install -y build-essential nodejs 
# End install Node.Js

COPY ["Sample.ElectronicCommerce.Chat/Sample.ElectronicCommerce.Chat.csproj", "Sample.ElectronicCommerce.Chat/"]
COPY ["Sample.ElectronicCommerce.Core/Sample.ElectronicCommerce.Core.csproj", "Sample.ElectronicCommerce.Core/"]
COPY ["Sample.ElectronicCommerce.Mail/Sample.ElectronicCommerce.Mail.csproj", "Sample.ElectronicCommerce.Mail/"]
COPY ["Sample.ElectronicCommerce.Security/Sample.ElectronicCommerce.Security.csproj", "Sample.ElectronicCommerce.Security/"]
COPY ["Sample.ElectronicCommerce.Web/Sample.ElectronicCommerce.Web.csproj", "Sample.ElectronicCommerce.Web/"]

RUN dotnet restore "Sample.ElectronicCommerce.Web/Sample.ElectronicCommerce.Web.csproj" --disable-parallel
COPY . .
WORKDIR "/src/Sample.ElectronicCommerce.Web"
RUN dotnet build "Sample.ElectronicCommerce.Web.csproj" -c Release -o /app/build

#Passo 4
FROM build AS publish
RUN dotnet publish "Sample.ElectronicCommerce.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Passo 4
FROM base AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:9898;http://+:443
ENV temp="%temp%"

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.ElectronicCommerce.Web.dll"]