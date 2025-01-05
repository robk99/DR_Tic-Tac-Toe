# DR_Tic-Tac-Toe
Simple application (PoC) for playing Tic-Tac-Toe with API calls only.
<br>
Consists of: 
- Register/Login with JWT Authentication
- Starting new game
- Joining an existing one
- Playing a move in a game you joined
- Getting details of a game or a user
- Getting paginated list of games with filters
- Swagger
- Postman collection and environment

## Table of contents  
1. [Prerequisties](#prerequisites)
2. [Technologies](#tech) 
3. [Basic usage](#basicUsage)
4. [Basic play](#basicPlay)
5. [Postman collection](#postman)
6. [Swagger](#swagger)

<a name="prerequisites"></a>
## Prerequisties
```
Installed locally:
.Net 8
```

<a name="tech"></a>
## Technologies
- .Net 8
- SQLite

<a name="basicUsage"></a>
## Basic usage  
- Just start the app with ```dotnet run```
- DB should be automatically seeded
- Import Postman collection and environment
- Login with: ``` {
    "username": "admin",
    "password": "admin"
}``` 
(_if you login with Postman, token will automatically be set inside headers for all the requests_)
- Or Register new user via Swagger or Postman
- Now you can create new game, get list of games, join an open game and play a move in a game you already joined

<a name="basicPlay"></a>
## Basic play
- X starts first
- <ins>staring new game</ins>: you just send a field (1-9) on which you'd like to put X on. 
1 being top left corner and 9 being the bottom right
- <ins>getting game details</ins>: you'll get a response consisting of: 
'nextTurn', 'status' and a 'board' which is for easier reading split into 3 arrays (rows)
- <ins>joining game</ins>: just send the gameId
- <ins>playing a move</ins> send gameId and a field (1-9)

<a name="postman"></a>
## Postman collection
![image](https://github.com/user-attachments/assets/b4de482a-dcd3-4ef9-9944-7251a9646db1)


<a name="swagger"></a>
## Swagger
/swagger
![image](https://github.com/user-attachments/assets/7f97929c-d30d-4816-9fd9-c86f7c51cb15)

