# Serviço Pedidos
API Rest que disponibiliza serviços de emissão de pedidos

## Dependencias
Esta API REST disponibiliza:
* *POST* em `./pedidos/`
* *PUT* e *GET* em `./pedidos/{id}`
* `./pedidos/rentabilidade?precoSugerido={preco}&precoUnitario={preco}`
* *GET* em `./produtos/`
* *GET* em `./clientes/`

## Instalação para desenvolvimento
Instale os seguintes
1. Visual Studio 2017
1. git
1. Clone este repositorio: `git clone https://github.com/victoralvesp/servico-pedidos` 
1. Abra o arquivo `ServicoPedidos.sln` com Visual Studio 2017

## Instalação em servidores
1. Abra o PowerShell na pasda do projeto de saida: `ServicoPedidos.WebAPI.csproj`
1. Execute `dotnet publish ServicoPedidos.WebAPI.csproj -c Release -o PublishOutput`
1. Comprima os arquivos com `Compress-Archive -Path .\PublishOutput\* -DestinationPath .\servico-pedidos-<versao>.zip` 
1. Descompacte em uma de destino de um servidor IIS.
