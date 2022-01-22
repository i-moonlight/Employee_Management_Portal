# Using a base image with JDK 17 and Maven
FROM maven:3.8.1-openjdk-17-slim AS build

# Set a working directory
WORKDIR /app

# Copy files from your project to the working directory
COPY . /app

# Run Maven to build the project
RUN mvn clean package

# Create a new image based on OpenJDK 17
FROM openjdk:17-jdk-slim

# Expose the port that the application will use
EXPOSE 9001

# Copy the JAR file built from the previous stage
COPY --from=build /app/target/demo-0.0.1-SNAPSHOT.jar /app/demo-0.0.1-SNAPSHOT.jar

# Set the entry point to run the application
ENTRYPOINT ["java", "-jar", "/app/demo-0.0.1-SNAPSHOT.jar"]
