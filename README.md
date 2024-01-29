# E-Commerce de Lanches

Este é um projeto de e-commerce de lanches desenvolvido em .NET.

## Pré-requisitos

Certifique-se de ter os seguintes requisitos instalados em sua máquina:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server)

## Configuração do Banco de Dados
dotnet ef database update
## Configuração do Projeto

1. Clone o repositório: `git clone https://github.com/seu-usuario/ecommerce-lanches.git`.
2. Navegue até o diretório do projeto: `cd LanchesMac`.

## Configuração do AppSettings

1. No diretório `./LanchesMac`, copie o arquivo `appsettings.example.json` e renomeie para `appsettings.json`.
2. Edite o arquivo `appsettings.json` e ajuste as configurações do banco de dados conforme necessário.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=ECommerceLanchesDB;Integrated Security=True;"
  }
}
