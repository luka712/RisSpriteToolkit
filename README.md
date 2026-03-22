# RisTextureToolkit

**RisTextureToolkit** is a standalone library and tool for creating **texture atlas bundles** for the **Ris Game Framework and Engine**. It can also be used independently with third-party engines.

## Usage

### Loading a Bundle

To load and consume a sprite bundle, use `TextureTKBundleLoader`.

```csharp
var loader = new TextureTKBundleLoader();

loader.OnBundleLoaded += () =>
{
    // Handle bundle loaded
};

loader.Load("bundle.json");
```

### Building a bundle

To create a texture atlas bundle, add individual images or directories to a `TextureTKBundleBuilder` instance.

```csharp
var builder = new TextureTKBundleBuilder();
builder.AddImage("image.png");
builder.SaveBundle("somepath", "bundle.json");
```

## Overview

A bundle contains packed texture atlaes and metadata describing the location of each sprite within the sheet. 
This allows efficient loading and rendering of many sprites while minimizing texture bindings.
