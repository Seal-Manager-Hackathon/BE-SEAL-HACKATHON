# POST - Auth

## `POST /api/v1/auth/register`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Đăng ký tài khoản mới.
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-register.md)

## `POST /api/v1/auth/login`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Đăng nhập. Trả về access token và refresh token qua cookies.
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-login.md)

## `POST /api/v1/auth/logout`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Đăng xuất. Xóa auth cookies.
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-logout.md)

## `POST /api/v1/auth/tokens/refresh`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Làm mới token. Đọc refresh token từ cookies và trả về cặp token mới.
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-tokens-refresh.md)

## `POST /api/v1/auth/email-verifications`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Xác thực email bằng mã OTP/code.
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-email-verifications.md)

## `POST /api/v1/auth/email-verifications/resend`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Gửi lại mã xác thực email.
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-email-verifications-resend.md)

## `POST /api/v1/auth/forgot-password`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Yêu cầu đặt lại mật khẩu (gửi email).
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-forgot-password.md)

## `POST /api/v1/auth/reset-password`
- **Policy:** Không yêu cầu xác thực (Public)
- **Trạng thái:** `CÓ SẴN`
- **Dùng chung với:** Tất cả
- **Ghi chú:** Đặt lại mật khẩu bằng token.
→ [📄 Doc chi tiết](../../ApiDocs/Auth/POST/api-v1-auth-reset-password.md)
