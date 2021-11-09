# ParkingControl
## Web API REST em .NET 5 para controle de estacionamento

### Para rodar a aplicação, precisa estar com docker desktop instalado e funcionando e rodar os seguintes comandos:
- docker network create mongoparking
- docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin 
-e MONGO_INITDB_ROOT_PASSWORD=Pass#word1 --network=mongoparking mongo
- docker run -it --rm -p 8080:80 -e MongoDBSettings:Host=mongo -e MongoDBSettings:Password=Pass#word1 --network=mongoparking hfvmarques/parkingcontrol:v4

Talvez dê alguns problemas de timeout quando rodar o comando do mongo, mas é só rodar novamente que ele retoma de onde parou o download;

A aplicação vai rodar em http://localhost:8080/parking;

### Endpoints:

- GET /parking : retorna todas as "reservas";
- GET /parking/id : retorna a reserva a partir do id, no formato { "id": "5bf142459b72e12b2b1b2cd", "plate": "AAA-1234", "entryDate": "2021-10-25T01:59:43.5822553+00:00", "exitDate": null, "paid": false, "left": false }
- GET /parking?plate=AAA-1234 : retorna histórico de reservas da placa informada no mesmo formato do item anterior;
- POST /parking { "plate": "AAA-1234" } : insere uma reserva com a placa informada, colocando data de entrada do instante de criação e setando nulo as propriedades exitDate, paid e left;
- PUT /parking/id/pay : marca fim da reserva com horário do instante e registra pagamento (paid = true);
- PUT /parking/id/out : registra saída do veículo (left = true);
- GET /health/live : verifica a saúde da aplicação;
- GET /health/ready : verifica a saúde do mongodb;
- DELETE /parking/id : deleta a reserva a partir do id informado;

### Observações:

- A API foi construída em .NET 5, que é a linguagem/framework que possuo mais familiaridade;
- O formato dos objetos ficaram da seguinte forma: 
{ "id": 1, "plate": "AAA-1234", "entryDate": "2021-10-25T01:59:43.5822553+00:00", "exitDate": null, "paid": false, "left": false }
- Dito isso, eu realizei várias tentativas e testes porém não consegui que o método GET /parking?plate=AAA-1234 retorne naquele formato do desafio que me foi enviado;
- Como solicitado pelo desafio, o método PUT /parking/id/out só registra a saída se o pagamento já tiver sido efetuado. 
Porém eu tentei mas também não consegui que ele emitisse uma mensagem. A regra é que o método simplesmente não faça nada caso não haja pagamento;
- Método POST retorna mensagem "Não é um formato válido de placa", caso a placa não esteja no formato "AAA-1234";
- Criei o método DELETE apenas para propósitos de desenvolvimento, para não ter que ficar excluindo o container do mongodb cada vez que precisasse dele vazio. 
Acredito que seja interessante que ele não exista em modo de produção;
- Como solicitado no teste, a aplicação está rodando em container do docker, assim como o mongodb, porém não consegui, apesar de diversas tentativas, 
configurar o docker-compose para rodar os dois no mesmo container;

### Foram construídos 13 testes unitários, para testá-los é necessário instalar a extensão .NET Core Test Explorer no VS Code:

- GetParkingAsync_WithUnexistingParking_ReturnsNotFound;
- GetParkingAsync_WithExistingParking_ReturnsExpectedParking;
- GetParkingsAsync_WithExistingParkings_ReturnsAllParkings;
- GetParkingsAsync_WithMatchingPlates_ReturnsMatchingParkings;
- GetParkings_WithExistingParkings_ReturnsAllParkings;
- CreateParkingAsync_WithParkingToCreate_ReturnsCreatedParking;
- UpdateParkingOutAsync_WithExistingParking_ReturnsNoContent;
- UpdateParkingOutAsync_WithUnpaidParking_ReturnsNoContent;
- UpdateParkingOutAsync_WithUnexistingParking_ReturnsNotFound;
- UpdateParkingPayAsync_WithExistingParking_ReturnsNoContent;
- UpdateParkingPayAsync_WithUnexistingParking_ReturnsNotFound;
- DeleteParkingAsync_WithExistingParking_ReturnsNoContent;
- DeleteParkingAsync_WithUnexistingParking_ReturnsNotFound;
