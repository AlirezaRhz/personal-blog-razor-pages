# Personal Blog — ASP.NET Core Razor Pages

A full-stack personal blog built with ASP.NET Core Razor Pages, Entity Framework Core, SQLite, ASP.NET Core Identity, and Bootstrap.

Public blog with Markdown-rendered articles and Persian/Tehran date formatting, plus a role-protected admin panel for managing content.

## Features

- **Public blog** — article previews, detail pages, Markdown rendering (via Markdig), Persian date/Tehran time, responsive layout
- **Admin panel** (Admin role only) — create/edit/delete articles, Markdown editor
- **Auth & security** — ASP.NET Core Identity with role-based access (Admin/User, seeded on startup), protected admin routes, sanitized Markdown rendering to prevent HTML/JS injection
- **Image uploads** — GUID filenames to avoid collisions, default-image fallback when none is provided (2 default images included)

## Tech Stack

ASP.NET Core · Razor Pages · Entity Framework Core · SQLite · ASP.NET Core Identity · Markdig · Bootstrap · C#

## Getting Started

\`\`\`bash
git clone https://github.com/AlirezaRhz/personal-blog-razor-pages.git
cd personal-blog-razor-pages
dotnet restore
dotnet ef database update
dotnet run
\`\`\`

Requires the [.NET SDK](https://dotnet.microsoft.com/download).

## Project Status

Core functionality is complete: public browsing, Markdown rendering, auth/authorization, full article CRUD, image uploads, and responsive styling.
