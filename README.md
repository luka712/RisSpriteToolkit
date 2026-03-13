# Ris.SpriteToolkit

**Ris.SpriteToolkit** is a standalone library and tool for creating **sprite sheet bundles** for the **Ris Game Framework and Engine**. It can also be used independently with third-party engines.

## Usage

### Loading a Bundle

To load and consume a sprite bundle, use `SpriteTKBundleLoader`.

```csharp
var loader = new SpriteTKBundleLoader();

loader.OnBundleLoaded += () =>
{
    // Handle bundle loaded
};

loader.Load("bundle.json");
```

### Building a bundle

To create a sprite bundle, add individual images or directories to a `SpriteTKBundleBuilder` instance.

```csharp
var builder = new SpriteTKBundleBuilder();
builder.AddImage("image.png");
builder.SaveBundle("somepath", "bundle.json");
```

## Overview

A bundle contains packed sprite sheets and metadata describing the location of each sprite within the sheet. 
This allows efficient loading and rendering of many sprites while minimizing texture bindings.
