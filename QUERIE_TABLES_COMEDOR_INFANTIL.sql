USE [comedor-infantil-01];

SELECT * FROM Activities;
SELECT * FROM Volunteers;
SELECT * FROM AssignmentActivities;
SELECT * FROM Beneficiaries;
SELECT * FROM Donors;
SELECT * FROM Inventory;
SELECT * FROM InKindDonations;
SELECT * FROM MoneyDonations;
SELECT * FROM Users;
SELECT * FROM Audits;
SELECT * FROM Modules;
SELECT * FROM ModulesForUser;
Select * FROM ModulesForUser MU
INNER JOIN Modules M ON M.ModuleId = MU.ModuleId
WHERE UserId = 1;

INSERT INTO ModulesForUser(ModuleId, UserId) 
VALUES (1,1);

