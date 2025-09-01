USE StudentDb;
CREATE TABLE Student (
    Rn INT PRIMARY KEY,
    Name NVARCHAR(100),
    Batch NVARCHAR(50),
    Marks INT
);
INSERT INTO Student (Rn, Name, Batch, Marks) VALUES
(1, 'Kavi', 'EC2025', 88),
(2, 'Sri', 'C52025', 75),
(3, 'Vishal', 'IT2025', 92),
(4, 'Harshath', 'EE2025', 97),
(5, 'Rahul', 'ME2025', 81);

SELECT * FROM Student;
