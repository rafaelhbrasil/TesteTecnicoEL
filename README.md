

# TesteTecnicoEL

Este projeto é um exemplo de API desenvolvida para uma locadora de veículos fictícia.
Não deve ser utilizada em ambiente produtivo por possuir somente dados "_mockados_".

## Sobre o projeto

A locadora deve ter um _back-end_ e um _front-end_, ambos com livre escolha da arquitetura.

O _back-end_ deve possuir as funcionalidades:
- Cadastro de usuários com perfil CLIENTE ou OPERADOR
- Autenticação do usuário com seleção automática de usuário autenticado pelo CPF ou pela matrícula no momento da autenticação
- Cadastro de marcas e modelos de veículos
- Possibilidade de simular e realizar uma locação de veículo
- Devolução do veículo com checklist de devolução

O _front-end_ deve possuir as funcionalidades:
- Cadastro e autenticação de usuário com perfil CLIENTE
- Listagem de veículos disponíveis organizados por categoria
- Possibilidade de simular e confirmar uma locação de veículo
- Exigir autenticação ou cadastro do usuário caso não esteja autenticado no momento da confirmação
- Exibir o histórico de locações organizados por data

## Arquitetura

Um desenho bem simplificado da arquitetura pode ser vista no arquivo <ARQ DRAW.IO>.

### Back-end

Desenvolvido usando ASP.NET Core versão 3.1, última versão LTS atualmente, pela familiaridade e alta produtividade da IDE, da linguagem C# e do framework.

A metodologia DDD foi adotada para facilitar a organização dos arquivos e facilitar a manutenção.

Testes unitários usando xUnit por ser pouco verboso (menos atributos) e fácil de usar e já criado para TDD. Além disso, é bastante extensível.

A autenticação exigida é do esquema Bearer. A chave deve ser obitada via autenticação na rota específica (vide Swagger) e enviado nas requisições como um header similar ao abaixo:
```
Authorization: Bearer XXXXXXXXXXXXXXXXXXXXXXXXX
```

### Front-end

Assim como o back-end Desenvolvido usando ASP.NET Core versão 3.1.

Arquitetura multimacadas com UI e DAL. A camada BLL não foi necessária pelo fato de as regras de negócio se concentrarem na API.

Views personalizadas com Bootstrap 4.3.1. A decisão de se deve graças à facilidade de uso, alta compatibilidade e grande variedade de componentes visuais já implementados que facilitam a estilização da tela.

O uso da biblioteca JQuery 3.5.1 complementa o JavaScript da tela pela facilidade de uso, alta compatibilidade e extensa comunidade ativa, permitindo chamadas Ajax e acesso simplificado aos componentes DOM com reduzida implementação de código.

## Execução o projeto

A execução é simples de rápida. Basta abrir o Visual Studio com cada uma das Soluções e executar ambas em qualquer ordem. O _front-end_ depende de o _back-end_ estar em execução para funcionar. Este, por sua vez, é independe.

### Requisitos para executar

- Visual Studio 2019, qualquer distribuição
- .NET Core 3.1 SDK

### Executar

Abra cada uma das Soluções abaixo no Visual Studio e aperte F5. Espere o navegador da web padrão abrir automaticamente.

- $/back-end/TesteTecnicoEL.sln (independente): automaticamente abrirá o Swagger.
- $/front-end/TesteTecnicoEL_Front.sln: automaticamente abrirá a tela principal do _site_.

