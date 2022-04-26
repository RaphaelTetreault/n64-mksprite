:: Set up variables
set mksprite="D:\github\n64-mksprite\src\mksprite\bin\Debug\net6.0\mksprite.exe"
set input= ".\spr"
set output=".\filesystem"

:: This example's usage
:: 1 - 				Call 'MakeSprite' program
:: 2 - [-m 0|1|2]		Set mode. Mode 1 is directory mode
:: 3 - [-i <dir>]		Pass input directory to process
:: 4 - [-o <dir>]		Pass output directory to place processed files into. Relative directory is preserved from root / input path.
:: 5 - [--searchSubdirs <dir>]	Search subdirectories beneath input directory
:: 6 - [--searchPattern <dir>]	The search query to find files in input directory
:: 7 - [-f <format>]		The sprite format to convert to. [rgba32|rgba16|ci8|ci4|i8|i4|ia8|ia4]
:: 8 - [--rmExts true|false]	Remove all extensions on processed files. Set on here since this example tags images with format extension.

%mksprite% -m 1 -i %input% -o %output% -f rgba32 --searchSubdirs=true --searchPattern="*.rgba32*.png" --rmExts=true
%mksprite% -m 1 -i %input% -o %output% -f rgba16 --searchSubdirs=true --searchPattern="*.rgba16*.png" --rmExts=true
%mksprite% -m 1 -i %input% -o %output% -f ci8    --searchSubdirs=true --searchPattern="*.ci8*.png"    --rmExts=true
%mksprite% -m 1 -i %input% -o %output% -f i8     --searchSubdirs=true --searchPattern="*.i8*.png"     --rmExts=true
%mksprite% -m 1 -i %input% -o %output% -f ia8    --searchSubdirs=true --searchPattern="*.ia8*.png"    --rmExts=true
%mksprite% -m 1 -i %input% -o %output% -f ci4    --searchSubdirs=true --searchPattern="*.ci4*.png"    --rmExts=true
%mksprite% -m 1 -i %input% -o %output% -f i4     --searchSubdirs=true --searchPattern="*.i4*.png"     --rmExts=true
%mksprite% -m 1 -i %input% -o %output% -f ia4    --searchSubdirs=true --searchPattern="*.ia4*.png"    --rmExts=true