# Musicians

## User story
The development presented is that of a fictional universal world-wide database that aims to catalog all the musicians that have ever existed, be they bands or soloists, including the genre they favored the most, the preferred instrument, and since when they have been active.

The application allows you to add, modify, delete and consult the database to interact, add, correct or delete musicians.
Some protected routes have been established that only authorized users can access.

### Execution
At the moment the execution is done directly from the development environment. The application is executed and it presents us with a page with all the endpoints that the API has.
The development follows the clean architecture approch promoted by "Uncle" Bob that tries to maintain and enforce loosly coupled applications for easy manteinability, testing and scalability.
In particular, this development presents the layers of
* Domain: All entities belonging to the bussines process are defined here, including data validation.
* Application: The definition of the application at the abstract level is implemented here. Questions like what the application should do, in which way, with what data are answered here and abstract code is created.
* Infrastructure: The concrete implementation of the previous layer is implemented here. Now the application is connected to real things (database, external frameworks, etc) needed to fullfil the application logic. One thing to notice is that this application makes use of Dapper micro-ORM instead of the usual Entity Framewor for database access.
* Presentation: This layer takes charge of the API the is exposed to the user in order to manage the application, including the protection of routes from public access

The application was developed following the test-driven development guidelines and there is a section of the project for it.

### Running the program
* Clone the project to your local repository
* The project whas developed using Visual Studio so it would be better if can be hosted here.
* Make sure all dependencies are fullfiled
* Run the application clicking in the green arrow in the top center of the IDE
* In case this is the first run, the database and the corresponding tables are created
* Wait for the browser to appear. From here you can try several routes including the token creation for accessing protected routes.
* For protected routes is much better to use tools like Postman.
