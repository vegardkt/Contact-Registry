CREATE DATABASE ContactRegistry;
USE ContactRegistry;
CREATE TABLE dbo.CONTACTS
(
    Contact_ID     CHAR(5)     PRIMARY KEY,
    Contact_FirstName   CHAR(50)    NOT NULL,
	Contact_Surname   CHAR(50)    NOT NULL,
	Contact_Company   CHAR(50)    NOT NULL,
	Contact_Phone   CHAR(10)    NOT NULL,
	Contact_Mail   CHAR(50)    NOT NULL,
	Contact_Title CHAR(50) NOT NULL
);

CREATE TABLE ContactRegistry.dbo.USERS
(
    Users_ID     CHAR(5)     PRIMARY KEY,
	Users_Name     CHAR(50)		NOT NULL,
    Users_Password   CHAR(50)    NOT NULL,
	
);

INSERT INTO USERS(Users_ID,Users_Name,Users_Password)
VALUES('2','User','Password');