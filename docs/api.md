# Api docs

These docs describe how to use the [GuessMyPass](https://guessmypass.herokuapp.com/) API.

`Requires token` means that you have to add token to your request headers

## Routes

| Controller | Methods | URI |
| :--- | :--- | :--- |
| `UserController` | `GET POST PUT` | /user |
| `DataController` | `GET POST PUT DELETE` | /data |
| `FolderController` | `GET POST PUT DELETE` | /folder |
| `DataController` | `POST` | /update/key |

-------------------
## UserController
-------------------

### Register

<br>

POST `/user/register`

Request
```JSON
{
    "email": "testuser@gmail.com",
    "username": "testusergmail",
    "password": "12345678",
    "passwordHelp": "Help"
}
```

The `email` attribute is your `unique` email.

The `username` attribute is your `unique` username.

The `password` attribute is your password.

The `passwordHelp` attribute is optional.

Response if status `200`:
```JSON
{
    "message": "User was created"
}
```

Response if status `400`:
```JSON
{
    "error": "Empty request" | "Wrong request. User already exists"
}
```

<br>

### Login

<br>

POST `/user/login`

Request:
```JSON
{
    "email": "testuser@gmail.com",
    "password": "12345678"
}
```

The `email` attribute is your `unique` email.

The `password` attribute is your password.

Response if status `200`:
```JSON
{
    "email": "your email",
    "username": "your username",
    "password": "your password",
    "passwordHelp": "Help",
    "createdOn": "Date",
    "token": "your token"
}
```
Response if status `404`:
```JSON
{
    "error": "Wrong email or password"
}
```

### Options

<br>

Change your password

`Requires token` 

PUT `/user/options/password`

Request:
```JSON
{
    "password": "current password",
    "newPassword": "new password"
}
```

The `password` attribute is your current password.

The `newPassword` attribute is your new password.

Response if status `200`:
```JSON
{
    "message": "Password updated"
}
```
Response if status `400`:
```JSON
{
    "error": "Wrong password provided"
}
```

Change your username

`Requires token` 

PUT `/user/options/username`

Request:
```JSON
{
    "username": "current username",
    "newUsername": "new username"
}
```

The `username` attribute is your current username.

The `newUsername` attribute is your new username.

Response if status `200`:
```JSON
{
    "message": "Username updated"
}
```
Response if status `400`:
```JSON
{
    "error": "Wrong Username" | "User with same username already exists" 
}
```

-------------------
## DataController
-------------------

### Add new data

<br>

`Requires token` 


POST `/data`

Request
```JSON
{
    "name": "name",
    "password": "password",
    "url": "url",
    "notes": "notes",
    "cardholderName": "",
    "number": "",
    "cvv": ""
}
```
All fields are optional

Response if status `200`:
```JSON
{
    "id": "id",
    "userId": "user Id",
    "name": "",
    "password": "password",
    "url": "",
    "notes": "",
    "cardholderName": "",
    "number": "",
    "cvv": ""
}
```
`id` is `unique`.

<br>

### Update your data

<br>

`Requires token` 


PUT `/data`

Request
```JSON
{
    "id": "id",
    "name": "name",
    "password": "password",
    "url": "url",
    "notes": "notes",
    "cardholderName": "my name",
    "number": "",
    "cvv": ""
}
```

`id` is required

Response if status `200`:
```JSON
{
    "id": "id",
    "userId": "user Id",
    "name": "name",
    "password": "password",
    "url": "url",
    "notes": "notes",
    "cardholderName": "my name",
    "number": "",
    "cvv": ""
}
```

Response if status `400`:
```JSON
{
    "error": "Wrong data! Try again!"
}
```

<br>

### Get your data

<br>

`Requires token` 

GET `/data`

Response if status `200`:
```JSON
[
    {
        "id": "id1",
        "userId": "user Id",
        "name": "name",
        "password": "password",
        "url": "url",
        "notes": "notes",
        "cardholderName": "",
        "number": "",
        "cvv": ""
    },
    {
        "id": "id2",
        "userId": "user Id",
        "name": "",
        "password": "password", 
        "url": "",
        "notes": "",
        "cardholderName": "",
        "number": "",
        "cvv": ""
    }
]
```

### Delete your data

<br>

`Requires token` 

DELETE `/data/{id}`

Response if status `200`:
```JSON
{   
    "message": "Data deleted successfully"
}
```

Response if status `400`:
```JSON
{
    "error": "Data with this id doesn't exist"
}
```

-------------------
## KeyController
-------------------

### Update all data

<br>

`Requires token` 


POST `/update/key`

Request
```JSON
[
    "data": {
        "id": "id1",
        "userId": "user Id",
        "name": "name",
        "password": "password",
        "url": "url",
        "notes": "notes",
        "cardholderName": "",
        "number": "",
        "cvv": ""
    },
    {
        "id": "id1",
        "userId": "user Id",
        "name": "name",
        "password": "password",
        "url": "url",
        "notes": "notes",
        "cardholderName": "",
        "number": "",
        "cvv": ""
    }
]
```

Response if status `200`:
```JSON
{
    "message": "Success"
}
```

<br>

##  Status Codes

API returns the following status codes in its API:

| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |  
| 201 | `CREATED` |
| 400 | `BAD REQUEST` |
| 401 | `UNAUTHORIZED`
| 404 | `NOT FOUND` |
| 409 | `CONFLICT` |
| 500 | `INTERNAL SERVER ERROR` |
