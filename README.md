# MHD Route Finder 🚍

## Introduction

The **MHD Route Finder** is a system designed and implemented as part of the **Advanced Database Systems (PDB)** course at **Brno University of Technology, Faculty of Information Technology (BUT FIT)**.

This team project focuses on leveraging **relational** and **NoSQL databases** to efficiently manage and query transportation data. The solution uses **Command-Query Responsibility Segregation (CQRS)** design principle and provides a RESTful API for easy data access.

The application enables users to plan routes and access real-time information about public transportation, such as:
- **Current vehicle positions**
- **Estimated delays based on incidents**
- **Vehicle occupancy levels and temperature**

---

## Features

- **Route Planning:** Quickly find optimal routes based on starting and ending locations.
- **Real-Time Updates:** Monitor vehicle locations, delays, and occupancy every 5–10 seconds.
- **User Profiles:** Save favorite routes and locations for a personalized experience.
- **Scalability:** Efficiently handles high volumes of real-time data and user queries.
- **CQRS Architecture:** Commands update relational data, while queries utilize NoSQL for faster responses.
- **Modern Technologies:** Built using **.NET Core**, **Microsoft SQL Server**, and **MongoDB**, ensuring compatibility with industry standards.

---

## Architecture

The system integrates the following components:

### 1. **SQL Database**
- **Technology:** Microsoft SQL Server
- **Purpose:** Manage transactional and structured data, such as routes, stops, and schedules.
- **Schema Highlights:**
    - **Stops**: Geographic data for each stop.
    - **Routes**: Static route definitions including stops and connections.
    - **Schedules**: Planned trips with start and end times.

### 2. **NoSQL Database**
- **Technology:** MongoDB
- **Purpose:** Store dynamic, real-time, and historical data.
- **Examples of Stored Data:**
    - **Vehicle Status:** Current position, speed, occupancy, and delays.
    - **User Search History:** Logs of recent queries for personalized recommendations.
    - **Cached Schedules:** Preprocessed trip data for quick lookups.

### 3. **CQRS Principle**
- **Commands:** SQL Server is used for data creation, updates, and deletion. All updates are synchronized to NoSQL database. 
- **Queries:** MongoDB is used for efficient, read-intensive operations like real-time vehicle status, search history retrieval and finding scheduled routes.

This architecture combines scalability with robust transactional reliability, ensuring efficient data handling even under heavy loads.

---

## REST API

The system exposes single RESTful API for interaction with both relational and non-relational databases.

### Key Endpoints

#### **Command Operations** (Relational Data - SQL):
- **Routes:** Create, update, and delete routes (`/api/Route`).
- **Stops:** Add stops to routes (`/api/Stop/add-to-location`).
- **Users:** Manage profiles, favorites, and reservations (`/api/User`).

#### **Query Operations** (Real-Time Data - MongoDB):
- **Real-Time Vehicle Information:** Fetch real-time updates (`/api/Vehicle/GetRTIByVehicleId/{id}`).
- **User Search History:** Retrieve recent user queries (`/api/User/getSearchHistory`).
- **Nearby Locations:** Get the closest stop to a user’s location (`/api/Location/GetClosest`).

For a detailed API specification, refer to the generated **Doxygen** documentation in the repository.

---

## Installation

### Prerequisites
1. **Microsoft SQL Server** with the configuration detailed in `/src/doc/SqlServerSetup.pdf`.
2. **MongoDB** connection named `pdb-project`.
3. **Visual Studio 2022** for building and deploying the application.

### Steps to Run Locally
1. **Update Entity Framework Tools**:
   ```bash
   dotnet tool update -g dotnet-ef
   ```  

2. **Apply Database Migrations**:
   ```bash
   dotnet ef database update
   ```  

3. **Seed Test Data**:
   ```bash
   dotnet run seed
   ```  

4. **Run the Application**:
   ```bash
   dotnet run
   ```  

---

## Testing

### **Unit Testing**
- Framework: **xUnit**
- Tests focus on individual components like controllers and services.
- Data generated using the `Bogus` library for realistic test scenarios.

### **Integration Testing**
- Verifies interaction between SQL and MongoDB databases.
- Ensures data consistency across systems via REST API endpoints.

### **Simulated Environments**
- **SQL**: Mocked using SQLite.
- **NoSQL**: Dedicated MongoDB test database (`transitconnex_test`).

---

## Project Structure

- **`TransitConnex.API`**: Contains controllers, middleware, and configuration for API endpoints.
- **`TransitConnex.Command`**: Handles relational data operations.
- **`TransitConnex.Query`**: Handles NoSQL data operations.
- **`TransitConnex.Test`**: Implements unit and integration tests.
- **`TransitConnex.Domain`**: Shared models, DTOs, and domain logic.

---

## Authors

- **Dominik Pop** - `xpopdo00`
- **David Drtil** - `xdrtil03`

---

## References

- [GTFS Timetable Data](https://data.brno.cz/datasets/j%C3%ADzdn%C3%AD-%C5%99%C3%A1d-ids-jmk-ve-form%C3%A1tu-gtfs-gtfs-timetable-data/about)
- [Public Transit Positional Data](https://data.brno.cz/datasets/mestobrno::polohy-vozidel-hromadn%C3%A9-dopravy-public-transit-positional-data/about)
