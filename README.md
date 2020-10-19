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
    "dbId": {
        "timestamp": 1601502042,
        "machine": 12066299,
        "pid": 9316,
        "increment": 1790873,
        "creationTime": "2020-09-30T21:40:42Z"
    },
    "email": "test@gmail.com",
    "username": "daunych",
    "password": "djaksdkaskdjaksjdksa312312312",
    "passwordHelp": "Asarann dodik",
    "createdOn": "2020-09-30T21:40:41.747Z"
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
