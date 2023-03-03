# MovieCharactersAPI
ASPNET API application "MOVIECHARACTERSAPI" user can create, read, update and delete, movies, characters and fransises.
More information on swagger documentation
# Technologies 
aspnet, entityframework, c#, sql, automapper, swashbuckler...

# FOLDER STRUCTURE
```bash
▒▒▒▒Controllers           contains movie, character and fransise controllers 
▒▒▒▒Data
▒   ▒▒▒▒DataContext         Applications datacontext file for entity Framework
▒   ▒▒▒▒DTOs                Data transfer objects for receiving and returning data
▒   ▒   ▒▒▒▒CharacterDTOs
▒   ▒   ▒▒▒▒FranchiseDTOs
▒   ▒   ▒▒▒▒MoviesDTOs
▒   ▒       ▒▒▒▒CreateMovieDTOs
▒   ▒       ▒▒▒▒GetMovieDto
▒   ▒       ▒▒▒▒UpdateMovie
▒   ▒▒▒▒MigrationStart       Initial seeds for Migrations
▒▒▒▒Exceptions              Exception warnings
▒▒▒▒Extensions              Adding scopes and creating connection to db also creating setup for documentation
▒▒▒▒Helpers                AUTOMAPPER 
▒▒▒▒Migrations
▒▒▒▒Models                 DATA MODELS FOR DB
▒▒▒▒Profiles
▒▒▒▒Properties
▒▒▒▒Repositories              MOVIE / CHARACTER / FRANSISES LOGIC AND INTERFACES
▒   ▒▒▒▒ConcreteRepository
▒   ▒▒▒▒InterfaceRepository
▒▒▒▒SwaggerExamples           DOCUMENTATION EXAMPLES FOR SWAGGER
    ▒▒▒▒Requests
    ▒   ▒▒▒▒CharacterRequest
    ▒   ▒▒▒▒MovieRequests
    ▒▒▒▒Responses
        ▒▒▒▒CharacterResponses
        ```
        
