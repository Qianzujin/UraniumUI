﻿using Plainer.Maui.Controls;
using System.Collections;
using System.ComponentModel;
using UraniumUI.Pages;
using UraniumUI.Resources;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace UraniumUI.Material.Controls;

[ContentProperty(nameof(Validations))]
public class PickerField : InputField
{
    public PickerView PickerView => Content as PickerView;

    public override View Content { get; set; } = new PickerView
    {
        VerticalOptions = LayoutOptions.Center,
        Margin = new Thickness(10, 0),
    };

    protected ContentView iconClear = new ContentView
    {
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.End,
        IsVisible = false,
        Padding = 10,
        Content = new Path
        {
            Data = UraniumShapes.X,
            Fill = ColorResource.GetColor("OnBackground", "OnBackgroundDark", Colors.DarkGray).WithAlpha(.5f),
        }
    };

    public override bool HasValue => SelectedItem != null;

    public PickerField()
    {
        var clearGestureRecognizer = new TapGestureRecognizer();
        clearGestureRecognizer.Tapped += OnClearTapped;
        iconClear.GestureRecognizers.Add(clearGestureRecognizer);

        endIconsContainer.Add(iconClear);

        PickerView.SetBinding(PickerView.SelectedItemProperty, new Binding(nameof(SelectedItem), source: this));
        PickerView.SetBinding(PickerView.SelectedIndexProperty, new Binding(nameof(SelectedIndex), source: this));
    }

    protected override object GetValueForValidator()
    {
        return SelectedItem;
    }

    protected void OnClearTapped(object sender, EventArgs e)
    {
        SelectedItem = null;
        PickerView.Unfocus();
    }

    protected virtual void OnSelectedItemChanged()
    {
        OnPropertyChanged(nameof(SelectedItem));
        CheckAndShowValidations();

        iconClear.IsVisible = SelectedItem != null;

        UpdateState();
    }

    public IList<string> Items => PickerView.Items;

    public object SelectedItem { get => GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem), typeof(object), typeof(PickerField),
        defaultValue: Picker.SelectedItemProperty.DefaultValue,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).OnSelectedItemChanged());

    public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
        nameof(SelectedIndex), typeof(int), typeof(PickerField),
        defaultValue: Picker.SelectedIndexProperty.DefaultValue,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).OnPropertyChanged(nameof(SelectedIndex)));

    public IList ItemsSource { get => (IList)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
       nameof(ItemsSource), typeof(IList), typeof(PickerField),
       defaultValue: Picker.ItemsSourceProperty.DefaultValue,
       propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).PickerView.ItemsSource = (IList)newValue);

    public BindingBase ItemDisplayBinding { get => PickerView.ItemDisplayBinding; set => PickerView.ItemDisplayBinding = value; }

    public FontAttributes FontAttributes { get => (FontAttributes)GetValue(FontAttributesProperty); set => SetValue(FontAttributesProperty, value); }

    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
       nameof(FontAttributes), typeof(FontAttributes), typeof(PickerField),
       defaultValue: Picker.FontAttributesProperty.DefaultValue,
       propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).PickerView.FontAttributes = (FontAttributes)newValue);

    public string FontFamily { get => (string)GetValue(FontFamilyProperty); set => SetValue(FontFamilyProperty, value); }

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
         nameof(FontFamily), typeof(string), typeof(PickerField),
         defaultValue: Picker.FontFamilyProperty.DefaultValue,
         propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).PickerView.FontFamily = (string)newValue);

    [TypeConverter(typeof(FontSizeConverter))]
    public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
        nameof(FontSize), typeof(double), typeof(PickerField), Picker.FontSizeProperty.DefaultValue,
        propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).PickerView.FontSize = (double)newValue);

    public bool FontAutoScalingEnabled { get => (bool)GetValue(FontAutoScalingEnabledProperty); set => SetValue(FontAutoScalingEnabledProperty, value); }

    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(
        nameof(FontAutoScalingEnabled), typeof(bool), typeof(PickerField), Picker.FontAutoScalingEnabledProperty.DefaultValue,
        propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).PickerView.FontAutoScalingEnabled = (bool)newValue);

    public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor), typeof(Color), typeof(PickerField), Picker.TextColorProperty.DefaultValue,
        propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).PickerView.TextColor = (Color)newValue);

    public double CharacterSpacing { get => (double)GetValue(CharacterSpacingProperty); set => SetValue(CharacterSpacingProperty, value); }

    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(
        nameof(CharacterSpacing), typeof(double), typeof(PickerField), Picker.CharacterSpacingProperty.DefaultValue,
        propertyChanged: (bindable, oldValue, newValue) => (bindable as PickerField).PickerView.CharacterSpacing = (double)newValue);
}
