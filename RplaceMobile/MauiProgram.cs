using SkiaSharp.Views.Maui.Controls.Hosting;

namespace RplaceMobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("RedditMono-Bold.ttf", "Mono");
				fonts.AddFont("RedditSans-Bold.ttf", "Bold");
				fonts.AddFont("RedditSans-Regular.ttf", "Regular");
			});
		return builder.Build();
	}
}
