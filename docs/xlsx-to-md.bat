:: Clear any existing MD files
DEL /S *.md

:: convert Excel tables to CSV files
CALL xlsx-to-csv.bat

:: convert CSV files to markdown tables
START /WAIT python csv-to-md.py

:: Delete unused CSV intermediary
DEL /S *.csv
