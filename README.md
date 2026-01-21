# PersonalBlogAPI
RESTful blog API  using ASP.NET, EF Core, and SQLite. Provides full CRUD operations.
## Features
- Create, read, update, and delete blog posts
- persistent storage using relational database
- API documentation with Swagger
- RESTful endpoints

## API Endpoints
| Method | Endpoint | Description |
|---|---|---|
| GET | /api/Posts | Gets all posts |
| POST | /api/Posts | Create post |
| GET | /api/Posts/{id} | Get post by ID |
| PUT | /api/Posts/{id} | Update existing specified post |
| DELETE | /api/Posts/{id} | Delete specified post |

## Get started
1. clone repo
2. build solution to restore NuGet packages
3. create local database
```bash
Add-Migration DBName
update-Database
```
4. run project
