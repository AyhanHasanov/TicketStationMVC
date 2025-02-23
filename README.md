
# TicketStationMVC

This is an ASP.NET Core MVC web application developed using .NET 8.0 that allows users to browse, purchase, and manage event tickets. The system provides different user roles, including admins, registered users, and unregistered users, each with specific permissions.

## Features

Admin Capabilities

- Create, edit, and delete events.
- Manage users (create, edit, delete).
- View all bought tickets.
- View tickets purchased by specific users.

Registered / Logged-in Users
- Browse and view all events.
- See event details.
- View their purchased tickets.
- Add tickets to their account.
- Delete tickets from their account.

Unregistered / Unlogged Users
- Browse and view all events.
- See event details.

## Tech Stack

**Backend:** ASP.NET Core MVC (.NET 8.0)

**Frontend:** Razor Views, CSS

**Server:** SQL-based relational database
## Database structure
The system consists of 7 tables with one-to-many and many-to-many relationships, including:

- Users & Roles (One-to-Many)
- Carts & CartItems (One-to-Many)
- Events & Categories (Many-to-Many)
