from pytablewriter import MarkdownTableWriter
from os import getcwd, listdir
from os.path import isfile, join

# file parameters when search directory
extension = ".csv"


# get current working directory
workingDir = getcwd()
print("Working directory: " + workingDir)

# get all files in this directory
allFiles = [f for f in listdir(workingDir) if isfile(join(workingDir, f))]

# iterate over all files in collection
for file in allFiles:
    # check to see if file is markdown file
    if file.endswith(extension):
        # create a markdown table writer
        writer = MarkdownTableWriter()
        writer.from_csv(file)
        # null name as it will gen one from file name and stick a big H1 heading in the output
        writer.table_name = ""
        # change from extension to .md
        name = file.replace(extension, ".md")
        # write the table to file
        with open(name, "w") as f:
            writer.stream = f
            writer.write_table()
