# TeamMapSyncApp

## What this repository contains

- `api/TeamMapSyncApi`: ASP.NET Core backend
- `client`: TanStack Start + Vite frontend

## Start here (fresh clone)

### 1) Choose where to clone

Pick a folder where you usually keep source code:

- macOS example: `~/Developer`
- WSL/Linux example: `~/code`
- Windows PowerShell example: `C:\dev`

If you are using WSL, keep Node.js and the project in the same filesystem for best reliability and performance.
See Microsoft guidance: [Install Node.js on WSL](https://learn.microsoft.com/en-us/windows/dev-environment/javascript/nodejs-on-wsl).

### 2) Clone the repo

Official clone docs: [GitHub - Cloning a repository](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository).

```bash
git clone https://github.com/Gramnaster/TeamMapSyncApp.git
cd TeamMapSyncApp
```

### 3) Install required tools

#### Git

- Install docs: [Git - Installing Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)

#### Node.js and pnpm

This project pins Node.js in `.tool-versions`:

- Required Node.js: `24.6.0`
- Required package manager: pnpm 11.x

pnpm install docs: [pnpm installation](https://pnpm.io/installation).

### 4) Setup on macOS

1. Install Xcode Command Line Tools (for Git/tooling):

```bash
xcode-select --install
```

1. Install nvm (official install script):

```bash
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.4/install.sh | bash
```

1. Restart terminal, then install and use the pinned Node version:

```bash
nvm install 24.6.0
nvm use 24.6.0
```

1. Enable pnpm through Corepack:

```bash
npm install --global corepack@latest
corepack enable
```

### 5) Setup on Windows

#### Option A: WSL (recommended if you already develop in WSL)

1. Install nvm in WSL:

```bash
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.4/install.sh | bash
```

1. Restart terminal, then install and use the pinned Node version:

```bash
nvm install 24.6.0
nvm use 24.6.0
```

1. Enable pnpm through Corepack:

```bash
npm install --global corepack@latest
corepack enable
```

#### Option B: PowerShell (native Windows)

1. Install a Node version manager for Windows, for example:

- [nvm-windows](https://github.com/coreybutler/nvm-windows)

1. Install and use Node `24.6.0` with your manager.

1. Enable pnpm through Corepack:

```powershell
npm install --global corepack@latest
corepack enable
```

### 6) Verify tool versions

```bash
node -v
pnpm -v
```

Expected Node output: `v24.6.0`.

### 7) Install frontend dependencies and run dev server

Important: run pnpm commands in `client`.

```bash
cd client
pnpm install
pnpm dev
```

Frontend runs on port `3000`.

## Running backend and frontend together

Use two terminals.

Terminal 1 (backend):

```bash
cd api/TeamMapSyncApi
dotnet watch run
```

Terminal 2 (frontend):

```bash
cd client
pnpm install   # first time only
pnpm dev
```

## Troubleshooting (error handling)

### pnpm dev fails from repository root

Cause: this repository has no root `package.json`, so root is not a pnpm app/workspace entry point.

Fix:

```bash
cd client
pnpm dev
```

or run from root with `-C`:

```bash
pnpm -C client install
pnpm -C client dev
```

### ERR_PNPM_NO_IMPORTER_MANIFEST_FOUND

Cause: pnpm was run in a directory without `package.json`.

Fix: run from `client`.

### pnpm: command not found

Cause: pnpm is not installed/enabled in your current shell.

Fix: re-run Corepack setup:

```bash
npm install --global corepack@latest
corepack enable
```

### Wrong Node version

Cause: shell is not using Node `24.6.0`.

Fix:

```bash
nvm install 24.6.0
nvm use 24.6.0
```

Then reinstall dependencies in `client`.

### VS Code shows "Cannot find module '@tanstack/react-router'" (TS2307)

Cause: VS Code's built-in TypeScript version is older than the workspace's `typescript@6.x`, which ships with TanStack Start. The built-in version cannot resolve the package's exports correctly even though `tsc` itself has no problem.

Fix: restart the TypeScript language server.

1. Open the Command Palette (`Ctrl+Shift+P` / `Cmd+Shift+P`).
2. Run **TypeScript: Restart TS Server**.

The errors clear immediately. No rebuild needed.
