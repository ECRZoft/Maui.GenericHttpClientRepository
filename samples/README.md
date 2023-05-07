# The Todo Example

The example consist of a .NET MAUI Todo app and a ASP.NET Minimal Api.

## MauiTodoApp
A .NET Maui App in a MVVM architecture and with two pages: 
- TodoListPage, which lists the Todo items and have a button for adding new items
- TodoItemPage, which acts as an Add/DetailsPage

The Mobile client has the following functions:

- Gets automatically all Todo items when the app starts
- A push-to-refresh gets all Todo items again
- A new Todo item is added when the button with +-icon is clicked, the `Name` and `Notes` is filled and the Save button is clicked.
- When a Todo item is selected, the `Name`, `Notes` and `IsComplete` can be edited. Click *Save* or *Delete*.

&nbsp;

## ItemWebApi

ItemWebApi is an ASP.NET Minimal Api which is build on this Tutorial: [Create a minimal API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio)

Data stores with EntityFramework in an Memory database.

The API can easyly be tested with the *todo.http* file.

### API operations

| API                     | Description               | Request body | Response body        |
|-------------------------|---------------------------|--------------|----------------------|
| GET /todoitems          | Get all to-do items       | None         | Array of to-do items |
| GET /todoitems/complete | Get completed to-do items | None         | Array of to-do items |
| GET /todoitems/{id}     | Get an item by ID         | None         | To-do item           |
| POST /todoitems         | Add a new item            | To-do item   | To-do item           |
| PUT /todoitems/{id}     | Update an existing item   | To-do item   | None                 |
| DELETE /todoitems/{id}  | Delete an item            | None         | None                 |
