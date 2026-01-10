# Cli-Editor

Cli-Editor is a lightweight command-line file editor built on .NET 9.0. It provides quick file creation, editing, renaming and deletion from the terminal and uses Terminal.Gui to present a simple text editor UI inside the console.

---

## Features

- Create or edit text and code files from the terminal.
- Rename and delete files with confirmation.
- Terminal-based editing UI using Terminal.Gui (TextView + StatusBar).
- In-editor shortcuts for saving and exiting.
- Validates and restricts file extensions to common text/code formats.
- Small and focused: designed for quick edits without leaving the terminal.

---

## Prerequisites

- .NET SDK 9.0 (dotnet 9) installed. Verify with:
  ```bash
  dotnet --version
  ```
  The project targets net9.0 as defined in `Cli-Editor/Cli-Editor.csproj`.

- The project depends on Terminal.Gui (version referenced in the csproj). `dotnet restore` will fetch it.

---

## Quick Start

There are two common ways to run Cli-Editor:

1) Run from source (recommended if you want to build or modify)
```bash
# from the repository root
cd Cli-Editor

# restore dependencies
dotnet restore

# run in debug/development configuration
dotnet run
```

Note: When compiled in DEBUG configuration the app currently calls KeyReader with a default file (`a.txt`) for convenience. Release behavior uses command-line arguments.

2) Use the published executable (if present in repository)
- Example path included in this repo:
  Cli-Editor/bin/Release/net9.0/win-x64/publish/Cli-Editor.exe
- Run it directly:
  ```powershell
  .\Cli-Editor.exe -n example.txt
  ```

To create a distribution build:
```bash
cd Cli-Editor
dotnet publish -c Release
```
Adjust runtime identifiers (RID) and additional publish flags if you require a self-contained or single-file publish (see dotnet publish docs).

---

## Command-line Usage

The main executable accepts a small set of flags:

- Create or open a file for editing:
  ```bash
  Cli-Editor -n filename.txt
  ```
  Opens the editor UI for filename.txt (creates it if it doesn't exist).

- Delete a file (with confirmation):
  ```bash
  Cli-Editor -d filename.txt
  ```
  The editor will prompt to confirm deletion. Press `Y` to confirm, or any other key to abort.

- Rename a file:
  ```bash
  Cli-Editor -r oldname.txt newname.txt
  ```
  The tool will attempt to move the file. If the destination already exists it will be overwritten. The extension is validated—rename will be refused if extensions don't match.

- Show help:
  ```bash
  Cli-Editor -h
  ```

- Open a filename directly (no flag):
  ```bash
  Cli-Editor filename.txt
  ```
  If you pass just a filename the program validates the extension and opens the editor.

Notes:
- If no args are provided in Release mode, the program prints an error and a suggestion to use `-h`.
- The program validates extensions; invalid extensions produce an "invalid Extension" message and the program exits.

---

## Supported Extensions

The editor validates file extensions. Supported (as of repository code) include:
- .txt .html .css .js .cpp .csv .xml .json .log .cs

If you pass a filename without an extension or with a disallowed extension, the program reports a missing/invalid extension and exits.

---

## In-Editor UI & Shortcuts

Cli-Editor uses Terminal.Gui for the editing UI (TextView). Shortcut behavior differs slightly between the two editor flavors implemented.

NewFile Editor (Append-like mode):
- Ctrl + A — Save (writes current text to disk)
- Ctrl + Q — Save & Exit
- Esc — Exit without saving
- Status bar shows:
  - Word/Character counts and file size
  - Save hints

OverWrite Editor:
- Ctrl + Z — Save & Exit
- Esc — Exit without saving

General notes about edit flow:
- The editor opens a TextView with WordWrap enabled.
- When you save (the configured shortcut), content is written to the file path shown in the window title.
- For the Terminal-based flow described in earlier versions, `Ctrl+Z` + Enter was used as an input termination sequence for console-based modes — when running the Terminal.Gui editor, you use the GUI shortcuts above.

---

## Delete Flow

- When using `Cli-Editor -d filename` or the delete command inside command mode, the app prompts:
  - Press `Y` to confirm deletion.
  - Any other key aborts deletion.
- If the file does not exist, the app reports it and returns.

---

## Messages & Errors

Common messages you may encounter and what they mean:
- "Missing Extention or File Name..." — you must provide a filename with an extension.
- "invalid Extension..." — the extension is not in the supported list.
- "Error No File Given" — no CLI arguments were provided when required (Release mode).
- UnauthorizedAccessException / permission errors — running without permission to read/write the target file or folder.

---

## Troubleshooting

- Make sure the .NET SDK for .NET 9 is installed and on your PATH.
- If you see permission errors, try running the editor in a directory you control (e.g., your user folder) or run with elevated permissions only when necessary.
- If Terminal.Gui rendering is odd in your terminal, try running in a standard Windows console or a terminal emulator known to work with curses-style UIs.

---

## Development notes

- Project file:
  - TargetFramework: net9.0 (see Cli-Editor/Cli-Editor.csproj)
  - Dependency: Terminal.Gui (1.19.0)

- Debug mode: the program will open a default `a.txt` in the current directory for quick testing.

- The code contains small helper functions:
  - DrawStatusBar — writes messages to the bottom console line.
  - Loader — displays a simple spinner animation while performing waits.

---

## Contributing

Contributions are welcome. A few ways to help:
- File bug reports or feature requests via issues.
- Open pull requests with small, focused changes (fixes, improved error messages, additional supported extensions).
- If you add features that change user-visible behavior, please update this README.

---

## License

No license file is present in this repository. If you are the repository owner, consider adding a LICENSE to clarify usage rights.

---

## Contact

Author (as noted in help text): Puneet

If you'd like help improving this README or documenting a new feature you add, open an issue or PR.

---

Enjoy small, fast edits without leaving the terminal!
