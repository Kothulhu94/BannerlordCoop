import subprocess
import sys
import os

def run_tests():
    dotnet_path = r"D:\Bannerlord_Coop\DevTools\dotnet\dotnet.exe"
    project_path = r"Source\source\GameInterface.Tests\GameInterface.Tests.csproj"
    cwd = r"d:\Bannerlord_Coop"
    
    command = [
        dotnet_path,
        "test",
        project_path,
        "--no-restore",
        "--logger",
        "console;verbosity=normal"
    ]
    
    print(f"Running command: {' '.join(command)}")
    print("-" * 50)
    
    try:
        # Run the command and capture output
        process = subprocess.run(
            command,
            cwd=cwd,
            capture_output=True,
            text=True,
            encoding='utf-8',
            errors='replace'
        )
        
        lines = process.stdout.splitlines()
        
        failed_tests = []
        current_failure = []
        in_failure = False
        
        for line in lines:
            if "Failed " in line and "GameInterface.Tests" in line:
                in_failure = True
                current_failure = [line]
            elif in_failure:
                if "Passed " in line or "Total tests:" in line:
                    in_failure = False
                    failed_tests.append(current_failure)
                    current_failure = []
                else:
                    current_failure.append(line)
        
        # Capture last failure if exists
        if in_failure and current_failure:
             failed_tests.append(current_failure)

        output_lines = []
        if not failed_tests:
            output_lines.append("No failed tests found (or passed successfully).")
            output_lines.append("\n".join(lines[-10:]))
        else:
            output_lines.append(f"Found {len(failed_tests)} failed tests:\n")
            for i, failure in enumerate(failed_tests, 1):
                output_lines.append(f"--- Failure {i} ---")
                output_lines.append("\n".join(failure[:20]))
                output_lines.append("..." if len(failure) > 20 else "")
                output_lines.append("-" * 30)

        if process.returncode == 0:
            output_lines.append("Tests passed successfully.")
        else:
            output_lines.append(f"Tests failed with return code {process.returncode}.")
            
        with open("cleaned_results.txt", "w", encoding="utf-8") as f:
            f.write("\n".join(output_lines))
        
        print("Output written to cleaned_results.txt")
            
    except Exception as e:
        print(f"An error occurred: {e}")

if __name__ == "__main__":
    run_tests()
