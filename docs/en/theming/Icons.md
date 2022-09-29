# Uranium UI: Icons

Uranium UI uses font icons to render icons on all platforms. Uranium Core provides a set of icons that can be used in your application.Themes provide their own icon sets. You can also use your own icons.

## Using icons

Uranium Core provides [Font Awesome](https://fontawesome.com/) by default. Each theme can provide its own icon set. Visit the theme documentation to learn more about the icons it provides.

### Fontawesome
It's included and configured in [UraniumUI.Icons.FontAwesome](https://www.nuget.org/packages/UraniumUI.Icons.FontAwesome) package.

After adding the package, you have to configure fonts in `MauiProgram.cs` file.

```csharp
builder
	.UseMauiApp<App>()
	.ConfigureFonts(fonts =>
	{
		fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		fonts.AddFontAwesomeIconFonts(); // 👈 Add this line
	})
```

#### Font Names
2 font names are provided by Font Awesome. They can be used as `FontFamily` parameter in `FontImageSource`.

- `FARegular`
- `FASolid`

#### Glyphs
Glyphs are provided with `FontAwesomeRegular` and `FontAwesomeSolid` classes. They can be accessed like `FontAwesomeRegular.Filter`. This class icluded in `UraniumUI` namespace. You should include following xml namespace to use it.

```
xmlns:fa="clr-namespace:UraniumUI.Icons.FontAwesome;assembly=UraniumUI.Icons.FontAwesome"
```

#### Usage
MAUI support font icons by using `FontImageSource` class. You can use it in `Image`, `Button` and any control that has a `ImageSource` typed property.

```xml
<Image>
    <Image.Source>
        <FontImageSource FontFamily="FASolid" Glyph="{x:Static fa:Solid.User}" Color="Orange" />
    </Image.Source>
</Image>
```

![MAUI FontAwesome](images/fontawesome-demo.png)

---

### Material Icons
Material icons are included in [UraniumUI.Icons.MaterialIcons](https://www.nuget.org/packages/UraniumUI.Icons.MaterialIcons) package. After adding the package, you have to configure fonts in `MauiProgram.cs` file.

```csharp
builder
	.UseMauiApp<App>()
	.ConfigureFonts(fonts =>
	{
		fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		fonts.AddMaterialconFonts(); // 👈 Add this line
	})
```

#### Font Names
4 font names are provided by Material Icons. They can be used as `FontFamily` parameter in `FontImageSource`.

- `MaterialOutlined`
- `MaterialRegular`
- `MaterialRound`
- `MaterialSharp`
- `MaterialTwoTone`

#### Glyphs
Glyphs are provided with `MaterialOutlined`, `MaterialRegular`, `MaterialRoundRegular`, `MaterialSharpRegular` and `MaterialTwoTone`  classes. They can be accessed like `MaterialTwoTone.Account_circle`. This class icluded in `UraniumUI` namespace. You should include following xml namespace to use it.

```
xmlns:m="clr-namespace:UraniumUI.Icons.MaterialIcons;assembly=UraniumUI.Icons.MaterialIcons"
```

### Usage
MAUI support font icons by using `FontImageSource` class. You can use it in `Image`, `Button` and any control that has a `ImageSource` typed property.

```xml
<Image>
    <Image.Source>
        <FontImageSource FontFamily="MaterialRegular" Glyph="{x:Static m:Regular.Warning}" Color="Red" />
    </Image.Source>
</Image>
```