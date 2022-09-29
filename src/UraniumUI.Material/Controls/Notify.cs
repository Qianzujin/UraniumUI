using Microsoft.Maui.Controls.Shapes;

using static System.Net.Mime.MediaTypeNames;

namespace UraniumUI.Material.Controls;

public partial class Notify : Border
{
    public Notify() {
        Stroke = Color.FromArgb("#C49B33");
        Background = Color.FromArgb("#2B0B98");
        StrokeThickness = 4;
        Padding = new Thickness(16, 8);
        HorizontalOptions = LayoutOptions.Center;
        StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(40, 0, 0, 40)
        };
        var label = new Label
        {
            Text = ".NET MAUI",
            TextColor = Colors.White,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold
        };
        this.Content = label;
    }
}
