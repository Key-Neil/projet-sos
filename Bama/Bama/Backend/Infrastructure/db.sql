-- Create the database if it does not exist
CREATE DATABASE IF NOT EXISTS bama;
USE bama;

-- Create Customer table if it does not exist
CREATE TABLE IF NOT EXISTS Customer (
    CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(256) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL
);


-- Create Burger table if it does not exist
CREATE TABLE IF NOT EXISTS Burger (
    BurgerId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL UNIQUE,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    Stock INT UNSIGNED DEFAULT 0,
    ImageUrl VARCHAR(2048)
);


-- Create Order table if it does not exist
CREATE TABLE IF NOT EXISTS CustomerOrder (
    CustomerOrderId INT AUTO_INCREMENT PRIMARY KEY,
    CustomerId INT NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE
);


-- Create OrderItem table if it does not exist
CREATE TABLE IF NOT EXISTS OrderItem (
    OrderItemId INT AUTO_INCREMENT PRIMARY KEY,
    CustomerOrderId INT NOT NULL,
    BurgerId INT NOT NULL,
    Quantity INT UNSIGNED NOT NULL DEFAULT 1,
    Price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CustomerOrderId) REFERENCES CustomerOrder(CustomerOrderId) ON DELETE CASCADE,
    FOREIGN KEY (BurgerId) REFERENCES Burger(BurgerId) ON DELETE CASCADE,
    UNIQUE (CustomerOrderId, BurgerId)
);


INSERT IGNORE INTO Customer (Username, PasswordHash) VALUES
('Anthony', 'hash1'),
('Maxime', 'hash2'),
('Brayan', 'hash3'),
('Asma', 'hash4');


INSERT INTO Burger (Name, Description, Price, Stock, ImageUrl)
VALUES
('Cheeseburger', 'Burger avec cheddar et steak', 6.99, 50, 'https://example.com/cheeseburger.jpg'),
('Bacon Burger', 'Burger avec bacon croustillant', 7.49, 40, 'https://example.com/baconburger.jpg'),
('Veggie Burger', 'Burger végétarien au tofu', 5.99, 30, 'https://example.com/veggieburger.jpg'),
('Double Beef Burger', 'Double steak, double fromage', 8.99, 25, 'https://example.com/doublebeef.jpg'),
('Chicken Burger', 'Burger au poulet grillé', 6.49, 35, 'https://example.com/chickenburger.jpg');
