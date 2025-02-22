# Notice
This code is outdated and includes a number of known bugs. This was a minor hobby project at one point and is not suitable as a substitue for the original [mksprite](https://github.com/DragonMinded/libdragon/tree/trunk/tools/mksprite).

# n64-mksprite
A C# implementation of libdragon's mksprite tool with a few more bells and whistles.

## Operation

| Short Command | Long Comamnd    | Value       | Default | Description                                                  |
| ------------- | --------------- | ----------- | ------- | ------------------------------------------------------------ |
|               | --verbose       | bool        | False   | Prints all internal messages to the console.                 |
| -i            | --inputPath     | [file\|dir] |         | [File mode] The input file path. [Directory mode] The input directory path. |
| -o            | --outputPath    | [file\|dir] |         | [File mode] The output file path. [Directory mode] The output directory path. |
| -s            | --searchSubdirs | bool        | False   | [Directory mode] Whether or not to search subdirectories for file. |
| -p            | --searchPattern | string      |         | [Directory mode] The search pattern used to find files. Ex: "*.png" |
| -m            | --mode          | int         | 0       | The mode of operation for this tool. 0 - Interactive mode: interactive prompt session with program. 1 - Directory mode: pass in directory to parse and convert. 2 - File mode: pass in file to parse and convert. |
| -f            | --format        | [format]    | rgba32  | The desired output sprite format. Argument is case-insensitive. Formats: RGBA32, RGBA16, CI8, I8, IA8, CI4, I4, IA4. |
| -h            | --slicesH       | int         | 1       | Number of slices across the horizontal axis (vertical cuts)  |
| -v            | --slicesV       | int         | 1       | Number of slices across the vertical axis (horizontal cuts)  |
|               | --resizeW       | int         |         | Resize image width                                           |
|               | --resizeH       | int         |         | Resize image height                                          |
|               | --resampler     | [resampler] | bicubic | The resampler to use when scaling images. Argument is case-insensitive. Resamplers: Bicubic, Box, CatmullRom, Hermite, Lanczos2, Lanczos3, Lanczos5, Lanczos8, MitchellNetravali, NearestNeighbor, Robidoux, RobidouxSharp, Spline, Triangle, Welch. |
|               | --rmExts        | bool        | False   | Remove all input files' extensions. Yields files formated as "*.sprite" |

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
* [CommandLineParser](https://github.com/commandlineparser/commandline) for capturing command line arguments
* [ImageSharp](https://github.com/SixLabors/ImageSharp) for image processing
* [Manifold.Core](https://github.com/RaphaelTetreault/Manifold.Core) for handling binary

## Credits

Written by Raphaël Tétreault.

Special thanks to the following for support on the N64Brew Discord:

- Meeq
- Mielke
- Rasky
