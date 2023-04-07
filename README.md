# AINouveau
## About
AINouveau is an online luxury AI generated art gallery.

## Features
* Data stored in SQLite db and accessed via an API. Backup data stored in json file
* Images stored locally for testing purposes
* Can filter gallery by type and price
* Can sort gallery by popularity, price, and name
* Generate your own luxury artworks via DALL-E on the Create page
* Create variations of selected artworks via DALL-E on the Detail page

## How to Use
* Contains 3 projects: Client (pages, images, etc.) Server (API), and Shared (Models)
* Must run both Client and Server for app to work
* API testing can be done via Swagger if only running Server project
* Need an OpenAI API key named 'OPENAI_API_KEY' stored as an environment variable
* API port number is hardcoded in Gallery.razor and ArtworkDetail.razor, change if needed

## Technologies Used
* .NET 7
* EF Core & SQLite 7.0.4
* Syncfusion.Blazor 21.1.35
* Betalgo.OpenAI.GPT3 6.8.1