Create database PizzaStoreApp


create table Store
(
	LocationID int primary key not null,
	Stock int not null
);

create table OrderDetails
(
	OrderID int primary key identity not null,
	Pizzas int not null,
	LocationID int not null,
	UserID int not null,
	Price decimal(8,2) not null,
	DatePlaced datetime2 not null,
	foreign key (LocationID) references Store(LocationID),
	foreign key (UserID) references Users(UserID)
);


create table Users
(
	UserID int primary key not null,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null,
	DefaultLocation int not null,
	foreign key (DefaultLocation) references Store(LocationID)
);

--drop table OrderDetails; drop table Store; drop table Users


insert into Store values (101, 20);
insert into Store values (102, 20);
insert into Store values (103, 20);

insert into Users values (201, 'Matt','Liner', 101);
insert into Users values (202, 'Ben', 'Johnson', 102);
insert into Users values (203, 'Kyle', 'Smith', 103);

select * from OrderDetails
select * from Store
select * from Users