# PersonService

This Project was created for a coding challenge.  The requirements are below:
Server Side Challenge: Build a Person
Service
Below is a problem that we’ll discuss in person. It is provided to you beforehand
so you can think about how you would solve the problem. We’ll spend about
half an hour on this. Be prepared to use a whiteboard to explain your solution.
Feel free to reach out before your interview if you want anything clarified.
Problem
Your client has an Access database used for contacts. They want to make a
simple web application that allows their sales people to find a contact. It is an
Angular based single page application (decision already made). Your
assignment is to design a simple backend service / API to create, retrieve, and
change person data in the database. Access will be replaced by the database
you define.
Requirements
1. Create a person
a. A person must have a name, birthdate, and state (as in PA, FL, CA, etc)
b. All other fields are optional
2. Get a specific person
3. Get a list of persons
a. Get all persons
b. Get all persons in a US State
4. Delete a person
a. Soft delete only
5. Change a person
a. Cannot change the birthdate, all other fields can change
All communications between the Angular application and the API must be
secure.
The API needs to verify that the caller of the API is allowed to call it. It must also
log that the user that made the request.
Person JSON
{
"person": {
 "firstName": "",
 "lastName": "",
 "dateOfBirth": "",
 "gender": "",
 "preferredName": "",
 "address": [
 {
 "state": ""
 }
 ]
}
}
Some things we will discuss in person
● You need to define a technology platform on which you’d build the API (.NET,
Java, NodeJS, etc) - and why you think it is the appropriate platform to use
● You could host this solution anywhere you want - be prepared to discuss
where and why
● You can use whatever db tech you like - nosql, key/value, graph, rdbms. Be
prepared to talk about a solution that you believe makes sense for this
solution. Also consider how you would get data from the old database to the
new one.
● How will you secure communications?
● What approach for the API will you use? REST? Web Services? GraphQL?
● What will the URLs for your API look like?
● We’ll get into other questions as well, but want to give you an idea of the
types of things we’ll be covering.


The bash scripts in the solution are as follows:
1) createsql - used to create initial db and sql login starts up sql server in docker container
2) sqlmigrate - runs db migration scripts (which are in the Migrations folder) using FluentMigrator
3) teardownsql - tears down the sql container

