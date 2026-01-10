# Cli-Editor

Cli-Editor is a simple command-line file editor built on .NET 9.0. It lets you create, edit, delete, and rename files directly from the terminal. The core logic lives in `Cli-Editor.dll`, and `Cli-Editor` is the native launcher that starts the .NET runtime and invokes the editor.

---

## About Cli-Editor

Cli-Editor is designed for quick text-based editing without opening a full GUI editor. It runs in the console and uses keyboard input to switch between modes like Append, Overwrite, and Command. The configuration files (`Cli-Editor.deps.json` and `Cli-Editor.runtimeconfig.json`) tell the .NET runtime which framework to use and how to load dependencies.

---

## How to Use It

The main entry point is `Cli-Editor`. You pass flags and filenames to perform different operations.

- **Create or Edit a file**

  ```bash
  Cli-Editor -n filename.txt
  ```

  Inside the editor:
  - `[A]` Append mode
  - `[O]` Overwrite mode
  - `[Esc]` Command mode
  - `Ctrl+Z` then `Enter` to exit append/overwrite input

- **Delete a file**

  ```bash
  Cli-Editor -d filename.txt
  ```

  The editor will ask for confirmation:
  - `Y` to confirm deletion
  - `N` to abort

- **Rename a file**

  ```bash
  Cli-Editor -r oldname.txt newname.txt
  ```

- **Show help**

  ```bash
  Cli-Editor -h
  ```

  This prints usage, flags, and a short description of each command.

---

## Supported Extensions

Cli-Editor validates file extensions and will report “Wrong Extension” or “Invalid Extension” if not allowed. Supported extensions include:

- `.txt`
- `.html`
- `.css`
- `.js`
- `.cpp`

If you omit the extension or filename, you get messages like “Missing Extension or File Name” or “Error: No File Given”.

---

## Features

Cli-Editor offers several console-based editing and file-management features.

- **File operations**
  - Create new files or edit existing ones.
  - Delete files with confirmation.
  - Rename existing files.

- **Editing modes**
  - **Append mode**: Add content to the end of the file.
  - **Overwrite mode**: Rewrite content from the start.
  - **Command mode**: Switch modes, delete, or exit.

- **Console UI**
  - Status bar drawing for current mode and hints.
  - Uses `ConsoleKey` input to read commands and characters.
  - Clears and redraws sections as you work.

- **Safety and feedback**
  - Prompts before deleting files.
  - Clear messages for missing files, invalid extensions, and aborted actions.
  - Exits cleanly with a closing message when you leave the editor.
