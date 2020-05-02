# API Doc

## Endpoints

### Send code for analysis (single file)

**POST** `/api/analyze`

Content-Type: application/json

#### Request payload:

```javascript
{
    "code": "js source code;\nvar l = 10;"
}
```

#### Response (200 OK):

```javascript
{
    "linesAnalyzed": 4,
    "smellsDetected": [
        {
            "smellName": "Line too long",
            "smellDescription": "Lines that are too long make your code less readable.",
            "occurrences": [
                {
                    "snippet": "TODO",
                    "lineStart": 2,
                    "colStart": 0,
                    "lineEnd": 2,
                    "colEnd": 541
                },
                {
                    "snippet": "TODO",
                    "lineStart": 3,
                    "colStart": 0,
                    "lineEnd": 3,
                    "colEnd": 540
                }
            ]
        },
        {
            "smellName": "Too many parameters for a function declaration",
            "smellDescription": "Maximum recommended number of parameters for a regular function is: 5.",
            "occurrences": []
        },
        {
            "smellName": "Too many parameters for arrow function",
            "smellDescription": "Maximum recommended number of parameters for an arrow function is: 4.",
            "occurrences": [
                {
                    "snippet": "TODO",
                    "lineStart": 2,
                    "colStart": 10,
                    "lineEnd": 2,
                    "colEnd": 42
                }
            ]
        }
    ]
}
```

#### Errors:

Parse error (400 Bad request):
```javascript
{
    "error": "PARSE_ERROR",
    "message": "Unexpected identifier",
    "line": 2,
    "column": 11
}
```

### Analyze code (upload up to 10 files)

**POST** `/api/analyzemultipart`

Content-Type: multipart/form-data

#### Request payload:

```
----------------------------123BOUNDARY123456789
Content-Disposition: form-data; name="code"; filename="firstfile.js"
<content of firstfile.js>
----------------------------123BOUNDARY123456789
Content-Disposition: form-data; name="code"; filename="secondfile.js"
<content of secondfile.js>
----------------------------123BOUNDARY123456789
```

#### Response

```javascript
[
    {
        "fileName": "file1",
        "linesAnalyzed": 0,
        "error": {
            "error": "PARSE_ERROR",
            "message": "Unexpected identifier",
            "line": 2,
            "column": 11
        }
    },
    {
        "fileName": "file2",
        "linesAnalyzed": 2,
        "smellsDetected": [<see example above>]
    }
]
```

### Analyze public GitHub repo

**POST** `/api/analyzerepo`

Content-Type: application/json

#### Request payload:

```javascript
{
    "username": "github-username",
    "reponame": "github-repo-name"
}
```

#### Response:

See: Analyze code (upload up to 10 files)

#### Errors:

404:

```javascript
{
    "error": "REPO_FETCH_ERROR",
    "message": "GitHub repo not found"
}
```
