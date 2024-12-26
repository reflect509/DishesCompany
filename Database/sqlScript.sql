CREATE TABLE roles (
	role_id serial PRIMARY KEY,
	role_name varchar(30) NOT NULL UNIQUE
);

CREATE TABLE products (
	product_id serial PRIMARY KEY,
	articul varchar(8) NOT NULL UNIQUE,
	product_name varchar(255) NOT NULL,
	measurement_unit varchar(30) NOT NULL,
	product_cost numeric(10, 2) NOT NULL CHECK(product_cost > 0),
	max_discount numeric(5, 2) CHECK(max_discount > 0),
	manufacturer varchar(30),
	supplier varchar(30),	
	product_type varchar(30) NOT NULL,
	current_discount numeric(5, 2) CHECK(current_discount >= 0),
	amount int NOT NULL CHECK(amount >= 0),
	description text,
	image varchar(50) DEFAULT('picture.jpg') -- image path
);

CREATE TABLE delivery_places (
	delivery_place_id serial PRIMARY KEY,
	delivery_place_address varchar(100) NOT NULL
);

CREATE TABLE users (
	user_id serial PRIMARY KEY,
	full_name varchar(50) NOT NULL,
	login varchar(30) NOT NULL UNIQUE,
	pass varchar(30) NOT NULL,
	role_id int NOT NULL,
	FOREIGN KEY (role_id) REFERENCES roles(role_id)
);
CREATE SEQUENCE receiving_code_seq
	START WITH 311;

CREATE TABLE orders (
	order_id serial PRIMARY KEY,
	order_date date NOT NULL CHECK(order_date <= CURRENT_DATE),
	delivery_date date CHECK(delivery_date <= CURRENT_DATE),
	delivery_place_id int NOT NULL,
	user_id int,
	receiving_code varchar(15) DEFAULT nextval('receiving_code_seq'::regclass),
	order_status varchar(30) NOT NULL,
	FOREIGN KEY (delivery_place_id) REFERENCES delivery_places(delivery_place_id),
	FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE product_orders (
	product_articul varchar(8) REFERENCES products(articul),
	order_id int REFERENCES orders(order_id),
	amount int NOT NULL CHECK(amount >= 0),
	PRIMARY KEY(product_articul, order_id)
);