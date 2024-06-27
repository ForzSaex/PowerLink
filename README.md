# Introdução
## PowerLink é um projeto que está sendo desenvolvido por uma das equipes do 4° período de Eletrotécnica do Instituto Federal do Norte De Minas Gerais - Campus Montes Claros.

A ideia inicial é um aplicativo que se comunica com um dispositivo equipado com um Esp32 para a coleta e manipulação de dados de tensão e corrente, afim de se obter um valor de potência consumido por um aparelho conectado à este dispositivo. Esses dados são enviados para o aplicativo através de um protocolo de comunicação HTTP (HyperText Transfer Protocol), onde o usuário poderá visualizar os dados coletados pelo dispositivo e realizar o desligamento de qualquer uma das portas através de um comando HTTP POST.

## Recursos

* Monitoramento detalhado do consumo de cada aparelho conectado ao dispositivo. Além da visualização das médias diárias, semanais e mensais através de um gráfico intuitivo.

* Desligamento de cada aparelho conectado ao dispositivo individualmente.

* Notificação em caso de sobrecargas nos aparelhos

* Proteção contra sobrecargas.

* Temporizador para desligamento do aparelho conectado.

## Preview do aplicativo


<img src="/folder/Captura de tela 2024-04-22 045949.png">

<img src="/folder/Captura de tela 2024-04-22 050010.png">

<img src="/folder/Captura de tela 2024-04-22 050028.png">

<img src="/folder/Captura de tela 2024-04-22 050114.png">

> [!IMPORTANT]
> Imagens ilustrativas. Não representam o produto final.

## Notificações
As notificações são enviadas pelo esp32 através de um servidor do Google Firebase, pela função Firebase Cloud Messaging(FCM). As notificações são recebidas independentemente se o usuário está ou não com o aplicativo aberto, graças aos eventos do lifeCycle do .net MAUI. Atualmente há um atraso entre o envio e o recebimento da notificação de aproximadamente 3 minutos. O que creio eu, que não haja uma solução.

## Autenticação
A autenticação também ocorre pelo servidor do Firebase, porém utilizando o Firebase Authentication, configurado para usar o e-mail e senha para o login.

## Observações

1.Muitos desses recursos ainda não estão disponíveis, para ver quais deles já estão disponíveis, acesse o projeto disponível no próprio repositório.

2.Outros recursos podem ser adicionados ao projeto futuramente.

> [!NOTE]
> Diariamente irei atualizar sobre o estado atual do desenvolvimento do projeto.
