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
"email": "test@test.com",
"hashedPassword": "1234567890" 
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
