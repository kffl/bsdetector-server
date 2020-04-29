# API Doc

## Endpoints

### Send code for analysis

**POST** `/api/analyze`
**POST** `/api/analyzemultipart`

Content-Type: application/json
or
Content-Type: multipart/form-data

#### Request payload:

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

## Development/debugging endpoints

### Get mock response regardless of input

**POST** `/api/analyzemock`

Content-Type: application/json

#### Request payload:

```javascript
{
    "code": "whatever"
}
```

### Get Abstract Syntax Tree in JSON

**POST** `/api/ast`

Content-Type: application/json

#### Request payload:

```javascript
{
    "code": "console.log(`JS code goes in here`);"
}
```

#### Sample response:

```javascript
{
    "type": "Program",
    "body": [
        {
            "type": "ExpressionStatement",
            "expression": {
                "type": "CallExpression",
                "callee": {
                    "type": "MemberExpression",
                    "computed": false,
                    "object": {
                        "type": "Identifier",
                        "name": "console",
                        "loc": {
                            "start": {
                                "line": 1,
                                "column": 0
                            },
                            "end": {
                                "line": 1,
                                "column": 7
                            }
                        }
                    },
                    "property": {
                        "type": "Identifier",
                        "name": "log",
                        "loc": {
                            "start": {
                                "line": 1,
                                "column": 8
                            },
                            "end": {
                                "line": 1,
                                "column": 11
                            }
                        }
                    },
                    "loc": {
                        "start": {
                            "line": 1,
                            "column": 0
                        },
                        "end": {
                            "line": 1,
                            "column": 11
                        }
                    }
                },
                "arguments": [
                    {
                        "type": "TemplateLiteral",
                        "quasis": [
                            {
                                "type": "TemplateElement",
                                "value": {
                                    "raw": "JS code goes in here",
                                    "cooked": "JS code goes in here"
                                },
                                "tail": true,
                                "loc": {
                                    "start": {
                                        "line": 1,
                                        "column": 12
                                    },
                                    "end": {
                                        "line": 1,
                                        "column": 34
                                    }
                                }
                            }
                        ],
                        "expressions": [],
                        "loc": {
                            "start": {
                                "line": 1,
                                "column": 12
                            },
                            "end": {
                                "line": 1,
                                "column": 34
                            }
                        }
                    }
                ],
                "loc": {
                    "start": {
                        "line": 1,
                        "column": 0
                    },
                    "end": {
                        "line": 1,
                        "column": 35
                    }
                }
            },
            "loc": {
                "start": {
                    "line": 1,
                    "column": 0
                },
                "end": {
                    "line": 1,
                    "column": 36
                }
            }
        }
    ],
    "sourceType": "script",
    "loc": {
        "start": {
            "line": 1,
            "column": 0
        },
        "end": {
            "line": 1,
            "column": 36
        }
    }
}
```
