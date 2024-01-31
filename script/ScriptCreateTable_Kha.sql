CREATE TABLE User (
    UserID INT PRIMARY KEY,
    Password VARCHAR(100),
    PhoneNumber VARCHAR(15) UNIQUE
);

CREATE TABLE TopUpMember (
    TopUpMemberID INT PRIMARY KEY,
    UserID INT UNIQUE,
    MemberName VARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES User(UserID)
);

CREATE TABLE Admin (
    AdminID INT PRIMARY KEY,
    UserID INT UNIQUE,
    AdminName VARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES User(UserID)
);


CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY,
    UserID INT UNIQUE,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    Address VARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES User(UserID)
);

CREATE TABLE StoreOwner (
    StoreOwnerID INT PRIMARY KEY,
    UserID INT UNIQUE,
    OwnerName VARCHAR(50),
    StoreID INT,
    FOREIGN KEY (UserID) REFERENCES User(UserID)
);


