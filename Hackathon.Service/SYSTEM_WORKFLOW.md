# SEAL Hackathon Management System - System Workflow

Tài liệu này dùng để mô tả tổng quan hệ thống, các luồng hoạt động chính và những trường hợp cần lưu ý khi triển khai backend.

> Hồng Trung lão đại có thể điền câu trả lời trực tiếp sau các dòng `//`.

---

## 1. Tổng quan hệ thống

### 1.1. Mục tiêu chính của hệ thống

Hệ thống dùng để quản lý toàn bộ quá trình tổ chức hackathon: tạo event, quản lý team, đăng ký tham gia, bốc thăm track/topic, nộp bài, chấm điểm, phúc khảo, thăng vòng và tạo leaderboard.

// Trả lời/bổ sung:

### 1.2. Nhóm người dùng chính

Các nhóm user dự kiến:

```text
Admin
Staff
Student
Lecture
Mentor
Judge
```

Trong đó:

- `Admin`, `Staff`, `Student`, `Lecture` là role toàn cục.
- `Mentor`, `Judge` là vai trò theo event/track.

// Trả lời/bổ sung:

### 1.3. Phạm vi tài liệu

Tài liệu này ưu tiên mô tả backend/API workflow, không đi sâu vào UI frontend.

// Trả lời/bổ sung:

---

## 2. Luồng tài khoản và profile

### 2.1. Đăng ký tài khoản

User đăng ký tài khoản xong có thể sử dụng ngay, không cần admin hoặc staff duyệt tài khoản.

// Xác nhận/sửa: đúng rồi, nhưng tài khoản khi tạo là student và chỉ coi được các sự kiện, muốn tham gia thi phải tạo team, muốn tạo team thì phải điền đủ profile

### 2.2. Thời điểm bắt buộc hoàn thiện profile

Student có thể tạo team hoặc join team trước khi hoàn thiện đầy đủ profile. Khi team leader đăng ký team vào event, hệ thống mới kiểm tra toàn bộ profile của các thành viên trong team.

// Xác nhận/sửa: đúng một phần, trước khi tạo team hoặc vào team thì phải điền đủ profile

### 2.3. Field profile bắt buộc

Các field profile cần kiểm tra khi đăng ký event:

```text
FullName
Email
PhoneNumber
StudentId
College
HashPassword

```

## // Trả lời/bổ sung field bắt buộc:tôi đã chỉnh sửa rồi

## 3. Luồng team

### 3.1. Tạo team

Student có thể tạo team và trở thành team leader.

// Xác nhận/sửa: khi tạo thì tự động làm leader, có thể trao quyền leader cho các thành viên khác trong team

### 3.2. Member trong team

Team leader có thể mời hoặc xóa member khi team chưa bị khóa.

// Xác nhận/sửa: đúng rồi

### 3.3. Giới hạn member trước khi đăng ký event

Team trước khi đăng ký event có thể có nhiều member nhưng tối đa không quá 50 người.

// Xác nhận/sửa: đúng, khi đăng kí vào event sẽ xét duyệt thành viên đáp ứng tiêu chuẩn số lượng thành viên 1 team cảu event không

### 3.4. Giới hạn member khi đăng ký event

Khi đăng ký vào event, hệ thống kiểm tra số lượng member theo min/max do admin cấu hình trong event.

// Xác nhận/sửa: đúng

### 3.5. Một student thuộc nhiều team

Quy tắc dự kiến: một student có thể thuộc nhiều team khác nhau, nhưng không được tham gia nhiều team trong cùng một event.

// Xác nhận/sửa: đúng

### 3.6. Khóa member khi đăng ký event

Quy tắc dự kiến:

```text
Pending: khóa tạm thời, không được đổi member
Approved: khóa cứng
Rejected: được mở lại để sửa và gửi lại
```

// Xác nhận/sửa: đúng, tôi nghĩ nên thêm 1 trạng thái mới tạo chưa làm gì cả, có thể đổi member

### 3.7. Khóa member theo năm/chapter

Nếu team được approved ở event đầu tiên trong năm, team đó không được đổi member cho các event còn lại trong cùng năm. Nếu muốn đổi member thì phải tạo team mới.

// Xác nhận/sửa: tôi nghĩ sẽ khóa cứng luôn, ko theo năm

---

## 4. Luồng event

### 4.1. Tạo event

Admin tạo event và cấu hình các thông tin chính: thời gian, round, track, topic, tiêu chí chấm điểm, giải thưởng, staff, mentor và judge.

// Xác nhận/sửa: có thể tạo trước 1 event bằng các thông tin đơn giản, như số longjw team, thời gian tổ chức, và mô tả,... còn tạo track và topic là ẩn, chỉ khi bốc thăm mới được staff gán, và có thể ẩn track, khi qua ngày bốc thăm track thì staff có thể hiện lên để có thể thấy track. phân công có thể phân công sau luôn, nhưng tiêu chí chấm và giải thưởng cũng cso thể tạo sau. event có thể khi nào muốn cho người khác coi thì cho coi, còn khi chưa muốn công bố, hoặc chưa setup xong thì chưa muốn hiện ra cho user thấy

### 4.2. Chapter theo năm

Không dùng bảng `Chapter` riêng. Một chapter tương ứng với một năm.

// Xác nhận/sửa:

### 4.3. Số event trong một năm

Một năm/chapter cố định có 3 event.

```text
EventNo = 1
EventNo = 2
EventNo = 3
```

// Xác nhận/sửa: bạn có thể tự giải quyết theo mô tả đã nói từ trước nhé

### 4.4. Round trong event

Mỗi event có thể có nhiều round. Số lượng round do admin cấu hình.

// Xác nhận/sửa: đúng, và mối round có nhiều tiêu chí chấm điểm, có thể tạo event trước và tạo thêm và chỉnh sửa round và tiêu chí chấm điểm sau

### 4.5. Track trong event

Mỗi event có nhiều track. Track do admin tạo theo từng event.

// Xác nhận/sửa: đúng rôi, mỗi event có nhiều track và mỗi track có nhiều chủ đề, giải thích thêm với bạn là mỗi track là 1 bảng đấu khác nhau, mỗi topic trong track là 1 chủ đề, là 1 đề thi.

### 4.6. Topic / Exam Paper

Hiện tại draw.io có bảng `Topic`. Cần chốt ý nghĩa:

```text
A. Topic = đề thi / exam paper
B. Topic = chủ đề lớn, cần thêm exam paper riêng
C. Topic = khái niệm khác
```

// Trả lời: topic là đề thi luôn, vì các track đề chung 1 round và mỗi 1 round có 1 tiêu chí chấm chung, mà topic là con của track nên chỉ cần ghi các thông tin cơ bản mà 1 đề thi có thôi, link nộp và phần ghi đề thi,...

---

## 5. Luồng đăng ký event

### 5.1. Team leader gửi đơn đăng ký event

Khi team leader gửi đơn đăng ký vào event, backend cần kiểm tra:

```text
- event còn trong thời gian đăng ký
- team chưa bị khóa sai trạng thái
- toàn bộ member đã hoàn thiện profile bắt buộc
- số lượng member phù hợp min/max của event
- không có member nào tham gia team khác trong cùng event
```

// Xác nhận/bổ sung: đúng

### 5.2. Staff duyệt team

Staff duyệt hoặc từ chối nguyên team, không duyệt từng member riêng lẻ.

// Xác nhận/sửa: đúng, có thể coi được từng thành viên trong team đó để ra quyết định, và ghi lý do

### 5.3. Reject đơn đăng ký

Nếu staff reject đơn đăng ký, bắt buộc nhập `RejectionReason`.

// Xác nhận/sửa: đúng

### 5.4. Đăng ký lại sau khi bị reject

Team bị reject có thể chỉnh sửa thông tin/member rồi gửi lại đơn đăng ký.

// Xác nhận/sửa: đúng

---

## 6. Luồng staff / mentor / judge

### 6.1. Staff theo event

Admin phân công staff vào event. Staff không được phân công thì không được thao tác nghiệp vụ trên event đó.

// Xác nhận/sửa: đúng

### 6.2. Mentor và judge theo track

Lecture được gán event role `Mentor` hoặc `Judge`, sau đó được assign vào track cụ thể.

// Xác nhận/sửa: đúng

### 6.3. Mentor và judge trong cùng event

Một lecture không được vừa là mentor vừa là judge trong cùng một event.

// Xác nhận/sửa: đúng

### 6.4. Mentor/judge ở event khác nhau

Một lecture có thể là mentor ở event này và judge ở event khác.

// Xác nhận/sửa: đúng

### 6.5. Quyền của mentor

Mentor dự kiến có quyền:

```text
- xem team trong track mình phụ trách
- xem tiến độ của team trong track
- gửi thông báo một chiều cho team trong track
- không chấm điểm
```

// Xác nhận/bổ sung: đúng

### 6.6. Quyền của judge

Judge dự kiến có quyền:

```text
- xem submission thuộc track được phân công
- biết submission thuộc team nào
- không xem chi tiết thành viên trong team
- chấm điểm theo criteria
- nhập feedback
- không xem điểm judge khác trước ScoreRevealAt
```

// Xác nhận/bổ sung: đúng

---

## 7. Luồng bốc thăm track/topic

### 7.1. Bốc thăm offline

Việc chọn track/topic diễn ra offline. Hệ thống không sinh random trực tuyến.

// Xác nhận/sửa: đúng, staff sẽ gán cho team đã đăng kí đó topic và track của họ

### 7.2. Staff lưu kết quả bốc thăm

Cần chốt nơi lưu kết quả team bốc thăm được track/topic:

```text
A. RegisterTeam
B. TrackOfRound
C. Bảng khác
D. Cần đề xuất lại
```

// Trả lời: 1 track đại diện cho toàn bộ round, 1 track đi theo tất cả các round, nên lưu ở TrackOfRound, nếu có đề xuất gì nói tôi nhé

### 7.3. Một team trong một event thuộc một track

Một team trong một event chỉ được thuộc một track.

// Xác nhận/sửa: đúng

---

## 8. Luồng nộp bài

### 8.1. Người được nộp bài

Chỉ team leader được nộp bài cho team.

// Xác nhận/sửa: đúng

### 8.2. Deadline theo round

Mỗi round có deadline nộp bài riêng.

// Xác nhận/sửa: đúng

### 8.3. Nộp nhiều lần

Team được nộp nhiều lần. Hệ thống lưu version/history và chỉ dùng bản mới nhất hợp lệ trước deadline để chấm.

// Xác nhận/sửa: đúng, chủ yếu là để lưu lại các lần chỉnh sửa chứ ko phải ghi đè lên

### 8.4. Sau deadline

Sau deadline, cổng nộp bài bị khóa.

Cần chốt:

```text
A. khóa tuyệt đối
B. staff/admin có thể mở khóa thủ công
C. có thể mở khóa nhưng bắt buộc ghi lý do/audit
```

// Trả lời: C. có thể mở khóa nhưng bắt buộc ghi lý do/audit

---

## 9. Luồng chấm điểm

### 9.1. Chấm theo criteria

Judge chấm submission theo từng criteria item.

// Xác nhận/sửa: đúng rồi, theo thống nhất ở đầu event theo từng round

### 9.2. Số judge chấm một submission

Cần chốt:

```text
A. một submission có nhiều judge chấm
B. một submission chỉ có một judge chấm
C. tùy round/track
```

// Trả lời: A. một submission có nhiều judge chấm, do staff phân công

### 9.3. Công thức điểm cuối round

Cần chốt:

```text
A. trung bình tổng điểm của các judge
B. trung bình từng criteria item rồi cộng lại
C. lấy điểm cuối cùng của judge được phân công chính
D. công thức khác
```

// Trả lời: A. trung bình tổng điểm của các judge

### 9.4. Sửa điểm

Cần chốt khi judge sửa điểm:

```text
A. tạo record/version mới
B. update record cũ
C. tùy trạng thái Draft/Finalized
```

// Trả lời: B. update record cũ

### 9.5. Xem điểm judge khác

Judge không được xem điểm của judge khác trước `ScoreRevealAt`.

// Xác nhận/sửa: đúng

### 9.6. Finalized score

Sau khi score finalized hoặc event finished, admin/staff/judge không được sửa điểm trực tiếp.

// Xác nhận/sửa: đúng

---

## 10. Luồng phúc khảo

### 10.1. Số lần phúc khảo

Team được gửi phúc khảo 1 lần duy nhất cho mỗi round.

// Xác nhận/sửa: đúng

### 10.2. Người gửi phúc khảo

Cần chốt:

```text
A. chỉ team leader
B. bất kỳ member nào trong team
C. member gửi report, nhưng appeal chính thức chỉ team leader
```

// Trả lời: A. chỉ team leader

### 10.3. Phân công chấm lại

Staff có thể phân công judge cũ hoặc judge mới để chấm lại.

// Xác nhận/sửa: đúng

### 10.4. Kết quả regrade

Điểm regrade là kết quả cuối cùng của round và không được appeal lần hai.

// Xác nhận/sửa: đúng

---

## 11. Luồng thăng vòng

### 11.1. Chọn team vào vòng sau

Hệ thống sắp xếp team theo điểm, staff chọn thủ công team vào vòng sau.

// Xác nhận/sửa: staff nhìn danh sách team cùng điểm trong round đó, và staff chọn các team đẻ cho lên vòng mới

### 11.2. Lý do chọn thủ công

Staff chọn team vào vòng sau không bắt buộc nhập lý do.

// Xác nhận/sửa: đúng

### 11.3. Trạng thái team không vào vòng sau

Cần chốt tên trạng thái:

```text
Eliminated
NotAdvanced
Stopped
Khác
```

// Trả lời: Stopped

---

## 12. Leaderboard

### 12.1. Leaderboard theo event

Leaderboard theo event lưu kết quả cuối cùng của các team trong một event.

Cần chốt công thức:

```text
A. trung bình điểm các round
B. tổng điểm các round
C. điểm round cuối
D. công thức khác
```

// Trả lời: B. tổng điểm các round

### 12.2. Leaderboard theo năm

Leaderboard theo năm tổng hợp kết quả từ các event trong cùng năm.

Cần chốt công thức:

```text
A. tổng điểm leaderboard event trong cùng năm
B. trung bình điểm leaderboard event trong cùng năm
C. tính theo rank từng event
D. công thức khác
```

// Trả lời: A. tổng điểm leaderboard event trong cùng năm

### 12.3. Snapshot leaderboard năm

Cần chốt:

```text
A. lưu snapshot vào LeaderBoardDetail
B. luôn tính động
C. Redis là chính, DB chỉ lưu event leaderboard
```

// Trả lời: tự lựa chọn tói ưu nhất, chưa chốt

### 12.4. Team thiếu event trong năm

Nếu team tham gia event #1 nhưng không tham gia event #2, cần chốt cách tính leaderboard năm:

```text
A. chỉ cộng điểm event team có tham gia
B. không tham gia thì tính 0 điểm event đó
C. không đủ điều kiện vào leaderboard năm
```

// Trả lời: A. chỉ cộng điểm event team có tham gia

---

## 13. Notification / Report

### 13.1. MentorNotification

MentorNotification là thông báo một chiều từ mentor tới team trong track. Team chỉ xem, không chat lại.

// Xác nhận/sửa: đúng

### 13.2. Report

Cần chốt `Report` dùng cho:

```text
A. khiếu nại/hỗ trợ chung của user gửi hệ thống
B. report submission sai phạm
C. report vấn đề trong event/team
D. tất cả các loại trên
```

// Trả lời: ở đây ghi lại toàn bộ, nếu ấn report ở trên bài nộp thì sẽ gán bài nộp vào để retake, hoặc judge khiếu nại bài chấm có gian lận, hoặc các report bình thường như app này xấu qá, tôi ko ddnagw kí vào event được. staff sẽ đọc các khiếu nại thủ công và quyết định có cho bài chấm lại khoogn, có ban team đó không.

### 13.3. Người được gửi report

Cần chốt ai được gửi report:

```text
Admin
Staff
Student
Lecture
Mentor
Judge
Tất cả user
```

// Trả lời: Tất cả user

---

## 14. Ban / khóa user / khóa team

### 14.1. User bị ban trước event

User bị ban trước event thì không đăng nhập được.

// Xác nhận/sửa: đúng, ban thì không vào được, kể cả ngoài và trong

### 14.2. User bị ban trong lúc event đang chạy

User bị ban trong lúc event đang chạy không tự động làm team bị khóa. Nếu cần, staff/admin xử lý cả team.

// Xác nhận/sửa: đúng

### 14.3. Ban team

Cần chốt có cần chức năng ban team không:

```text
A. có ban team riêng
B. không ban team, chỉ reject/disable register team
C. xử lý bằng trạng thái RegisterTeam
```

// Trả lời:xử lý bằng status, ban team là team đó bị ban vĩnh viễn, có chỉ disable trong 1 event này thôi, từ chối và chấp chận team

---

## 15. Audit log

### 15.1. Hành động cần audit

Danh sách đề xuất:

```text
- Admin tạo/sửa event
- Admin phân công staff
- Admin phân công mentor/judge
- Staff duyệt/reject team
- Staff nhập kết quả bốc thăm track/topic
- Judge finalized score
- Staff xử lý appeal/regrade
- Staff chọn team vào vòng sau
- Admin/staff publish result
- Khóa/mở khóa các trạng thái quan trọng
```

// Xác nhận/bổ sung/bỏ bớt: những thứ có ảnh hưởng lớn đến event như sửa tiêu chí thi, sửa đề, ban team,... còn những cái như phân công, gán team và track thì không cần, vì các chức năng phụ đó đa số là staff làm, admin vẫn có thể làm nhưng thường không làm

### 15.2. Điểm và leaderboard sau khi event kết thúc

Sau khi event finished, score và leaderboard khóa read-only.

// Xác nhận/sửa: đúng

---

## 16. Điểm cần kiểm tra lại với draw.io

### 16.1. Số bảng hiện tại

Cần chốt số bảng trong `Hackathon2026.drawio`:

```text
A. 35 bảng
B. 36 bảng
C. số khác
```

// Trả lời: 35 bảng

### 16.2. Bảng còn thiếu nếu có

Nếu draw.io có 36 bảng, cần ghi tên bảng còn thiếu ở đây.

// Trả lời: lúc đó tôi hỏi thử xem xem bạn có coi đugns hay ko thôi

---

## 17. Ghi chú quyết định cuối cùng

Phần này dùng để ghi lại các quyết định sau khi Hồng Trung lão đại trả lời các câu hỏi bên trên.

// Ghi chú: nếu con thắc mắc gì hãy bổ xung nhé
