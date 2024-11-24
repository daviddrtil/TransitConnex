-- DROP DATABASE TransitConnex;
-- 
-- CREATE DATABASE TransitConnex;
-- 
-- USE TransitConnex;
-- GO

CREATE TABLE Icon
(
    Id   UNIQUEIDENTIFIER PRIMARY KEY,
    Svg  NVARCHAR(MAX),
    Name NVARCHAR(255)
);

CREATE TABLE Location
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY,
    Name         NVARCHAR(255),
    LocationType INT, -- 1 - "city", 2 - "parts in city - streets, main stations, squares, ..."
    Latitude     FLOAT,
    Longitude    FLOAT
);

CREATE TABLE Stop
(
    Id        UNIQUEIDENTIFIER PRIMARY KEY,
    Name      NVARCHAR(255),
    Latitude  FLOAT,
    Longitude FLOAT,
    StopType  INT -- 1 - "busStop", 2 - "tramStop", 3 - "trainStop"
);

CREATE TABLE Location_Stop
(
    LocationId UNIQUEIDENTIFIER NOT NULL,
    StopId     UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (LocationId, StopId),
    CONSTRAINT FK_Location_Stop_Location FOREIGN KEY (LocationId)
        REFERENCES Location (Id) ON DELETE CASCADE,
    CONSTRAINT FK_Location_Stop_Stop FOREIGN KEY (StopId)
        REFERENCES Stop (Id) ON DELETE CASCADE
);

CREATE TABLE Line
(                -- TODO -> how do we deal with two routes being active ? for example route reckovice-hlavas and hlavas-reckovice -> or are those two separate lines?
    Id       UNIQUEIDENTIFIER PRIMARY KEY,
    -- ActiveRouteId UNIQUEIDENTIFIER, -- shows which main route is active rn -> MBY switch to route?? to show if its active?
    Name     NVARCHAR(255),
    Label    NVARCHAR(255),
    LineType INT -- 1 - "busLine", 2 - "tramLine", 3 - "trainLine"
    -- FOREIGN KEY (ActiveRouteId) REFERENCES Route(Id)
);

CREATE TABLE Route
(
    Id             UNIQUEIDENTIFIER PRIMARY KEY,
    Name           NVARCHAR(255),
    DurationTime   TIME,
    LineId         UNIQUEIDENTIFIER,
    StartStopId    UNIQUEIDENTIFIER,
    EndStopId      UNIQUEIDENTIFIER,
    IsActive       BIT,
    IsWeekendRoute BIT,
    IsHolydayRoute BIT,
    HasTickets     BIT,
    CONSTRAINT FK_Route_Line FOREIGN KEY (LineId)
        REFERENCES Line (Id),
    CONSTRAINT FK_Route_Stop_Start FOREIGN KEY (StartStopId)
        REFERENCES Stop (Id),
    CONSTRAINT FK_Route_Stop_End FOREIGN KEY (EndStopId)
        REFERENCES Stop (Id)
);

CREATE TABLE Route_Stop
(
    RouteId                   UNIQUEIDENTIFIER NOT NULL,
    StopId                    UNIQUEIDENTIFIER NOT NULL,
    TimeDurationFromFirstStop TIME,
    StopOrder                 INT,
    PRIMARY KEY (RouteId, StopId),
    CONSTRAINT FK_Route_Stop_Route FOREIGN KEY (RouteId)
        REFERENCES Route (Id) ON DELETE CASCADE,
    CONSTRAINT FK_Route_Stop_Stop FOREIGN KEY (StopId)
        REFERENCES Stop (Id) ON DELETE CASCADE
);

CREATE TABLE RouteSchedulingTemplate
(
    Id       UNIQUEIDENTIFIER PRIMARY KEY,
    RouteId  UNIQUEIDENTIFIER NOT NULL,
    Template NVARCHAR(MAX),
    CONSTRAINT FK_RouteSchedulingTemplate_Route FOREIGN KEY (RouteId)
        REFERENCES Route (Id)
);

CREATE TABLE Vehicle
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY,
    Label        NVARCHAR(255),
    Spz          NVARCHAR(255),
    Manufacturer NVARCHAR(255),
    Capacity     INT,
    VehicleType  INT, -- 1 - "bus", 2 - "tram", 3 - "train"
    IconId       UNIQUEIDENTIFIER NULL,
    LineId       UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Vehicle_Icon FOREIGN KEY (IconId) REFERENCES Icon (Id),
    CONSTRAINT FK_Vehicle_Line FOREIGN KEY (LineId) REFERENCES Line (Id)
);

CREATE TABLE ScheduledRoute
(
    Id        UNIQUEIDENTIFIER PRIMARY KEY,
    StartTime TIME,
    EndTime   TIME,
    VehicleId UNIQUEIDENTIFIER,
    RouteId   UNIQUEIDENTIFIER,
    CONSTRAINT FK_ScheduledRoute_Vehicle FOREIGN KEY (VehicleId) REFERENCES Vehicle (Id),
    CONSTRAINT FK_ScheduledRoute_Route FOREIGN KEY (RouteId) REFERENCES Route (Id)
);

CREATE TABLE Service
(
    Id          UNIQUEIDENTIFIER PRIMARY KEY,
    Name        NVARCHAR(255),
    IconId      UNIQUEIDENTIFIER NULL,
    Description NVARCHAR(255),
    CONSTRAINT FK_Service_Icon FOREIGN KEY (IconId) REFERENCES Icon (Id)
);

CREATE TABLE Vehicle_Service
(
    VehicleId UNIQUEIDENTIFIER NOT NULL,
    ServiceId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (VehicleId, ServiceId),
    CONSTRAINT FK_Vehicle_Service_Vehicle FOREIGN KEY (VehicleId)
        REFERENCES Vehicle (Id) ON DELETE CASCADE,
    CONSTRAINT FK_Vehicle_Service_Service FOREIGN KEY (ServiceId)
        REFERENCES Service (Id) ON DELETE CASCADE
);

CREATE TABLE Seat
(
    Id          UNIQUEIDENTIFIER PRIMARY KEY,
    SeatNumber  INT,
    VagonNumber INT NULL,
    VehicleId   UNIQUEIDENTIFIER,
    FOREIGN KEY (VehicleId) REFERENCES Vehicle (Id)
);

CREATE TABLE [User]
(
    Id
    UNIQUEIDENTIFIER
    PRIMARY
    KEY,
    Email
    NVARCHAR
(
    255
),
    Password NVARCHAR
(
    255
),
    Created DATETIME,
    Updated DATETIME,
    Deleted BIT,
    IsAdmin BIT
    );

CREATE TABLE ScheduledRoute_Seat
(
    ScheduledRouteId UNIQUEIDENTIFIER NOT NULL,
    SeatId           UNIQUEIDENTIFIER NOT NULL,
    IsBought       BIT,
    ReservedUntil   DATETIME,
    ReservedById     UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (ScheduledRouteId, SeatId),
    CONSTRAINT FK_ScheduledRoute_Seat_ScheduledRoute FOREIGN KEY (ScheduledRouteId)
        REFERENCES ScheduledRoute (Id) ON DELETE CASCADE,
    CONSTRAINT FK_ScheduledRoute_Seat_Seat FOREIGN KEY (SeatId)
        REFERENCES Seat (Id) ON DELETE CASCADE,
    CONSTRAINT FK_ScheduledRoute_Seat_User FOREIGN KEY (ReservedById)
        REFERENCES [User] ON DELETE CASCADE
);

CREATE TABLE RouteTicket
(
    Id        UNIQUEIDENTIFIER PRIMARY KEY,
    Price     FLOAT,
    ValidFrom DATETIME,
    ValidTo   DATETIME,
    UserId    UNIQUEIDENTIFIER,
    RouteId   UNIQUEIDENTIFIER,
    SeatId    UNIQUEIDENTIFIER,
    FOREIGN KEY (UserId) REFERENCES [User],
    FOREIGN KEY (RouteId) REFERENCES ScheduledRoute (Id),
    FOREIGN KEY (SeatId) REFERENCES Seat (Id)
);

CREATE TABLE User_Location_Favourite
(
    UserId     UNIQUEIDENTIFIER,
    LocationId UNIQUEIDENTIFIER,
    PRIMARY KEY (UserId, LocationId),
    CONSTRAINT FK_User_Location_Favourite_User FOREIGN KEY (UserId)
        REFERENCES [User] ON DELETE CASCADE,
    CONSTRAINT FK_User_Location_Favourite_Location FOREIGN KEY (LocationId)
        REFERENCES Location (Id) ON DELETE CASCADE
);

CREATE TABLE User_Line_Favourite
(
    UserId UNIQUEIDENTIFIER,
    LineId UNIQUEIDENTIFIER,
    PRIMARY KEY (UserId, LineId),
    CONSTRAINT FK_User_Line_Favourite_User FOREIGN KEY (UserId)
        REFERENCES [User] ON DELETE CASCADE,
    CONSTRAINT FK_User_Line_Favourite_Line FOREIGN KEY (LineId)
        REFERENCES Line (Id) ON DELETE CASCADE
);