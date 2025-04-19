-- Insertar datos en Beneficiaries
INSERT INTO Beneficiaries (FirstName, LastName, BirthDate, Status)
VALUES
('Juan', 'P�rez', '2005-05-15', 'Activo'),
('Mar�a', 'Gonz�lez', '1990-09-23', 'Inactivo'),
('Pedro', 'L�pez', '2000-03-12', 'Activo'),
('Ana', 'Ram�rez', '1985-07-21', 'Inactivo'),
('Carlos', 'Mart�nez', '2010-11-01', 'Activo'),
('Julia', 'S�nchez', '1995-02-17', 'Inactivo'),
('Jos�', 'Fern�ndez', '2003-09-28', 'Activo'),
('Patricia', 'G�mez', '1992-05-10', 'Inactivo'),
('Miguel', 'Torres', '2007-01-04', 'Activo'),
('Luisa', 'Hern�ndez', '1998-12-31', 'Inactivo');

-- Insertar datos en Inventory
INSERT INTO Inventory (ProductName, Description, Quantity, EntryDate, ExpiryDate)
VALUES
('Arroz', 'Arroz de grano largo', 100, '2025-02-23', '2025-12-31'),
('Frijoles', 'Frijoles negros', 200, '2025-02-20', '2025-11-30'),
('Az�car', 'Az�car refinada', 150, '2025-02-18', '2025-10-31'),
('Aceite', 'Aceite vegetal', 50, '2025-02-22', '2025-06-30'),
('Sal', 'Sal de mesa', 300, '2025-02-21', '2025-12-01'),
('Harina', 'Harina de trigo', 120, '2025-02-19', '2025-09-15'),
('Pasta', 'Pasta de trigo', 200, '2025-02-23', '2025-11-15'),
('Leche', 'Leche en polvo', 80, '2025-02-20', '2025-05-30'),
('Galletas', 'Galletas dulces', 500, '2025-02-24', '2025-08-15'),
('Fruta', 'Fruta deshidratada', 70, '2025-02-18', '2025-06-01');

-- Insertar datos en Activities
INSERT INTO Activities (Name, Description, StartDate, EndDate)
VALUES
('Recolecci�n de alimentos', 'Actividad de recolecci�n de alimentos', '2025-03-01 08:00', '2025-03-01 12:00'),
('Entrega de v�veres', 'Entrega de v�veres a familias necesitadas', '2025-03-02 09:00', '2025-03-02 13:00'),
('Reforestaci�n', 'Plantar �rboles en �reas afectadas', '2025-03-05 10:00', '2025-03-05 16:00'),
('Clases de educaci�n financiera', 'Taller de educaci�n financiera para familias', '2025-03-10 09:00', '2025-03-10 12:00'),
('Taller de cocina', 'Taller de cocina con productos locales', '2025-03-12 15:00', '2025-03-12 18:00'),
('Campa�a de donaci�n de ropa', 'Recolecta de ropa para personas necesitadas', '2025-03-15 08:00', '2025-03-15 14:00'),
('Campamento infantil', 'Actividades recreativas para ni�os', '2025-03-20 08:00', '2025-03-20 18:00'),
('Torneo deportivo', 'Competencia deportiva para j�venes', '2025-03-22 09:00', '2025-03-22 16:00'),
('Exposici�n de arte', 'Muestra de arte de artistas locales', '2025-03-25 17:00', '2025-03-25 20:00'),
('Charla sobre medio ambiente', 'Charla educativa sobre el cuidado del medio ambiente', '2025-03-30 10:00', '2025-03-30 12:00');

-- Insertar datos en Donors
INSERT INTO Donors (FirstName, LastName, DonorType, Phone, Address)
VALUES
('Carlos', 'Mart�nez', 'Individual', '88888888', 'Calle Ficticia 123'),
('Ana', 'L�pez', 'Empresa', '77777777', 'Avenida Ejemplo 456'),
('Luis', 'Garc�a', 'Individual', '66666666', 'Calle Real 789'),
('Mar�a', 'P�rez', 'Individual', '55555555', 'Avenida Central 321'),
('Jorge', 'Torres', 'Empresa', '44444444', 'Plaza Mayor 123'),
('Rosa', 'Fern�ndez', 'Individual', '33333333', 'Calle Luna 456'),
('Miguel', 'Ram�rez', 'Individual', '22222222', 'Calle Sol 789'),
('Carlos', 'Gonz�lez', 'Empresa', '11111111', 'Bulevar Norte 123'),
('Laura', 'Hern�ndez', 'Individual', '00000000', 'Avenida Norte 456'),
('Jos�', 'S�nchez', 'Empresa', '99999999', 'Calle Primavera 321');

-- Insertar datos en TypeIdentifications
INSERT INTO TypeIdentifications (TypeIdentification, Status)
VALUES
('C�dula de identidad', 'Activo'),
('Pasaporte', 'Inactivo'),
('Carn� de extranjer�a', 'Activo'),
('DNI', 'Inactivo'),
('RUC', 'Activo'),
('Licencia de conducir', 'Inactivo'),
('C�dula de residente', 'Activo'),
('C�dula diplom�tica', 'Inactivo'),
('Tarjeta de identidad', 'Activo'),
('Carn� de trabajo', 'Inactivo');

-- Insertar datos en Volunteers
INSERT INTO Volunteers (FirstName, LastName, Identification, TypeIdentification, Phone, Availability, Status)
VALUES
('Luis', 'S�nchez', '123456789', 1, '99999999', 'Lunes a Viernes 8am-12pm', 'Activo'),
('Julia', 'Ram�rez', '987654321', 2, '66666666', 'S�bados 9am-1pm', 'Inactivo'),
('Carlos', 'Mart�nez', '123123123', 3, '88888888', 'Lunes a Viernes 1pm-5pm', 'Activo'),
('Patricia', 'G�mez', '456456456', 4, '77777777', 'Lunes a Viernes 9am-3pm', 'Inactivo'),
('Juan', 'L�pez', '789789789', 5, '55555555', 'Fines de semana', 'Activo'),
('Jos�', 'Torres', '321321321', 6, '44444444', 'Martes y Jueves 8am-12pm', 'Inactivo'),
('Mar�a', 'Fern�ndez', '654654654', 7, '33333333', 'Lunes a Viernes 10am-2pm', 'Activo'),
('Rosa', 'Garc�a', '987987987', 8, '22222222', 'Mi�rcoles y Viernes 9am-1pm', 'Inactivo'),
('Miguel', 'Ram�rez', '321321321', 9, '11111111', 'Martes a Jueves 2pm-6pm', 'Activo'),
('Carlos', 'Gonz�lez', '159159159', 10, '00000000', 'Fines de semana 10am-4pm', 'Inactivo');

-- Insertar datos en AssignmentActivities
INSERT INTO AssignmentActivities (VolunteerId, ActivityId, AssignmentDate)
VALUES
(1, 1, '2025-02-23 08:00'),
(2, 2, '2025-02-23 09:00'),
(3, 3, '2025-02-24 10:00'),
(4, 4, '2025-02-24 11:00'),
(5, 5, '2025-02-25 12:00'),
(6, 6, '2025-02-25 13:00'),
(7, 7, '2025-02-26 14:00'),
(8, 8, '2025-02-26 15:00'),
(9, 9, '2025-02-27 16:00'),
(10, 10, '2025-02-27 17:00');

-- Insertar datos en MoneyDonations
INSERT INTO MoneyDonations (DonorId, Amount, DonationDate, Porpuse)
VALUES
(1, 500.00, '2025-02-20', 'Apoyo a la recolecci�n de alimentos'),
(2, 1000.00, '2025-02-22', 'Compra de v�veres'),
(3, 200.00, '2025-02-21', 'Compra de material educativo'),
(4, 150.00, '2025-02-23', 'Apoyo a la reforestaci�n'),
(5, 300.00, '2025-02-24', 'Construcci�n de viviendas'),
(6, 400.00, '2025-02-25', 'Rehabilitaci�n de parques'),
(7, 250.00, '2025-02-26', 'Acondicionamiento de espacios deportivos'),
(8, 500.00, '2025-02-27', 'Apoyo a ni�os con necesidades'),
(9, 350.00, '2025-02-28', 'Donaci�n de becas educativas'),
(10, 600.00, '2025-03-27', 'Campa�a contra el hambre');

-- Insertar datos en InKindDonations
INSERT INTO InKindDonations (DonorId, ProductId, DonationDate)
VALUES
(1, 1, '2025-02-21'),
(2, 2, '2025-02-22'),
(3, 3, '2025-02-23'),
(4, 4, '2025-02-24'),
(5, 5, '2025-02-25'),
(6, 6, '2025-02-26'),
(7, 7, '2025-02-27'),
(8, 8, '2025-02-28'),
(9, 9, '2025-03-01'),
(10, 10, '2025-03-02');

-- Insertar datos en Users
INSERT INTO Users (Email, Password, FirstName, LastName, Status)
VALUES
('juan.perez@example.com', 'password123', 'Juan', 'P�rez', 'Activo'),
('maria.gonzalez@example.com', 'password456', 'Mar�a', 'Gonz�lez', 'Inactivo'),
('pedro.lopez@example.com', 'password789', 'Pedro', 'L�pez', 'Activo'),
('ana.ramirez@example.com', 'password101', 'Ana', 'Ram�rez', 'Inactivo'),
('carlos.martinez@example.com', 'password202', 'Carlos', 'Mart�nez', 'Activo'),
('julia.sanchez@example.com', 'password303', 'Julia', 'S�nchez', 'Inactivo'),
('jose.fernandez@example.com', 'password404', 'Jos�', 'Fern�ndez', 'Activo'),
('patricia.gomez@example.com', 'password505', 'Patricia', 'G�mez', 'Inactivo'),
('miguel.torres@example.com', 'password606', 'Miguel', 'Torres', 'Activo'),
('luisa.hernandez@example.com', 'password707', 'Luisa', 'Hern�ndez', 'Inactivo');

-- Insertar datos en Audits
INSERT INTO Audits (UserId, Action, Description, ActionDate)
VALUES
(1, 'I', 'Insert� un nuevo beneficiario', '2025-02-23 10:00'),
(2, 'U', 'Actualiz� el estado de un voluntario', '2025-02-23 11:00'),
(3, 'D', 'Elimin� un donante', '2025-02-23 12:00'),
(4, 'I', 'Insert� una nueva actividad', '2025-02-23 13:00'),
(5, 'U', 'Actualiz� un inventario', '2025-02-23 14:00'),
(6, 'D', 'Elimin� un registro de donaci�n', '2025-02-23 15:00'),
(7, 'I', 'Insert� un nuevo voluntario', '2025-02-23 16:00'),
(8, 'U', 'Actualiz� el prop�sito de una donaci�n', '2025-02-23 17:00'),
(9, 'D', 'Elimin� un registro de actividad', '2025-02-23 18:00'),
(10, 'I', 'Insert� un nuevo m�dulo', '2025-02-23 19:00');

-- Insertar datos en Modules
INSERT INTO Modules (ModuleName, ClassCSS, Link)
VALUES
('M�dulo de Gesti�n', 'class-gestion', '/gestion'),
('M�dulo de Donaciones', 'class-donaciones', '/donaciones'),
('M�dulo de Voluntarios', 'class-voluntarios', '/voluntarios'),
('M�dulo de Beneficiarios', 'class-beneficiarios', '/beneficiarios'),
('M�dulo de Actividades', 'class-actividades', '/actividades'),
('M�dulo de Inventario', 'class-inventario', '/inventario'),
('M�dulo de Informes', 'class-informes', '/informes'),
('M�dulo de Audits', 'class-audits', '/audits'),
('M�dulo de Usuarios', 'class-usuarios', '/usuarios'),
('M�dulo de Campa�as', 'class-campanas', '/campanas');

-- Insertar datos en ModulesForUser
INSERT INTO ModulesForUser (ModuleId, UserId)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);
