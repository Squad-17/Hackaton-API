
# Hackathon-FCamara API

API tem como objetivo fornecer o serviço para agendamento de colaboradores da FCamara em seus escritórios ou filiais, visando garantir a segurança respeitando a capacidade máxima de cada ambiente de acordo com a legislação imposta por conta da pandemia (COVID-19).


## API Referência

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

## Documentação com Swagger

```http
  https://localhost:5001/swagger/index.html
```
