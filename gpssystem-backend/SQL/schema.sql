CREATE TABLE users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(120) NOT NULL,
	password VARCHAR(120) NOT NULL,
	email VARCHAR(120) NOT NULL,
	address VARCHAR(120) NOT NULL
);

CREATE TABLE hospitals (
    hospital_id INT IDENTITY(1,1) PRIMARY KEY,
	hospital_name VARCHAR(120) NOT NULL,
	hospital_address VARCHAR(120) NOT NULL,
	hospital_pos VARCHAR(120) NOT NULL,
);

CREATE TABLE drivers (
    driver_id INT IDENTITY(1,1) PRIMARY KEY,
	imei VARCHAR(40) NOT NULL,
	hospital_id INT NOT NULL FOREIGN KEY REFERENCES hospitals(hospital_id),
);

CREATE TABLE requests (
    request_id BIGINT IDENTITY(1,1) PRIMARY KEY,
	driver_id INT NOT NULL FOREIGN KEY REFERENCES drivers(driver_id),
	user_id INT NULL,
	client_name VARCHAR(120) NULL,
	address_name VARCHAR(120) NOT NULL,
	address_pos VARCHAR(120) NOT NULL,
	start_time DATETIME NOT NULL,
	arrival_time DATETIME NULL,
	back_time DATETIME NULL,
	status VARCHAR(120) NOT NULL
);

CREATE TABLE locations (
    [location_id] BIGINT IDENTITY(1,1) PRIMARY KEY,
	[driver_id] INT NOT NULL,
	[pos] VARCHAR(max) NOT NULL,
	[time] DATETIME NOT NULL
);

CREATE TABLE token (
    token_id BIGINT IDENTITY(1,1) PRIMARY KEY,
	token VARCHAR(max) NOT NULL,
	start_time DATETIME NOT NULL
);


CREATE VIEW [AvaliableDrivers] AS
SELECT d.* FROM drivers as d
LEFT JOIN (SELECT r.* FROM requests AS r
		   JOIN (SELECT MAX(r.request_id) AS request_id FROM requests AS r GROUP BY r.driver_id) AS last_request_id
		   ON r.request_id = last_request_id.request_id) AS r
ON d.driver_id=r.driver_id
JOIN (SELECT l.location_id, l.driver_id, l.time FROM locations AS l
	JOIN (SELECT MAX(l.location_id) AS location_id FROM locations AS l GROUP BY l.driver_id) AS recent 
	ON recent.location_id = l.location_id) AS l ON d.driver_id = l.driver_id
WHERE (r.driver_id IS NULL OR r.status = 'Completed') AND l.time >= (SELECT DATEADD(hh,-1,GETUTCDATE()))



CREATE VIEW [CurrentPos] AS
SELECT r.request_id, r.driver_id, r.address_pos, l.pos AS driver_pos, l.time, r.status FROM requests AS r
JOIN (SELECT l.* FROM locations AS l
	JOIN (SELECT MAX(l.location_id) AS location_id FROM locations AS l GROUP BY l.driver_id) AS recent 
	ON recent.location_id = l.location_id) AS l ON r.driver_id = l.driver_id
WHERE r.status != 'Completed'


insert into users values('Turar', '123', 'asqaria@hotmail.com', '1.305140, 103.763909')
insert into users values('Azamat', '123', 'azamat@hotmail.com', '1.353045, 103.847801')


insert into hospitals values ('National clinic N2', 'Kazakhstan, Petropavl, Vasilev street, 123', '54.869150, 69.159171')
insert into hospitals values ('Balalar klinikasy', 'Kazakhstan, Petropavl, Aleksandr Pushkin alleyway, 23А', '54.867644, 69.117000')
insert into drivers values('359633106503621', 1)
insert into drivers values('893247693373453', 2)
insert into drivers values('324734543532643', 1)
insert into drivers values('798355645633452', 2)


insert into locations values (2, '54.866076039881314, 69.14528889831979', GETDATE())
insert into locations values (4, '54.86886611641185, 69.12995281152092', GETDATE())
insert into locations values (4, '54.860278618017524, 69.14417902198693', GETDATE())
insert into locations values (3, '54.86721413089328, 69.15725190905238', GETDATE())