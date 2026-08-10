# E-commerce API — Clean Architecture (.NET)

> 🚧 Projeto em desenvolvimento — portfólio de estudo e prática de arquitetura de software.

API de e-commerce single-vendor construída em **ASP.NET Core**, aplicando **Clean Architecture** e princípios **SOLID** de forma prática, com foco em modelagem de domínio bem pensada e uso consciente de design patterns.

## 🎯 Objetivo do projeto

Este projeto tem dois objetivos principais:

1. **Aprofundar conhecimento prático** de arquitetura em camadas, DDD e padrões de projeto aplicados a um cenário real de e-commerce.
2. **Servir como peça de portfólio**, demonstrando decisões técnicas justificadas

## 🏗️ Arquitetura

O projeto segue **Clean Architecture**, organizado em uma solução multi-projeto com quatro camadas, respeitando a *Dependency Rule* (dependências sempre apontam para dentro, em direção ao domínio):

- **Domain** — entidades, enums, regras de negócio e contratos de repositório.
- **Application** — casos de uso, orquestrados via **MediatR** (CQRS), validações e DTOs.
- **Infrastructure** — implementação de persistência (EF Core), integrações externas (Mercado Pago, Frenet) e outros serviços concretos.
- **API** — camada de apresentação (controllers, configuração de DI, middlewares).

## 🧩 Modelagem de domínio

O domínio conta com 8 entidades centrais, com decisões deliberadas como:

- `Guid` como chave primária das entidades.
- `decimal` para todos os valores monetários (evitando problemas de precisão de ponto flutuante).
- Máquinas de estado via `enum` para status de pedido/pagamento.
- **Congelamento de preço** no momento da criação do pedido (o preço do produto no pedido não é afetado por alterações futuras no catálogo).

## 🎨 Design Patterns em foco

Um dos focos do projeto é aplicar padrões de projeto de forma justificada (não decorativa), entre eles:

- **Repository** — abstração de acesso a dados sobre o EF Core.
- **Strategy** — para regras de cálculo/comportamento que variam (ex: frete, formas de pagamento).
- **Adapter** — para integração com serviços externos (Mercado Pago, Frenet).
- **CQRS / Mediator** — separação de comandos e queries via MediatR.

## 🛠️ Stack tecnológica

| Camada / Necessidade | Tecnologia |
|---|---|
| Framework | ASP.NET Core |
| ORM | Entity Framework Core |
| Banco de dados | SQL Server |
| Pagamentos | Mercado Pago |
| Cálculo de frete | Frenet |
| Padrão de mensageria interna | MediatR |

## 📌 Status atual

O projeto passou por uma etapa de planejamento aprofundado (escopo via MoSCoW, modelagem de domínio e estudo da fundamentação de Clean Architecture) e está atualmente na fase de implementação, aplicando os padrões e princípios descritos acima.

## 🚀 Como rodar o projeto

> Seção a ser preenchida conforme o projeto evolui (instruções de setup, variáveis de ambiente, migrations, etc.).

```bash
# em breve
```

## 📄 Licença

Este projeto tem fins de estudo e portfólio.
