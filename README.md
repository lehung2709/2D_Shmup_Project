# 🚀 2D_Shmup_Project

A 2D Shoot-'Em-Up (Shmup) game prototype built with Unity. Take control of a aircraft, blast through waves of enemies, and upgrade your firepower!

---

## 🎮 Features

- **✈️ Plane Shop**  
  Unlock and purchase different planes, each with unique bullet and skill.

- **🧠 Strategy Pattern for Behavior**  
  Implemented for enemy and bullet movement (e.g., straight, accelerated, sine wave, etc.).

-**📈 Bullet Spawn Patterns**  
  Create complex bullet patterns such as Linear, Multiway, Waving, and more.  
  
- **⚔️ Enemy Phases**  
  Enemies—especially bosses—can change bullet and spawn patterns depending on their current health.

- **🔫 Bullet Upgrade System**  
  Players can collect power-ups to enhance their firepower.

- **💾 Save/Load System**  
  Save player progress, settings, and unlocked content using **JSON serialization**.

- **🔊 Audio System**  
  Custom sound system using **Unity Audio Mixer**:  
  - Adjustable volume sliders for **Master**, **Music**, and **SFX**.  
  - Easy-to-manage audio architecture.

- **📐 Design Patterns Implemented**  
  - **Singleton**: GameManager, AudioManager, etc.  
  - **Object Pooling**: For bullets, enemies, and effects (reduce GC allocation).  
  - **Strategy**: Cleanly separates movement logic for enemies and bullets.

---

## 📺 Demo
🎬 Watch the Gameplay on YouTube

🎮 Play it on Itch.io
https://hungle2772004.itch.io/shmup-project





