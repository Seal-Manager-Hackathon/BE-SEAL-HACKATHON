import os
import re

enum_mapping = {
    "RoleEnum": {"Admin": 0, "Staff": 1, "Student": 2, "Lecturer": 3},
    "EventRoleEnum": {"Mentor": 0, "Judge": 1},
    "EmailVerificationStatusEnum": {"Pending": 0, "Verified": 1, "Expired": 2},
    "EventStatusEnum": {"Draft": 0, "Published": 1, "Closed": 2, "Cancelled": 3},
    "InvitationStatusEnum": {"Pending": 0, "Accepted": 1, "Rejected": 2, "Expired": 3},
    "NotificationStatusEnum": {"Pending": 0, "Unread": 1, "Read": 2},
    "RegisterTeamStatusEnum": {"Pending": 0, "Approved": 1, "Rejected": 2},
    "ReportStatusEnum": {"Open": 0, "Closed": 1},
    "SubmissionStatusEnum": {"Submitted": 0},
    "TeamDetailStatusEnum": {"Active": 0, "Inactive": 1},
    "UserStatusEnum": {"Active": 0, "Inactive": 1, "Banned": 2},
    "LeaderBoardsStatusEnum": {"IsDisabled": 0},
    "ScoresStatusEnum": {"IsRetake": 0, "IsMock": 1, "IsDisable": 2}
}

docs_dir = "./Hackathon.Api/Docs/ApiDocs/"

for root, dirs, files in os.walk(docs_dir):
    for filename in files:
        if not filename.endswith(".md") or filename == "enum-values.md":
            continue

        filepath = os.path.join(root, filename)
        with open(filepath, "r", encoding="utf-8") as f:
        content = f.read()

    changed = False

    # Look for patterns like RoleEnum: "Admin" or RoleEnum: "0" and comments
    for enum_name, values in enum_mapping.items():
        # This regex tries to find the field definition and its example
        # Example format in markdown docs:
        # - `role` (RoleEnum): Admin, Staff...
        # "role": "Admin"
        
        # We'll just do a simple string replacement for now for common occurrences
        # Or look for JSON block and replace "Admin" with 0 for RoleEnum fields.
        pass

    # A more robust approach for JSON payloads:
    # Find JSON blocks
    
    def process_json_block(match):
        block = match.group(0)
        # Inside JSON block, replace string enums with ints
        # But we need to know which field is which enum. 
        # This is tricky without knowing the schema perfectly.
        return block

    # Let's search for the field descriptions first
    # e.g., `Role` (string) -> `Role` (integer) // 0: Admin, 1: Staff, ...
    
    lines = content.split('\n')
    new_lines = []
    
    for line in lines:
        # Match something like: - `role` (RoleEnum)
        # or - `status` (string, Enum: "Active", "Inactive")
        
        # We will try to find any mention of the enum and add a comment
        line_changed = False
        for enum_name, mapping in enum_mapping.items():
            if enum_name in line:
                # Add a comment if it doesn't exist
                comment = " // " + ", ".join([f"{v}: {k}" for k, v in mapping.items()])
                if comment not in line:
                     # Check if it's a table row or a list item
                     if line.startswith("-") or "|" in line:
                         # Append comment
                         line += comment
                         changed = True
                         line_changed = True
                         break
                         
        # If no enum name is explicitly mentioned, look for common field names and values
        if not line_changed:
            pass
            
        new_lines.append(line)
        
    if changed:
        with open(filepath, "w", encoding="utf-8") as f:
            f.write('\n'.join(new_lines))
        print(f"Updated {filename}")

