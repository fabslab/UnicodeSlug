# UnicodeSlug

Permissive slug generator that works with unicode. Port to C#/.NET of https://github.com/jeremys/uslug. Adds support for Korean characters.

Only characters from the Unicode categories Letter, Number and Separator (see [Unicode Categories](http://www.unicode.org/versions/Unicode6.0.0/ch04.pdf))
and the common [CJK Unified Ideographs](http://www.unicode.org/versions/Unicode6.0.0/ch12.pdf) as defined in the version 6.0.0 of the Unicode specification plus the Hangul set.

## Quick Examples

```csharp
using UnicodeSlug;

var slugOptions = new SlugOptions();
slugOptions.GenerateSlug("Быстрее и лучше!"); // "быстрее-и-лучше""
slugOptions.GenerateSlug("汉语/漢語") // "汉语漢語""

var slugOptionsCasing = new SlugOptions 
{
	Lowercase = false
};
var slugOptionsSpaces = new SlugOptions 
{
	Spaces = true
};
var slugOptionsChars = new SlugOptions 
{
	AllowedChars = new [] { '|' }
};
slugOptionsCasing.GenerateSlug("Y U NO") // "Y-U-NO"
slugOptionsSpaces.GenerateSlug("Y U NO") // "y u no"
slugOptionsChars.GenerateSlug("Y-U|NO") // "yu|no""
```

## Installation

From NuGet https://www.nuget.org/packages/unicodeslug

    Install-Package UnicodeSlug


## Options

* Public Properties
    * AllowedChars: an array of chars that you want to be whitelisted. Default: '-_~'.  
    * Lowercase: a Boolean to force the slug to lowercase. Default: true.  
    * Spaces: a Boolean to allow spaces. Default: false.  


## License

[MIT](opensource.org/licenses/MIT) 