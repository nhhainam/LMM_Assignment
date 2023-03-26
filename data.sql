-- Create tables

create database LMM_Assignment
use LMM_Assignment

CREATE TABLE Roles (
  role_id INT PRIMARY KEY IDENTITY(1,1),
  role_name VARCHAR(50) NOT NULL
);

CREATE TABLE Users (
  user_id INT PRIMARY KEY IDENTITY(1,1),
  role_id INT NOT NULL,
  username VARCHAR(50) NOT NULL,
  password VARCHAR(255) NOT NULL,
  user_code VARCHAR(50) NOT NULL,
  fullname VARCHAR(50) NOT NULL,
  email VARCHAR(100),
  phone VARCHAR(25),
  FOREIGN KEY (role_id) REFERENCES Roles(role_id),
);

CREATE TABLE Class (
  class_id INT PRIMARY KEY IDENTITY(1,1),
  class_code VARCHAR(25) NOT NULL,
  description VARCHAR(100),
);

CREATE TABLE UserClass (
  user_id INT NOT NULL,
  class_id INT NOT NULL,
  PRIMARY KEY (user_id, class_id),
  FOREIGN KEY (user_id) REFERENCES Users(user_id),
  FOREIGN KEY (class_id) REFERENCES Class(class_id)
);

CREATE TABLE Material (
  material_id INT PRIMARY KEY IDENTITY(1,1),
  class_id INT NOT NULL,
  title VARCHAR(100) NOT NULL,
  file_path VARCHAR(1000),
  FOREIGN KEY (class_id) REFERENCES Class(class_id)
);

CREATE TABLE Assignment (
  assignment_id INT PRIMARY KEY IDENTITY(1,1),
  class_id INT NOT NULL,
  owner_id INT NOT NULL,
  title VARCHAR(100) NOT NULL,
  description VARCHAR(255),
  deadline datetime,
  FOREIGN KEY (class_id) REFERENCES Class(class_id),
  FOREIGN KEY (owner_id) REFERENCES Users(user_id)
);

CREATE TABLE Submission (
  submission_id INT PRIMARY KEY IDENTITY(1,1),
  assignment_id INT NOT NULL,
  owner_id INT NOT NULL,
  submission_time datetime,
  file_path VARCHAR(1000),
  FOREIGN KEY (assignment_id) REFERENCES Assignment(assignment_id),
  FOREIGN KEY (owner_id) REFERENCES Users(user_id)
);

CREATE TABLE Grade (
  grade_id INT PRIMARY KEY IDENTITY(1,1),
  submission_id INT NOT NULL,
  grade float NOT NULL,
  feedback VARCHAR(1000),
  FOREIGN KEY (submission_id) REFERENCES Submission(submission_id),
);

-- Insert sample data

INSERT INTO Roles (role_name)
VALUES
('Admin'),
('Teacher'),
('Student');

INSERT INTO Users (role_id, username, password, user_code, fullname, email, phone)
VALUES
(1, 'admin', 'admin', 'A001', 'Admin User', 'admin@example.com', '123456789'),
(2, 'teacher1', 'teacher1', 'T001', 'Teacher One', 'teacher1@example.com', '987654321'),
(2, 'teacher2', 'teacher2', 'T002', 'Teacher Two', 'teacher2@example.com', '987654321'),
(3, 'student1', 'student1', 'S001', 'Student One', 'student1@example.com', '0123456789'),
(3, 'student2', 'student2', 'S002', 'Student Two', 'student2@example.com', '0123456789');

INSERT INTO Class (class_code, description)
VALUES
('CS101', 'Introduction to Computer Science'),
('CS102', 'Data Structures and Algorithms'),
('CS201', 'Programming for Scientists and Engineers');

INSERT INTO UserClass (user_id, class_id)
VALUES
(2, 1),
(3, 1),
(3, 2),
(4, 1),
(4, 2),
(5, 2);

INSERT INTO Material (class_id, title, file_path)
VALUES
(1, 'Syllabus', 'syllabus.pdf'),
(1, 'Lecture 1', 'lecture1.pdf'),
(1, 'Lecture 2', 'lecture2.pdf'),
(2, 'Syllabus', 'syllabus.pdf'),
(2, 'Lecture 1', 'lecture1.pdf'),
(2, 'Lecture 2', 'lecture2.pdf');

INSERT INTO Assignment (class_id, owner_id, title, description, deadline)
VALUES
(1, 2, 'Ex1', 'Do this exercise', '2023-04-22 10:34:23.55'),
(1, 2, 'Ex2', 'Do this exercise', '2023-04-22 10:34:23.55'),
(1, 2, 'Ex3', 'Do this exercise', '2023-04-22 10:34:23.55'),
(2, 3, 'Ex1', 'Do this exercise', '2023-04-22 10:34:23.55'),
(2, 3, 'Ex2', 'Do this exercise', '2023-04-22 10:34:23.55'),
(2, 3, 'Ex3', 'Do this exercise', '2023-04-22 10:34:23.55');

INSERT INTO Submission (assignment_id, owner_id, submission_time, file_path)
VALUES
(1, 4, '2023-04-15 10:34:23.55', 'submission1.docx'),
(2, 4, '2023-04-15 10:34:23.55', 'submission2.docx'),
(3, 4, '2023-04-15 10:34:23.55', 'submission3.docx'),
(4, 5, '2023-04-15 10:34:23.55', 'submission4.docx'),
(5, 5, '2023-04-15 10:34:23.55', 'submission5.docx'),
(6, 5, '2023-04-15 10:34:23.55', 'submission6.docx');

INSERT INTO Grade (submission_id, grade, feedback)
VALUES
(1, 9, 'Good'),
(2, 6, 'Try better'),
(3, 8, 'Good'),
(4, 7, 'Okay'),
(5, 7, 'Okay'),
(6, 9, 'Good');