USE CompanyDb;

--drop function if already exist
DROP FUNCTION GetEmployeeData;


--Create function GetEmployeeData for get the data

CREATE FUNCTION GetEmployeeData
(
    @Active VARCHAR(5) = NULL,
    @DepartmentName VARCHAR(50) = NULL
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        e.ID,
        e.PIN,
        e.EmployeeName,
        e.DateOfBirth,
        e.ActiveStatus,
        d.DepartmentName
    FROM 
        Employee e
    INNER JOIN 
        Department d ON e.DepartmentID = d.ID
    WHERE 
        (@Active IS NULL OR e.ActiveStatus = CASE LOWER(@Active) WHEN 'true' THEN 1 ELSE 0 END) AND
        (@DepartmentName IS NULL OR LOWER(d.DepartmentName) LIKE '%' + LOWER(@DepartmentName) + '%')
);


-- Retrieve all data
SELECT * FROM GetEmployeeData(NULL, NULL);

-- Retrieve active employees in the "ICT" department
SELECT * FROM GetEmployeeData('TRUE', 'ICT');


-- Retrieve inactive employees in the "Program" department
SELECT * FROM GetEmployeeData(NULL, 'Pro');

-- Retrieve inactive employees in the "Program" department
SELECT * FROM GetEmployeeData('FALSE', NULL);