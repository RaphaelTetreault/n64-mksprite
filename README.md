# n64-mksprite
A C# implementation of of libdragon's mksprite tool with a few more bells and whistles.

## Operation

| Short Command | Long Comamnd    | Value       | Default | Description                                                  |
| ------------- | --------------- | ----------- | ------- | ------------------------------------------------------------ |
| -v            | --verbose       | bool        | False   | Prints all internal messages to the console.                 |
| -i            | --inputPath     | [file\|dir] |         | [File mode] The input file path. [Directory mode] The input directory path. |
| -o            | --outputPath    | [file\|dir] |         | [File mode] The output file path. [Directory mode] The output directory path. |
|               | --searchSubdirs | bool        | False   | [Directory mode] Whether or not to search subdirectories for file. |
|               | --searchPattern | string      |         | [Directory mode] The search pattern used to find files. Ex: "*.png" |
| -m            | --mode          | int         | 0       | The mode of operation for this tool. 0 - Interactive mode: interactive prompt session with program. 1 - Directory mode: pass in directory to parse and convert. 2 - File mode: pass in file to parse and convert. |
| -f            | --format        | [format]    | rgba32  | The desired output sprite format. Argument is case-insensitive. Formats: RGBA32, RGBA16, CI8, I8, IA8, CI4, I4, IA4. |
|               | --slicesH       | int         | 1       | Number of slices across the horizontal axis (vertical cuts)  |
|               | --slicesV       | int         | 1       | Number of slices across the vertical axis (horizontal cuts)  |
|               | --resizeW       | int         |         | Resize image width                                           |
|               | --resizeH       | int         |         | Resize image height                                          |
|               | --resampler     | [resampler] | bicubic | The resampler to use when scaling images. Argument is case-insensitive. Resamplers: Bicubic, Box, CatmullRom, Hermite, Lanczos2, Lanczos3, Lanczos5, Lanczos8, MitchellNetravali, NearestNeighbor, Robidoux, RobidouxSharp, Spline, Triangle, Welch. |
|               | --rmExts        | bool        | False   | Remove all input files' extensions. Yields files formated as ".sprite" |

## Supported Formats

- Bmp
- Gif
- Jpeg
- Pbm
- Png
- Tiff
- Tga
- WebP

## Dependencies

* .NET 6.0
* [ImageSharp](https://github.com/SixLabors/ImageSharp) for image processing
* [CommandLineParser](https://github.com/commandlineparser/commandline) for capturing command line arguments

## Credits

Written by Raphaël Tétreault.

Special thanks to the following for support on the N64Brew Discord:

- Rasky
- Meeq
- Mielke
