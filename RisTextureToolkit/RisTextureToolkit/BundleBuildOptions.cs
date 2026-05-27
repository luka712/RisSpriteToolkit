namespace RisTextureToolkit;

/// <summary>
/// Root configuration for building a texture bundle.
/// Controls encoding format, pixel layout, mip generation, and compression settings
/// for different target pipelines (UASTC, ETC1S, PNG).
/// </summary>
public class BundleBuildOptions
{
    /// <summary>
    /// The target high-level image format for the output bundle
    /// (e.g., KTX2, PNG, DDS depending on pipeline support).
    /// </summary>
    public ImageFormat TargetImageFormat { get; set; }

    /// <summary>
    /// The target GPU pixel format used after transcoding
    /// (e.g., BC7, ASTC, ETC2 depending on a platform).
    /// </summary>
    public PixelFormat TargetPixelFormat { get; set; }

    /// <summary>
    /// Whether mipmaps should be generated for textures that support them.
    /// Recommended for 3D rendering to improve sampling quality and performance.
    /// </summary>
    public bool GenerateMipmaps { get; set; } = true;

    /// <summary>
    /// Settings for UASTC compression (high-quality basis encoding).
    /// Used for normal maps, masks, UI, and high-fidelity textures.
    /// </summary>
    public BasisUastcOptions? Uastc { get; set; }

    /// <summary>
    /// Settings for ETC1S compression (high-efficiency basis encoding).
    /// Used for albedo textures and bandwidth-sensitive assets.
    /// </summary>
    public BasisEtc1sOptions? Etc1s { get; set; }

    /// <summary>
    /// Settings for PNG encoding when lossless output is required.
    /// Typically used for UI assets or debugging workflows.
    /// </summary>
    public PngCompressionOptions? Png { get; set; }
}

/// <summary>
/// Configuration options for UASTC (Unoptimized ASTC-like) Basis Universal encoding.
/// Provides high-quality texture compression suitable for detailed assets.
/// </summary>
public class BasisUastcOptions
{
    private int _quality = 2;

    /// <summary>
    /// Compression quality level for UASTC encoding.
    /// Range: 0–4
    /// 
    /// 0 = fastest encoding, the lowest quality  
    /// 4 = slowest encoding, the highest quality
    /// </summary>
    public int Quality
    {
        get => _quality;
        set => _quality = System.Math.Clamp(value, 0, 4);
    }
}

/// <summary>
/// Configuration options for ETC1S (Basis Universal low bitrate encoding).
/// Optimized for small file sizes and fast transmission.
/// </summary>
public class BasisEtc1sOptions
{
    private int _qualityLevel = 128;

    /// <summary>
    /// ETC1S quality level controlling compression vs fidelity tradeoff.
    /// Range: 1–255
    /// 
    /// Lower values:
    /// - Smaller file size
    /// - Faster encoding
    /// - Lower visual quality
    /// 
    /// Higher values:
    /// - Larger file size
    /// - Slower encoding
    /// - Better visual quality
    /// </summary>
    public int QualityLevel
    {
        get => _qualityLevel;
        set => _qualityLevel = System.Math.Clamp(value, 1, 255);
    }
}

/// <summary>
/// Configuration options for PNG encoding.
/// PNG is a lossless format where "quality" refers to compression effort only.
/// </summary>
public class PngCompressionOptions
{
    private int _compressionLevel = 6;

    /// <summary>
    /// PNG compression level.
    /// Range: 0–9
    /// 
    /// 0 = no compression (fastest, largest output)  
    /// 9 = maximum compression (slowest, smallest output)
    /// </summary>
    public int CompressionLevel
    {
        get => _compressionLevel;
        set => _compressionLevel = System.Math.Clamp(value, 0, 9);
    }
}