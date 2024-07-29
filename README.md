I also created an Azure Function HTTP Trigger serverless for the creation and management of products. I decided to use the same database as the REST API, even though microservices typically use unique databases, to simplify the process. I created the application context by looking at the already created database.

Used .NET 8 for the REST API and .NET 6 for the Azure Function.

Replace the placeholder with the actual connection string that I will send by email for local testing in the Program.cs file. Sry for not pass to app.settings :(
