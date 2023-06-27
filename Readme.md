# GUS Data Integration

Welcome to My Awesome App! This application was created as part of a project for the course "Systems Integration."

## Screenshots

Screenshots of working application:

### Home Screen
![Home Screen](/images/main.png)

### Generating Graph
![Generating Graph](/images/graph.png)

### Exporting Data
![Exporting Data](/images/export.png)

### Exporting Data To Database
![Exporting Data To Database](/images/export_database.png)

### Data In Exported Json File
![Data In Exported Json File](/images/json.png)

## Features

- Register and log in to your account to access featuers
- Request data for selected region and data type
- Export and import data from .json or .xml files
- Export and import data from database
- Generate graph from currently loaded data

## Docker containers

Docker stack consists of 3 containers:

- Api running on port 8080
- MySQL database running on port 3306
- PhpMyAdmin running on port 8081

MySQL credentails for root access are "root" and "my_secret". Api is running on database named "integration_api"

## Installation

To install and run the app locally using docker-compose, follow these steps:

1. Clone the repository:
   git clone https://github.com/Nevirone/GUS_Data_Integration.git
2. Open terminal in application folder
3. Run docker-compose:
   docker-compose up
