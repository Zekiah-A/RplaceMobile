using ABI.Windows.ApplicationModel.Background;

namespace RplaceMobile;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		Task.Run(CheckHideWelcomeAsync);
		
		WebApp.Source = "https://rplace.tk/";
		WebApp.Navigating += async (_, args) =>
		{
			// Reddit exception is done so that oauth can stay within the app
			if (!args.Url.StartsWith("https://rplace.tk/") && !args.Url.StartsWith("https://www.reddit.com/"))
			{
				args.Cancel = true;
				await Launcher.OpenAsync(args.Url);
			}
		};
	}

	private async Task CheckHideWelcomeAsync()
	{
		if (await SecureStorage.Default.GetAsync("seen_welcome") == "true")
		{
			WelcomePage.IsVisible = false;
		}
	}

	private void WelcomePageContinueClicked(object? sender, EventArgs e)
	{
		WelcomePage.IsVisible = false;
		Task.Run(async () =>
		{
			await SecureStorage.Default.SetAsync("seen_welcome", "true");
		});
	}
}

