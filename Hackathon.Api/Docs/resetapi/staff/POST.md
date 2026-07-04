# POST - Staff

## `POST /api/v1/staff/events/{eventId}/assign-lecturers`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Phân công giảng viên vào một sự kiện.

## `POST /api/v1/staff/events/{eventId}/tracks/{trackId}/assign-lecturers`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Phân công giảng viên vào một track trong sự kiện.

## `POST /api/v1/staff/submissions/{submissionId}/assign-judges`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Phân công giám khảo chấm bài cho một bài nộp.

## `POST /api/v1/staff/reports/{reportId}/regrade`
- **Policy:** StaffOrAdminPolicy
- **Trạng thái:** `CÓ SẴN`
- **Nguồn:** Staff.cs
- **Ghi chú:** Phê duyệt yêu cầu chấm lại (regrade) cho một báo cáo.
