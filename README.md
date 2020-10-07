# GuessMyPassBackend

![](https://github.com/NaUKMA-NIT-20/GuessMyPass/blob/Backend-3/GuessMyPassBackend/Img/123414.jpg)


## Stack

[MongoDB](https://www.mongodb.com/), [.NET Core](https://dotnet.microsoft.com/)



## Routes


### Get all users

```bash
GET /user/
```

### Login


```bash
POST /user/login 
```
#### Request example : 

```
{
"email": "test@gmail.com",
"hashedPassword": "da ya i sho" 
}
```

#### Response example ** : 

```
{
    "dbId": {
        "timestamp": 1601502042,
        "machine": 12066299,
        "pid": 9316,
        "increment": 1790873,
        "creationTime": "2020-09-30T21:40:42Z"
    },
    "email": "test@gmail.com",
    "username": "daunych",
    "hashedPassword": "da ya i sho",
    "confirmed": false,
    "passwordHelp": "Asarann dodik",
    "createdOn": "2020-09-30T21:40:41.747Z",
    "dataReference": []
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
"hashedPassword": "1234567890",
"username": "roflanuser",
"passwordHelp": "Some shit"
}
```

#### Response example ** : 

```
true / false                
```

** - possible changes in future
