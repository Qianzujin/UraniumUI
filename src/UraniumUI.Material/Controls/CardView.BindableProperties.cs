namespace UraniumUI.Material.Controls
{
    public partial class CardView : ContentView
    {
        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardDescriptionProperty = BindableProperty.Create(nameof(CardDescription), typeof(string), typeof(CardView), string.Empty);
        public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColorProperty), typeof(Color), typeof(CardView), new Color(0));
        public static readonly BindableProperty CardImageProperty = BindableProperty.Create(nameof(CardImageProperty), typeof(ImageSource), typeof(CardView), null);

        public string CardTitle
        {
            get => (string)GetValue(CardTitleProperty);
            set => SetValue(CardTitleProperty, value);
        }

        public string CardDescription
        {
            get => (string)GetValue(CardDescriptionProperty);
            set => SetValue(CardDescriptionProperty, value);
        }

        public Color CardColor
        {
            get => (Color)GetValue(CardColorProperty);
            set => SetValue(CardColorProperty, value);
        }

        public ImageSource CardImage
        {
            get => (ImageSource)GetValue(CardImageProperty);
            set => SetValue(CardImageProperty, value);
        }
    }
}