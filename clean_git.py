import os
import shutil
import subprocess

def main():
    base_dir = "Hackathon.Api/Docs/ApiDocs"
    subdirs_to_delete = [
        "Auth", "Critical", "Events", "Invitations", "RegisterTeams",
        "Rounds", "Staff", "Teams", "Topics", "Tracks", "Users"
    ]

    for subdir in subdirs_to_delete:
        path = os.path.join(base_dir, subdir)
        if os.path.exists(path):
            print(f"Deleting directory: {path}")
            shutil.rmtree(path)
            # Stage the deletion in git
            subprocess.run(["git", "rm", "-rf", path], capture_output=True)

    rej_file = "Hackathon.Api/Program.cs.rej"
    if os.path.exists(rej_file):
        print(f"Deleting file: {rej_file}")
        os.remove(rej_file)
        subprocess.run(["git", "rm", "-f", rej_file], capture_output=True)

    doc02_file = "newapi/doc02.md"
    if os.path.exists(doc02_file):
        print(f"Deleting file: {doc02_file}")
        os.remove(doc02_file)
        subprocess.run(["git", "rm", "-f", doc02_file], capture_output=True)

    print("Cleanup done!")

if __name__ == "__main__":
    main()
