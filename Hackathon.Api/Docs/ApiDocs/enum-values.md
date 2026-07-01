# Danh sách Enum Values (Dành cho FE)

Dưới đây là danh sách toàn bộ các Enum được sử dụng trong hệ thống, bao gồm số giá trị (int) tương ứng với tên trạng thái (string) để FE tiện mapping:

## 1. RoleEnum (Quyền người dùng)
Đại diện cho phân quyền trong hệ thống.
- `0`: Admin
- `1`: Staff
- `2`: Student
- `3`: Lecturer

## 2. UserStatusEnum (Trạng thái người dùng)
Trạng thái hoạt động của một User.
- `0`: Active (Đang hoạt động)
- `1`: Inactive (Ngừng hoạt động)
- `2`: Banned (Bị khóa/cấm)

## 3. EventStatusEnum (Trạng thái sự kiện)
Trạng thái của một Event.
- `0`: Draft (Bản nháp, chưa công bố)
- `1`: Published (Đã công bố/Đang diễn ra)
- `2`: Closed (Đã đóng)
- `3`: Cancelled (Bị hủy)

## 4. RegisterTeamStatusEnum (Trạng thái đăng ký Event của Team)
Trạng thái xét duyệt khi một team nộp đơn tham gia Event.
- `0`: Pending (Đang chờ duyệt)
- `1`: Approved (Đã được chấp nhận)
- `2`: Rejected (Bị từ chối)

## 5. TeamDetailStatusEnum (Trạng thái thành viên trong Team)
Trạng thái của một User nằm trong một Team.
- `0`: Active (Đang là thành viên)
- `1`: Inactive (Không còn là thành viên / đã rời nhóm)

## 6. InvitationStatusEnum (Trạng thái Lời mời vào Team)
Trạng thái khi Leader mời một User vào Team.
- `0`: Pending (Đang chờ phản hồi)
- `1`: Accepted (Đã đồng ý tham gia)
- `2`: Rejected (Đã từ chối tham gia)
- `3`: Expired (Lời mời đã hết hạn)

## 7. EmailVerificationStatusEnum (Trạng thái xác thực Email)
Trạng thái của mã/link xác thực Email (OTP/Token).
- `0`: Pending (Đang chờ xác thực)
- `1`: Verified (Đã xác thực thành công)
- `2`: Expired (Mã/Link đã hết hạn)

## 8. NotificationStatusEnum (Trạng thái Thông báo)
Trạng thái của một thông báo gửi đến người dùng.
- `0`: Pending (Đang chờ gửi)
- `1`: Unread (Chưa đọc)
- `2`: Read (Đã đọc)

## 9. ReportStatusEnum (Trạng thái báo cáo hệ thống)
Trạng thái các Report/Feedback của người dùng gửi cho Admin.
- `0`: Open (Đang mở/Chưa xử lý)
- `1`: Closed (Đã đóng/Đã giải quyết)
- `2`: Approved (Đã duyệt/Chấp nhận chấm lại)

## 10. SubmissionStatusEnum (Trạng thái nộp bài)
Trạng thái nộp bài tập / Assignment.
- `0`: Submitted (Đã nộp bài)
- `1`: Unsubmitted (Chưa nộp bài)
- `2`: Failed (Nộp thất bại)

## 11. EventRoleEnum (Vai trò trong Event)
Vai trò cụ thể của một User (Staff/Lecturer) trong một Event.
- `0`: Mentor (Người hướng dẫn)
- `1`: Judge (Giám khảo)
- `2`: Staff (Nhân viên vận hành)

## 12. LeaderBoardsStatusEnum / ScoresStatusEnum
Các cờ trạng thái dùng trong Bảng xếp hạng và Chấm điểm.
- `LeaderBoardsStatusEnum.IsDisabled` = `0`
- `ScoresStatusEnum.IsRetake` = `0`
- `ScoresStatusEnum.IsMock` = `1`
- `ScoresStatusEnum.IsDisable` = `2`

*(Lưu ý: FE khi gửi request các tham số lọc trạng thái thường có thể dùng string như "Pending", "Approved" (tùy API có parse string hay không), nhưng khi nhận kết quả từ backend nếu kiểu dữ liệu trả về là chuỗi thì sẽ hiện `"Approved"`, nếu kiểu trả về là int thì sẽ hiện `1`. Mapping theo bảng trên là chính xác tuyệt đối).*

## 13. NotificationTargetTypeEnum (Phân loại thông báo)
Phân loại phạm vi/đối tượng nhận của một thông báo.
- `0`: Personal (Gửi riêng cho một người dùng)
- `1`: Team (Gửi cho một team)
- `2`: System (Gửi toàn hệ thống, tất cả user đều nhận được)
