:: Set up variables
set mksprite="D:\github\n64-mksprite\src\mksprite\bin\Debug\net6.0\mksprite.exe"
set input= ".\spr"
set output=".\filesystem"

:: This example's usage
:: 1 - 				Call 'MakeSprite' program
:: 2 - [-m <int>]		Set mode. Mode 1 is directory mode.
:: 3 - [-i <dir>]		Pass input directory to process
:: 4 - [-o <dir>]		Pass output directory to place processed files into. Relative directory is preserved from root / input path.
:: 5 - [--searchSubdirs <bool>]	Search subdirectories beneath input directory
:: 6 - [--searchPattern <str>]	The search query to find files in input directory
:: 7 - [-f <format>]		The sprite format to convert to. [rgba32|rgba16|ci8|ci4|i8|i4|ia8|ia4]
:: 8 - [--rmExts <bool>]	Remove all extensions on processed files. Set on here since this example tags images with format extension.

%mksprite% -m 1 -i %input% -o %output% -f rgba32 --searchSubdirs --searchPattern="*.rgba32*.png" --rmExts
%mksprite% -m 1 -i %input% -o %output% -f rgba16 --searchSubdirs --searchPattern="*.rgba16*.png" --rmExts
%mksprite% -m 1 -i %input% -o %output% -f ci8    --searchSubdirs --searchPattern="*.ci8*.png"    --rmExts
%mksprite% -m 1 -i %input% -o %output% -f i8     --searchSubdirs --searchPattern="*.i8*.png"     --rmExts
%mksprite% -m 1 -i %input% -o %output% -f ia8    --searchSubdirs --searchPattern="*.ia8*.png"    --rmExts
%mksprite% -m 1 -i %input% -o %output% -f ci4    --searchSubdirs --searchPattern="*.ci4*.png"    --rmExts
%mksprite% -m 1 -i %input% -o %output% -f i4     --searchSubdirs --searchPattern="*.i4*.png"     --rmExts
%mksprite% -m 1 -i %input% -o %output% -f ia4    --searchSubdirs --searchPattern="*.ia4*.png"    --rmExts