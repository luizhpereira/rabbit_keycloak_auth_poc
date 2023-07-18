# Comunicações, Autenticações, Persistência e Keycloak 
Projeto de autenticação de utilizadores, tópicos e outros recursos RabbitMQ.

# Start
- Clone o projeto
```console
git clone <endereço do projeto>
```
- Aceda ao diretório do projeto com um console
- Dê o comando para levantar os containers através de Docker-Compose
```console
docker-compose up -d
```

# RabbitMQ
Imagem base do RabbitMQ utilizada foi **rabbitmq:3-management-alpine**.  
Para contornar custas de armazenamento a imagem foi customizada no ficheiro rabbitmq.Dockerfile.
Isso evita a necessidade de um volume de configuração. 
A imagem tomou o nome de **caz-rabbitmq** com a tag **dev**.

*Importante sempre verificar o ficheiro rabbitmq.conf para garantir
que as rotas definidas para os endpoints condiz com as dispostas pelo AuthService.*

### Build
Clone do projeto Authservice e então na pasta onde há os ficheiros rabbitmq.Docker
```console
$ docker build -f rabbitmq.Dockerfile -t <nome_acr>/rabbitmq:dev .
```
* *docker build* trata-se do comando padrão para fazer build
* *-f rabbitmq.Dockerfile* trata-se da definição de que **file** será base para imagem
* *-t caz-rabbitmq:dev* é o nome da imagem e a tag que ela terá
* todo build é finalizado por ponto, isso define que o caminho é relativo

*Executando o build acima, todos os ficheiros de configuração que seriam normalmente 
importados através de volumes já são previamente incorporados a imagem criada, 
como exemplo no ficheiro **docker-compose.yml** temos o service **build-rabbitmq**. 
como exemplo de uso desta imagem no mesmo ficheiro docker-compose temos **caz-rabbitmq**.*

### Push para Azure Container Registry
O processo deve ser feito através do:
* Login Azure
```console
az login
```
* Login Azure Container Registry
```console
az acr login --name <nome_acr>
```
E então realizar o push da imagem para o container registry em questão:
```console
docker push <acr_login_string>/keycloak:dev
```

# PostgreSQL
[Azure File Share, volumes para ACA.](https://learn.microsoft.com/en-us/azure/container-apps/storage-mounts-azure-files?tabs=powershell#prerequisites) 
Esta foi a documentação base para implementação dos volumes para garantir persistência aos dados dos containers de base de dados. 
Abaixo referenciaremos os passos que foram dados para alcançar o objetivo.

## Imagem Docker
[postgres:latest](https://hub.docker.com/_/postgres)

## Variáveis de ambiente mínimas
A definição do banco de dados e do user foi feita com base na recomendação da documentação Keycloak.
> POSTGRES_USER keycloak  
POSTGRES_PASSWORD admin  
POSTGRES_DB keycloak

## Create Storage Account
Aqui criaremos uma storage account Azure, faremos uso do Azure Files
e por fim criaremos um volume para atender as necessidades de persistência de dados do PostgreSQL.

[Demonstração em vídeo](https://youtu.be/2xCrYkWgHKc)

## Definir volume
Após as implementações para criação da storage account e file share, deve seguir ao *container app* criado 
e definir propriamente o volume referenciando o link do file share definido.

### recepção e atualização do YAML Container App Model
Receber num ficheiro YAML as configurações para futuros updates na infraestrutura ACA

# Keycloak
A implementação do Keycloak também foi customizada para atender as necessidades on-premise e cloud.
Dentre as principais mudanças está o maior número de variáveis de ambiente definidas diretamente no
Dockerfile.

## Imagem Docker
[quay.io/keycloak/keycloak:latest](https://quay.io/repository/keycloak/keycloak?tab=tags&tag=latest)

## Variáveis de ambiente mínimas

### database
> ENV KC_DB=postgres  
ENV KC_DB_URL=jdbc:postgresql://postgres-db:5432/keycloak  
ENV KC_DB_USERNAME=keycloak  
ENV KC_DB_PASSWORD=admin  

### login  
> ENV KEYCLOAK_ADMIN=admin  
ENV KEYCLOAK_ADMIN_PASSWORD=admin  
ENV KC_HOSTNAME=localhost  
ENV KC_HOSTNAME_ADMIN_URL=http://localhost:8080  

### security
> ENV KC_HOSTNAME_STRICT=false  
ENV KC_HOSTNAME_STRICT_HTTPS=false  
ENV KC_HTTP_ENABLED=true  

### cloud-infrastructure
> ENV KC_PROXY=edge  
ENV PROXY_ADDRESS_FORWARDING=true  

Algumas variáveis são dispensáveis no ambiente on-premise. 
Para maiores informações consultar a documentação oficial do 
[Keycloak](https://www.keycloak.org/documentation).

# Contributo

### Keycloak
   - Fine gradient permission ainda encontra-se em versões de preview, a protect API também. Para maiores informações: [Fine Gradient Permission](https://www.keycloak.org/docs/latest/server_admin/#_fine_grain_permissions)

### ACA Keycloak
   - Requisito mínimo no Azure Container App *1 Core 2Gb RAM*.

   - O cenário de autenticação/autorização para já permite qualquer utilizador que contenha *"test"* será autorizado. 
   - O cenário de autenticação/autorização já faz uso do IdM Keycloak.
   - Não esquecer de fazer map das rotas de autenticação do AuthService no **rabbitmq.conf** para garantir a comunicação.

