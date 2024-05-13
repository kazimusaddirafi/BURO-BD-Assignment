
# Assignment

In this repo, I have added a total of 5 questions answer.





## Ques-3.API Reference

![image](https://github.com/kazimusaddirafi/BURO-BD-Assignment/assets/169454107/59d4628e-653d-49b1-9ef5-e62125e06415)

#### Get all employees

```http
  GET /api/Employee
```


#### Get employee details

```http
  GET /api/Employee/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |

#### Create new employee
```http
  POST /api/Employee
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `EmployeeName`      | `string` | **Required** |
| `PIN`      | `string` | **Required & Unique** |
| `DepartmentId`      | `int` | **Required** |
| `DateOfBirth`      | `Date` | **Required** |

#### Update employee
```http
  PUT /api/Employee/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |

#### Delete employee
```http
  DELETE /api/Employee/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |






