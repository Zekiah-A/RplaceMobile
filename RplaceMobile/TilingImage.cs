using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Image = Microsoft.UI.Xaml.Controls.Image;

namespace RplaceMobile;

public class TilingImage : SKCanvasView
{
    public static readonly BindableProperty SourceProperty =
        BindableProperty.Create(nameof(Source), typeof(string), typeof(TilingImage), "",
        BindingMode.Default, null, async (bindable, _, newValue) =>
        {
            var stream = await FileSystem.OpenAppPackageFileAsync((string) newValue);
            ((TilingImage) bindable).ImageBitmap = SKBitmap.Decode(stream);
        });
    
    public static readonly BindableProperty TileWidthProperty =
        BindableProperty.Create(nameof(TileWidth), typeof(int), typeof(TilingImage), 0);

    public static readonly BindableProperty TileHeightProperty =
        BindableProperty.Create(nameof(TileHeight), typeof(int), typeof(TilingImage), 0);
    public string Source
    {
        get => (string) GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>
    /// If zero will default to the size of the input image
    /// </summary>
    public int TileWidth
    {
        get => (int) GetValue(TileWidthProperty);
        set => SetValue(TileWidthProperty, value);
    }

    /// <summary>
    /// If zero will default to the size of the input image
    /// </summary>
    public int TileHeight
    {
        get => (int) GetValue(TileHeightProperty);
        set => SetValue(TileHeightProperty, value);
    }
    
    public SKBitmap? ImageBitmap;

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs args)
    {
        base.OnPaintSurface(args);
        if (string.IsNullOrEmpty(Source) || ImageBitmap is null)
        {
            return;
        }
        
        var canvas = args.Surface.Canvas;
        using var paint = new SKPaint();
        using var bitmapScaled = new SKBitmap(TileWidth == 0 ? ImageBitmap.Width : TileWidth,
            TileHeight == 0 ? ImageBitmap.Height : TileHeight);
        ImageBitmap.ScalePixels(bitmapScaled, SKFilterQuality.Medium);
        paint.Shader = SKShader.CreateBitmap(bitmapScaled, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat);
        canvas.DrawRect(args.Info.Rect, paint);
    }
}