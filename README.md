# HotelBooking

This project implements a simple Hotel Booking Web API, leveraging tools such as .NET and Entity Framework Core.

##  Requirements 

The solution fulfills the following requirements:

- Find a hotel by name.
- Check room availability between two dates for a given number of people.
- Book a room.
- Retrieve booking details using a booking reference.
- Seed/reset data for testing purposes.

### Business Rules

- Each hotel has exactly 6 rooms, where each room can be one of the types: **Single**, **Double**, or **Deluxe**.
- A room cannot be double booked for any given night.
- A booking must not require the guest to **change rooms** during their stay.
- A booking must have a unique booking reference.
- A room cannot be booked for more people than its maximum capacity.

### Technical Requirements

The solution fulfills the following technical requirements:

- Swagger documentation made available for testing.
- Exposes a Seed and Reset endpoint to test the API.

## API Design

The API was designed following RESTful best practices. The API exposes the following endpoints to the consumer which are available via the Swagger UI:

- GET **/api/hotels/search** - Retrieve a hotel by its name.
- GET **/api/availability** - Retrieves availability for a given number of people between two dates.
- GET **/api/bookings/{reference}** - Retrieves a booking by the given booking reference.
- POST **/api/bookings** - Creates a booking.
- POST **/api/data/seed** - Seeds the database with test data.
- POST **/api/data/reset** - Resets the database.

No authorisation is required in order to execute these endpoints.

---

##  Assumptions

The following assumptions have been made when implementing the solution:


- The availability search finds available rooms across all hotels. I have added the ability to narrow this down by passing in an optional **hotelId** param.
- A booking is created against the specified room, otherwise the user is unable to create a booking if the room isn't available. This could be extended to fetch the next best availability but out of scope for this project.
- The timestamp of dates is ignored, and we are only concerned about the date.

---

## Project Structure

### HotelBookingApi

- **Controllers** - contains controller classes grouped by endpoint functionality/domain.
- **Services** - Contains business logic to support the API.
- **Repositories** - Data access layer using Entity Framework Core.
- **Models** - Contains the domain models used by Entity Framework.
- **Migrations** - Contains a single migration script to create the initial tables.
- **Context** - The database context.
- **Helpers** - Contains a single helper class to fetch test data to be used for seeding.
- **Mappers** - Contains mapping profiles to convert domain level objects to dtos.

### HotelBookingApi.Tests

Contains Unit tests to test the Controller and Service classes.

... This could also include Integration tests and further Unit tests to test each area of the application. (Some tests have been omitted due to time constraints.)

--- 

## Running Locally

### Prerequisites

- .NET 9 installed on machine - https://dotnet.microsoft.com/en-us/download/dotnet/9.0
- Instance of Sql Server running on machine.
- Create a new database called HotelDb (or something similar).
- Populate your sql connection string in **appsettings.development** > **DefaultConnection**.

Navigate to the **HotelBookingApi** project and run the command: 

```
dotnet run
```

The Swagger UI should be displayed on your localhost by default, if not then try navigating to /swagger.






