FROM registry.access.redhat.com/dotnet/dotnet-20-rhel7 AS base

USER 0

RUN mkdir -p /opt/app-root/src
RUN mkdir -p /opt/app-root/app

ENV LD_LIBRARY_PATH=/opt/rh/rh-nodejs6/root/usr/lib64:/opt/rh/rh-dotnet20/root/usr/lib64
ENV PATH=/opt/rh/rh-nodejs6/root/usr/bin:/opt/rh/rh-dotnet20/root/usr/bin:/opt/rh/rh-dotnet20/root/usr/sbin:/opt/app-root/src/.local/bin:/opt/app-root/src/bin:/opt/app-root/node_modules/.bin:/opt/app-root/bin:/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin

WORKDIR /opt/app-root/src
COPY NuGet.Config ./
COPY *.sln ./
COPY Agora.Brokerage.Cadastro.DadosAcesso.Shared/Agora.Brokerage.Cadastro.DadosAcesso.Shared.csproj Agora.Brokerage.Cadastro.DadosAcesso.Shared/
COPY Agora.Brokerage.Cadastro.DadosAcesso.Core/Agora.Brokerage.Cadastro.DadosAcesso.Core.csproj Agora.Brokerage.Cadastro.DadosAcesso.Core/
COPY Agora.Brokerage.Cadastro.DadosAcesso.Model/Agora.Brokerage.Cadastro.DadosAcesso.Model.csproj Agora.Brokerage.Cadastro.DadosAcesso.Model/
COPY Agora.Brokerage.Cadastro.DadosAcesso.API/Agora.Brokerage.Cadastro.DadosAcesso.API.csproj Agora.Brokerage.Cadastro.DadosAcesso.API/
COPY Agora.Brokerage.Cadastro.DadosAcesso.Tests/Agora.Brokerage.Cadastro.DadosAcesso.Tests.csproj Agora.Brokerage.Cadastro.DadosAcesso.Tests/

RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .

WORKDIR /opt/app-root/src/Agora.Brokerage.Cadastro.DadosAcesso.API

RUN dotnet build -c Release -o /opt/app-root/app

FROM base AS publish
RUN dotnet publish -c Release -o /opt/app-root/app

FROM registry.access.redhat.com/dotnet/dotnet-20-runtime-rhel7 AS final 

USER 0

RUN groupadd -g 1001 dotnet
RUN usermod -g dotnet default

WORKDIR /opt/app-root/app
RUN chown -R 1001:dotnet /opt/app-root/app

COPY --from=publish /opt/app-root/app .

USER 1001
CMD ["dotnet", "Agora.Brokerage.Cadastro.DadosAcesso.API.dll"]
