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
"Email": "test@test.com",
"HashedPassword": "1234567890" 
}
```


### Register
```bash
POST /user/register
```

#### Request example : 

```
{
"Email": "test@test.com",
"HashedPassword": "1234567890",
"Username": "roflanuser",
"PasswordHelp": "Some shit", 
}
