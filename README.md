# Introdução
## PowerLink é um projeto que foi desenvolvido por uma das equipes do 4° período de Eletrotécnica de 2024 do Instituto Federal do Norte De Minas Gerais - Campus Montes Claros.

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
### Trecho do código para envio das notificações
``` C++
void sendNotification(const String& title, const String& body) {
  if(app.ready())
  {
    Messages::Message msg;
    msg.topic("test");
    Messages::Notification notification;
    notification.title(title).body(body);
    Messages::AndroidConfig androidConfig;
    androidConfig.priority(Messages::AndroidMessagePriority::_HIGH);
    Messages::AndroidNotification androidNotification;
    androidNotification.notification_priority(Messages::NotificationPriority::PRIORITY_HIGH);
    androidConfig.notification(androidNotification);
    msg.android(androidConfig);
    msg.notification(notification);
    messaging.send(aClient, Messages::Parent(FIREBASE_PROJECT_ID), msg, aResult_no_callback);
  }
}
```

## Autenticação
A autenticação também ocorre pelo servidor do Firebase, porém utilizando o Firebase Authentication, configurado para usar o e-mail e senha para o login.

## Comunicação
A comunicação ocorre através de protocolos HTTP. No qual, para que não seja necessário descobrir qual o IP o servidor irá iniciar( já que ele utiliza um IP dinâmico), é utilizado um DNS para a página esp32.local, facilitando a comunicação entre ambas as partes.
### Trecho do código para definição do DNS
``` C++
  if (!MDNS.begin("esp32")) 
  {
    Serial.println("Error setting up MDNS responder!");
    while(1)
    {
      delay(1000);
    }
  }
  Serial.println("mDNS responder started");
  server.begin();
  MDNS.addService("http", "tcp", 80);
```
# Projetos futuros
## Alteração da plataforma
O principal plano para o futuro do aplicativo é alterar a plataforma no qual ele está operando. A ideia é substitui-la pelo Android Studio, no qual poderá oferecer um desempenho muito superior, um consumo de recursos do usuário menor, e por ser uma plataforma desenvolvida pelo próprio google, é possível uma comunicação mais eficiente com o sistema operacional sem a necessidade de bibliotecas externas. além disso, o firebase já é compatível diretamente com o Android Studio, facilitando a sua implementação.
Outro ponto importante para essa mudança está relacionada com a aparência. O Android Studio, juntamente com Jetpack Compose, permite com que sejam seguidos os Material Android Guidelines através do Material 3, o que permite que o aplicativo se assemelhe a um aplicativo do sistema. Além disso, também permite a utilização do Dynamic Colors para usuários que utilizam o Android 12+.
## Substituição do método de comunicação
Outro projeto em vista para as próximas atualizações está relacionada a comunicação com o PowerLink. Atualmente ocorre um desperdício dos pacotes enviados pelo dispositivo que poderiam ser enviatos. Atualmente são enviados 4 solicitações em um intervalo de 1s, 3 dessas solicitações poderiam ser evitadas já que possuem os mesmos dados da primeira solicitação. E para isso seria necessário uma reformulação de como o ViewModel é inicializado, O que, com o Android Studio, poderia ser evitado com a utilização de Companion Objects, que permitem ser referenciados em qualquer local do aplicativo com uma unica inicialização dos dados do ViewModel.
## Melhoria da segurança
Na parte do dispositivo é previsto uma melhoria na segurança da comunicação. Atualmente é utilizado uma comunicação HTTP, o que abre brechas de segurança, como a interceptação de dados e a manipulação do dispositivo por parte de invasores. Para solucionar isso, é previsto a substituição da comunicação HTTP por HTTPS, além de um processo de autenticação do usuário através de uma chave criptografada vinculada a conta do usuário.
