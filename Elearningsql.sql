create database Education


select * from Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY identity,  -- AUTO_INCREMENT for auto-incrementing IDs
    UserRole NVARCHAR(50), 
    Name NVARCHAR(100) NOT NULL,
    UserEmail NVARCHAR(100) UNIQUE NOT NULL, 
    Contact NVARCHAR(100),
    UserStatus NVARCHAR(50), 
    Password NVARCHAR(255) NOT NULL,
    ProfileImg NVARCHAR(255)
);

CREATE TABLE Reviews (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    Rating INT NOT NULL,
    ReviewText NVARCHAR(MAX) NOT NULL,
	CourseName varchar(100),
	UserEmail varchar(100),
    CreatedDate DATETIME DEFAULT GETDATE()
);

drop table Reviews
CREATE TABLE Support (
    SupportID INT IDENTITY(1,1) PRIMARY KEY,
    UserEmail NVARCHAR(100),
    CourseName VARCHAR(255),
    MasterCourseName VARCHAR(255),
    ProblemDescription NVARCHAR(MAX),
    Solution NVARCHAR(MAX) NULL,
    SubmittedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserEmail) REFERENCES Users(UserEmail)
);
CREATE TABLE Certificates (
    CertificateID INT PRIMARY KEY IDENTITY,
    UserEmail NVARCHAR(100) NOT NULL,
    CertificatePath NVARCHAR(255) NOT NULL,
    GeneratedDate DATETIME DEFAULT GETDATE()
);
select * from Users
delete  from Certificates
insert into Users values('Student','Onkar','onkarsalunkhe.off@gmail.com','8999960364','Active','123',null)

CREATE TABLE MasterCourses (
    MasterCourseID INT identity ,  
    MasterCourseName VARCHAR(255) PRIMARY KEY NOT NULL,         
	Pic varchar(100)
);

alter table Course add  Pic varchar(100);

select * from MasterCourses
INSERT INTO MasterCourses (MasterCourseName) VALUES ('Fullstack Java')
truncate table MasterCourses
delete from MasterCourses
CREATE TABLE Course (
    CourseId int identity,
    CourseName VARCHAR(255) PRIMARY KEY,
    MasterCourseName VARCHAR(255),
    Description Varchar NULL,
    Price DECIMAL(10, 2) NOT NULL,
	Pic varchar(100),
    FOREIGN KEY (MasterCourseName) REFERENCES MasterCourses(MasterCourseName) ON DELETE CASCADE -- Fixed the reference
);
select * from course
ALTER TABLE Course ALTER COLUMN CourseName NVARCHAR(MAX);
ALTER TABLE Course ALTER COLUMN Description NVARCHAR(MAX);


alter proc CourseList1
 @MasterCourseName VARCHAR(255)
 as
 begin
SELECT * FROM dbo.Course WHERE MasterCourseName = @MasterCourseName;
end

select * from Topic
CREATE TABLE Question (
QuestionID int identity,
    QuestionText VARCHAR(500) PRIMARY KEY,
    TopicName VARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (TopicName) REFERENCES Topic(TopicName) ON DELETE CASCADE
);
SELECT * FROM Topic WHERE CourseName = 'Html';

select * from Choice
CREATE TABLE Choice (
    choiceid INT Identity PRIMARY KEY,  -- Added missing primary key
    ChoiceText VARCHAR(500),
    QuestionText VARCHAR(500) NOT NULL,
    IsCorrect BIT NOT NULL,  -- Changed BOOLEAN to BIT
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (QuestionText) REFERENCES Question(QuestionText) ON DELETE CASCADE
);




CREATE TABLE UserReports (
    ReportID INT IDENTITY PRIMARY KEY,
    UserEmail NVARCHAR(100),
    TopicName NVARCHAR(255),
    CorrectAnswers INT,
    WrongAnswers INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);


select * from UserReports

CREATE TABLE Purchase (
    PurchaseID INT IDENTITY(1,1) PRIMARY KEY,  -- Correct identity definition
    UserEmail NVARCHAR(100),  -- Fixed from INT to NVARCHAR(100)
    MasterCourseName VARCHAR(255) NULL,
    CourseName VARCHAR(255) NULL,
    PurchaseType NVARCHAR(50) NOT NULL,  -- Changed ENUM to NVARCHAR(50)
    Price DECIMAL(10, 2) NOT NULL,
    PurchaseDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserEmail) REFERENCES Users(UserEmail) ON DELETE NO ACTION,  -- Fixed UserEmail reference
    FOREIGN KEY (MasterCourseName) REFERENCES MasterCourses(MasterCourseName) ON DELETE CASCADE, -- Retaining cascading delete for MasterCourseName
    FOREIGN KEY (CourseName) REFERENCES Course(CourseName) ON DELETE NO ACTION  -- Prevent cascading delete for CourseName
);
			select * from Purchase
			Delete from Purchase
			update Purchase set PurchaseDate='2024-11-01 02:25:10.187' where CourseName='Html'
delete from Purchase
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY identity,
    Email varchar(100),
    NotificationText NVARCHAR(MAX),
    SentAt DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0 -- 0 = Unread, 1 = Read
);
select * from Notifications
drop table Notifications


CREATE PROCEDURE CourseList
    @MasterCourseName VARCHAR(255)
AS
BEGIN
    -- Select the details of all courses belonging to the specified master course
    SELECT 
        *
    FROM 
        Course
    WHERE 
        MasterCourseName = @MasterCourseName
END

delete proc CourseList
select * from Course
select * from Topic

	create proc saveCourse
	@CourseName varchar(100),
	@MasterCourseName varchar(100),
	 @Description TEXT NULL,
	@Price decimal(10,2),
	@Pic varchar(100)
	as
	begin
	insert into Course values(@CourseName,@MasterCourseName,@Description,@Price,@Pic);
	end


	create PROC GetMasterCourseDetails
	@MasterCourseName varchar(225)
AS
BEGIN
    -- Fetch aggregated data from courses and master courses
     SELECT 
     mc.MasterCourseName,
     STRING_AGG(c.CourseName, ', ') AS CourseNames, 
     STRING_AGG(c.Description, ', ') AS Descriptions,
     SUM(c.Price) AS TotalPrice,
     mc.Pic
 FROM 
     Course c
 JOIN 
     MasterCourses mc ON c.MasterCourseName = mc.MasterCourseName
 WHERE 
     mc.MasterCourseName = @MasterCourseName
 GROUP BY 
     mc.MasterCourseName,mc.Pic
END;


create proc CourseList1
 @MasterCourseName VARCHAR(255)
 as
 begin
SELECT Pic, CourseName, Description, Price FROM dbo.Course WHERE MasterCourseName = @MasterCourseName;
end

ALTER TABLE Course ALTER COLUMN CourseName NVARCHAR(MAX);
ALTER TABLE Course ALTER COLUMN Description NVARCHAR(MAX);
SELECT Pic FROM Course;
SELECT Pic FROM MasterCourses;


create PROCEDURE LoginProc
    @Email NVARCHAR(100),
    @Password NVARCHAR(255)
AS
BEGIN
    SELECT 
        UserEmail,
        Password ,
        UserRole 
    FROM Users
    WHERE 
        UserEmail = @Email AND Password = @Password;
END;


create proc FetchUser
@Email Nvarchar(100)
as
begin
select UserEmail from Users where UserEmail=@Email;
end;


CREATE PROCEDURE InsertUser
    @Name NVARCHAR(100),
    @UserEmail NVARCHAR(100),
    @Password NVARCHAR(100),
    @Contact NVARCHAR(20),
    @UserRole NVARCHAR(20)
AS
BEGIN
 
    INSERT INTO Users (Name, UserEmail, Password, Contact, UserRole)
    VALUES (@Name, @UserEmail, @Password, @Contact, @UserRole);
END;


CREATE TABLE Discussions (
    DiscussionID INT IDENTITY PRIMARY KEY,
    CourseID INT NOT NULL,
    UserEmail NVARCHAR(100) NOT NULL, -- Using UserEmail as FK
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserEmail) REFERENCES Users(UserEmail) ON DELETE CASCADE
);


CREATE TABLE DiscussionReplies (
    ReplyID INT IDENTITY PRIMARY KEY,
    DiscussionID INT NOT NULL,
    UserEmail NVARCHAR(100) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (DiscussionID) REFERENCES Discussions(DiscussionID) ON DELETE NO ACTION,
    FOREIGN KEY (UserEmail) REFERENCES Users(UserEmail) ON DELETE NO ACTION
);


CREATE PROCEDURE InsertDiscussion
    @CourseID INT,
    @UserEmail NVARCHAR(100),
    @Title NVARCHAR(255),
    @Content NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Discussions (CourseID, UserEmail, Title, Content)
    VALUES (@CourseID, @UserEmail, @Title, @Content);
END;


CREATE PROCEDURE FetchDiscussionsByCourse
    @CourseID INT
AS
BEGIN
    SELECT 
        d.DiscussionID,
        d.Title,
        d.Content,
        d.CreatedAt,
        u.Name AS Author
    FROM Discussions d
    INNER JOIN Users u ON d.UserEmail = u.UserEmail
    WHERE d.CourseID = @CourseID
    ORDER BY d.CreatedAt DESC;
END;


CREATE PROCEDURE InsertDiscussionReply
    @DiscussionID INT,
    @UserEmail NVARCHAR(100),
    @Content NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO DiscussionReplies (DiscussionID, UserEmail, Content)
    VALUES (@DiscussionID, @UserEmail, @Content);
END;



CREATE PROCEDURE FetchRepliesByDiscussion
    @DiscussionID INT
AS
BEGIN
    SELECT 
        r.ReplyID,
        r.Content,
        r.CreatedAt,
        u.Name AS Author
    FROM DiscussionReplies r
    INNER JOIN Users u ON r.UserEmail = u.UserEmail
    WHERE r.DiscussionID = @DiscussionID
    ORDER BY r.CreatedAt ASC;
END;


select * from DiscussionReplies

delete from Discussions


select * from Notifications


CREATE PROCEDURE GetUnreadNotificationCount
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT COUNT(*) AS UnreadCount
    FROM Notifications
    WHERE Email = @Email AND IsRead = 0;
END;


CREATE PROCEDURE GetNotifications
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT NotificationID, NotificationText, SentAt, IsRead
    FROM Notifications
    WHERE Email = @Email
    ORDER BY SentAt DESC;
END;


CREATE PROCEDURE MarkNotificationsAsRead
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Notifications
    SET IsRead = 1
    WHERE Email = @Email;
END;




CREATE TABLE Cart (
    CartID INT IDENTITY PRIMARY KEY,
    UserEmail NVARCHAR(100) NOT NULL, -- User's email
    ItemType NVARCHAR(50) NOT NULL, -- 'Course' or 'MasterCourse'
    ItemName NVARCHAR(255) NOT NULL, -- CourseName or MasterCourseName
    Price DECIMAL(10, 2) NOT NULL, -- Price of the course or master course
    Quantity INT DEFAULT 1,
    AddedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserEmail) REFERENCES Users(UserEmail)
);

select * from Cart

create  proc fetchcourses 
@email  NVARCHAR(100)
as
begin

SELECT UserEmail, MasterCourseName,CourseName FROM Purchase where UserEmail=@email;

end


select * from 