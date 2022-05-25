#Passo 1
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app

# Start variables environment
#ENV ASPNETCORE_ENVIRONMENT "Development"
#ENV ASPNETCORE_ENVIRONMENT "Homolog"
#ENV ASPNETCORE_ENVIRONMENT "Production"
# End variables environment

#Start
RUN mkdir -pv /var/lib/docker/tmp/
RUN chmod 777 -R  /var/lib/docker/tmp/
EXPOSE 5000
EXPOSE 443
EXPOSE 8080
RUN apt-get update
RUN apt-get install -y --no-install-recommends libgdiplus libc6-dev
RUN apt-get clean
RUN rm -rf /var/lib/apt/lists/*
#End

#Passo 2
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

# Start install Node.Js
RUN apt-get update && \ 
apt-get install -y wget && \ 
apt-get install -y gnupg2 && \ 
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \ 
apt-get install -y build-essential nodejs 
# End install Node.Js

COPY ["Sample.ElectronicCommerce.Web/Sample.ElectronicCommerce.Web.csproj", "Sample.ElectronicCommerce.Web/"]

RUN dotnet restore "Sample.ElectronicCommerce.Web/Sample.ElectronicCommerce.Web.csproj"
COPY . .
WORKDIR "/src/Sample.ElectronicCommerce.Web"
RUN dotnet build "Sample.ElectronicCommerce.Web.csproj" -c Release -o /app/build

#Passo 3
FROM build AS publish
RUN dotnet publish "Sample.ElectronicCommerce.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Passo 4
FROM base AS final
WORKDIR /app

#Start
ENV ASPNETCORE_URLS=http://+:5000;http://+:443;http://+:8080
ENV temp="%temp%"
#End

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.ElectronicCommerce.Web.dll"]