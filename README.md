# MontyHallGame 🎲

Rick vs. Morty meets Monty Hall — a provably-fair console simulation in C#.

- **Provably fair randomness**: HMAC-SHA3-256 + fresh 256-bit keys
- **Pluggable Morties**: Load DLL + class name at runtime
- **Statistics**: Experimental vs. exact win probabilities
- **Cross-platform**: Windows, macOS, Linux

---

## 🧱 Solution layout

RickAndMortySolution/                       # repo root (GitHub project)
├─ GameCore/
│  ├─ GameCore.csproj
│  ├─ Program.cs
│  ├─ ArgParser.cs
│  ├─ MortyLoader.cs
│  ├─ IMorty.cs
│  ├─ MortyBase.cs
│  ├─ Game.cs
│  ├─ FairNumberProtocol.cs
│  ├─ KeyManager.cs
│  ├─ Stats.cs
│  ├─ ConsoleUi.cs
│  └─ Utils.cs
├─ Morties.Classic/
│  ├─ Morties.Classic.csproj
│  └─ ClassicMorty.cs
├─ Morties.Lazy/
│  ├─ Morties.Lazy.csproj
│  └─ LazyMorty.cs
├─ README.md                          # crystal-clear step-by-step guide
└─ RickAndMortySolution.sln
