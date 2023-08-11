# PrisonBot
![badge](https://img.shields.io/static/v1?label=Language&message=C%23&color=blueviolet&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=architecture&message=Pure-Model&color=red&style=for-the-badge)
![badge](https://img.shields.io/static/v1?label=database&message=postgresql&color=blue&style=for-the-badge)

## Specifics
- Project using **OOP** and **SOLID**
- Good abstractions
- Using [**my database library**](https://github.com/Atennop1/Relational-Databases-Via-OOP)

## About project

This is a telegram bot-game that I made for fun and RP in one conversation where I often chat
<br>The essence of the game is this: each person has his own "prisoner's passport", which stores some information, namely: 
- ```Real name```
- ```Pseudonym```
- ```Number of years spent in prison```
- ```Social rating (how respected)```
- ```Specialization```
- ```Specialization level```
- ```Status (also as far as respect, but already a certain rank)```
- ```Date of entry into the chat```
- ```Whether a person was noticed by the python programming language```
- ```Was the person mentioned in bugurts(not joustings)```

The passport must be linked to at least one telegram account. But the bot supports the function of storing one passport for several people at once. All information is stored in a database and can be obtained in the form of a passport by anyone. 
<br>This is done in several ways, but they all use the /passport command:
- ```/passport``` without reply to message. Displays the passport of the person who sent the message, if it exists
- ```/passport``` with reply to message. Displays the passport of the person whose message this command was sent to, if it exists
- ```/passport {id}``` displays the passport of a person with the given telegram id, if it exists
- ```/passport {pseudonym}``` displays the passport of a person with the given pseudonym, if it exists

![image](https://github.com/Atennop1/PrisonBot/assets/73060890/aab34e2c-f289-4136-937c-5bda2f38d246)

## Conclusion


This project helped me gain experience in databases and showed in practice how useful [**my database library**](https://github.com/Atennop1/Relational-Databases-Via-OOP) is. Thanks to the development of this bot, I understood how to identify users by ID, learned how to work with several tables at once, how to move data to a new table, and in general I learned PostrgeSQL better. Well, my chat friends were also happy to receive such RP

