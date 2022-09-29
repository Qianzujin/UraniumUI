﻿using Microsoft.Maui.Controls.Shapes;
using UraniumUI.Resources;
using UraniumUI.Extensions;

namespace UraniumUI.Material.Controls;

[ContentProperty(nameof(Content))]
public partial class InputField : Grid
{
    internal const double FirstDash = 6;
    private View content;
    public virtual View Content
    {
        get => content;
        set
        {
            content = value;
            if (value != null)
            {
                border.Content = value;
                RegisterForEvents();
            }
        }
    }

    protected Label labelTitle = new Label()
    {
        Text = "Title",
        HorizontalOptions = LayoutOptions.Start,
        InputTransparent = true,
        Margin = 15,
        TextColor = ColorResource.GetColor("OnBackground", "OnBackgroundDark", Colors.Gray)
    };

    protected Border border = new Border
    {
        Padding = 0,
        Stroke = ColorResource.GetColor("OnBackground", "OnBackgroundDark", Colors.Gray),
        StrokeThickness = 1,
        StrokeDashOffset = 0,
        BackgroundColor = Colors.Transparent,
    };

    protected Grid rootGrid = new Grid();

    protected Lazy<Image> imageIcon = new Lazy<Image>(() =>
    {
        return new Image
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 20,
            HeightRequest = 20,
            Margin = new Thickness(10,0),
        };
    });

    protected HorizontalStackLayout endIconsContainer = new HorizontalStackLayout
    {
        HorizontalOptions = LayoutOptions.End,
        Margin = 5,
    };

    private Color LastFontimageColor;

    private bool hasValue;

    public virtual bool HasValue
    {
        get => hasValue;
        set
        {
            hasValue = value;
            UpdateState();
        }
    }

    public InputField()
    {
        border.StrokeShape = new RoundRectangle
        {
            CornerRadius = this.CornerRadius
        };

        RegisterForEvents();

        this.Add(border);
        this.Add(labelTitle);

        border.Content = rootGrid;

        rootGrid.AddColumnDefinition(new ColumnDefinition(GridLength.Auto));
        rootGrid.AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        rootGrid.Add(Content, column: 1);

        rootGrid.Add(endIconsContainer, column: 2);

        labelTitle.Scale = 1;
        labelTitle.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));

        InitializeValidation();
    }

    protected override async void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        await Task.Delay(100);
        InitializeBorder();
    }

    private void InitializeBorder()
    {
        var perimeter = (this.Width + this.Height) * 2;

        var space = (labelTitle.Width);

#if WINDOWS
        if (space <= 0 || perimeter <= 0)
        {
            return;
        }

        border.Content = null;
        this.Remove(border);
        border = new Border
        {
            Padding = 0,
            Stroke = ColorResource.GetColor("OnBackground", "OnBackgroundDark", Colors.Gray),
            StrokeThickness = 2,
            StrokeDashOffset = 0,
            BackgroundColor = Colors.Transparent,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = CornerRadius
            },
            Content = rootGrid
        };
#endif

        border.StrokeDashArray = new DoubleCollection { FirstDash + CornerRadius.Clamp(FirstDash, double.MaxValue) * 0.9 , space, perimeter, 0 };

#if WINDOWS
        this.Add(border);
#endif

        UpdateState(animate: false);
        border.StrokeThickness = 1;
    }

    protected virtual void UpdateState(bool animate = true)
    {
        if (border.StrokeDashArray == null || border.StrokeDashArray.Count == 0 || labelTitle.Width <= 0)
        {
            return;
        }

        if (HasValue || Content.IsFocused)
        {
            UpdateOffset(0.01, animate);
            labelTitle.TranslateTo(0, -25, 90, Easing.BounceOut);
            labelTitle.AnchorX = 0;
            labelTitle.ScaleTo(.8, 90);
        }
        else
        {
            var offsetToGo = border.StrokeDashArray[0] + border.StrokeDashArray[1] + FirstDash;
            UpdateOffset(offsetToGo, animate);

            labelTitle.CancelAnimations();
            labelTitle.TranslateTo(imageIcon.IsValueCreated ? imageIcon.Value.Width : 0, 0, 90, Easing.BounceOut);
            labelTitle.AnchorX = 0;
            labelTitle.ScaleTo(1, 90);
        }
    }

    protected virtual void UpdateOffset(double value, bool animate = true)
    {
        if (!animate)
        {
            border.StrokeDashOffset = value;
        }
        else
        {
            border.Animate("borderOffset", new Animation((d) =>
            {
                border.StrokeDashOffset = d;
            }, border.StrokeDashOffset, value, Easing.BounceIn), 2, 90);
        }
    }

    protected virtual void RegisterForEvents()
    {
        if (Content != null)
        {
            Content.Focused -= Content_Focused;
            Content.Focused += Content_Focused;
            Content.Unfocused -= Content_Unfocused;
            Content.Unfocused += Content_Unfocused;
        }
    }

    private void Content_Unfocused(object sender, FocusEventArgs e)
    {
        border.Stroke = BorderColor;
        labelTitle.TextColor = TitleColor;
        UpdateState();

        if (Icon is FontImageSource fontImageSource)
        {
            fontImageSource.Color = LastFontimageColor;
        }
    }

    private void Content_Focused(object sender, FocusEventArgs e)
    {
        border.Stroke = AccentColor;
        labelTitle.TextColor = AccentColor;
        UpdateState();

        if (Icon is FontImageSource fontImageSource && fontImageSource.Color != AccentColor)
        {
            LastFontimageColor = fontImageSource.Color?.WithAlpha(1); // To createnew instance.
            fontImageSource.Color = AccentColor;
        }
    }

    protected virtual void OnIconChanged()
    {
        imageIcon.Value.Source = Icon;

        if (Icon is FontImageSource font && font.Color.IsNullOrTransparent())
        {
            font.Color = ColorResource.GetColor("OnBackground", "OnBackgroundDark", Colors.Gray);
        }

        if (!rootGrid.Contains(imageIcon.Value))
        {
            rootGrid.Add(imageIcon.Value, column: 0);
        }
    }
    protected virtual void OnCornerRadiusChanged()
    {
        if (border.StrokeShape is RoundRectangle roundRectangle)
        {
            roundRectangle.CornerRadius = CornerRadius;
        }
    }

    #region BindableProperties
    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(InputField),
        string.Empty,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var textField = (bindable as InputField);
            textField.labelTitle.Text = (string)newValue;
            textField.InitializeBorder();
        });

    public Color AccentColor { get => (Color)GetValue(AccentColorProperty); set => SetValue(AccentColorProperty, value); }

    public static readonly BindableProperty AccentColorProperty = BindableProperty.Create(
        nameof(AccentColor),
        typeof(Color),
        typeof(InputField),
        ColorResource.GetColor("Primary", "PrimaryDark", Colors.Purple),
        propertyChanged: (bindable, oldValue, newValue) => (bindable as InputField).OnPropertyChanged(nameof(AccentColor)));

    public Color TitleColor { get => (Color)GetValue(TitleColorProperty); set => SetValue(TitleColorProperty, value); }

    public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
        nameof(TitleColor),
        typeof(Color),
        typeof(InputField),
        ColorResource.GetColor("OnBackground", "OnBackgroundDark", Colors.Gray),
        propertyChanged: (bindable, oldValue, newValue) => (bindable as InputField).labelTitle.TextColor = (Color)newValue
        );

    public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(InputField),
        ColorResource.GetColor("OnBackground", "OnBackgroundDark", Colors.Gray),
        propertyChanged: (bindable, oldValue, newValue) => (bindable as InputField).labelTitle.TextColor = (Color)newValue);

    public ImageSource Icon { get => (ImageSource)GetValue(IconProperty); set => SetValue(IconProperty, value); }

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(InputField),
        propertyChanged: (bindable, oldValue, newValue) => (bindable as InputField).OnIconChanged());

    public double CornerRadius { get => (double)GetValue(CornerRadiusProperty); set => SetValue(CornerRadiusProperty, value); }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(double),
        typeof(InputField),
        defaultValue: 8.0,
        propertyChanged: (bindable, oldValue, newValue) => (bindable as InputField).OnCornerRadiusChanged());
    #endregion
}
