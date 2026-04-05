# Multi Threading - Python Net Rust
- locally ran (Windows OS) tests to compare performance and memory between 3 technologies
- single threaded and multi threaded variants of Monte Carlo PI and Merge Sort
- more overview & details https://github.com/mjoniec/MultiThreading_PythonNetRust/wiki

# Troubleshoot & Setup

## NET
- just install VisualStudio 2026 & run F5 (debug)

## Python
- install Visual Studio Code, use build-in terminal
- 2 installation variants: normal 3.14.3 (installer) and 3.14t (experimental, uv, powershell)
- run multi-thread (custom no GIL) using command: `python3.14t -X gil=0 .\main.py`
- run single-thread (default with GIL) using command: `python3.14 .\main.py`
- `powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex"`
- `winget install --id=astral-sh.uv  -e`
- `uv python install 3.14t`
- issue: ModuleNotFoundError: No module named 'psutil' >> `uv add psutil`
- error: No pyproject.toml found in current directory or any parent directory >> If you are using uv add: This command specifically requires a pyproject.toml to track dependencies. >> `uv pip install psutil`
- error: No virtual environment found; run uv venv to create an environment, or pass... >> (Create virtual environment in project root) >> `uv venv`
- (Activate it:) `.venv\Scripts\activate`
- issue: .venv\Scripts\activate : File .venv\Scripts\activate.ps1 cannot be loaded. The file .venv\Scripts\activate.ps1 is not digitally signed. You cannot run this script on the current system. >> This error occurs because Windows PowerShell's default security policy blocks the execution of unsigned scripts. Temporarily Enable for the Current Session: `Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass`
- info: after all that the ".venv\Scripts\activate" worked and then "uv pip install psutil" also worked ... import psutil stopped showing errors in IDE. 
- info: When using uv + virtual >> pip is no longer used

## Rust
- Install (VS installer) C++ Build Tools, select "Desktop development with C++" (Rust requires a C++ linker on Windows)
- install Rust: Visit rustup.rs to download and run the installer (rustup-init.exe). This installs the compiler (rustc) and the package manager (cargo).
- install VS Code Extensions: rust-analyzer(language server) and CodeLLDB (debugging)
- `rustc --version` (check if installed & what version)
- `cargo new MultiThreadingRust` (cargo recommended, as rust-analyzer performs best when it can find a Cargo.toml project manifest)
- `cargo run` - run app (debug slow!)
- `cargo run --release` performance test run - 100x faster than debug!
- error link.exe >> re-install C++ Build Tools
