# API Doc

## Endpoints

### Send code for analysis

**POST** `/api/analyze`
**POST** `/api/analyzemultipart`

Content-Type: application/json
or
Content-Type: multipart/form-data

Request payload:

```javascript
{
    "code": "js source code;\nvar l = 10;"
}
```

or:

```
-----------------------------158456752212orAnyOtherBoundary
Content-Disposition: form-data; name="code"

content of uploaded file goes here
-----------------------------158456752212orAnyOtherBoundary--
```

Sample response:

```javascript
{
    "linesAnalyzed": 2,
    "smellsDetected": [
        {
            "name": "Sample_name",
            "description": "Sample description of this smell",
            "occurances": [
                {
                    "snippet": "cont l = 5;",
                    "lineStart": 5,
                    "colStart": 0,
                    "lineEnd": 5,
                    "colEnd": 12
                },
                {
                    "snippet": "var l = 10;\nvar m = 10;",
                    "lineStart": 9,
                    "colStart": 0,
                    "lineEnd": 10,
                    "colEnd": 11
                }
            ]
        },
        {
            "name": "Too many arguments for a function declaration",
            "description": "A function declaration takes too many arguments. At most, 5 arguments per function are recommended.",
            "occurances": [
                {
                    "snippet": "function x(a, b, c, d, e, f) {",
                    "lineStart": 5,
                    "colStart": 10,
                    "lineEnd": 5,
                    "colEnd": 39
                },
                {
                    "snippet": "function qwerty(x, y, z, a, b, c) {",
                    "lineStart": 9,
                    "colStart": 0,
                    "lineEnd": 9,
                    "colEnd": 43
                }
            ]
        }
    ]
}
```
