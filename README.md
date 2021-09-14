
# 🧡 Hackathon-FCamara API

API tem como objetivo fornecer o serviço para agendamento de colaboradores da FCamara em seus escritórios ou filiais, visando garantir a segurança respeitando a capacidade máxima de cada ambiente de acordo com a legislação imposta por conta da pandemia (COVID-19).


## ☕ API Referência

<details open> 
  <summary>
    Agendamento
  </summary>
  
#### Retornar todos Agendamento do Funcionario
```http
  GET /api/Agendamento/
```
#### Salvar Agendamento
```http
  POST /api/Agendamento/
```
#### Datas para Agendamento disponiveis no local
```http
  GET /api/Agendamento/disponiveis/:localId
```
#### Deletar Agendamento
```http
  DELETE /api/Agendamento/
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `localId` | `integer` | **Required**. Chave estrangeira de local |
| `Data` | `dateTime` | Armazena a data de agendamento |
| `funcionarioId` | `string` | **Required**. Chave estrangeira de funcionário  |
  
</details>

<details open> 
  <summary>
    Funcionario
  </summary>
  
#### Cadastrar Funcionario
```http
  POST /api/Funcionario/cadastrar
```
#### Login Funcionario
```http
  POST /api/Funcionario/login
```
#### Retornar informações do Funcionario
```http
  GET /api/Funcionario/info
```
#### Altera o avatar do Funcionario
```http
  PATCH /api/Funcionario/avatar
```
  
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `integer` | **Required**. Chave primária do funcionário |
| `Nome` | `string` | **Required**. Nome do funcionário |
| `Email` | `string` | **Required**. E-mail do funcionário  |
| `Senha` | `string` | **Required**. Senha do funcionário |
| `avatar` | `integer enum: array` | Avatar do funcionário |
| `Cargo` | `string` | **Required**. Cargo do funcionário |

</details>

<details open> 
  <summary>
    Local
  </summary>
  
#### Retornar informações do Local
```http
  GET /api/Local/
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Id` | `integer` | **Required**. Chave primária de local |
| `Cidade` | `string` | Cidade onde fica o escritório/filial |
| `Endereco` | `string` | Endereço onde se encontra o local  |
| `Capacidade` | `integer` | **Required**. Capacidade total do local |
  
</details>

## 📄 Documentação com Swagger

```http
  https://localhost:5001/swagger/index.html
```

## 🚀 Contribuições
 
 - DEV
 
|[<img src="https://avatars.githubusercontent.com/u/47722523?v=4" width=85 > <br> <sub> **`Anderson Fonseca`** </sub>](https://github.com/theandersonfonseca)||[<img src="https://avatars.githubusercontent.com/u/69330412?v=4" width=85 > <br> <sub> **`Matheus Carvalho`** </sub>](https://github.com/Matheus-Galdino)||[<img src="https://avatars.githubusercontent.com/u/19680010?v=4" width=85 > <br> <sub> **`Renan Marques`** </sub>](https://github.com/Re04nan)||
| -------- | -------- | -------- | -------- | -------- | -------- |

 - UX

|[<img src="https://media-exp1.licdn.com/dms/image/C4E03AQHvnIKmLx0S4A/profile-displayphoto-shrink_800_800/0/1622383457911?e=1637193600&v=beta&t=n7ylzdMfjkykfQg4ma9MPy9CQJH3_khmn8J22vpUt0g" width=85 > <br> <sub>  **`Julia Resende`** </sub>](https://www.linkedin.com/in/juliaresende/)||[<img src="https://miro.medium.com/fit/c/96/96/1*BT8E4KsLSeeDvwDAYk4wXw.png" width=85 > <br> <sub> **`Thiago Falchet`** </sub>](https://www.linkedin.com/in/thiago-falchet/)||
| -------- | -------- | -------- | -------- |
