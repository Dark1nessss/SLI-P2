# SLI - Sistema de Legalização e Importação

## 1. Enquadramento Académico
O **SLI (Sistema de Legalização e Importação)** é uma solução de software desktop desenvolvida como objeto de avaliação prática para a unidade curricular de **Programação II**. O projeto foi estruturado em conformidade com as diretrizes do curso de Licenciatura em Engenharia Informática do **Instituto Superior de Tecnologias Avançadas do Porto (ISTEC Porto)**.

* **Discente:** Dmytro Bohutskyy (Nº 2022298)
* **Docente:** Prof. Dr. João Rebelo
* **Data de Emissão:** Junho de 2026

## 2. Descrição do Sistema
O sistema consiste num motor lógico e simulador aduaneiro concebido para centralizar, gerir e automatizar o cálculo tributário de veículos introduzidos no mercado de Portugal Continental. A aplicação processa as variáveis técnicas de cada viatura para determinar com rigor o Imposto Sobre Veículos (ISV) e o Imposto sobre o Valor Acrescentado (IVA). De forma a mitigar erros acumulados de arredondamento financeiro, o núcleo computacional opera estritamente com o tipo de dado de alta precisão `decimal`.

## 3. Arquitetura e Funcionalidades Core
* **Hierarquia de Classes Especializada:** Segmentação das entidades de acordo com a sua tipologia de propulsão (Veículos Elétricos Puros, Híbridos Plug-In e a Combustão).
* **Motor Fiscal Polimórfico:** Delegação do cálculo de taxas e isenções diretamente nas subclasses através de polimorfismo dinâmico, erradicando estruturas condicionais complexas (`if/else`) na camada de apresentação.
* **Rastreabilidade Documental (DAV):** Associação e gestão das Declarações Aduaneiras de Veículos (DAV) em formato PDF, controladas por um mecanismo de composição forte.
* **Persistência de Dados em Memória:** Mapeamento, filtragem e manipulação estruturada dos registos ativos através duma componente visual de alta densidade (`ListView`).

## 4. Requisitos Tecnológicos
* **Linguagem de Programação:** C# (.NET)
* **Tecnologia de Interface:** WPF (Windows Presentation Foundation) / XAML
* **Padrão Arquitetural:** Separação rigorosa entre a lógica de negócio (Domain Core) e a camada de apresentação (UI).