# GuessMyPassBackend


## Stack

[MongoDB](https://www.mongodb.com/), [.NET Core](https://dotnet.microsoft.com/)



## Routes

### Login


```bash
POST /user/login 
```
#### Request example : 

```
{
"email": "test@gmail.com",
"password": "12345678" 
}
```

#### Response example ** : 

```
{
    "email": "test2@test.com",
    "username": "test2",
    "password": "$2a$11$RMAujxDZaDz1912Ej2DD5uy0SaS5JycG3SimlpWc6Oi9206URIJ7u",
    "passwordHelp": "",
    "createdOn": "2020-10-18T10:40:41.567Z",
    "token": "token"
}
```

### Register
```bash
POST /user/register
```

#### Request example : 

```
{
"email": "test@test.com",
"password": "1234567890",
"username": "roflanuser",
"passwordHelp": "Some shit"
}
```

#### Response example ** : 

```
"User created"               
```

** - possible changes in future
