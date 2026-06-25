# Modelo de Classes e Arquitetura de Software

A arquitetura de dados do sistema **SLI** foi desenhada estritamente sobre os pilares fundamentais do paradigma de **Programação Orientada a Objetos (POO)**: Encapsulamento, Herança, Abstração e Polimorfismo.

---

## 1. Arquitetura das Entidades

### Classe Abstrata: `Veiculo`
Superclasse do domínio que encapsula os atributos e comportamentos partilhados por todas as tipologias de veículos importados.
* **Membros Estruturais:** Atributos de identificação e logística, nomeadamente `Id`, `Marca`, `Modelo`, `Vin`, `Ano`, `PrecoBase`, `CustosTransporte` e `IsImportacaoUe`.
* **Estrutura de Associação:** Coleção `Documentos` (`List<Documento>`), materializando a relação com os ficheiros anexados.
* **Assinaturas Polimórficas:** Declaração dos métodos abstratos/virtuais `CalcularISV()`, `CalcularIVA()` e `CalcularCustoTotal()`.

### Subclasses Especializadas (Herança)
A herança é aplicada para segmentar o comportamento fiscal específico de cada motorização:
1. **`VeiculoCombustao`:** Introduz os atributos `Cilindrada` e `CO2`. Sobreescreve o método `CalcularISV()` aplicando as tabelas progressivas oficiais.
2. **`VeiculoEletrico`:** Introduz a propriedade `KwhBateria`. Sendo isento de taxas ambientais, a sua sobreposição do método `CalcularISV()` retorna o valor nulo (`0.00M`).
3. **`VeiculoHibridoPlugin`:** Expande o modelo com os campos de `AutonomiaEletrica` e `CO2`, aplicando os descontos parciais previstos na legislação sobre o cálculo base do ISV.
4. **`Motociclo`:** Especialização que considera a cilindrada e as taxas específicas aplicadas ao setor das duas rodas.

### Classe de Suporte: `Documento`
Entidade utilitária que representa os metadados dos ficheiros legais anexados (propriedades `Nome`, `CaminhoFicheiro` e `DataAnexo`). Possui um vínculo de **composição forte** com a classe `Veiculo`, garantindo que a destruição dum veículo elimine em cascata as instâncias dos seus documentos em memória.

---

## 2. Fluxo de Execução e Instanciação

O diagrama conceptual abaixo ilustra o comportamento do fluxo de dados desde a captura na camada de apresentação até ao tratamento polimórfico na camada de negócio:

```text
┌─────────────────────────┐
│    MainWindow (WPF)     │ ──> Captura inputs e gera instâncias
└────────────┬────────────┘
             │
             ▼ (Abstração por Referência da Superclasse)
┌─────────────────────────┐
│    Objeto: Veiculo      │ ──> Determinado em Runtime para a subclasse correta
└────────────┬────────────┘
             │
             ├──────────────────────────────┐
             ▼ (Execução Polimórfica)       ▼ (Mecanismo de Composição)
┌─────────────────────────┐    ┌──────────────────────────────────┐
│  .CalcularCustoTotal()  │    │  List<Documento>                 │
│  (Usa precisão decimal) │    │  (Destruição em cascata gerida)  │
└─────────────────────────┘    └──────────────────────────────────┘