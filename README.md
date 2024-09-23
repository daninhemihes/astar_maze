## Projeto AstarMaze 

## Visão Geral do Projeto

Este projeto foi desenvolvido para a disciplina de **Serviços Cognitivos**, do **6º período** de **Engenharia de Software**, na **UniBrasil**, sob orientação do professor **Mozart Hasse**. O objetivo do sistema é implementar um robô de resgate, capaz de navegar em um labirinto e encontrar um ser humano perdido, utilizando o algoritmo de busca A* e aplicando os princípios de **Design Orientado a Domínio** (DDD). O projeto está desenvolvido em **C#**.

## Integrantes
- Daniel Henrique - 2022101974
- Gabrielli Cristini - 2022100944
- Sthefany Camile - 2022100150

### Funcionalidades

- **Algoritmo A***: O algoritmo de busca A* é utilizado para encontrar o melhor caminho no labirinto, permitindo que o robô encontre o humano e retorne à saída de forma eficiente.
- **Design Orientado a Domínio (DDD)**: O código é estruturado em torno dos princípios do DDD, com uma clara separação de responsabilidades entre `Application`, `Domain` e `ValueObjects`.
- **Log de Operações**: O sistema registra todas as ações realizadas pelo robô, incluindo leituras de sensores e comandos executados.
- **Simulação de Sensores e Movimento**: O robô possui sensores que detectam paredes, espaços vazios e a presença do humano diretamente à frente, à esquerda ou à direita.

  ### Como Executar

1. **Pré-requisitos**:
   - .NET SDK 6 ou superior
   - Compilador C# compatível com Linux (como requisitado pelo projeto)

2. **Configuração do ambiente**:
   - Clone o repositório.
   - Assegure-se de que o arquivo `Maze.cs` contém uma representação válida do labirinto, usando o formato fornecido (*E* para entrada, *H* para o humano, *\** para paredes e espaços para caminhos).

3. **Execução da Aplicação**:
   - Compile e execute o projeto `AstarMaze.App`.
   - Forneça um labirinto baseado em texto como entrada, no diretorio Simulator.
   - A aplicação irá simular os movimentos do robô, registrar as ações e gerar os resultados em um arquivo CSV.
   
### Conclusão

O **Projeto AstarMaze** demonstra a aplicabilidade de conceitos avançados de algoritmos de busca, como o A*, dentro de um ambiente simulado de resgate em labirinto. A implementação em **C#** permite uma clara organização do código, respeitando os princípios do **Design Orientado a Domínio (DDD)**. Este projeto exemplifica como podemos integrar a lógica de navegação autônoma, o uso de sensores simulados e o gerenciamento de logs de operações em um único sistema coeso. Além disso, a utilização de boas práticas de arquitetura de software promove a manutenibilidade e expansibilidade do projeto, tornando-o uma excelente base para estudos e aplicações práticas em áreas como robótica e sistemas de resgate.
     
