# 🎮 SFML.Game.Framework

**SFML.Game.Framework** is a modern, modular **2D game framework** built on top of **SFML.NET**.  
It provides a complete foundation for building high-performance 2D games with C#, combining simplicity, extensibility, and flexibility in one lightweight package.

Whether you’re creating a pixel art platformer, an arcade shooter, or a simulation,  
**SFML.Game.Framework** gives you the tools to get started fast — and scale with confidence.

---

## 🚀 Features

### 🖼️ Graphics
- High-performance **2D rendering system** built on SFML.
- Draw sprites, shapes, text, and primitives with a simple API.
- **Render targets** for off-screen rendering and compositing.
- Smooth scaling and transformation via **Transform2D** and **Camera2D**.
- Full **post-processing pipeline** with effects such as:
  - Blur
  - Glow / Bloom
  - Chromatic Aberration
  - Color Grading (LUT)

---

### 🎮 Input System
- Unified **Keyboard**, **Mouse**, and **Controller** input management.
- Polling and event-driven modes supported.
- Easily customizable bindings and input maps.
- Built-in helper functions for state transitions (Pressed, Released, Held).

---

### 🧩 Scene Management
- Flexible **SceneSystem** supporting layered scenes and transitions.
- Popup scenes (like menus or overlays) supported natively.
- Each scene manages its own:
  - Controls
  - GameObjects
  - Updates
  - Input
  - Rendering
- Integrated with the `AnimationManager` and `GraphicsDrawer`.

---

### 🎞️ Animation
- Simple yet powerful **AnimationManager** for sprite or value-based animations.
- Sprite sheet / atlas support.
- Looping, ping-pong, and one-shot playback.
- Easing support for smooth motion.

---

### 🧠 Component System
- Lightweight, extendable **Component Model**.
- Add custom logic via `IGameComponent` or `IDrawableGameComponent`.
- Commonly used for:
  - UI controls
  - Debug overlays
  - Entity behavior systems

---

### 💥 Particle System
- Fully customizable **ParticleEmitter**.
- Supports particle textures, random velocities, lifetimes, and blending.
- Gravity, fade, and color transitions built-in.
- Great for explosions, trails, smoke, and environmental effects.

---

### ⚙️ Physics System
- Simplified **2D Physics Engine** for collision and movement.
- Basic rigid body support with gravity and friction.
- Collision detection using AABBs and circles.
- Easily extendable for more advanced behavior.

---

### 🎵 Audio & Media Management
- Centralized **MediaManager** for all `SoundEffect`s and `Song`s.
- Supports:
  - `.wav`, `.ogg`, `.mp3`
- Load and play sounds directly, or manage them by name.
- Per-sound and per-song volume and looping control.
- Integration-ready for dynamic audio mixing.

---

### 📦 Content Management
- Flexible **Content Management System** supporting:
  - Standard file loading.
  - In-built **Content Builder** integration.
- Easily load assets such as:
  - Textures
  - Sounds
  - Fonts
  - Shaders
- Supports caching and memory-safe disposal.
  
```csharp
// Load from file
var texture = new Texture2D("the filename");
var font = new Font("the filename", 12);
var soundeffect = new SoundEffect("the filename");
var song = new Song("the filename");
var shader = new Shader("fragmentPath");
var shader2 = new Shader("vertexPath", "fragmentPath");

// Load from embedded content
var texture = Content.GetTexture("the name in the asset pack");
var font = Content.GetFont("the name in the asset pack");
var soundeffect = Content.GetSoundEFfect("the name in the asset pack");
var song = Content.GetFont("the name in the asset pack");
var shader = Content.GetShader("the name in the asset pack");
```
---
## 🚀 Example
```csharp
public class Game : SFML.Game.Framework
{
     public override void Update(GameTime gameTime)
     {
     }

     public override void Draw(GameTime gameTime)
     {
           GraphicsDevice.Clear(Color.CornflowerBlue);
     }
}
```
