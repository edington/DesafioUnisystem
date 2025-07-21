
Este projeto é uma API(.NET Core V8)  feita em C# e estruturada em camadas com arquitetura limpa, foi ultilizada algumas abordagens de DDD.

O projeto foi estruturado nas seguintes camadas : Presentation , Infrastructure, Application Service, Domain e Tests, assim para separar responsabilidads e facilitar a manutenção.

1)Presentation : Responsável pela entrada do sistema.
2)Domain : Contém as entidades com regras de negócio.
3)Application Service: Orquestra os casos de uso do sistema.
4)Infrastructure: Implementações técnicas (ex: Entity Framework )
5)Test : Armazena os testes da aplicação, garatindo o funcionamento das funcionalidades.


--Tecnologias Utilizadas--
1..NET Core V8
2.C#
3.Entity Framework Core
4.xUnit (para testes)
5.Banco de dados - Sql Server
6.Angular 19



                                --Testes--

O projeto contém testes de Integração e Unitários para garantir a confiabilidade das funcionalidades implementadas.


                           --Executar o Back-End--
1) Pré- requisito
    * Ter um broker RabbitMQ em execução(pode ser local ou via docker) OU
    * Ter o SQL Server instalado ou acesso a uma instância remota.
2) Configuração do Banco de Dados
    * Crie um banco de dados com o nome DesafioUnisystem.
    * Executar o script abaixo para criar a tabela Users.

CREATE TABLE Users (
    Id uniqueidentifier DEFAULT newid(),
    Name  nvarchar(100) NOT NULL,
    Email nvarchar(30) NOT NULL UNIQUE,
    Password nvarchar(100) NOT NULL
);

3) Configurar connection string no arquivo appsettings.json, conforme o seu ambiente (ex: usuário/ senha).


                         -- Executar o Front-end--

1) Acesse a pasta do projeto Angular.
1) Instale as dependências (npm install).
2) Se necessário configure a URL da API no arquivo auth.service.ts.
3) Rode a aplicação ng serve.


                         --Executar o projeto de Teste--

1- No projeto DesafioUnisystem.Tests , clique com o botão direito do mouse e selecione Executar testes.
2- Caso queira depurar algum teste classe ou método específico(a) :
    - Selecione  algum método e classe desejado(a). 
    - Clique com o botão direito sobre ele(a) a classe e selecione depurar teste.
