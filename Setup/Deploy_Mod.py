import os
import shutil
import argparse
import sys

def main():
    parser = argparse.ArgumentParser(description="Deploy Bannerlord Coop Mod")
    parser.add_argument("game_path", nargs="?", help="Path to Bannerlord installation")
    args = parser.parse_args()

    game_path = args.game_path
    if not game_path:
        # Default fallback or asking user would go here, for automation we'll imply it needs to be passed or hardcoded if known
        # Try to read from a config if exists, or just ask
        print("Please provide the Game Path as an argument.")
        # Attempt to guess from sibling script or just return
        return

    script_dir = os.path.dirname(os.path.abspath(__file__))
    root_dir = os.path.dirname(script_dir)
    
    # Source Build Output
    # The project path is Source/source/Coop
    # The output path is bin/Debug (since AppendTargetFrameworkToOutputPath is false)
    
    source_bin = os.path.join(root_dir, "Source", "source", "Coop", "bin", "Debug")
    
    if not os.path.exists(source_bin):
        print(f"Build output not found at: {source_bin}")
        print("Please build the solution first: dotnet build Source/BannerlordCoop.sln")
        sys.exit(1)

    target_mod_dir = os.path.join(game_path, "Modules", "Bannerlord.Coop", "bin", "Win64_Shipping_Client")
    
    if not os.path.exists(target_mod_dir):
        print(f"Creating module directory: {target_mod_dir}")
        os.makedirs(target_mod_dir)
        
    print(f"Deploying from {source_bin} to {target_mod_dir}...")
    
    for filename in os.listdir(source_bin):
        if filename.endswith(".dll") or filename.endswith(".pdb") or filename.endswith(".xml"):
            src = os.path.join(source_bin, filename)
            dst = os.path.join(target_mod_dir, filename)
            try:
                shutil.copy2(src, dst)
                print(f"Copied {filename}")
            except Exception as e:
                print(f"Failed to copy {filename}: {e}")

    print("Deployment complete.")

if __name__ == "__main__":
    main()
