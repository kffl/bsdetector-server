![Production deploy](https://github.com/kffl/bsdetector-server/workflows/Production%20deploy/badge.svg) ![Master PR](https://github.com/kffl/bsdetector-server/workflows/Master%20PR/badge.svg)

# BSDetector server

*Detector of bad smells in JavaScript code*

This is a uni project for Technologies of Software Development course. It's a web app that allows developers to check their JavaScript code for bad smells by performing static code analysis of uploaded code. The project is split into two repositories. This one contains backend service written in ASP.NET Core, whereas the frontend repo is [here](https://github.com/kffl/bsdetector-client/).

Our MVP would be a web app capable of detecting bad smells in uploaded JavaScript code by performing static code analysis.

A nice-to-have feature that we will possibly try to implement would be integration with GitHub via Oauth or Github Apps API that would allow users to login via GitHub and analyze specified branch of a given GitHub repo.

## Static code analysis

This project uses two different approaches for detecting code smells: simplified (line-based) analysis and AST analysis.

### Simplified analysis 

Simplified analysis is performed by analyzing the input code line-by line. It may utilize RegEx in the process, and doesn't check the code for it's syntactic validity.

### Abstract syntax tree analysis

The second step of code analysis (another set of code smells) relies on parsing the input code, building an abstract syntax tree and DFS-ing through that tree in search for known patterns.

## Tech stack

This project is written in ASP.NET Core 3. It uses esprima-dotnet for parsing JS code and building an abstract syntax tree.

## CI/CD

The project has a CI/CD pipeline set up. When a new PR is opened to master, the branch from which it was opened is built and tested. Upon every commit to production branch, it is additionally deployed to Azure App Service.
