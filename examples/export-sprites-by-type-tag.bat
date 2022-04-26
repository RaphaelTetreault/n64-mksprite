:: Set up variables
set mksprite="D:\github\n64-mksprite\src\mksprite\bin\Debug\net6.0\mksprite.exe"
set input= ".\spr"
set output=".\filesystem"

:: This example's usage
:: 1 - 				Call 'MakeSprite' program
:: 2 - [-m 0|1|2]		Set mode. Mode 1 is directory mode
:: 3 - [-i <directory>]		Pass input directory to process
:: 4 - [-o <directory>]		Pass output directory to place processed files into. Relative directory is preserved from root / input path.
:: 5 - [--searchSubdirs]		Search subdirectories beneath input directory
:: 6 - [--searchPattern]		The search query to find files in input directory
:: 7 - [-f <sprite format>]	The sprite format to convert to. [rgba32|rgba16|ci8|ci4|i8|i4|ia8|ia4]
:: 8 - [--rmExts true|false]	Remove all extensions on processed files. Set on here since this example tags images with format extension.

%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.rgba32*.png" -f rgba32 --rmExts=true
%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.rgba16*.png" -f rgba16 --rmExts=true
%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.ci8*.png"    -f ci8    --rmExts=true
%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.i8*.png"     -f i8     --rmExts=true
%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.ia8*.png"    -f ia8    --rmExts=true
%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.ci4*.png"    -f ci4    --rmExts=true
%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.i4*.png"     -f i4     --rmExts=true
%mksprite% -m 1 -i %input% -o %output% --searchSubdirs=true --searchPattern="*.ia4*.png"    -f ia4    --rmExts=true