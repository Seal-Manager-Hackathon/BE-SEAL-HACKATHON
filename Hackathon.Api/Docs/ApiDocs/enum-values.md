# Enum values

## Tác dụng
File này liệt kê các enum đang dùng trong hệ thống và giá trị số tương ứng khi client gửi/nhận enum dạng number.

## Lưu ý
- Trong C#, nếu enum không gán số cụ thể thì phần tử đầu tiên mặc định là `0`, các phần tử sau tăng dần `+1`.
- Một số enum trong `AppDbContext` đang được cấu hình `HasConversion<string>()`, khi lưu database sẽ là string thay vì number.
- FE/API client nên ưu tiên gửi string nếu endpoint/model đang nhận enum trực tiếp và Swagger hiển thị string; dùng bảng này khi cần map number.

## RoleEnum
| Number | Name |
|---:|---|
| 0 | Admin |
| 1 | Staff |
| 2 | Student |
| 3 | Lecturer |

## EventRoleEnum
| Number | Name |
|---:|---|
| 0 | Mentor |
| 1 | Judge |

## EmailVerificationStatusEnum
| Number | Name |
|---:|---|
| 0 | Pending |
| 1 | Verified |
| 2 | Expired |

## EventStatusEnum
| Number | Name |
|---:|---|
| 0 | Draft |
| 1 | Published |
| 2 | Closed |
| 3 | Cancelled |

## InvitationStatusEnum
| Number | Name |
|---:|---|
| 0 | Pending |
| 1 | Accepted |
| 2 | Rejected |
| 3 | Expired |

## NotificationStatusEnum
| Number | Name |
|---:|---|
| 0 | Pending |
| 1 | Unread |
| 2 | Read |

## RegisterTeamStatusEnum
| Number | Name |
|---:|---|
| 0 | Pending |
| 1 | Approved |
| 2 | Rejected |

## ReportStatusEnum
| Number | Name |
|---:|---|
| 0 | Open |
| 1 | Closed |

## SubmissionStatusEnum
| Number | Name |
|---:|---|
| 0 | Submitted |

## TeamDetailStatusEnum
| Number | Name |
|---:|---|
| 0 | Active |
| 1 | Inactive |

## UserStatusEnum
| Number | Name |
|---:|---|
| 0 | Active |
| 1 | Inactive |
| 2 | Banned |

## LeaderBoardsStatusEnum
| Number | Name |
|---:|---|
| 0 | IsDisabled |

## ScoresStatusEnum
| Number | Name |
|---:|---|
| 0 | IsRetake |
| 1 | IsMock |
| 2 | IsDisable |
