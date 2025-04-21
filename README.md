# 🏁 NegDelta

**NegDelta** is a modern, local-first telemetry analysis app for iRacing.  
It helps drivers organize laps into stints and sessions, laying the groundwork for custom data visualizations, lap comparisons, and session sharing. Cloud-based features for collaboration and analysis are planned.

---

## 🎯 Project Goals

- 🧠 Organize and store laps within stints and sessions for structured review
- 📊 Visualize telemetry data (throttle, brake, steering, etc.) over time
- 🧪 Enable lap comparisons between users in future updates
- 🎛️ Support flexible, user-defined data visualization layouts
- 💻 Use modern UI tech (Blazor Hybrid + Plotly) for a fast, native experience

---

## 🛠 Tech Stack

- [.NET MAUI Blazor Hybrid](https://learn.microsoft.com/en-us/dotnet/maui/) – for local UI across desktop platforms
- [Entity Framework Core + SQLite](https://learn.microsoft.com/en-us/ef/core/) – for lightweight, local session storage
- [Plotly.NET](https://plotly.com/dotnet/) – (in progress) for interactive charting
- [irsdkSharpEr](https://github.com/mherbold/IRSDKSharper) – wrapper around the iRacing telemetry API

> 🧪 *This stack may evolve as the project grows and new challenges arise.*

---

## 📦 Status

🔧 **Actively in development**  
The project is still early, but the core structure is live:

- ✅ Session, Stint, and Lap domain models
- ✅ EF Core database with auto-migrations
- ✅ Data ingestion from iRacing via irsdkSharpEr
- ⏳ Charting + UI components (coming soon)

---

## 🔧 Getting Started

1. Clone this repo
2. Open `NegDelta.sln` in Visual Studio 2022+
3. Build and run — the app will create a local `session.db` with all migrations applied automatically

> ⚠️ No telemetry will be captured unless iRacing is running with live session data.

---

## 📁 Repo Structure

| Project | Purpose |
|-------------|---------|
| `NegDelta.Core`      | Core models, enums, and DTOs |
| `NegDelta.Data`      | EF Core `SessionDbContext` and related setup |
| `NegDelta.Migrations`| Migrations and design-time factory |
| `NegDelta.Services`  | Builders, factories, storage, and runtime services |
| `NegDelta.Tests`     | Unit tests for core services and logic |
| `docs/`              | *(WIP)* Project documentation and telemetry breakdowns |
| `resources.md`       | Links to official iRacing telemetry docs |

---

## 📚 Resources

- [iRacing Telemetry Guide (PDF)](https://forums.iracing.com/discussion/62/iracing-sdk/p1)
- [irsdkSharpEr GitHub](https://github.com/mherbold/IRSDKSharper) — Telemetry wrapper used in this app
- [EF Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [MAUI Blazor Hybrid Docs](https://learn.microsoft.com/en-us/dotnet/maui/)

---

## 🤝 Contributing

This project is open-source and will remain so.  
Right now I’m not actively accepting PRs, but feedback, stars, and ideas are always welcome.

You can also:
- Open issues for bugs, questions, or feature ideas
- Follow the repo to watch progress
- Reach out via GitHub or iRacing forums if you want to chat

---

## 👋 About the Author

Hi, I’m Eddie — I’m a recent grad and sim racing enthusiast building NegDelta as a way to combine my passion for racing, telemetry, and software engineering.  
This is both a portfolio project and a real tool I hope others find value in.

---

## ⭐ Star the Repo

If you like where this project is going, feel free to star it!  
It helps others find it — and keeps me motivated. 🏎️💨

---

## 📅 Roadmap (Next Up)

- ⌛ Lap comparison UI
- 📊 Throttle/brake/steering charts (Plotly)
- ☁️ Setup sharing between drivers
