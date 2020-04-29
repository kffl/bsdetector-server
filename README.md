![Production deploy](https://github.com/newt0npk/TSD-Project-Backend/workflows/Production%20deploy/badge.svg) ![Master PR](https://github.com/newt0npk/TSD-Project-Backend/workflows/Master%20PR/badge.svg)

# Bad smells in code detector

We have assembled a team (Tomasz Pućka, Marcin Rochowiak, Bartosz Paulewicz and Paweł Kuffel) and chosen a topic _**#8: Bad smells in code detector**_.

We will use ASP.NET with PostgreSQL for backend and Vue.js for frontend. For simplicity sake, we decided to split the project into two repositories. The frontend (client) repository [is here](https://github.com/newt0npk/TSD-Project-Frontend), wheras this is the backend repo.

Our MVP would be a web app capable of detecting bad smells in uploaded JavaScript code by performing static code analysis.

A nice-to-have feature that we will possibly try to implement would be integration with GitHub via Oauth or Github Apps API that would allow users to login via GitHub and analyze specified branch of a given GitHub repo.

## CI/CD

The project has a CI/CD pipeline set up. When a new PR is opened to master, the branch from wich it was opened is built and tested. Upon every commit to production branch, it is additionaly deployed to Azure App Service.
