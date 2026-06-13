# Weather forecast

## Tác dụng
API mẫu mặc định trả 5 dự báo thời tiết ngẫu nhiên.

## URL
`GET /WeatherForecast`

## Request body
Không có.

## Response body
```json
[
  {
    "date": "date",
    "temperatureC": 0,
    "temperatureF": 0,
    "summary": "string|null"
  }
]
```

## Business rules
- Đây là API mẫu mặc định, không yêu cầu đăng nhập.
- Mỗi lần gọi trả 5 dự báo thời tiết ngẫu nhiên.
- Không dùng API này cho nghiệp vụ hackathon.

## Lỗi có thể xảy ra
| HTTP | messageCode | message/detail |
|---:|---|---|
| 500 | INTERNAL_SERVER_ERROR | An unexpected error occurred. |
